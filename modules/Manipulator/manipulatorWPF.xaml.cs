using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
namespace HAL062app.moduly.manipulator
{

    class Joint
    {
        public Model3D model = null;
        public float angle = 0;
        public float angleMin = -180;
        public float angleMax = 180;
        public float rotPointX = 0;
        public float rotPointY = 0;
        public float rotPointZ = 0;
        public float rotAxisX = 0;
        public float rotAxisY = 0;
        public float rotAxisZ = 0;

        public Joint(Model3D pModel)
        {
            model = pModel;
        }
        
    }






    public partial class manipulatorWPF : UserControl
    {
        Model3DGroup _RobotModelGroup = new Model3DGroup();
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
        private Transform3DGroup[] _jointTransforms = new Transform3DGroup[6];
        RotateTransform3D R;
        TranslateTransform3D T;

        SphereVisual3D[] sphere;
        BoxVisual3D box;

        Color oldColor = Colors.White;
        bool switchingJoint = false;
        bool isAnimating = false;
        bool isInitialized = false;
        float LearningRate = 0.01F;
        float SamplingDistance = 0.15F;
        float DistanceThreshold = 20;
        int movements = 10;
        Vector3D reachingPoint;
        System.Windows.Forms.Timer timer1;


        private const string MODEL_PATH1 = "dof1.STL";
        private const string MODEL_PATH2 = "dof2.STL";
        private const string MODEL_PATH3 = "dof3.STL";
        private const string MODEL_PATH4 = "dof4.STL";
        private const string MODEL_PATH5 = "dof5.STL";
        private const string MODEL_PATH6 = "gripper.STL";
        private const string MODEL_PATH7 = "rover.STL";

        public event Action<int> test;

