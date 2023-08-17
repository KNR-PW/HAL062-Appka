using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Resources;
using System.Windows.Shapes;
using HelixToolkit.Wpf;
namespace HAL062app.moduly.manipulator
{

    class Joint
    {
        public Model3D model = null;
        public double angle = 0;
        public double angleMin = -180;
        public double angleMax = 180;
        public double rotPointX = 0;
        public double rotPointY = 0;
        public double rotPointZ = 0;
        public double rotAxisX = 0;
        public double rotAxisY = 0;
        public double rotAxisZ = 0;

        public Joint(Model3D pModel)
        {
            model = pModel;
        }
    }





    
    public partial class manipulatorWPF : UserControl
    {
        Model3DGroup RA = new Model3DGroup();
        Model3D geometry = null;
        List<Joint> joints = null;

        string basePath = "";
        ModelVisual3D visual;
        ModelVisual3D RoboticArm = new ModelVisual3D();
        Transform3DGroup F1;
        Transform3DGroup F2;
        Transform3DGroup F3;
        Transform3DGroup F4;
        Transform3DGroup F5;
        Transform3DGroup F6;
        RotateTransform3D R;
        TranslateTransform3D T;

        Color oldColor = Colors.White;
        bool switchingJoint = false;
        bool isAnimating = false;
        bool isInitialized = false;
        double LearningRate = 0.01;
        double SamplingDistance = 0.15;
        double DistanceThreshold = 20;
        int movements = 10;
        Vector3D reachingPoint;
        System.Windows.Forms.Timer timer1;

        
        private const string MODEL_PATH1 = "dof1.STL";
        private const string MODEL_PATH2 = "dof2.STL";
        private const string MODEL_PATH3 = "dof3.STL";
        private const string MODEL_PATH4 = "dof4.STL";
        private const string MODEL_PATH5 = "dof5.STL";
        private const string MODEL_PATH6 = "gripper.STL";
        private const string MODEL_PATH7 = "base.STL";
        


        public manipulatorWPF()
        {
            
            InitializeComponent();
            basePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\moduly\\manipulator\\models\\";
            List<string> modelsNames = new List<string>();
     

            modelsNames.Add(MODEL_PATH1);
            modelsNames.Add(MODEL_PATH2);
            modelsNames.Add(MODEL_PATH3);
            modelsNames.Add(MODEL_PATH4);
            modelsNames.Add(MODEL_PATH5);
            modelsNames.Add(MODEL_PATH6);
            modelsNames.Add(MODEL_PATH7);

            RoboticArm.Content = Initialize_Environment(modelsNames);
            isInitialized = true;
            var builder = new MeshBuilder(true, true);
            var position = new Point3D(0, 0, 0);
            builder.AddSphere(position, 30, 30, 30);
            geometry = new GeometryModel3D(builder.ToMesh(), Materials.Brown);
            visual = new ModelVisual3D();
            visual.Content = geometry;


            var camera = new PerspectiveCamera
            {
                Position = new Point3D(-1000, 1000, 500),
                LookDirection = new Vector3D(400, -520, -4),
                UpDirection = new Vector3D(0, 0, 1),
                
            };
            viewport.Camera = camera;
            viewport.Children.Add(visual);
            viewport.Children.Add(RoboticArm);
           

            double[] angles = { joints[0].angle, joints[1].angle, joints[2].angle, joints[3].angle, joints[4].angle, joints[5].angle};
            ForwardKinematics(angles);

            changeSelectedJoint();

            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 5;
            timer1.Tick += new System.EventHandler(timer1_Tick);

            
        }
        private double ToDegrees(double radian)
        {
            return radian * 180.0 / Math.PI;
        }
        private double ToRadians(double degrees)
        {
            return degrees * (Math.PI / 180.0);
        }


       

   



