using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using Color = System.Windows.Media.Color;
using Model3D = System.Windows.Media.Media3D.Model3D;

namespace HAL062app.moduly.wizualizacja
{
    /// <summary>
    /// aplikacja pisana na wersji HelixToolKit 24
    /// todo
    /// funkcja ładująca config
    /// 
    /// </summary>




    internal class Loader
    {
        private objectConfig _objectsToLoad { get; set; }
        private ModelVisual3D visual3D { get; set; }
        private List<AssembledObject> _assembledObjects { get; set; }
        private Model3DGroup _group { get; set; }

        public Loader(objectConfig objectsToLoad, ModelVisual3D scene)
        {
            _group = new Model3DGroup();
            visual3D = scene;
            _objectsToLoad = objectsToLoad;
            _assembledObjects = new List<AssembledObject>();

            LoadAssembly<Manipulator>("Manipulator_actualPosition");
            LoadAssembly<Manipulator>("Manipulator_simulation");

            scene.Content = _group;

        }



        public void LoadModel(string ID) //TODO: zrobić tworzenie pojedynczych statycznych obiektów
        {



        }


        public void LoadAssembly<T>(string name) where T : AssembledObject
        {
            var assembly = (T)Activator.CreateInstance(typeof(T), name, _objectsToLoad.Objects, _group); //TODO: zrobić obsługę nazw grup z objectsToLoad, bo aktualnie ładuje wszystko
            _assembledObjects.Add(assembly);

        }

        public void transformAssembly(string name, float[] x)
        {
            var assembly = _assembledObjects.FirstOrDefault(obj => obj.Name == name);
            if (assembly != null)
                assembly.TransformAssembly(x);

        }



    }
    internal class AssembledObject //Klasa bazowa dla wszystkich obiektów złożonych
    {

        public string Name { get; private set; }
        protected List<objectConfig.SpatialObject> _assembly;
        protected List<Model3D> _models;
        protected List<Color> _actualColorsOfModel;

        public MaterialGroup materialGroup = new MaterialGroup();
        protected EmissiveMaterial _emissiveMaterial;
        public Model3DGroup _visualGroup { get; private set; }

        public AssembledObject(string name, List<objectConfig.SpatialObject> assembly, Model3DGroup visualGroup, bool importModelFromFile)
        {
            _visualGroup = visualGroup;
            Name = name;
            _assembly = assembly;//assembly.Select(obj => obj.Clone()).ToList();
            _actualColorsOfModel = new List<Color>();
            RestoreOriginalColors();
            if (importModelFromFile)
                Download3DModels();


        }
        public void RestoreOriginalColors()
        {
            foreach (var obj in _assembly)
            {
                if (obj is objectConfig.iModel3D model)
                    _actualColorsOfModel.Add(model.color);
            }
        }
        public void Download3DModels() //pobiera modele3D z plików i zapisuje do pamięci
        {
            foreach (var obj in _assembly)
            {
                if (obj is objectConfig.iModel3D model) //Sprawdź, czy obiekt jest zbudowany na bazie iModel3D
                {
                    model.DownloadModel();
                    if (model.geometryModel3D != null)
                        _visualGroup.Children.Add((Model3D)model.geometryModel3D);
                    else
                        Console.WriteLine($"Model {obj.ID} nie został poprawnie załadowany.");
                }
            }
        }

        public void ChangeColorTemporary(string ID, Color color)
        {
            var model = _assembly.FirstOrDefault(obj => obj.ID == ID);
            if (model is objectConfig.iModel3D newColorModel)   //Sprawdź, czy obiekt jest zbudowany na bazie iModel3D
                newColorModel.ChangeModelColor(color);

        }

        public virtual void TransformAssembly(float[] x) { }

    }
    internal class Manipulator : AssembledObject
    {



        public Manipulator(string name, List<objectConfig.SpatialObject> assembly, Model3DGroup visualGroup) : base(name, assembly, visualGroup, true)
        {
        }