        public manipulatorWPF()
        {

            InitializeComponent();
            basePath = AppDomain.CurrentDomain.BaseDirectory + @"\\modules\\manipulator\\models\\";
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
            builder.AddBox(new Point3D(0, 0, 0), 30, 30, 60);
            geometry = new GeometryModel3D(builder.ToMesh(), Materials.Brown);
            visual = new ModelVisual3D();
            visual.Content = geometry;

            sphere = new SphereVisual3D[2];
            var camera = new PerspectiveCamera
            {
                Position = new Point3D(1500, -2060, 1300),
                LookDirection = new Vector3D(-1285, 2140, -1020),
                UpDirection = new Vector3D(-0.20, 0.32, 0.93),

            };
            viewport.Camera = camera;
            viewport.Children.Add(visual);
            viewport.Children.Add(RoboticArm);


            //float[] angles = { joints[0].angle, joints[1].angle, joints[2].angle, joints[3].angle, joints[4].angle, joints[5].angle};
            //ForwardKinematics(angles);

            //changeSelectedJoint();

            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 5;
          //  timer1.Tick += new System.EventHandler(timer1_Tick);

            sphere[0] = new SphereVisual3D
            {
                Center = new Point3D(0, 0, 0),
                Radius = 20,
                Fill = Brushes.Red
            };

            sphere[1] = new SphereVisual3D
            {
                Center = new Point3D(0, 0, 0),
                Radius = 20,
                Fill = Brushes.Green
            };

            box = new BoxVisual3D
            {
                Center = new Point3D(0, 0, 0),
                Length = 30,
                Width = 30,
                Height = 60,
                Fill = Brushes.Red
            };
            viewport.Children.Add(sphere[0]);
            viewport.Children.Add(sphere[1]);
            viewport.Children.Add(box);
        }
        private float ToDegrees(float radian)
        {

            return radian * 180.0F / (float)Math.PI;
        }
        private float ToRadians(float degrees)
        {
            return degrees * ((float)Math.PI / 180.0F);
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


                changeModelColor(joints[0], Colors.LightBlue);
                _RobotModelGroup.Children.Add(joints[0].model);
                changeModelColor(joints[1], Colors.Red);
                _RobotModelGroup.Children.Add(joints[1].model);
                changeModelColor(joints[2], Colors.LightGreen);
                _RobotModelGroup.Children.Add(joints[2].model);
                changeModelColor(joints[3], Colors.LightPink);
                _RobotModelGroup.Children.Add(joints[3].model);
                changeModelColor(joints[4], Colors.LightYellow);
                _RobotModelGroup.Children.Add(joints[4].model);
                changeModelColor(joints[5], Colors.LightSlateGray);
                _RobotModelGroup.Children.Add(joints[5].model);
                _RobotModelGroup.Children.Add(joints[6].model);



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
                joints[1].rotPointZ = 169.5F;
                joints[1].angle = 50;
                //dof2-dof3
                joints[2].angleMin = -163;
                joints[2].angleMax = 11;
                joints[2].rotAxisX = 0;
                joints[2].rotAxisY = 1;
                joints[2].rotAxisZ = 0;
                joints[2].rotPointX = -423.52F;
                joints[2].rotPointY = 0;
                joints[2].rotPointZ = 520.4F;
                joints[2].angle = 11;
                //dof3-dof4
                joints[3].angleMin = -240;
                joints[3].angleMax = 240;
                joints[3].rotAxisX = 0.9867F;
                joints[3].rotAxisY = 0;
                joints[3].rotAxisZ = -0.162538F;
                joints[3].rotPointX = -423.52F;
                joints[3].rotPointY = -14;
                joints[3].rotPointZ = 520.40F;
                joints[3].angle = 0;

                //dof4-dof5
                joints[4].angleMin = 13;
                joints[4].angleMax = 110;
                joints[4].rotAxisX = 0;
                joints[4].rotAxisY = 1;
                joints[4].rotAxisZ = 0;
                joints[4].rotPointX = 79.205F;
                joints[4].rotPointY = -14;
                joints[4].rotPointZ = 437.587F;
                joints[4].angle = 60;

                //dof5-gripper
                joints[5].angleMin = -360;
                joints[5].angleMax = 360;
                joints[5].rotAxisX = 1;
                joints[5].rotAxisY = 0;
                joints[5].rotAxisZ = 0;
                joints[5].rotPointX = 162.696F;
                joints[5].rotPointY = -14;
                joints[5].rotPointZ = 438.799F;
                joints[5].angle = 0;


            }
            catch (Exception e)
            {
                Console.WriteLine("Exception Error:" + e.StackTrace);
            }
            return _RobotModelGroup;
        }

        public Vector3D ForwardKinematics(float[] anglesFloat)
        {
            float[] angles = new float[6];
            for (int i = 0; i < anglesFloat.Length; i++)
                angles[i] = (float)anglesFloat[i];


            for (int i = 0; i < _jointTransforms.Length; i++)
            {
                var group = new Transform3DGroup();
                var axis = new AxisAngleRotation3D(new Vector3D(joints[i].rotAxisX, joints[i].rotAxisY, joints[i].rotAxisZ), angles[i]);
                var rotationPoint = new Point3D(joints[i].rotPointX, joints[i].rotPointY, joints[i].rotPointZ);
                var rotation = new RotateTransform3D(axis, rotationPoint);
                group.Children.Add(rotation);

                if(i > 0)
                {
                    group.Children.Add(new TranslateTransform3D(0, 0, 0));
                    group.Children.Add(_jointTransforms[i - 1]);
                }
                _jointTransforms[i] = group;
                joints[i].model.Transform = group;
            }


            return new Vector3D(joints[5].model.Bounds.Location.X, joints[5].model.Bounds.Location.Y, joints[5].model.Bounds.Location.Z);
        }



