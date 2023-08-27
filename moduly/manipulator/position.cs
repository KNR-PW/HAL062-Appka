using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL062app.moduly.manipulator
{
    public class Position
    {
        public double[] joints { get; set; }
        public int id { get; set; }
        public int Duration { get; set; }
        public Position(double[] angles)
        {

            if (angles != null && angles.Length >= 6)
            {
                joints = new double[6];
                for (int i = 0; i < 6; i++)
                {
                    joints[i] = angles[i];
                }
            }
        }
        public void update(double[] angles)
        {
            joints[0] = angles[0]; // w stopniach 
            joints[1] = angles[1];
            joints[2] = angles[2];
            joints[3] = angles[3];
            joints[4] = angles[4];
            joints[5] = angles[5];
        }
        

    }
}