        private Model3DGroup Initialize_Environment(List<string> modelsNames)
        {

            try
            {
                ModelImporter import = new ModelImporter();
                joints = new List<Joint>();

                foreach (string modelName in modelsNames)
                {
                    var materialGroup = new MaterialGroup();
                    Color mainColor = Colors.LightCyan;
                    EmissiveMaterial emissMat = new EmissiveMaterial(new SolidColorBrush(mainColor));
                    DiffuseMaterial diffMat = new DiffuseMaterial(new SolidColorBrush(mainColor));
                    SpecularMaterial specMat = new SpecularMaterial(new SolidColorBrush(mainColor), 200);
                    materialGroup.Children.Add(emissMat);
                    materialGroup.Children.Add(diffMat);
                    materialGroup.Children.Add(specMat);

                    var link = import.Load(basePath + modelName);
                    GeometryModel3D model = link.Children[0] as GeometryModel3D;
                    model.Material = materialGroup;
                    model.BackMaterial = materialGroup;
                    
                    joints.Add(new Joint(link));
                }
                RA.Children.Add(joints[0].model);
                RA.Children.Add(joints[1].model);
                RA.Children.Add(joints[2].model);
                RA.Children.Add(joints[3].model);
                RA.Children.Add(joints[4].model);
                RA.Children.Add(joints[5].model);
                RA.Children.Add(joints[6].model);

                //base-dof1
                joints[0].angleMin = -180;
                joints[0].angleMax = 180;
                joints[0].rotAxisX = 0;
                joints[0].rotAxisY = 0;
                joints[0].rotAxisZ = 1;
                joints[0].rotPointX = 0;
                joints[0].rotPointY = 0;
                joints[0].rotPointZ = 0;
                joints[0].angle = 0;
                //dof1-dof2
                joints[1].angleMin = -14;
                joints[1].angleMax = 110;
                joints[1].rotAxisX = 0;
                joints[1].rotAxisY = 1;
                joints[1].rotAxisZ = 0;
                joints[1].rotPointX = 0;
                joints[1].rotPointY = 0;
                joints[1].rotPointZ = 169.5;
                joints[1].angle = 0;
                //dof2-dof3
                joints[2].angleMin = -163;
                joints[2].angleMax = 11;
                joints[2].rotAxisX = 0;
                joints[2].rotAxisY = 1;
                joints[2].rotAxisZ = 0;
                joints[2].rotPointX = -423.52;
                joints[2].rotPointY = 0;
                joints[2].rotPointZ = 520.4;
                joints[2].angle = 11;
                //dof3-dof4
                joints[3].angleMin = -240;
                joints[3].angleMax = 240;
                joints[3].rotAxisX = 0.9867;
                joints[3].rotAxisY = 0;
                joints[3].rotAxisZ = -0.162538;
                joints[3].rotPointX = -423.52;
                joints[3].rotPointY = -14;
                joints[3].rotPointZ = 520.40;
                joints[3].angle = 0;

                //dof4-dof5
                joints[4].angleMin = 13;
                joints[4].angleMax = 110;
                joints[4].rotAxisX = 0;
                joints[4].rotAxisY = 1;
                joints[4].rotAxisZ = 0;
                joints[4].rotPointX = 79.205;
                joints[4].rotPointY = -14;
                joints[4].rotPointZ = 437.587;
                joints[4].angle = 60;

                //dof5-gripper
                joints[5].angleMin = -360;
                joints[5].angleMax = 360;
                joints[5].rotAxisX = 1;
                joints[5].rotAxisY = 0;
                joints[5].rotAxisZ = 0;
                joints[5].rotPointX = 162.696;
                joints[5].rotPointY = -14;
                joints[5].rotPointZ = 438.799;
                joints[5].angle = 0;


            }
            catch (Exception e)
            {
                Console.WriteLine("Exception Error:" + e.StackTrace);
            }
            return RA;
        }

        public Vector3D ForwardKinematics(double[] angles)
        {
            F1 = new Transform3DGroup();
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(joints[0].rotAxisX, joints[0].rotAxisY, joints[0].rotAxisZ), angles[0]), new Point3D(joints[0].rotPointX, joints[0].rotPointY, joints[0].rotPointZ));
            F1.Children.Add(R);

