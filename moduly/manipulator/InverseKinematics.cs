using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;
using MathNet.Numerics.LinearAlgebra.Solvers;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml.XPath;
using MathNetVector = MathNet.Numerics.LinearAlgebra.Vector<float>;

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
            float[] _angle = new float[angle.Length];
            for (int i = 0; i < angle.Length; i++)
            {
                _angle[i] = angle[i];
                _angle[i] = (_angle[i] * (float)Math.PI) / 180f;
            }
            return _angle;
        }

        public float[] rad2deg(float[] angle)
        {
            float[] _angle = new float[angle.Length];
            for (int i = 0; i < angle.Length; i++)
            {
                _angle[i] = angle[i];
                _angle[i] = (_angle[i] * 180f / (float)Math.PI);
            }
            return _angle;
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
                this.offset = _offset * (float)Math.PI / 180f;
                T.Create(angle, d, a, _psi * (float)Math.PI / 180f);
            }
            public DHMatrix SolveDH(float theta)
            {
                T.Solve(theta + offset);
                return T;
            }

            public float Clamp(float _angle)
            {
                if (_angle < MinimalAngle)
                    return MinimalAngle;
                if (_angle > MaximalAngle) return MaximalAngle;
                return _angle;
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




            public Matrix<float> CalculateJacobian3DOF(float[] q) // Tworzy Jacobian 6x3 dla 3 DOF
            {
                Matrix<float> T = DenseMatrix.CreateIdentity(4);
                DenseMatrix[] T_column = new DenseMatrix[3];
                Matrix<float> o = DenseMatrix.Create(3, 3, 0f);

                // Tworzymy macierz 3x3 z pozycjami
                for (int i = 0; i < 3; i++)
                {
                    _manipulatorParts[i].SolveDH(q[i]); // Zakładając, że masz metodę SolveDH, która aktualizuje macierz transformacji dla danego przegubu
                    Matrix<float> multiplier = _manipulatorParts[i].T.matrix; // Pobieranie macierzy transformacji
                    T = T.Multiply(multiplier);
                    T_column[i] = T.Clone() as DenseMatrix;
                    o.SetColumn(i, T.SubMatrix(0, 3, 3, 1).Column(0)); // Ustawianie kolumny pozycji
                }

                MathNet.Numerics.LinearAlgebra.Vector<float> z = DenseVector.OfArray(new float[] { 0, 0, 1 });
                MathNet.Numerics.LinearAlgebra.Vector<float>[] z_vectors = new MathNet.Numerics.LinearAlgebra.Vector<float>[3];

                for (int i = 0; i < 3; i++)
                {
                    if (i == 0)
                    {
                        z_vectors[i] = z; // Pierwszy przegub, oś Z
                    }
                    else
                    {
                        z_vectors[i] = T_column[i - 1].SubMatrix(0, 3, 2, 1).Column(0); // Wektor osi Z dla kolejnych przegubów
                    }
                }

                Matrix<float> J = DenseMatrix.Create(6, 3, 0f);
                MathNet.Numerics.LinearAlgebra.Vector<float> o_n = o.Column(2); // Pozycja końcówki narzędzia
                for (int i = 0; i < 3; i++)
                {
                    MathNet.Numerics.LinearAlgebra.Vector<float> z_vector = z_vectors[i];
                    MathNet.Numerics.LinearAlgebra.Vector<float> o_i = o.Column(i);
                    MathNet.Numerics.LinearAlgebra.Vector<float> crossProduct = DenseVector.OfArray(new float[]
                    {
            z_vector[1] * (o_n[2] - o_i[2]) - z_vector[2] * (o_n[1] - o_i[1]),
            z_vector[2] * (o_n[0] - o_i[0]) - z_vector[0] * (o_n[2] - o_i[2]),
            z_vector[0] * (o_n[1] - o_i[1]) - z_vector[1] * (o_n[0] - o_i[0])
                    });

                    J.SetSubMatrix(0, 3, i, 1, crossProduct.ToColumnMatrix()); // Pierwsze trzy wiersze - pozycje
                    J.SetSubMatrix(3, 3, i, 1, z_vector.ToColumnMatrix()); // Kolejne trzy wiersze - orientacje
                }

                return J;
            }
            public Matrix<float> CalculateJacobian(float[] q, int dof) //Tworzy jakobian 6x6, pierwsze 3 wiersze - pozycje, 3 kolejne wiersze - orientacja
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

            public float[] ForwardPositionXYZ(float[] q, int DOF)
            {
                float[] result = new float[3];
                Matrix<float> T = DenseMatrix.CreateIdentity(4);
                for (int i = 0; i < DOF; i++)
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
                float[] xyz = ForwardPositionXYZ(a, 5);
                return (float)Math.Sqrt((xyz[0] - b[0]) * (xyz[0] - b[0]) + (xyz[1] - b[1]) * (xyz[1] - b[1]) + (xyz[2] - b[2]) * (xyz[2] - b[2]));
            }
            public Matrix<float> ForwardKinematics(float[] q, int DOF)
            {
                Matrix<float> T = DenseMatrix.CreateIdentity(4);
                for (int i = 0; i < DOF; i++)
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
            private MathNetVector ObjectiveFunction(float[] q, MathNetVector targetPosition, int DOF)
            {
                Matrix<float> endEffectorTransform = ForwardKinematics(q, 5);

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

                        MathNetVector f1 = ObjectiveFunction(q1, targetPosition, 5);
                        MathNetVector f2 = ObjectiveFunction(q2, targetPosition, 5);
                        MathNetVector f3 = ObjectiveFunction(q3, targetPosition, 5);
                        MathNetVector f4 = ObjectiveFunction(q4, targetPosition, 5);

                        float hessianValue = (f1[0] - f2[0] - f3[0] + f4[0]) / (4 * epsilon * epsilon);

                        hessian[i, j] = hessianValue;
                    }
                }

                return hessian;
            }

            private float DistanceFromDOFtoPoint(float[] angles, MathNetVector b, int dof)
            {

                float[] xyz = ForwardPositionXYZ(angles, dof);
                return (float)Math.Sqrt((xyz[0] - b[0]) * (xyz[0] - b[0]) + (xyz[1] - b[1]) * (xyz[1] - b[1]) + (xyz[2] - b[2]) * (xyz[2] - b[2]));

            }
            public float[] NewRaph(float[] startPosition, MathNetVector targetPosition)
            {
                int maxIterations = 5000;
                float tolerance = 0.1f;
                float initialScale = 1.0f;
                int numVariables = 3;
                float[] a = new float[6];
                for (int iteration = 0; iteration < maxIterations; iteration++)
                {
                    MathNetVector functionValues = ObjectiveFunction(startPosition, targetPosition, 5);
                    Matrix<float> jacobianMatrix = CalculateJacobian(startPosition,5);
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

                    float[] sasas1 = ForwardPositionXYZ(startPosition, 5);



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
                    a = rad2deg(startPosition);
                    float[] sasas = ForwardPositionXYZ(newPosition, 5);
                    previousError = newError;

                    Console.WriteLine($"Iteration {iteration}, Error: {newError}, Scale: {scale}");

                    if (newError < tolerance)
                    {
                        Console.WriteLine("Converged after " + iteration + " iterations.");
                        return startPosition;
                    }

                }
                //return startPosition;
                throw new Exception("Algorithm did not converge");
            }
          

            bool checkAngles(float[] oldAngles, float[] angles)
            {
                for (int i = 0; i < angles.Length; i++)
                    if (oldAngles[i] != angles[i])
                        return false;
                return true;
            }
            float distanceFromAtoBpoint(float[] a, float[] b)
            {
                return (float)Math.Sqrt((a[0] - b[0]) * (a[0] - b[0]) + (a[1] - b[1]) * (a[1] - b[1]) + (a[2] - b[2]) * (a[2] - b[2]));
            }

            public float partialGradient(float[] startAngle, MathNetVector targetPosition, int i)
            {
                float[] _angles = new float[startAngle.Length];
                startAngle.CopyTo(_angles, 0);

                float samplingDistance = 0.01f;
                float f_x = DistanceFromPoint(_angles, targetPosition);
                float gradient = new float();

                for (int j = 0; j < _angles.Length; j++)
                    _angles[j] += samplingDistance;
                float f_x_plus_d = DistanceFromPoint(_angles, targetPosition);

                gradient = (f_x_plus_d - f_x) / samplingDistance;


                return gradient;
            }
            public float[] rad2deg(float[] angle)
            {
                float[] _angle = new float[angle.Length];
                for (int i = 0; i < angle.Length; i++)
                {
                    _angle[i] = angle[i];
                    _angle[i] = (_angle[i] * 180f / (float)Math.PI);
                }
                return _angle;
            }

            private float deg2rad(float a)
            {
                return (float)(Math.PI * a / 180f);
            }
            ///                         Kinematyka 5DOF
            //////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////
            public float[] solveEffector(float[] startAngles, float[] destination)
            {
                float[] result = new float[6];
                float[] result2 = new float[6];
                float[] resultd = new float[6];
                float[] result2d = new float[6];

                MathNetVector _destination = DenseVector.OfArray(new float[] { destination[0], destination[1], destination[2], destination[3], destination[4], destination[5] });
                //testJacobians();
                 result2 = FindSoultion3DOF(startAngles, _destination);
                 result = FindSoultion5DOF(startAngles, _destination,5);
                result2d = rad2deg(result2);
                resultd = rad2deg(result);
                return result;
            }
            public float[] FindSoultion5DOF(float[] _startAngle, MathNetVector targetPosition, int dof)
            {
                float[] startAngle = new float[6];
                float[] functionResult = new float[7];
                _startAngle.CopyTo(startAngle, 0);
                float[] result = new float[6];
                float[] newAngles = new float[6];
                float movementWeight = 10e7f;
                float actualWeight = new float();
                int iterations = 10;
                float a1diff = (_manipulatorParts[1].MaximalAngle - _manipulatorParts[1].MinimalAngle) / (float)iterations;
                for (int a1 = 0; a1 < 10; a1++)
                {
                  //  startAngle[1] = _manipulatorParts[1].MinimalAngle + a1diff * (float)a1;
                    functionResult = Calculate5DOFAngles(startAngle, targetPosition, dof);
                    for (int i = 0; i < newAngles.Length; i++)
                        newAngles[i] = functionResult[i];

                    actualWeight = CalculateMovementWeight(_startAngle, newAngles);

                    if (actualWeight < movementWeight && functionResult[6] < 10000f)
                    {
                        newAngles.CopyTo(result, 0);
                        movementWeight = actualWeight;
                    }

                }
                if (movementWeight > 30)
                {
                    return _startAngle;
                }
                return result;
            }

            private void testJacobians()
            {
                float []angles = new float[6];
                float[] xyz = new float[3];
                Matrix<float> jacobian = Matrix<float>.Build.Dense(6, 6);
                MathNetVector xyzv = MathNetVector.Build.DenseOfArray(xyz);
                float[] maximals = new float[3];
                float[] minimals= new float[3];
                minimals[0] = (float)10e7;
                minimals[1] = (float)10e7;
                minimals[2] = (float)10e7;
                float det = new float();
                int x, y, z;
                for ( x = 200; x < 1000; x+=10)
                {
                    for( y = 0; y< 1000; y += 100)
                    {
                        for( z = 0; z < 1000; z += 100)
                        {
                            xyz[0] = (float)x;
                            xyz[1] = (float)y;
                            xyz[2] = (float)z;
                            xyzv = MathNetVector.Build.DenseOfArray(xyz);
                            angles = FindSoultion3DOF(angles, xyzv);
                            jacobian = CalculateJacobian(angles, 6);
                            det = jacobian.Determinant();
                            if(det !=0)
                            {
                                maximals[0] = Math.Max(maximals[0], x);
                                minimals[0] = Math.Min(minimals[0], x);

                                maximals[1] = Math.Max(maximals[1], y);
                                minimals[1] = Math.Min(minimals[1], y);
                                maximals[2] = Math.Max(maximals[2], z);
                                minimals[2] = Math.Min(minimals[2], z);
                            }

                        }
                        Console.WriteLine($"{x}  {y}");
                    }
                    //Console.WriteLine($"{ x}" );

                }

            }
            public float[] Calculate5DOFAngles(float[] startAngle, MathNetVector targetPosition, int dof)
            {
                float[] result = new float[7]; // kąty + distance
                float[] targetPoint = new float[6];
                targetPoint[0] = targetPosition[0];
                targetPoint[1] = targetPosition[1];
                targetPoint[2] = targetPosition[2];
                
                float[] newAngles = new float[6];
                startAngle.CopyTo(newAngles, 0);
                float[] oldAngles = new float[6];
                newAngles.CopyTo(oldAngles, 0);
                float distance = DistanceFromDOFtoPoint(newAngles, targetPosition, dof);

                float radius = (float)Math.Sqrt(targetPoint[0] * targetPoint[0] + targetPoint[1] * targetPoint[1]);
                //float rotationZ = (float)Math.Atan2(targetPosition[1], targetPosition[0]) + (float)Math.Atan2(_manipulatorParts[2].length, radius);
               // newAngles[0] = rotationZ;
                float tolerance = 0.1f;
                float[] newRaphValues = new float[6];
                float[] anglesWithOffsets = new float[6];
                Matrix<float> jacobian = Matrix<float>.Build.Dense(6, 6);
                Matrix<float> F = Matrix<float>.Build.Dense(6,1);
                Matrix<float> newAnglesVector = Matrix<float>.Build.Dense(3, 1);
                Matrix<float> oldAnglesVector = Matrix<float>.Build.Dense(3, 1);
                for (int steps = 0; steps < 200; steps++)
                {
                    
                    newAngles.CopyTo(anglesWithOffsets, 0);
                    anglesWithOffsets[1] = -anglesWithOffsets[0] + deg2rad(25);
                    anglesWithOffsets[1] = anglesWithOffsets[1] + deg2rad(50);
                   anglesWithOffsets[2] = anglesWithOffsets[2] - deg2rad(60);

                    float[] deg = rad2deg(anglesWithOffsets);
                    distance = DistanceFromDOFtoPoint(anglesWithOffsets, targetPosition, dof);
                    
                    jacobian = CalculateJacobian3DOF(anglesWithOffsets);
                    if (distance < tolerance)
                    {
                        for (int i = 0; i < anglesWithOffsets.Length; i++)
                            result[i] = anglesWithOffsets[i];
                        result[6] = distance;
                        return result;
                    }
                    
                    
                        Matrix <float> a= 0.1f * jacobian.PseudoInverse() * F_values(oldAngles, targetPoint, dof);
                    newAngles.CopyTo(oldAngles, 0);
                        newAnglesVector = oldAnglesVector - 0.1f*jacobian.PseudoInverse()* F_values(oldAngles,targetPoint, dof);
                    newAngles[0] = newAnglesVector[0, 0];
                    newAngles[1] = newAnglesVector[1, 0];
                    newAngles[2] = newAnglesVector[2, 0];
                    deg = rad2deg(newAngles);
                    distance = DistanceFromDOFtoPoint(newAngles, targetPosition, dof);
                    if (distance < tolerance)
                    {
                        for (int i = 0; i < anglesWithOffsets.Length; i++)
                            result[i] = anglesWithOffsets[i];
                        result[6] = distance;
                        return result;
                    }


                }
                for (int i = 0; i < anglesWithOffsets.Length; i++)
                    result[i] = anglesWithOffsets[i];
                result[6] = distance;
                return result;
            }
            
            Matrix<float> F_values(float[] angles, float[] point, int dof)
            {
               Matrix <float> endEffectorTransform = ForwardKinematics(angles, 5);
                

                float[] endEffectorPositionArray = new float[6];
                endEffectorPositionArray[0] = endEffectorTransform[0, 3]; // x
                endEffectorPositionArray[1] = endEffectorTransform[1, 3]; // y
                endEffectorPositionArray[2] = endEffectorTransform[2, 3]; // z
                Matrix<float> destination= Matrix<float>.Build.DenseOfColumnArrays(point);

                Matrix<float> endEffectorPosition = Matrix<float>.Build.DenseOfColumnArrays(endEffectorPositionArray);

                // Oblicz wektor różnicy między pozycją końcówki a targetPosition
                Matrix<float> difference = endEffectorPosition - destination;

                return difference;
            }


            ///                         Kinematyka 3DOF
            //////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////
            public float[] solveTool(float[] startAngles, float[] destination)
            {
                float[] result = new float[6];
                MathNetVector _destination = DenseVector.OfArray(new float[] { destination[0], destination[1], destination[2], destination[3], destination[4], destination[5] });
                result = FindSoultion3DOF(startAngles, _destination);
                return result;
            }

            public float[] NewRaphValuesDOF3(float angle1, float angle2, float r, float z) //zwraca 6 zmiennych, funkcję i jakobian. [F(a) F(b) F'1(a) F'1(b) F'2(a) F'2(b)]
            {
                float d1 = _manipulatorParts[1].length;
                float d2 = _manipulatorParts[3].length;
                angle1 -= (float)(Math.PI / 2);
                angle2 += (float)(Math.PI / 2);
                float[] result = new float[6];
                result[0] = (float)(d1 * Math.Cos(angle1) + d2 * Math.Cos(angle1 + angle2)) - r;
                result[1] = (float)(d1 * Math.Sin(angle1) + d2 * Math.Sin(angle1 + angle2)) - z;

                //Jakobian
                result[2] = (float)(-d1 * Math.Sin(angle1) - d2 * Math.Sin(angle1 + angle2));
                result[3] = (float)(-d2 * Math.Sin(angle1 + angle2));
                result[4] = (float)(d1 * Math.Cos(angle1) + d2 * Math.Cos(angle1 + angle2));
                result[5] = (float)(d2 * Math.Cos(angle1 + angle2));
                return result;
            }
            float CalculateMovementWeight(float[] oldAngle, float[] newAngle) //Ocena najlepszego rozwiązania na podstawie ilości ruchu i dokładności
            {
                float result = new float();
                float[] diff = new float[6];
                for(int i = 0; i<oldAngle.Length; i++)
                    diff[i] = (float)Math.Abs(newAngle[i] - oldAngle[i]);
                result = (diff[0]*10f + diff[1] * 5f + diff[2]*2f + diff[3] *1f + diff[4] * 0.5f + diff[5]*0.1f);
                return result;
            }
            public float[] FindSoultion3DOF(float[] _startAngle, MathNetVector targetPosition)
            {
                float[] startAngle = new float[6];
                float[] functionResult = new float[7];
                _startAngle.CopyTo(startAngle, 0); 
                float[] result = new float[6];
                float[] newAngles = new float[6];
                float movementWeight = 10e7f;
                float actualWeight = new float();
                int iterations = 10;
                float a1diff = (_manipulatorParts[1].MaximalAngle - _manipulatorParts[1].MinimalAngle) / (float) iterations;
                for (int a1 = 0; a1 < 10; a1++)
                {
                    startAngle[1] = _manipulatorParts[1].MinimalAngle + a1diff * (float)a1;
                    functionResult = Calculate3DOFAnglesNewton(startAngle, targetPosition);
                    for(int i=0; i<newAngles.Length; i++)
                        newAngles[i] = functionResult[i];

                    actualWeight = CalculateMovementWeight(_startAngle, newAngles);
                 
                    if (actualWeight < movementWeight && functionResult[6]<1f)
                    {
                        newAngles.CopyTo(result, 0);
                        movementWeight = actualWeight;
                    }
                    
                }
                if(movementWeight>30)
                {
                    return _startAngle;
                }
                return result;
            }
            public float[] Calculate3DOFAnglesNewton(float[] startAngle, MathNetVector targetPosition)
            {
                float[] result = new float[7]; // kąty + distance
                float[] targetPoint = new float[3];
                targetPoint[0] = targetPosition[0];
                targetPoint[1] = targetPosition[1];
                targetPoint[2] = targetPosition[2];

                float[] newAngles = new float[6];
                startAngle.CopyTo(newAngles, 0);
                float[] oldAngles = new float[6];
                newAngles.CopyTo(oldAngles, 0);
                float distance = DistanceFromDOFtoPoint(newAngles, targetPosition,4);

                float radius = (float)Math.Sqrt(targetPoint[0] * targetPoint[0] + targetPoint[1] * targetPoint[1]);
                float rotationZ = (float)Math.Atan2(targetPosition[1], targetPosition[0]) + (float)Math.Atan2(_manipulatorParts[2].length, radius);
                newAngles[0] = rotationZ;
                float tolerance = 0.1f;
                float[] newRaphValues = new float[6];
                float[] anglesWithOffsets = new float[6];
                for (int steps = 0; steps < 100; steps++)
                {

                    newAngles.CopyTo(anglesWithOffsets, 0);
                    anglesWithOffsets[1] = anglesWithOffsets[1] + deg2rad(50);
                    anglesWithOffsets[2] = anglesWithOffsets[2] - deg2rad(60);
                    distance = DistanceFromDOFtoPoint(anglesWithOffsets, targetPosition,4);
                    if (distance < tolerance)
                    {
                        for (int i = 0; i < anglesWithOffsets.Length; i++)
                            result[i] = anglesWithOffsets[i];
                        result[6] = distance;
                        return result;
                    }
                    newRaphValues = NewRaphValuesDOF3(newAngles[1], newAngles[2], radius, (-targetPoint[2] + _manipulatorParts[0].length));
                    float determinant = newRaphValues[2] * newRaphValues[5] - newRaphValues[3] * newRaphValues[4];
                    if (determinant != 0)
                    {
                        newAngles.CopyTo(oldAngles, 0);
                        newAngles[1] = oldAngles[1] - 0.1f * (1.0f / determinant) * (newRaphValues[5] * newRaphValues[0] - newRaphValues[3] * newRaphValues[1]);
                        newAngles[2] = oldAngles[2] - 0.1f * (1.0f / determinant) * (-newRaphValues[4] * newRaphValues[0] + newRaphValues[2] * newRaphValues[1]);
                        newAngles[1] = _manipulatorParts[1].Clamp(newAngles[1]);
                        newAngles[2] = _manipulatorParts[2].Clamp(newAngles[2]);
                    }

                    distance = DistanceFromDOFtoPoint(newAngles, targetPosition,4);
                    if (distance < tolerance)
                    {
                        for(int i = 0; i< anglesWithOffsets.Length; i++)
                            result[i] = anglesWithOffsets[i];
                        result[6] = distance;
                        return result;
                    }


                }
                for (int i = 0; i < anglesWithOffsets.Length; i++)
                    result[i] = anglesWithOffsets[i];
                result[6] = distance;
                return result;
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
            manipulatorParts[0] = new Part(0f, -180f, 90f, 169.5f, 169.5f, 0f, -90f, 0f);         //Inicjacja startowa wartości manipulatora, to tutaj można zmieniać ograniczenia i parametry
            manipulatorParts[1] = new Part(50f, -60f, 90f, 550f, 0f, 550f, 0f, -90f);
            manipulatorParts[2] = new Part(11f, -60f, 70f, 14f, -14f, 0f, -90f, 0f);
            manipulatorParts[3] = new Part(0f, -180, 180, 509.5f, 509.5f, 0, 90f, 100f);
            manipulatorParts[4] = new Part(60f, -90f, 60f, 83.5f, 0, 252f, 0f, 90f);
            manipulatorParts[5] = new Part(0, -360f, 360f, 83.5f, 0f, 0f, 0f, 0f);
            solver = new Jacobian(manipulatorParts);
        }

        public float[] ToolPosition(float[] angles)
        {
            float[] result = new float[3];
            float[] _angles = (float[])angles.Clone();
            result = solver.ForwardPositionXYZ(deg2rad(_angles), 4);

            return result;
        }
        public float[] inverseKinematicsTool(float[] angles, float[] destination)
        {
            float[] result = new float[6];
            result = solver.solveEffector(deg2rad(angles), destination);
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
