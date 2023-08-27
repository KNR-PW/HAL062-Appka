 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Windows.Media.Media3D;
using System.Windows.Media;

namespace HAL062app.moduly.manipulator
{
    public struct xyz
    {
        public double x;
        public double y;
        public double z;
    }
    public struct Orientation
    {
        public double yaw;
        public double pitch;
        public double roll;
    }
    public class F2I
    {
        public double[] f = new double[6];
        public uint[] i = new uint[6];
    }
    public enum Disconnect
    {
        NO,
        DOF6,
        DOF456
    }
    public enum MMode
    {
        DISABLED = 0,           //wylaczone sterowniki
        HOLD,               //trzymanie pozycji
        ANGLES,             //podawanie kolejnych katow dof
        VELOCITY,           //praca na predkosci zlaczowa
        XYZ_GLOBAL,
        VEL_GLOB,
        VEL_TOOL,
        DELTA_TOOL,
        TRAJECTORY
    }
    public class InverseKinematics
    {
        private static double DOF1MAX = Math.PI * 2.0 / 3.0;
        private static double DOF1MIN = -Math.PI * 2.0 / 3.0;
        private static double DOF2MAX = Math.PI / 2;
        private static double DOF2MIN = -Math.PI / 3;
        private static double DOF3MAX = Math.PI / 3.0;
        private static double DOF3MIN = -Math.PI / 3.0;
        private static double DOF4MAX = Math.PI;
        private static double DOF4MIN = -Math.PI;
        private static double DOF5MAX = Math.PI * 13.0 / 18.0;
        private static double DOF5MIN = -Math.PI * 4.0 / 9.0;
        private static double DOF6MAX = Math.PI;
        private static double DOF6MIN = -Math.PI;
        private static double TOLERANCE = 0.01;
        private static double VELOCITY_DELTA_T = 0.01;

        private static readonly double[] DOF_MAX = { DOF1MAX, DOF2MAX, DOF3MAX, DOF4MAX, DOF5MAX, DOF6MAX };
        private static readonly double[] DOF_MIN = { DOF1MIN, DOF2MIN, DOF3MIN, DOF4MIN, DOF5MIN, DOF6MIN };

        private static double[,] UpperAngles = new double[2, 3];
        private static double[,] LowerAngles = new double[4, 3];
        private static double[,] Solutions = new double[8, 6];

        public xyz WristPosition, TCPPosition, Velocity;
        public Orientation TCPOrientation, Omega;
        public MMode ManipulatorMode;
        public Disconnect DisconnectDoF;

        public F2I angle, speed, meas_angle, read_value, kinematics_in;
        

        public byte GripperState; // tryb chwytaka 0 - STOP, 1 - OTWORZ, 2 - ZAMKNIJ
        public double JointVelocity;
        public volatile byte newmode; // flaga oznaczajaca nowy tryb


        private double [,] DHMatrix = new double[4, 4];

        void CalculateInverseKinematics456DOF(bool XYZ_GLOBAL, double[] kinematics_in) // tylko dof1, dof2, dof3
        {
            WristPosition.x = kinematics_in[0];
            WristPosition.y = kinematics_in[1];
            WristPosition.z = kinematics_in[2];
            CalculateLowerAngles();

        }
        void CalculateLowerAngles()
        {
            double thetaDOF1fwd, thetaDOF1bwd;
            double thetaDOF2fwdTop, thetaDOF2bwdTop, thetaDOF2bwdBottom, thetaDOF2fwdBottom;
            double thetaDOF3Bottom, thetaDOF3Top;
            double temp1, temp2;

            if(Math.Abs(WristPosition.x)< 0.001 && Math.Abs(WristPosition.y) <0.001)
            {
                thetaDOF1fwd = angle.f[0];
                thetaDOF1bwd = angle.f[0];
            }
            else
            {
                temp1 = Math.Atan2(WristPosition.y, WristPosition.x);
                
                if (temp1 > 0.0) temp2 = temp1 - Math.PI;
                else temp2 = temp1 + Math.PI;
                
                if(WristPosition.x>0.0)
                {
                    thetaDOF1fwd = temp1;
                    thetaDOF1bwd = temp2;
                }else
                {
                    thetaDOF1fwd = temp2;
                    thetaDOF1bwd = temp1;
                }
            }

            double x2y2 = Math.Pow(WristPosition.x, 2.0) + Math.Pow(WristPosition.y, 2.0);
            double c2 = x2y2 + Math.Pow(WristPosition.z, 2.0);
            double c = Math.Sqrt(c2);
            //double alfa = Math.Acos()
        }

