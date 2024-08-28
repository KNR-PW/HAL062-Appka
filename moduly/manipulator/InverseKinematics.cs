using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;
using MathNetVector = MathNet.Numerics.LinearAlgebra.Vector<float>;
using System.Security.Cryptography.X509Certificates;
using System.Drawing.Text;

/*  Poniższy plik może wydawać się pojebany i taki jest.
 *  Historia liczenia kinematyki odwrotnej manipulatora sięga dziejów, gdy ten powstawał. Niestety soft napisany na STM (który liczył inverse kinematics) nie został nigdzie zapisany i był problematyczny ze względu na swoje mało zaawansowane sposoby liczenia pozycji i częste wypadki np. losowe obrócenie i walnięcie w coś 
 *  W związku z tym powstała myśl, aby wszystko liczone było w aplikacji i chciałem, aby było to zrobione konkretnie i profesjonalnie.
 *  Poniższy plik zawiera kompleksowy silnik matematyczny do wyliczenia całej kinematyki manipulatora (w tym prędkości i przyśpieszeń). W planie było wykrywanie kolizji oraz poruszanie członami na podstawie wzorów matematycznych i innych instrukcji.
 *  Do napisania bardzo przydały się materiały z przedmiotu Dynamika Układów Wieloczłonowych (DUW) prowadzone przez Wojtyrę na Melu
 *  Jeśli nie wiesz co robisz, to lepiej nie ruszaj tego pliku.
 *  
 *  Zmiany dotyczące parametrów manipulatora (długości, DH, kąty) można przeprowadzić w deklaracji funkcji InverseKinematics poniżej
 * 
 *  Co więcej na napisanie go planowałem poświęcić tydzień, bo 4 zostały do zawodów ERC 2024
 *  ~ Dominik Chmielak
 *  
 *  
 *  Operacje na macierzach są obliczane za pomocą biblioteki Math.NET Numerics https://numerics.mathdotnet.com/
 *  
 *  
 * */

namespace HAL062app.moduly.manipulator
{

    public class InverseKinematics
    {
        public Part[] manipulatorParts;
        public float[] deg2rad(float[] angle)
        {
            for (int i = 0; i < angle.Length; i++)
                angle[i] = (angle[i] * (float)Math.PI) / 180f;
            return angle;
        }
     
        private float[] rad2deg(float[] angle)
        {
            for (int i = 0; i < angle.Length; i++)
                angle[i] = (angle[i] *  180f / (float)Math.PI);
            return angle;
        }
        public class DHMatrix
        {
            float theta, d, a, psi = new float();
            public Matrix<float> matrix;
            public DHMatrix()
            {
                matrix = DenseMatrix.OfArray(new float[,]
                 {
                {0.0f,0.0f,0.0f,0.0f},
                {0.0f,0.0f,0.0f,0.0f},
                {0.0f,0.0f,0.0f,0.0f},
                {0.0f,0.0f,0.0f,0.0f}
                 });
            }

          
           
