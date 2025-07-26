using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Color = System.Windows.Media.Color;

namespace HAL062app.moduly.wizualizacja
{
    public class objectConfig
    {

        private string basePath = AppDomain.CurrentDomain.BaseDirectory + @"\\moduly\\wizualizacja\\models\\";


        // Obsługa przetrzymywania wszystkich obiektów 
        public List<SpatialObject> Objects { get; private set; } = new List<SpatialObject>() //DOROBIC PRZECHOWYWANIE DANYCH W PLIKU
        {
            new ManipulatorPart("DOF_1",    0f, -180f,  180f,   0,        0,  1,          0,          0,              0,                                    "dof1.STL"),
            new ManipulatorPart("DOF_2",   50f,  -14f,  110f,   0,        1,  0,          0,          0,         169.5F,                                    "dof2.STL"),
            new ManipulatorPart("DOF_3",   11f, -163f,   11f,   0,        1,  0,         -423.52F,          0,         520.4F,                              "dof3.STL"),
            new ManipulatorPart("DOF_4",    0f, -240f,  240f,   0.9867F,  0,  -0.162538F,          -423.52F,          -14F,             520.40F,            "dof4.STL"),
            new ManipulatorPart("DOF_5",   60f,   13f,  110f,   0,        1,  0,          79.205F,          -14F,              437.587F,                    "dof5.STL"),
            new ManipulatorPart("Gripper",  0f, -180f,  180f,   1,        0,  0,          162.696F,          -14,              438.799F,                    "gripper.STL"),



        };

        public List<string> GetObjectsNames()
        {
            return Objects.Select(obj => obj.ID).ToList();
        }

        public bool ObjectExists(string ID)
        {
            return GetObjectsNames().Contains(ID);
        }


        public void AddPart(SpatialObject spatialObject)
        {
            Objects.Add(spatialObject);
        }

        public string GetFilePath(string ID)
        {
            var Model = Objects.FirstOrDefault(obj => obj.ID == ID);
            if (Model is iModel3D model)
                return basePath + model.ModelPath;
            throw new KeyNotFoundException($"Obiekt z '{ID}' nie posiada pliku.");
        }


        // Klasa bazowa reprezentująca obiekt przestrzenny
        public class SpatialObject
        {
            public string ID { get; private set; }
            public float X { get; private set; }
            public float Y { get; private set; }
            public float Z { get; private set; }

            public SpatialObject(string id, float x = 0f, float y = 0f, float z = 0f)
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    throw new ArgumentException("ID obiektu nie może być puste lub null", nameof(id));
                }

                ID = id;
                X = x;
                Y = y;
                Z = z;
            }

            public float[] Coordinates => new float[] { X, Y, Z };

            public virtual SpatialObject Clone()
            {
                return new SpatialObject(ID, X, Y, Z);
            }



        }

        public interface iModel3D
        {
            string ModelPath { get; }
            Color color { get; set; }

            GeometryModel3D geometryModel3D { get; }

            void DownloadModel();
            void ChangeModelColor(Color NewTemporaryColor);
        }

        public class ManipulatorPart : SpatialObject, iModel3D
        {
            public float Angle { get; private set; }
            public float MinAngle { get; private set; }
            public float MaxAngle { get; private set; }
            public float StartAngle { get; private set; }

            // Dane do macierzy DH
            public float RotationAxisX { get; private set; }
            public float RotationAxisY { get; private set; }
            public float RotationAxisZ { get; private set; }
            public float RotationPointX { get; private set; }
            public float RotationPointY { get; private set; }
            public float RotationPointZ { get; private set; }

            // Dane do kolizji
            public float Length { get; set; }
            public float Width { get; set; }
            public float Height { get; set; }

            public string ModelPath { get; private set; }

            public Color color { get; set; }

            public GeometryModel3D geometryModel3D { get; private set; }


            public ManipulatorPart(
                string id,
                float startAngle,
                float minAngle,
                float maxAngle,
                float rotationAxisX,
                float rotationAxisY,
                float rotationAxisZ,
                float rotationPointX,
                float rotationPointY,
                float rotationPointZ,
                string modelPath
            ) : base(id, 0, 0, 0) // Domyślne współrzędne dla manipulatora
            {
                Angle = startAngle;
                StartAngle = startAngle;
                MinAngle = minAngle;
                MaxAngle = maxAngle;

                RotationAxisX = rotationAxisX;
                RotationAxisY = rotationAxisY;
                RotationAxisZ = rotationAxisZ;
                RotationPointX = rotationPointX;
                RotationPointY = rotationPointY;
                RotationPointZ = rotationPointZ;
                ModelPath = modelPath;

                color = Colors.Blue;
            }

            public void SetAngle(float newAngle)
            {
                if (newAngle < MinAngle || newAngle > MaxAngle)
                {
                    throw new ArgumentOutOfRangeException(nameof(newAngle), $"Kąt '{ID}' wychodzi poza zakres.");
                }
                Angle = newAngle;
            }

            public override SpatialObject Clone()
            {
                return new ManipulatorPart(
                    ID,
                    StartAngle,
                    MinAngle,
                    MaxAngle,
                    RotationAxisX,
                    RotationAxisY,
                    RotationAxisZ,
                    RotationPointX,
                    RotationPointY,
                    RotationPointZ,
                    ModelPath
                )
                {
                    Length = Length,
                    Width = Width,
                    Height = Height,
                    color = color
                };
            }

            public void DownloadModel()
            {
                try
                {
                    ModelImporter import = new ModelImporter();
                    var link = import.Load(AppDomain.CurrentDomain.BaseDirectory + @"\\moduly\\wizualizacja\\models\\" + ModelPath);
                    this.geometryModel3D = link.Children[0] as GeometryModel3D;
                    ChangeModelColor(color);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Problem podczas ładowania modelu {ID}:" + e.StackTrace);

                }

            }
            public void ChangeModelColor(Color NewTemporaryColor)
            {
                var materialGroup = new MaterialGroup();
                EmissiveMaterial emissMat = new EmissiveMaterial(new SolidColorBrush(NewTemporaryColor));
                DiffuseMaterial diffMat = new DiffuseMaterial(new SolidColorBrush(NewTemporaryColor));
                SpecularMaterial specMat = new SpecularMaterial(new SolidColorBrush(NewTemporaryColor), 200);
                materialGroup.Children.Add(emissMat);
                materialGroup.Children.Add(diffMat);
                materialGroup.Children.Add(specMat);
                this.geometryModel3D.Material = materialGroup;
                this.geometryModel3D.BackMaterial = materialGroup;

            }
        }
    }
}