            F2 = new Transform3DGroup();
            T = new TranslateTransform3D(0, 0, 0);
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(joints[1].rotAxisX, joints[1].rotAxisY, joints[1].rotAxisZ), angles[1]), new Point3D(joints[1].rotPointX, joints[1].rotPointY, joints[1].rotPointZ));
            F2.Children.Add(T);
            F2.Children.Add(R);
            F2.Children.Add(F1);


            F3 = new Transform3DGroup();
            T = new TranslateTransform3D(0, 0, 0);
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(joints[2].rotAxisX, joints[2].rotAxisY, joints[2].rotAxisZ), angles[2]), new Point3D(joints[2].rotPointX, joints[2].rotPointY, joints[2].rotPointZ));
            F3.Children.Add(T);
            F3.Children.Add(R);
            F3.Children.Add(F2);
            
            //as before
            F4 = new Transform3DGroup();
            T = new TranslateTransform3D(0, 0, 0); //1500, 650, 1650
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(joints[3].rotAxisX, joints[3].rotAxisY, joints[3].rotAxisZ), angles[3]), new Point3D(joints[3].rotPointX, joints[3].rotPointY, joints[3].rotPointZ));
            F4.Children.Add(T);
            F4.Children.Add(R);
            F4.Children.Add(F3);

            F5 = new Transform3DGroup();
            T = new TranslateTransform3D(0, 0, 0);
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(joints[4].rotAxisX, joints[4].rotAxisY, joints[4].rotAxisZ), angles[4]), new Point3D(joints[4].rotPointX, joints[4].rotPointY, joints[4].rotPointZ));
            F5.Children.Add(T);
            F5.Children.Add(R);
            F5.Children.Add(F4);

            F6 = new Transform3DGroup();
            T = new TranslateTransform3D(0, 0, 0);
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(joints[5].rotAxisX, joints[5].rotAxisY, joints[5].rotAxisZ), angles[5]), new Point3D(joints[5].rotPointX, joints[5].rotPointY, joints[5].rotPointZ));
            F6.Children.Add(T);
            F6.Children.Add(R);
            F6.Children.Add(F5);

            joints[0].model.Transform = F1; //First joint
            joints[1].model.Transform = F2; //Second joint (the "biceps")
            joints[2].model.Transform = F3; //third joint (the "knee" or "elbow")
            joints[3].model.Transform = F4; //the "forearm"
            joints[4].model.Transform = F5; //the tool plate
            joints[5].model.Transform = F6; //the tool

            Tx.Content = joints[5].model.Bounds.Location.X;
            Ty.Content = joints[5].model.Bounds.Location.Y;
            Tz.Content = joints[5].model.Bounds.Location.Z;

            return new Vector3D(joints[5].model.Bounds.Location.X, joints[5].model.Bounds.Location.Y, joints[5].model.Bounds.Location.Z);
        }



        public void timer1_Tick(object sender, EventArgs e)
        {
            double[] angles = { joints[0].angle, joints[1].angle, joints[2].angle, joints[3].angle, joints[4].angle, joints[5].angle };
            angles = InverseKinematics(reachingPoint, angles);
            joint1.Value = joints[0].angle = angles[0];
            joint2.Value = joints[1].angle = angles[1];
            joint3.Value = joints[2].angle = angles[2];
            joint4.Value = joints[3].angle = angles[3];
            joint5.Value = joints[4].angle = angles[4];
            joint6.Value = joints[5].angle = angles[5];
            joint1TextBox.Text = joint1.Value.ToString("0.00");
            joint2TextBox.Text = joint2.Value.ToString("0.00");
            joint3TextBox.Text = joint3.Value.ToString("0.00");
            joint4TextBox.Text = joint4.Value.ToString("0.00");
            joint5TextBox.Text = joint5.Value.ToString("0.00");
            joint6TextBox.Text = joint6.Value.ToString("0.00");

            if ((--movements) <= 0)
            {
                KinematicPointBtn.Content = "Go to position";
                isAnimating = false;
                timer1.Stop();
            }
        }


        public static T Clamp<T>(T value, T min, T max)
            where T : System.IComparable<T>
        {
            T result = value;
            if (value.CompareTo(max) > 0)
                result = max;
            if (value.CompareTo(min) < 0)
                result = min;
            return result;
        }

        public void StartInverseKinematics(object sender, RoutedEventArgs e)
        {
            if (timer1.Enabled)
            {
                KinematicPointBtn.Content = "Go to position";
                isAnimating = false;
                timer1.Stop();
                movements = 0;
            }
            else
            {
                geometry.Transform = new TranslateTransform3D(reachingPoint);
                movements = 5000;
                KinematicPointBtn.Content = "STOP";
                isAnimating = true;
                timer1.Start();
            }
        }
       
        public double[] InverseKinematics(Vector3D target, double[] angles)
        {
            if (DistanceFromTarget(target, angles) < DistanceThreshold)
            {
                movements = 0;
                return angles;
            }

            double[] oldAngles = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
            angles.CopyTo(oldAngles, 0);
            for (int i = 5; i >= 0; i--)
            {
                // Gradient descent
                // Update : Solution -= LearningRate * Gradient
                double gradient = PartialGradient(target, angles, i);
                angles[i] -= LearningRate * gradient;

                // Clamp
                angles[i] = Clamp(angles[i], joints[i].angleMin, joints[i].angleMax);

                // Early termination
                if (DistanceFromTarget(target, angles) < DistanceThreshold || checkAngles(oldAngles, angles))
                {
                    movements = 0;
                    return angles;
                }
            }

            return angles;
        }
        public double DistanceFromTarget(Vector3D target, double[] angles)
        {
            Vector3D point = ForwardKinematics(angles);
            return Math.Sqrt(Math.Pow((point.X - target.X), 2.0) + Math.Pow((point.Y - target.Y), 2.0) + Math.Pow((point.Z - target.Z), 2.0));
        }
        public bool checkAngles(double[] oldAngles, double[] angles)
        {
            for (int i = 0; i <= 5; i++)
            {
                if (oldAngles[i] != angles[i])
                    return false;
            }

            return true;
        }
        private Color changeModelColor(Joint pJoint, Color newColor)
        {
            Model3DGroup models = ((Model3DGroup)pJoint.model);
            return changeModelColor(models.Children[0] as GeometryModel3D, newColor);
        }

        private Color changeModelColor(GeometryModel3D pModel, Color newColor)
        {
            if (pModel == null)
                return oldColor;

            Color previousColor = Colors.Black;

            MaterialGroup mg = (MaterialGroup)pModel.Material;
            if (mg.Children.Count > 0)
            {
                try
                {
                    previousColor = ((EmissiveMaterial)mg.Children[0]).Color;
                    ((EmissiveMaterial)mg.Children[0]).Color = newColor;
                    ((DiffuseMaterial)mg.Children[1]).Color = newColor;
                }
                catch (Exception exc)
                {
                    previousColor = oldColor;
                }
            }

            return previousColor;
        }
        private void execute_fk()
        {
            /** Debug sphere, it takes the x,y,z of the textBoxes and update its position
             * This is useful when using x,y,z in the "new Point3D(x,y,z)* when defining a new RotateTransform3D() to check where the joints is actually  rotating */
            double[] angles = { joints[0].angle, joints[1].angle, joints[2].angle, joints[3].angle, joints[4].angle, joints[5].angle };
            ForwardKinematics(angles);
            updateSpherePosition();
        }
        public double PartialGradient(Vector3D target, double[] angles, int i)
        {
            // Saves the angle,
            // it will be restored later
            double angle = angles[i];

            // Gradient : [F(x+SamplingDistance) - F(x)] / h
            double f_x = DistanceFromTarget(target, angles);

            angles[i] += SamplingDistance;
            double f_x_plus_d = DistanceFromTarget(target, angles);

            double gradient = (f_x_plus_d - f_x) / SamplingDistance;

            // Restores
            angles[i] = angle;

            return gradient;
        }

        
        private void joint_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (isAnimating)
                return;
            if (!isInitialized)
                return;
            joints[0].angle = joint1.Value;
            joints[1].angle = joint2.Value;
            joints[2].angle = joint3.Value;
            joints[3].angle = joint4.Value;
            joints[4].angle = joint5.Value;
            joints[5].angle = joint6.Value;
            joint1TextBox.Text = joint1.Value.ToString("0.00");
            joint2TextBox.Text = joint2.Value.ToString("0.00");
            joint3TextBox.Text = joint3.Value.ToString("0.00");
            joint4TextBox.Text = joint4.Value.ToString("0.00");
            joint5TextBox.Text = joint5.Value.ToString("0.00");
            joint6TextBox.Text = joint6.Value.ToString("0.00");
            execute_fk();
            
        }
        private void ReachingPoint_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                reachingPoint = new Vector3D(Double.Parse(TbX.Text), Double.Parse(TbY.Text), Double.Parse(TbZ.Text));
                geometry.Transform = new TranslateTransform3D(reachingPoint);
            }
            catch (Exception exc)
            {

            }
        }

        private void changeSelectedJoint()
        {
            if (joints == null)
                return;

            int sel = ((int)jointSelector.Value) - 1;
            switchingJoint = true;
           // unselectModel();
            if (sel < 0)
            {
                jointX.IsEnabled = false;
                jointY.IsEnabled = false;
                jointZ.IsEnabled = false;
                jointXAxis.IsEnabled = false;
                jointYAxis.IsEnabled = false;
                jointZAxis.IsEnabled = false;
            }
            else
            {
                if (!jointX.IsEnabled)
                {
                    jointX.IsEnabled = true;
                    jointY.IsEnabled = true;
                    jointZ.IsEnabled = true;
                    jointXAxis.IsEnabled = true;
                    jointYAxis.IsEnabled = true;
                    jointZAxis.IsEnabled = true;
                }
                jointX.Value = joints[sel].rotPointX;
                jointY.Value = joints[sel].rotPointY;
                jointZ.Value = joints[sel].rotPointZ;
                jointXAxis.IsChecked = joints[sel].rotAxisX == 1 ? true : false;
                jointYAxis.IsChecked = joints[sel].rotAxisY == 1 ? true : false;
                jointZAxis.IsChecked = joints[sel].rotAxisZ == 1 ? true : false;
               // selectModel(joints[sel].model);
                updateSpherePosition();
            }
            switchingJoint = false;
        }
        private void rotationPointChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (switchingJoint)
                return;

            int sel = ((int)jointSelector.Value) - 1;
            joints[sel].rotPointX = (int)jointX.Value;
            joints[sel].rotPointY = (int)jointY.Value;
            joints[sel].rotPointZ = (int)jointZ.Value;
            updateSpherePosition();
        }
        private void updateSpherePosition()
        {
            int sel = ((int)jointSelector.Value) - 1;
            if (sel < 0)
                return;

            Transform3DGroup F = new Transform3DGroup();
            F.Children.Add(new TranslateTransform3D(joints[sel].rotPointX, joints[sel].rotPointY, joints[sel].rotPointZ));
            F.Children.Add(joints[sel].model.Transform);
            geometry.Transform = F;
        }

        private void CheckBox_StateChanged(object sender, RoutedEventArgs e)
        {
            if (switchingJoint)
                return;

            int sel = ((int)jointSelector.Value) - 1;
            joints[sel].rotAxisX = jointXAxis.IsChecked.Value ? 1 : 0;
            joints[sel].rotAxisY = jointYAxis.IsChecked.Value ? 1 : 0;
            joints[sel].rotAxisZ = jointZAxis.IsChecked.Value ? 1 : 0;
        }

        private void jointSelector_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            changeSelectedJoint();
        }
        public HitTestResultBehavior ResultCallback(HitTestResult result)
        {
            // Did we hit 3D?
            RayHitTestResult rayResult = result as RayHitTestResult;
            if (rayResult != null)
            {
                // Did we hit a MeshGeometry3D?
                RayMeshGeometry3DHitTestResult rayMeshResult = rayResult as RayMeshGeometry3DHitTestResult;
                geometry.Transform = new TranslateTransform3D(new Vector3D(rayResult.PointHit.X, rayResult.PointHit.Y, rayResult.PointHit.Z));

                if (rayMeshResult != null)
                {
                    // Yes we did!
                }
            }

            return HitTestResultBehavior.Continue;
        }

        

        private void SetJointAngle(object sender, RoutedEventArgs e)
        {
            if (isAnimating)
                return;
            if (!isInitialized)
                return;
            try
            {
                joints[0].angle = Double.Parse(joint1TextBox.Text);
                joints[1].angle = Double.Parse(joint2TextBox.Text);
                joints[3].angle = Double.Parse(joint3TextBox.Text);
                joints[2].angle = Double.Parse(joint4TextBox.Text);
                joints[4].angle = Double.Parse(joint5TextBox.Text);
                joints[5].angle = Double.Parse(joint6TextBox.Text);
                isInitialized = false;
                joint1.Value = joints[0].angle;
                joint2.Value = joints[1].angle;
                joint4.Value = joints[3].angle;
                joint3.Value = joints[2].angle;
                joint5.Value = joints[4].angle;
                joint6.Value = joints[5].angle;
                isInitialized = true;
                execute_fk();
            }
            catch (Exception exc)
            {

            }
        }

       
    }
}
