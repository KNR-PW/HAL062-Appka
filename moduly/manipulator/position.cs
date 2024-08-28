using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL062app.moduly.manipulator
{
    public class Position
    {
        public float[] joints { get; set; }
        public float[] relative0 { get; set; } = new float[] { 0,50,-60,100,10,0};
        public int id { get; set; }
        public int Duration { get; set; }
        public Position(float[] angles)
        {
            Duration = 5;
            if (angles != null && angles.Length >= 6)
            {
                joints = new float[6];
                for (int i = 0; i < 6; i++)
                {
                    joints[i] = angles[i];
                }
            }

            
        }
        public void addRelative0(float[] relative)
        {
            this.relative0 = new float[6];

            this.relative0[0] = relative[0];
            this.relative0[1] = relative[1];
            this.relative0[2] = relative[2];
            this.relative0[3] = relative[3];
            this.relative0[4] = relative[4];
            this.relative0[5] = relative[5];

        }
        public void update(float[] angles)
        {
            joints[0] = angles[0]; // w stopniach 
            joints[1] = angles[1];
            joints[2] = angles[2];
            joints[3] = angles[3];
            joints[4] = angles[4];
            joints[5] = angles[5];
        }
        public Position deepCopy()
        {
            Position copy = new Position(joints);
            copy.Duration = this.Duration;
            copy.id = this.id;
            //copy.relative0 = this.relative0;
            return copy;
        }
        public void addRelativeToJoints()
        {
            for(int i = 0; i<6; i++)
                joints[i] += relative0[i];

            
        }

    }
}