        void CalculatePositionIncrement()
        {

            if (ManipulatorMode ==  MMode.VEL_GLOB)
            {
                if (DisconnectDoF == Disconnect.DOF456)
                {
                    WristPosition.x = WristPosition.x + Velocity.x * VELOCITY_DELTA_T;
                    WristPosition.y = WristPosition.y + Velocity.y * VELOCITY_DELTA_T;
                    WristPosition.z = WristPosition.z + Velocity.z * VELOCITY_DELTA_T;

                }
                else
                {
                    TCPPosition.x = TCPPosition.x + Velocity.x * VELOCITY_DELTA_T;
                    TCPPosition.y = TCPPosition.y + Velocity.y * VELOCITY_DELTA_T;
                    TCPPosition.z = TCPPosition.z + Velocity.z * VELOCITY_DELTA_T;
                    TCPOrientation.yaw = TCPOrientation.yaw + Omega.yaw * VELOCITY_DELTA_T;
                    TCPOrientation.pitch = TCPOrientation.pitch + Omega.pitch * VELOCITY_DELTA_T;
                    TCPOrientation.roll = TCPOrientation.roll + Omega.roll * VELOCITY_DELTA_T;
                }
            }
            if (ManipulatorMode == MMode.VEL_TOOL)
            {
                if (DisconnectDoF == Disconnect.DOF456)
                {
                    double[] tempVector = { Velocity.x, Velocity.y, Velocity.z }; ;
                    
                    double[] VectorOut = new double[3];
                  //  TransformToolWrist(tempVector, VectorOut);
                    WristPosition.x = WristPosition.x + VectorOut[0] * VELOCITY_DELTA_T;
                    WristPosition.y = WristPosition.y + VectorOut[1] * VELOCITY_DELTA_T;
                    WristPosition.z = WristPosition.z + VectorOut[2] * VELOCITY_DELTA_T;
                }
                else
                {
                    double[] tempVector = { Velocity.x, Velocity.y, Velocity.z, Omega.yaw, Omega.pitch, Omega.roll };
                    double[] TransformedVector = new double[6];
                    TransformToolFull(tempVector, TransformedVector);
                    TCPPosition.x = TCPPosition.x + TransformedVector[0] * VELOCITY_DELTA_T;
                    TCPPosition.y = TCPPosition.y + TransformedVector[1] * VELOCITY_DELTA_T;
                    TCPPosition.z = TCPPosition.z + TransformedVector[2] * VELOCITY_DELTA_T;
                    TCPOrientation.yaw = TCPOrientation.yaw + TransformedVector[3] * VELOCITY_DELTA_T;
                    TCPOrientation.pitch = TCPOrientation.pitch + TransformedVector[4] * VELOCITY_DELTA_T;
                    TCPOrientation.roll = TCPOrientation.roll + TransformedVector[5] * VELOCITY_DELTA_T;


                }
            }

        }