            public void Create(float theta, float d, float a, float psi) //funkcja obliczajaca macierz denavita-hartenberga
            {
                this.theta = theta;
                this.d = d;
                this.a = a;
                this.psi = psi;
                this.matrix[0, 0] = (float)Math.Cos(theta);
                this.matrix[0, 1] = (float)(-Math.Sin(theta) * Math.Cos(psi));
                this.matrix[0, 2] = (float)(Math.Sin(theta) * Math.Sin(psi));
                this.matrix[0, 3] = (float)(a * Math.Cos(theta));

                this.matrix[1, 0] = (float)(Math.Sin(theta));
                this.matrix[1, 1] = (float)(Math.Cos(theta) * Math.Cos(psi));
                this.matrix[1, 2] = (float)(-Math.Cos(theta) * Math.Sin(psi));
                this.matrix[1, 3] = (float)(a * Math.Sin(theta));

                this.matrix[2, 0] = 0.0f;
                this.matrix[2, 1] = (float)(Math.Sin(psi));
                this.matrix[2, 2] = (float)(Math.Cos(psi));
                this.matrix[2, 3] = d;

                this.matrix[3, 0] = 0.0f;
                this.matrix[3, 1] = 0.0f;
                this.matrix[3, 2] = 0.0f;
                this.matrix[3, 3] = 1.0f;
            }
            public void Solve(float angle)
            {
                this.Create(angle, d, a, psi);
            }

        }
        public class Part //klasa odpowiedzialna za wszystkie komponenty, które mają być brane w symulacji. Głownie chodziło też o przyszłą implementację kolizji, a więc pozycje wiertła, innych dofów itp.
        {
            //Vector<float> position;
            public float angle;
            public float MaximalAngle;
            public float MinimalAngle;
            public float length;
            public float offset;
            public DHMatrix T = new DHMatrix();
            public Part() //Implementacja nieruszających się części - do detekcji kolizji
            {

            }
            public Part(float _angle, float _MinimalAngle, float _MaximalAngle, float length, float d, float a, float _psi, float _offset) //Klasa part może posiadać też inne części, więc oddzielne przypisanie wartości dofów. 3 wartości d,a,psi, to wymóg do korzystania z macierzy DH. Offset to kąt, który wynika z jakiegoś błedu w stworzeniu modelu 3D, ale za duzo poswiecilem czasu na znalezienie i nie chcialem sie z tym dalej bawic 
            {
                this.angle = _angle * (float)Math.PI / 180f;
                this.MaximalAngle = _MaximalAngle * (float)Math.PI / 180f;
                this.MinimalAngle = _MinimalAngle * (float)Math.PI / 180f;
                this.length = length;
                this.offset = _offset * (float)Math.PI/180f;
                T.Create(angle, d, a, _psi*(float)Math.PI/180f);
            }
            public DHMatrix SolveDH(float theta)
            {
                T.Solve(theta + offset);
                return T;
            }






        }

        class Jacobian //Jak nie wiesz co robisz, to zostaw.
        {
            Matrix<float> J_matrix = DenseMatrix.Create(6, 6, 0f);


            Part[] _manipulatorParts = new Part[6];


            public Jacobian(Part[] parts)
            {
                _manipulatorParts = parts;
            }

            public float[] solve(float[] startAngles, float[] destination) //dorobić liniowe przejścia (dalekie trajektorie dzielisz na kilka pozycji)
            {
                float[] result = new float[6];
                MathNetVector _destination = DenseVector.OfArray(new float[] { destination[0], destination[1], destination[2], destination[3], destination[4], destination[5] });
                result = NewRaph(startAngles, _destination);
                return result;
            }

           