        public void timer1_Tick(object sender, EventArgs e)
        {
            float[] angles = { joints[0].angle, joints[1].angle, joints[2].angle, joints[3].angle, joints[4].angle, joints[5].angle };
            angles = InverseKinematics(reachingPoint, angles);
            joints[0].angle = angles[0];
            joints[1].angle = angles[1];
            joints[2].angle = angles[2];
            joints[3].angle = angles[3];
            joints[4].angle = angles[4];
            joints[5].angle = angles[5];


            if ((--movements) <= 0)
            {

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

    

        public bool checkAngles(float[] oldAngles, float[] angles)
        {
            for (int i = 0; i <= 5; i++)
            {
                    if( Math.Abs(oldAngles[i] - angles[i]) > 0.01)
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

            float[] angles = { (float)joints[0].angle, (float)joints[1].angle, (float)joints[2].angle, (float)joints[3].angle, (float)joints[4].angle, (float)joints[5].angle };
            ForwardKinematics(angles);

        }
        public float PartialGradient(Vector3D target, float[] angles, int i)
        {

            float angle = angles[i];


            float f_x = DistanceFromTarget(target, angles);

            angles[i] += SamplingDistance;
            float f_x_plus_d = DistanceFromTarget(target, angles);

            float gradient = (f_x_plus_d - f_x) / SamplingDistance;

            angles[i] = angle;

            return gradient;
        }

        public void UpdateSphere(float[] xyz, int ID)
        {
            sphere[ID].Center = new Point3D(xyz[0], xyz[1], xyz[2]);

        }
        public void UpdateBox(float[] xyzRPY)
        {

            double x = xyzRPY[0];
            double y = xyzRPY[1];
            double z = xyzRPY[2];

            // Konwersja radianów na stopnie
            double roll = xyzRPY[3] * (180 / Math.PI);
            double pitch = xyzRPY[4] * (180 / Math.PI);
            double yaw = xyzRPY[5] * (180 / Math.PI);


            Transform3DGroup transformGroup = new Transform3DGroup();
            

            // TranslateTransform3D moveToCenter = new TranslateTransform3D(-x, -y, -z);
            box.Center = new Point3D(0, 0, 0);

            RotateTransform3D rotateX = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), roll));
            RotateTransform3D rotateY = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), pitch));
            RotateTransform3D rotateZ = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), yaw));


            TranslateTransform3D moveBack = new TranslateTransform3D(x, y, z);



            transformGroup.Children.Add(rotateX);
            transformGroup.Children.Add(rotateY);
            transformGroup.Children.Add(rotateZ);
            transformGroup.Children.Add(moveBack);


            box.Transform = transformGroup;

        }

        private void SetJointAngle(object sender, RoutedEventArgs e)
        {
            if (isAnimating)
                return;
            if (!isInitialized)
                return;
            try
            {

                isInitialized = false;

                isInitialized = true;
                execute_fk();
            }
            catch (Exception exc)
            {

            }
        }

        public float DistanceFromTarget(Vector3D target, float[] angles)
        {

            Vector3D point = ForwardKinematics(angles);
            return (float)Math.Sqrt(Math.Pow((point.X - target.X), 2.0) + Math.Pow((point.Y - target.Y), 2.0) + Math.Pow((point.Z - target.Z), 2.0));
        }

        public HitTestResultBehavior ResultCallback(HitTestResult result)
        {

            RayHitTestResult rayResult = result as RayHitTestResult;
            if (rayResult != null)
            {

                RayMeshGeometry3DHitTestResult rayMeshResult = rayResult as RayMeshGeometry3DHitTestResult;
                geometry.Transform = new TranslateTransform3D(new Vector3D(rayResult.PointHit.X, rayResult.PointHit.Y, rayResult.PointHit.Z));

                if (rayMeshResult != null)
                {

                }
            }

            return HitTestResultBehavior.Continue;
        }
        private float[] InverseKinematics(Vector3D target, float[] angles)
        {
            if (DistanceFromTarget(target, angles) < DistanceThreshold)
            {
                movements = 0;
                return angles;
            }

            float[] oldAngles = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
            angles.CopyTo(oldAngles, 0);
            for (int i = 0; i <= 5; i++)
            {

                float gradient = PartialGradient(target, angles, i);
                angles[i] -= LearningRate * gradient;


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


    }
}