        void TransformToolFull(double[] VectorIn, double[] VectorOut)
        {
            double[,] temp = new double[6, 3]; // Zmienne temp powinny być wcześniej zainicjowane
            double yaw = VectorIn[3];
            double pitch = VectorIn[4];
            double roll = VectorIn[5];

            double[] DHMat = new double[16]; // Zmienne DHMat powinny być wcześniej zainicjowane
            //CalculateForwardKinematics(meas_angle, temp, 0, ref DHMat);

           // double[] DHMatInv = InvertDHMatrix(DHMat);
            double[,] DHTool = new double[4, 4];

            // Create DH Matrix for desired transformation
            DHTool[0, 0] = (double)Math.Cos(yaw) * (double)Math.Cos(pitch);
            DHTool[0, 1] = (double)Math.Cos(yaw) * (double)Math.Sin(pitch) * (double)Math.Sin(roll) - (double)Math.Sin(yaw) * (double)Math.Cos(roll);
            DHTool[0, 2] = (double)Math.Cos(yaw) * (double)Math.Sin(pitch) * (double)Math.Cos(roll) + (double)Math.Sin(yaw) * (double)Math.Sin(roll);
            DHTool[0, 3] = VectorIn[0];
            DHTool[1, 0] = (double)Math.Sin(yaw) * (double)Math.Cos(pitch);
            DHTool[1, 1] = (double)Math.Sin(yaw) * (double)Math.Sin(pitch) * (double)Math.Sin(roll) + (double)Math.Cos(yaw) * (double)Math.Cos(roll);
            DHTool[1, 2] = (double)Math.Sin(yaw) * (double)Math.Sin(pitch) * (double)Math.Cos(roll) - (double)Math.Cos(yaw) * (double)Math.Sin(roll);
            DHTool[1, 3] = VectorIn[1];
            DHTool[2, 0] = -(double)Math.Sin(pitch);
            DHTool[2, 1] = -(double)Math.Cos(pitch) * (double)Math.Sin(roll);
            DHTool[2, 2] = (double)Math.Cos(pitch) * (double)Math.Cos(roll);
            DHTool[2, 3] = VectorIn[2];
            DHTool[3, 0] = 0.0f;
            DHTool[3, 1] = 0.0f;
            DHTool[3, 2] = 0.0f;
            DHTool[3, 3] = 1.0f;

            double[,] DHOutput = new double[4, 4];

            // Multiply 2 matrices
            
           // DHOutput = MultiplyMatrices(DHMatInv, DHTool);
            VectorOut[0] = DHOutput[0, 3];
            VectorOut[1] = DHOutput[1, 3];
            VectorOut[2] = DHOutput[2, 3];

            // Pitch First
            VectorOut[4] = -(double)Math.Asin(DHOutput[2, 0]);

            // Then roll
            VectorOut[5] = -(double)Math.Asin(DHOutput[2, 1] / Math.Cos(VectorOut[4]));

            // Finally roll
            VectorOut[3] = (double)Math.Acos(DHOutput[0, 0] / Math.Cos(VectorOut[4]));

            return;
        }
    /*    public double[] InvertDHMatrix(double[] In)
        {
            double[] RotMat = new double[9];
            double[] RotMatT = new double[9];
            double[] OutPosition = new double[3];
            double[] InPosition = new double[3];

            // Copy Rotation Matrix from input
            RotMat[0] = In[0];
            RotMat[1] = In[1];
            RotMat[2] = In[2];
            RotMat[3] = In[4];
            RotMat[4] = In[5];
            RotMat[5] = In[6];
            RotMat[6] = In[8];
            RotMat[7] = In[9];
            RotMat[8] = In[10];

            // Invert Rotation Matrix <==> Transpose
            RotMatT =TransposeMatrix(RotMat);

            // Calculate T-part of Inverted DH-Matrix
            InPosition[0] = In[3];
            InPosition[1] = In[7];
            InPosition[2] = In[11];
            OutPosition = MultiplyMatrixVector(RotMatT, InPosition);

            // Copy everything
            //Out[0] = RotMatT[0];
            //Out[1] = RotMatT[1];
            //Out[2] = RotMatT[2];
            //Out[3] = -OutPosition[0];
            //Out[4] = RotMatT[3];
            //Out[5] = RotMatT[4];
            //Out[6] = RotMatT[5];
            //Out[7] = -OutPosition[1];
            //Out[8] = RotMatT[6];
            //Out[9] = RotMatT[7];
            //Out[10] = RotMatT[8];
            //Out[11] = -OutPosition[2];
            //Out[12] = 0.0f;
            //Out[13] = 0.0f;
            //Out[14] = 0.0f;
            //Out[15] = 1.0f;
            //return Out;
        }
        public void TransformToolWrist(double[] VectorIn, ref double[] VectorOut, double[] meas_angle)
        {
            VectorOut[2] = VectorIn[2];
            VectorOut[0] = VectorIn[0] * (double)Math.Cos(meas_angle[0]) - VectorIn[1] * (double)Math.Sin(meas_angle[0]);
            VectorOut[1] = VectorIn[0] * (double)Math.Sin(meas_angle[0]) + VectorIn[1] * (double)Math.Cos(meas_angle[0]);
        }*/
        public static double[,] MultiplyMatrices(double[,] matrix1, double[,] matrix2)
        {
            if (matrix1.GetLength(1) != matrix2.GetLength(0))
            {
                throw new ArgumentException("Number of columns in the first matrix must be equal to the number of rows in the second matrix.");
            }

            int rows1 = matrix1.GetLength(0);
            int cols1 = matrix1.GetLength(1);
            int cols2 = matrix2.GetLength(1);

            double[,] result = new double[rows1, cols2];

            for (int i = 0; i < rows1; i++)
            {
                for (int j = 0; j < cols2; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < cols1; k++)
                    {
                        sum += matrix1[i, k] * matrix2[k, j];
                    }
                    result[i, j] = sum;
                }
            }

            return result;
        }


        public static double[] TransposeMatrix(double[] matrix)
        {
            double[] result = new double[9];

            result[0] = matrix[0];
            result[1] = matrix[3];
            result[2] = matrix[6];
            result[3] = matrix[1];
            result[4] = matrix[4];
            result[5] = matrix[7];
            result[6] = matrix[2];
            result[7] = matrix[5];
            result[8] = matrix[8];

            return result;
        }
        public static double[] MultiplyMatrixVector(double[] matrix, double[] vector)
        {
            double[] result = new double[3];

            result[0] = matrix[0] * vector[0] + matrix[1] * vector[1] + matrix[2] * vector[2];
            result[1] = matrix[3] * vector[0] + matrix[4] * vector[1] + matrix[5] * vector[2];
            result[2] = matrix[6] * vector[0] + matrix[7] * vector[1] + matrix[8] * vector[2];

            return result;
        }
    }

}