            public Matrix<float> CalculateJacobian(float[] q) //Tworzy jakobian 6x6, pierwsze 3 wiersze - pozycje, 3 kolejne wiersze - orientacja
            {
                Matrix<float> _T = DenseMatrix.CreateIdentity(4);
                DenseMatrix[] _T_column = new DenseMatrix[6];
                Matrix<float> o = DenseMatrix.Create(3, 6, 0f);

                for (int i = 0; i < 6; i++) //tworzymy macierz 3x6 z pozycjami
                {
                    _manipulatorParts[i].SolveDH(q[i]);
                    Matrix<float> multiplier = _manipulatorParts[i].T.matrix;
                    _T = _T.Multiply(multiplier);
                    _T_column[i] = _T.Clone() as DenseMatrix;
                    o.SetColumn(i, _T.SubMatrix(0, 3, 3, 1).Column(0));
                }

                MathNetVector z = DenseVector.OfArray(new float[] { 0, 0, 1 });
                MathNetVector[] z_vectors = new MathNetVector[6];
                for (int i = 0; i < 6; i++)
                {
                    if (i == 0)
                    {
                        z_vectors[i] = z;
                    }
                    else
                    {
                        z_vectors[i] = _T_column[i - 1].SubMatrix(0, 3, 2, 1).Column(0);
                    }
                }


                Matrix<float> J = DenseMatrix.Create(6, 6, 0f);
                MathNetVector o_n = o.Column(5); // end effector position
                for (int i = 0; i < 6; i++)
                {
                    MathNetVector z_vector = z_vectors[i];
                    MathNetVector o_i = o.Column(i);
                    MathNetVector crossProduct = DenseVector.OfArray(new float[]
                    {
                        z_vector[1] * (o_n[2] - o_i[2]) - z_vector[2] * (o_n[1] - o_i[1]),
                        z_vector[2] * (o_n[0] - o_i[0]) - z_vector[0] * (o_n[2] - o_i[2]),
                        z_vector[0] * (o_n[1] - o_i[1]) - z_vector[1] * (o_n[0] - o_i[0])
                    });

                    J.SetSubMatrix(0, 3, i, 1, crossProduct.ToColumnMatrix());
                    J.SetSubMatrix(3, 3, i, 1, z_vector.ToColumnMatrix());
                }

                return J;
            }
            public float[] ForwardPositionXYZ(float[] q)
            {
                float[] result = new float[3];
                Matrix<float> T = DenseMatrix.CreateIdentity(4);
                for (int i = 0; i < 6; i++)
                {
                    _manipulatorParts[i].SolveDH(q[i]);
                    T = T * _manipulatorParts[i].T.matrix;
                }
                result[0] = T[0, 3];
                result[1] = T[1, 3];
                result[2] = T[2, 3];

                return result;
            }
            private float DistanceFromPoint(float[] a, MathNetVector b)
            {
                float[] xyz = ForwardPositionXYZ(a);
                return (float)Math.Sqrt((xyz[0] - b[0])* (xyz[0] - b[0]) + (xyz[1] - b[1])* (xyz[1] - b[1]) + (xyz[2] - b[2])* (xyz[2] - b[2]));
            }
            public Matrix<float> ForwardKinematics(float[] q)
            {
                Matrix<float> T = DenseMatrix.CreateIdentity(4);
                for (int i = 0; i < 5; i++)
                {
                    _manipulatorParts[i].SolveDH(q[i]);
                    T = T * _manipulatorParts[i].T.matrix;
                }
                return T;
            }
            private float Taylor(float q, float dq, float d2q, float dt) //Przybliżenie miejsca startowego dla metody Newtona, wykorzystuje rozwinięcie w szereg Taylora
            {
                float q0 = q + dq + 0.5f * d2q * dt * dt;
                return q0;
            }
            private MathNetVector ObjectiveFunction(float[] q, MathNetVector targetPosition)
            {
                Matrix<float> endEffectorTransform = ForwardKinematics(q);

                float[] endEffectorPositionArray = new float[6];
                endEffectorPositionArray[0] = endEffectorTransform[0, 3]; // x
                endEffectorPositionArray[1] = endEffectorTransform[1, 3]; // y
                endEffectorPositionArray[2] = endEffectorTransform[2, 3]; // z

                MathNetVector endEffectorPosition = DenseVector.OfArray(endEffectorPositionArray);

                // Oblicz wektor różnicy między pozycją końcówki a targetPosition
                MathNetVector difference = endEffectorPosition - targetPosition;

                return difference;
            }
            public Matrix<float> CalculateHessian(float[] q, MathNetVector targetPosition)
            {
                int n = q.Length;
                Matrix<float> hessian = DenseMatrix.Create(n, n, 0f);
                float epsilon = 1e-2f; // Mała wartość dla różnic skończonych

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        float[] q1 = (float[])q.Clone();
                        float[] q2 = (float[])q.Clone();
                        float[] q3 = (float[])q.Clone();
                        float[] q4 = (float[])q.Clone();

                        q1[i] += epsilon;
                        q1[j] += epsilon;
                        q2[i] += epsilon;
                        q2[j] -= epsilon;
                        q3[i] -= epsilon;
                        q3[j] += epsilon;
                        q4[i] -= epsilon;
                        q4[j] -= epsilon;

                        MathNetVector f1 = ObjectiveFunction(q1, targetPosition);
                        MathNetVector f2 = ObjectiveFunction(q2, targetPosition);
                        MathNetVector f3 = ObjectiveFunction(q3, targetPosition);
                        MathNetVector f4 = ObjectiveFunction(q4, targetPosition);

                        float hessianValue = (f1[0] - f2[0] - f3[0] + f4[0]) / (4 * epsilon * epsilon);

                        hessian[i, j] = hessianValue;
                    }
                }