        public override void TransformAssembly(float[] x) //Jest to transformacja modelu za pomocą narzędzi wbudowanych w helixToolKit, docelowo ma być własne obliczanie transformacji za pomocą DH
        {
            int numberOfElements = _assembly.Count();
            Transform3DGroup[] F = new Transform3DGroup[numberOfElements];
            RotateTransform3D R = null;
            //TranslateTransform3D T = new TranslateTransform3D(0, 0, 0);
            float[] angles = new float[numberOfElements];
            for (int i = 0; i < numberOfElements; i++) //przemnóż wszystkie kolejne modele przez obrót i daj wynik w postaci współrzędnych końcówki
            {
                F[i] = new Transform3DGroup();
                if (_assembly[i] is objectConfig.ManipulatorPart element) //sprawdź czy obiekt jest manipulatorem
                {
                    R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(element.RotationAxisX, element.RotationAxisY, element.RotationAxisZ), x[i]), new Point3D(element.RotationPointX, element.RotationPointY, element.RotationPointZ)); //TODO: sprawdzić czy x sie nie zmienia po użyciu funkcji globalnie
                    F[i].Children.Add(R);
                    if (i > 0)
                        F[i].Children.Add(F[i - 1]);
                    element.geometryModel3D.Transform = F[i]; //TODO: sprawdzić czy sie zmienia globalnie
                }
            }

        }

        public Vector3D PartCoordinate(int partNumber)
        {
            if (_assembly[partNumber - 1] is objectConfig.ManipulatorPart part)
                return new Vector3D(part.geometryModel3D.Bounds.Location.X, part.geometryModel3D.Bounds.Location.Y, part.geometryModel3D.Bounds.Location.Z);
            return new Vector3D(0, 0, 0);

        }
    }
    internal class Wiertlo : AssembledObject
    {

        public Wiertlo(string name, List<objectConfig.SpatialObject> assembly, Model3DGroup visualGroup) : base(name, assembly, visualGroup, true)
        {

        }


        public override void TransformAssembly(float[] x)
        {


        }

    } //TODO, zrobić obsługę modeli wiertła i poruszania się




    internal class CameraManager //Klasa odpowiedzialna za trzymanie i aktualizowanie pozycji kamer na podstawie poruszeń assembly
    {
        private List<CameraPosition> _cameras;

        public CameraManager()
        {
            _cameras = new List<CameraPosition>();
            _cameras.Add(new CameraPosition("Main", new Point3D(0, 0, 0), new Vector3D(0, 0, -1), new Vector3D(0, 1, 0)));
            _cameras.Add(new CameraPosition("Gripper", new Point3D(0, 0, 0), new Vector3D(0, 0, -1), new Vector3D(0, 1, 0)));
            _cameras.Add(new CameraPosition("Podstawa", new Point3D(0, 0, 0), new Vector3D(0, 0, -1), new Vector3D(0, 1, 0)));
            _cameras.Add(new CameraPosition("Ramie", new Point3D(0, 0, 0), new Vector3D(0, 0, -1), new Vector3D(0, 1, 0)));

        }

        public void AddCamera(string name, Point3D position, Vector3D lookDirection, Vector3D upDirection)
        {
            var camera = new CameraPosition(name, position, lookDirection, upDirection);
            _cameras.Add(camera);
        }

        public void MoveCamera(string name, Point3D newPosition)
        {
            var camera = _cameras.FirstOrDefault(c => c.Name == name);
            if (camera != null)
            {
                camera.Position = newPosition;
            }
        }

        public void RotateCamera(string name, Vector3D newLookDirection, Vector3D newUpDirection)
        {
            var camera = _cameras.FirstOrDefault(c => c.Name == name);
            if (camera != null)
            {
                camera.LookDirection = newLookDirection;
                camera.UpDirection = newUpDirection;
            }
        }

        internal class CameraPosition
        {
            public string Name { get; }
            public Point3D Position { get; set; }
            public Vector3D LookDirection { get; set; }
            public Vector3D UpDirection { get; set; }

            public CameraPosition(string name, Point3D position, Vector3D lookDirection, Vector3D upDirection)
            {
                Name = name;
                Position = position;
                LookDirection = lookDirection;
                UpDirection = upDirection;
            }
        }
        internal List<string> GetCameraPositions()
        {
            return _cameras.Select(c => c.Name).ToList();
        }
    }






    public partial class wizualizacjaWPF : UserControl
    {

        Loader loader;
        float val = 0;
        objectConfig config;
        ModelVisual3D visual3D;
        CameraManager CameraManager;


        public List<string> CameraPositions { get; set; }
        public wizualizacjaWPF()
        {
            InitializeComponent();
            config = new objectConfig();
            visual3D = new ModelVisual3D();
            loader = new Loader(config, visual3D);
            viewport.Children.Add(visual3D);

            CameraManager = new CameraManager();
            CameraPositions = CameraManager.GetCameraPositions();
            DataContext = this;

        }



        private void Test_click(object sender, RoutedEventArgs e)
        {
            val += 10f;
            loader.transformAssembly("Manipulator_simulation", new float[] { val, 0, 0, 0, 0, 0 });
        }


        private void NewViewport_Click(object sender, RoutedEventArgs e)
        {


        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (sender is Slider slider)
            {
                loader.transformAssembly("Manipulator_simulation", new float[] { (float)slider.Value, 0, 0, 0, 0, 0 });
            }
        }




    }
}
