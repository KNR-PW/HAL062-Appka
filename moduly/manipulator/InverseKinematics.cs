using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL062app.moduly.manipulator
{
   

    class DHMatrix
    {
        public float[,] matrix = new float[4,4];

        public DHMatrix ()
        {
            matrix = new float[4, 4];


        }

    }

/*
    private DHMatrix DHfunction(float theta, float d, float a, float alpha)
    {
        DHMatrix DH = new DHMatrix();

        DH.matrix[0, 0] = (float)Math.Cos(theta);
        DH.matrix[0, 1] = (float)(-Math.Sin(theta) * Math.Cos(alpha));
        DH.matrix[0, 2] = (float)(Math.Sin(theta) * Math.Sin(alpha));
        DH.matrix[0, 3] = (float)(a * Math.Cos(theta));
          
        DH.matrix[1, 0] = (float)(Math.Sin(theta));
        DH.matrix[1, 1] = (float)(Math.Cos(theta) * Math.Cos(alpha));
        DH.matrix[1, 2] = (float)(-Math.Cos(theta) * Math.Sin(alpha));
        DH.matrix[1, 3] = (float)(a * Math.Sin(theta));       
          
        DH.matrix[2, 0] = 0;
        DH.matrix[2, 1] = (float)(Math.Sin(alpha));
        DH.matrix[2, 2] = (float)(Math.Cos(alpha));
        DH.matrix[2, 3] = d;
          
        DH.matrix[3, 0] = 0;
        DH.matrix[3, 1] = 0;
        DH.matrix[3, 2] = 0;
        DH.matrix[3, 3] = 1;

        return DH;
    }

    public class InverseKinematics
    {
    }
*/
}