                return hessian;
            }
            public float[] NewRaph(float[] startPosition, MathNetVector targetPosition)
            {
                int maxIterations = 5000;
                float tolerance = 0.1f;
                float initialScale = 1.0f;
                int numVariables = startPosition.Length;

                for (int iteration = 0; iteration < maxIterations; iteration++)
                {
                    MathNetVector functionValues = ObjectiveFunction(startPosition, targetPosition);
                    Matrix<float> jacobianMatrix = CalculateJacobian(startPosition);
                    Matrix<float> hessianMatrix = CalculateHessian(startPosition, targetPosition);

                    // Sprawdzenie, czy Hessian jest osobliwy
                    float determinant = hessianMatrix.Determinant();
                    Matrix<float> hessianInverse;

                    if (Math.Abs(determinant) < 1e-6) // Jeśli determinant jest bliski zeru, użyj pseudoinwersji
                    {
                        Console.WriteLine("Hessian matrix is singular or near-singular. Using SVD for pseudoinverse.");
                        var svd = hessianMatrix.Svd();
                        hessianInverse = svd.U * svd.W.PseudoInverse() * svd.VT;
                    }
                    else
                    {
                        hessianInverse = hessianMatrix.Inverse();
                    }

                    MathNetVector deltaThetaVector = hessianInverse * (-functionValues);

                    // Kopia poprzedniej pozycji do porównania
                    float[] previousPosition = (float[])startPosition.Clone();

                    float scale = initialScale;
                    float previousError = DistanceFromPoint(previousPosition, targetPosition);
                    float newError;
                    float[] newPosition;
                    float[] sasas1 = ForwardPositionXYZ(startPosition);
                    do
                    {
                        // Aktualizuj przybliżenie
                        newPosition = (float[])previousPosition.Clone();
                        for (int i = 0; i < numVariables; i++)
                        {
                            newPosition[i] += scale * deltaThetaVector[i];
                        }

                        newError = DistanceFromPoint(newPosition, targetPosition);

                        if (newError < previousError)
                        {
                            break;
                        }

                        scale *= 0.5f; // Zmniejsz skalę o połowę
                    } while (scale > 1e-3);

                    startPosition = newPosition;
                    float[] sasas = ForwardPositionXYZ(newPosition);
                    previousError = newError;

                    Console.WriteLine($"Iteration {iteration}, Error: {newError}, Scale: {scale}");

                    if (newError < tolerance)
                    {
                        Console.WriteLine("Converged after " + iteration + " iterations.");
                        return startPosition;
                    }
                    if ( iteration == 4999)
                    { }
                }
                //return startPosition;
                throw new Exception("Algorithm did not converge");
            }


        }











        //Zostały zaprojektowane różne możliwe tryby obsługi manipulatora, które można poniżej wybrać 
        // mode 0 - podajemy xyz chwytaka (najdalszy punkt, a nie podstawa)
        // mode 1 - podajemy xyz 3 dofu (historycznie pierwszy powstały tryb, bo potrzebny do obliczania innych)
        //
        Jacobian solver;
        public InverseKinematics(/*Position start, int mode, MathNetVector destination*/)
        {
            manipulatorParts = new Part[6];
            manipulatorParts[0] = new Part(0f,   -180f,   180f, 169.5f, 169.5f, 0f, -90f, 0f);         //Inicjacja startowa wartości manipulatora, to tutaj można zmieniać ograniczenia i parametry
            manipulatorParts[1] = new Part(50f,  -14f,    110f,  550f,      0f,        550f,     0f,   -90f);
            manipulatorParts[2] = new Part(11f,  -163f,   11f,   14f,       -14f,        0f,    -90f,    0f);
            manipulatorParts[3] = new Part(0f,   -240f,   240f,  509.5f,    509.5f,    0,     90f,   100f);
            manipulatorParts[4] = new Part(60f,   13f,    110f,  83.5f,     0,         252f,   0f,          90f);
            manipulatorParts[5] = new Part(0,    -360f,   360f,  83.5f,     0f,        0f,      0f,     0f);
            solver = new Jacobian(manipulatorParts);
        }

        public float[] ToolPosition(float[] angles)
        {
            float[] result = new float[3];
            float[] _angles = (float[])angles.Clone();
            result = solver.ForwardPositionXYZ(deg2rad(_angles));

            return result;
        }
        public float[] inverseKinematics6DOF(float[] angles, float[] destination)
        {
            float[] result = new float[6];
            float[] aaaaa = solver.ForwardPositionXYZ(angles);
            result=solver.solve(deg2rad(angles),destination);
            return rad2deg(result);
        }

        private float[] forwardKinematicsDOF3(Part dof1, Part dof2, Part dof3) //obliczenie kątów 3 pierwszych członów na podstawie XYZ środka dof 3 
        {
            float[] result = new float[3];
            //  dof1.SolveDH();
            // dof2.SolveDH();
            //  dof3.SolveDH();
            var vector = DenseVector.OfArray(new float[] { 0, 0, 0, 1 });
            var resultMatrix = dof1.T.matrix.Multiply(dof2.T.matrix).Multiply(dof3.T.matrix);
            var resultVector = resultMatrix.Multiply(vector);
            return resultVector.ToArray();
        }


    }

}
