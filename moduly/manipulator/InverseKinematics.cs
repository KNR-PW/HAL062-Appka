using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/*  Poniższy plik może wydawać się pojebany i taki jest.
 *  Historia liczenia kinematyki odwrotnej manipulatora sięga dziejów, gdy ten powstawał. Niestety soft napisany na STM (który liczył inverse kinematics) nie został nigdzie zapisany i był problematyczny ze względu na swoje mało zaawansowane sposoby liczenia pozycji i częste wypadki np. losowe obrócenie i walnięcie w coś 
 *  W związku z tym powstała myśl, aby wszystko liczone było w aplikacji i chciałem, aby było to zrobione konkretnie i profesjonalnie.
 *  Poniższy plik zawiera kompleksowy silnik matematyczny do wyliczenia całej kinematyki manipulatora (w tym prędkości i przyśpieszeń). W planie było wykrywanie kolizji oraz poruszanie członami na podstawie wzorów matematycznych i innych instrukcji.
 *  Do napisania bardzo przydały się materiały z przedmiotu Dynamika Układów Wieloczłonowych prowadzone przez Wojtyrę na Melu
 *  Jeśli nie wiesz co robisz, to lepiej nie ruszaj tego pliku.
 * 
 * 
 *  Co więcej na napisanie go planowałem poświęcić tydzień, bo 4 zostały do zawodów ERC 2024
 *  ~ Dominik Chmielak
 * */
namespace HAL062app.moduly.manipulator
{
   
    class Part
    {
        Vector<float> position;
        float angle;
        float MaximalAngle;
        float MinimalAngle;
        float length;

    }

    class Jacobian
    {
        public Jacobian()
        {

        }
        public void Solver()
        {

        }

        private float[] NewRaph(float[] newton) //metoda Newtona-Raphsona do znajdowania miejsc zerowych funkcji
        {
            float[] result = new float[newton.Length];

            return result;

        }

        private float Taylor(float q, float dq, float d2q, float dt) //Przybliżenie miejsca startowego dla metody Newtona, wykorzystuje rozwinięcie w szereg Taylora
        {
            float q0 = q+dq+0.5f*d2q*dt*dt;
            return q0;
        }
    }
    class DHMatrix
    {
        public float[,] matrix = new float[4,4];

        public DHMatrix ()
        {
            matrix = new float[4, 4];
        }
        public void Create(float theta, float d, float a, float alpha) //funkcja obliczajaca macierz denavita-hartenberga
        {
            this.matrix[0, 0] = (float)Math.Cos(theta);
            this.matrix[0, 1] = (float)(-Math.Sin(theta) * Math.Cos(alpha));
            this.matrix[0, 2] = (float)(Math.Sin(theta) * Math.Sin(alpha));
            this.matrix[0, 3] = (float)(a * Math.Cos(theta));
            
            this.matrix[1, 0] = (float)(Math.Sin(theta));
            this.matrix[1, 1] = (float)(Math.Cos(theta) * Math.Cos(alpha));
            this.matrix[1, 2] = (float)(-Math.Cos(theta) * Math.Sin(alpha));
            this.matrix[1, 3] = (float)(a * Math.Sin(theta));
            
            this.matrix[2, 0] = 0.0f;
            this.matrix[2, 1] = (float)(Math.Sin(alpha));
            this.matrix[2, 2] = (float)(Math.Cos(alpha));
            this.matrix[2, 3] = d;
            
            this.matrix[3, 0] = 0.0f;
            this.matrix[3, 1] = 0.0f;
            this.matrix[3, 2] = 0.0f;
            this.matrix[3, 3] = 1.0f;
        }
        public DHMatrix Multiply(DHMatrix a, DHMatrix b)
        {
            DHMatrix result = new DHMatrix();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    result.matrix[i, j] = 0;


                    for (int k = 0; k < 4; k++)
                    {
                        result.matrix[i, j] += a.matrix[i, k] * b.matrix[k, j];
                    }
                }
            }

            return result;
        }
        public float[] MultiplyWithVector(float[] vector)
        {
            float[] result = new float[4];

            for (int i = 0; i < 4; i++)
            {
                result[i] = 0;
                for (int j = 0; j < 4; j++)
                {
                    result[i] += matrix[i, j] * vector[j];
                }
            }

            return result;
        }

        public DHMatrix Inverse()
        {

            DHMatrix result = new DHMatrix();
            float[,] m = (float[,])matrix.Clone();
            float[,] inv = new float[4, 4];
            float det;

            // Przygotowanie macierzy jednostkowej
            for (int i = 0; i < 4; i++)
                inv[i, i] = 1;

            // Rozpoczęcie procesu eliminacji Gaussa-Jordana
            for (int i = 0; i < 4; i++)
            {
                // Szukaj wiersza z największym elementem w kolumnie
                int maxRow = i;
                for (int k = i + 1; k < 4; k++)
                    if (Math.Abs(m[k, i]) > Math.Abs(m[maxRow, i]))
                        maxRow = k;

                // Zamień wiersze
                if (i != maxRow)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        float temp = m[i, k];
                        m[i, k] = m[maxRow, k];
                        m[maxRow, k] = temp;

                        temp = inv[i, k];
                        inv[i, k] = inv[maxRow, k];
                        inv[maxRow, k] = temp;
                    }
                }

                // Sprawdź, czy macierz jest osobliwa
                if (Math.Abs(m[i, i]) < 1e-10)
                    throw new InvalidOperationException("Macierz jest osobliwa i nie ma odwrotności.");

                // Skaluj wiersz, aby uzyskać 1 na przekątnej
                float scale = m[i, i];
                for (int k = 0; k < 4; k++)
                {
                    m[i, k] /= scale;
                    inv[i, k] /= scale;
                }

                // Zrób zera w innych wierszach
                for (int k = 0; k < 4; k++)
                {
                    if (k == i) continue;

                    float factor = m[k, i];
                    for (int j = 0; j < 4; j++)
                    {
                        m[k, j] -= factor * m[i, j];
                        inv[k, j] -= factor * inv[i, j];
                    }
                }
            }

            // Skopiuj wynik do obiektu result
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    result.matrix[i, j] = inv[i, j];

            return result;
        }
    }


    

    public class InverseKinematics
    {
        private Vector<float> xyz;



        //Zostały zaprojektowane różne możliwe tryby obsługi manipulatora, które można poniżej wybrać 
        // mode 0 - podajemy xyz chwytaka (najdalszy punkt, a nie podstawa)
        // mode 1 - podajemy xyz 3 dofu (historycznie pierwszy powstały tryb, bo potrzebny do obliczania innych)
        //

        public InverseKinematics(Position start, int mode, Vector <float> destination) {
        
        
        }

        private float[] forwardDOF3(float[] joints, Vector <float> xyz) //obliczenie kątów 3 pierwszych członów na podstawie XYZ środka dof 3 
        {
            float[] result = new float[3];



            return result;
        }


    }

}
