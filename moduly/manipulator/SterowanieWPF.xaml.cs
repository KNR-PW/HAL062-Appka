using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
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
using System.Windows.Shapes;
using System.Xml;
using Newtonsoft.Json;
using System.Timers;
using SharpDX.XInput;
using static HAL062app.ControllerState;
using Newtonsoft.Json.Linq;
using HAL062app.moduly.manipulator;
//using System.Windows.Forms;

namespace HAL062app.moduly.manipulator
{

    public partial class SterowanieWPF : UserControl
    {
        private class Joint
        {
            public float angle = 0;
            public float angleMin = -180;
            public float angleMax = 180;
            public float rotPointX = 0;
            public float rotPointY = 0;
            public float rotPointZ = 0;
            public float rotAxisX = 0;
            public float rotAxisY = 0;
            public float rotAxisZ = 0;
            public float defaultAngle = 0;
            public float lastAngle = 0;
            public float relative0 = 0; //to jest wartosc, ktora odpowiada za zmiane katow zerowych w modelu 3D na katy zerowe w mainboardzie manipulatora
            public Joint(float min, float max, float angle, float relative0)
            {
                this.angleMin = min;
                this.angleMax = max;
                this.angle = angle;
                this.defaultAngle = angle;
                this.lastAngle = 0;
                this.relative0 = relative0;
            }
        }


        public Action<Position> SendPosition_action;
        public Action<Position> CreateVisualization_action;
        public Action<float[]> ChangeSpherePosition_action;
        public Action<Message> SendMessage_action;

        Slider[] _JointSliders = new Slider[6];
        Label[] _JointLabels = new Label[6];

        Joint[] joints = new Joint[6];
        Position actualPosition = null;
        float[] angles = new float[6];
        bool initialization = true;
        int activeJointChange = 1;
        object activeSenderSlider = null;
        private Dictionary<string, double> addValues = new Dictionary<string, double>();
        private List<Position> positionsHistory = new List<Position>();
        private Sequence history = null;
        //sekwencje
        private int _id = 0;
        private float[] relativeZeros = new float[6]; //Dla modelu wgrywanego z plików kąty są inne, niż te dla manipulatora w łaziku. Tutaj przechowujemy różnicę tych kątów, aby przy wysyłaniu do łazika ten kąt odjąć
        Sequence loadedSequence;
        int selectedPositionInSequence = 0; //Która pozycja z sekwencji aktualnie jest wyświetlana
        bool turnOffSequencePositionChanging = false; //Klikanie w pozycję na historii w sekwencji zmienia/ nie zmienia wizualizacji
        Position settingPosition = null; //to jest zmienna przechowująca aktualne położenie, wykorzystywane w zapisywaniu w sekcji sekwencji
        //manager sekwencji - zapisywanie do pliku
        private SequenceManager sequenceManager = new SequenceManager();
        private string sequencePath = "sequenceList.json";
        bool simulatingSequence = false;


        // Xbox
        private bool usingXboxPad = false;
        private XboxPad _XboxPad;
        private bool isClosing = false;
        private bool isOpening = false;


        private Timer timer;
        private int readInterval = 50;
        private int timerInterval = 50;
        private int elapsedTime = 0;
        private bool isTimerRunning = false;


        /// Kinematyka odwrotna 
        /// 
        /// Kinematyka odwrotna


        InverseKinematics inverseKinematics;



        public SterowanieWPF()
        {
            InitializeComponent(); //to tutaj zmienia się maksymalne, minimalne i startowe kąty dla symulacji
            InitializeTimer();
            joints[0] = new Joint(-90, 90, 0, 0);
            joints[1] = new Joint(-60, 90, 0, 50);
            joints[2] = new Joint(-60, 70, 0, -60);
            joints[3] = new Joint(-180, 180, 0, 100);
            joints[4] = new Joint(-90, 60, 0, 10);
            joints[5] = new Joint(-360, 360, 0, 0);
            addValues.Add("-5.0", -5.0); //to odpowiada tylko za przyciski do szczegółowej zmiany kąta, nic ważnego
            addValues.Add("5.0", 5.0);
            addValues.Add("-1.0", -1.0);
            addValues.Add("1.0", 1.0);
            addValues.Add("-0.5", -0.5);
            addValues.Add("0.5", 0.5);
            addValues.Add("-0.1", -0.1);
            addValues.Add("0.1", 0.1);

            XboxControlBus.XboxControlMode += OnXboxControlModeChanged;
            Slider[] JointSliders = { Joint1Slider, Joint2Slider, Joint3Slider, Joint4Slider, Joint5Slider, Joint6Slider };
            Label[] JointLabels = { Joint1Label, Joint2Label, Joint3Label, Joint4Label, Joint5Label, Joint6Label };
            _JointSliders = JointSliders;
            _JointLabels = JointLabels;
            for (int i = 0; i < 6; i++)
            {
                UpdateSlidersValues(joints[i], _JointSliders[i], _JointLabels[i]);
                relativeZeros[i] = joints[i].relative0;
            }


            actualPosition = new Position(returnAnglesFromJoints(joints, true));
            actualPosition.Duration = 5;
            history = new Sequence("History", new List<Position>());
            history.sequence.Add(actualPosition);
            initialization = false;

            _XboxPad = XboxPad.Instance;
            _XboxPad.ControllerStateChanged += OnXboxPadStateChanged;

            inverseKinematics = new InverseKinematics();
        }
        private void InitializeTimer()
        {
            timer = new Timer(150);
            timer.Interval = timerInterval;
            timer.AutoReset = true;
            timer.Elapsed += OnTimedEvent;

        }
        /*
        private async void Timer_Tick(object sender, EventArgs e)
        {
            elapsedTime += timer.Interval;
            if (elapsedTime >= readInterval)
            {
                SendManipulatorFrames_Xbox();
                elapsedTime = 0; 
            }
        }*/
        private async void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            await Task.Run(() => SendManipulatorFrames_Xbox());


        }
        private void UpdateSlidersValues(Joint joint, Slider slider, Label label)
        {
            slider.Minimum = joint.angleMin;
            slider.Maximum = joint.angleMax;
            slider.Value = joint.angle;
            label.Content = joint.angle;
        }

        private void JointGridClicked(object sender, MouseButtonEventArgs e)
        {
            if (sender is Grid grid)
            {
                ResetSelectedGrid();
                grid.Background = Brushes.LightGray;
                activeJointChange = Convert.ToInt32(grid.Tag);
                Slider associatedSlider = FindSliderInGrid(grid);
                if (associatedSlider != null)
                {
                    associatedSlider.Background = Brushes.LightGray;
                    activeSenderSlider = associatedSlider;
                }
                JointControlPanel(activeJointChange);
            }

        }

        private void JointSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!initialization)
                if (sender is Slider slider)
                {
                    Grid parentGrid = slider.Tag as Grid;
                    JointGridClicked(parentGrid, null);
                    joints[Convert.ToInt32(parentGrid.Tag.ToString()) - 1].angle = (float)slider.Value;
                    slider.Background = Brushes.LightGray;
                    angleTextbox.Text = slider.Value.ToString("0.00");
                    Label associatedLabel = FindLabelInGrid(parentGrid);
                    if (associatedLabel != null)
                    {
                        associatedLabel.Content = slider.Value.ToString("0.00");
                    }
                    actualPosition = new Position(returnAnglesFromJoints(joints, true));
                    SendVisualizationPosition();

                }

        }
        private void ChangeSlidersValue(Position position)
        {
            for (int i = 0; i < 6; i++)
            {
                _JointSliders[i].Value = position.joints[i] - relativeZeros[i];
                _JointLabels[i].Content = (position.joints[i] - relativeZeros[i]).ToString("0.00");
            }



        }
        private Label FindLabelInGrid(Grid grid) // To tylko do zmiany wartości w sliderze
        {
            foreach (var child in grid.Children)
            {
                if (child is Label label)
                {
                    return label;
                }
            }
            return null;
        }
        private Slider FindSliderInGrid(Grid grid)// To tylko do zmiany kolorów po kliknięciu
        {
            foreach (var child in grid.Children)
            {
                if (child is Slider slider)
                {
                    return slider;
                }
            }
            return null;
        }

        private void ResetSelectedGrid()
        {
            Joint1Grid.Background = Brushes.White;
            Joint2Grid.Background = Brushes.White;
            Joint1Slider.Background = Brushes.White;
            Joint2Slider.Background = Brushes.White;
            Joint3Grid.Background = Brushes.White;
            Joint3Slider.Background = Brushes.White;
            Joint4Grid.Background = Brushes.White;
            Joint4Slider.Background = Brushes.White;
            Joint5Grid.Background = Brushes.White;
            Joint5Slider.Background = Brushes.White;
            Joint6Grid.Background = Brushes.White;
            Joint6Slider.Background = Brushes.White;

        }

        private float CalculateToBounds(int jointID, float newValue)
        {
            if (newValue < joints[jointID].angleMin)
                return joints[jointID].angleMin;
            else if (newValue > joints[jointID].angleMax)
                return joints[jointID].angleMax;
            else
                return newValue;
        }

        private void DOFvalueBtn_clicked(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                joints[activeJointChange - 1].angle = CalculateToBounds(activeJointChange - 1, joints[activeJointChange - 1].angle + (float)addValues[btn.Tag.ToString()]);
                angleTextbox.Text = joints[activeJointChange - 1].angle.ToString("0.00");
                if (activeSenderSlider is Slider slider)
                    slider.Value = joints[activeJointChange - 1].angle;


            }

        }

        private void UndoButton_Click(object sender, EventArgs e)
        {
            angleTextbox.Text = joints[activeJointChange - 1].lastAngle.ToString("0.00");
            if (activeSenderSlider is Slider slider)
                slider.Value = joints[activeJointChange - 1].lastAngle;
        }
        private void ResetButton_Click(object sender, EventArgs e)
        {
            angleTextbox.Text = joints[activeJointChange - 1].defaultAngle.ToString("0.00");
            if (activeSenderSlider is Slider slider)
                slider.Value = joints[activeJointChange - 1].defaultAngle;
        }
        private void SendVisualizationPosition()
        {
            float[] xyz = inverseKinematics.ToolPosition(returnAnglesFromJoints(joints, false));
            ToolPositon_X_label.Content = "X: " + xyz[0].ToString("0.00");
            ToolPositon_Y_label.Content = "Y: " + xyz[1].ToString("0.00");
            ToolPositon_Z_label.Content = "Z: " + xyz[2].ToString("0.00");
            ChangeSpherePosition_action(xyz);
            Position Visualization = new Position(returnAnglesFromJoints(joints, true));
            CreateVisualization_action(Visualization);
        }
        private void SendButton_Click(object sender, EventArgs e)
        {
            joints[activeJointChange - 1].lastAngle = joints[activeJointChange - 1].angle;
            Position actPosition = new Position(returnAnglesFromJoints(joints, true));
            actPosition.addRelative0(relativeZeros);
            actPosition.Duration = 3;
            actPosition.id = history.sequence.Count;
            history.sequence.Add(actPosition);
            SendPosition_action(actPosition);

            if (History_sequenceCombobox.SelectedItem is Sequence selectedSequence)
            {
                CollectionViewSource.GetDefaultView(History_SequenceListBox.ItemsSource).Refresh();
            }
        }

        private void JointControlPanel(int activeJoint)
        {
            DOFcontrolText.Text = "DOF " + activeJoint.ToString();
            angleTextbox.Text = joints[activeJoint - 1].angle.ToString("0.00");

        }

        float[] returnAnglesFromJoints(Joint[] joints, bool AddRelative) //Pozycje dla modelu po dodaniu offsetu
        {
            float[] ans = new float[joints.Length];
            int id = 0;
            if (AddRelative)
            {
                foreach (Joint joint in joints)
                    ans[id++] = joint.angle + joint.relative0;
            }else
            {
                foreach (Joint joint in joints)
                    ans[id++] = joint.angle;
            }
            return ans;
        }
       




       

        private void Control_turnOnManipulator(object sender, RoutedEventArgs e)
        {

            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(128);
            frame.buffer[2] = (byte)(1);
            frame.buffer[3] = (byte)(1);
            frame.buffer[4] = (byte)(2);
            frame.buffer[5] = (byte)(2);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)('x');
            frame.buffer[9] = (byte)('x');
            frame.text = new string(frame.encodeMessage());

            SendMessage_action(frame);


        }

        private async void Control_turnOffManipulator(object sender, RoutedEventArgs e)
        {
            actualPosition.addRelative0(relativeZeros);
            Position TurnOffPosition = actualPosition.deepCopy();
            Position[] returnHomePositions = new Position[3];
            returnHomePositions[0] = TurnOffPosition.deepCopy();
            returnHomePositions[0].joints[1] = 0;
            returnHomePositions[0].joints[2] = 40;
            returnHomePositions[1] = returnHomePositions[0].deepCopy();
            returnHomePositions[1].joints[0] = 0;
            returnHomePositions[1].joints[3] = 0;
            returnHomePositions[1].joints[4] = 0;
            returnHomePositions[1].joints[5] = 0;
            returnHomePositions[2] = returnHomePositions[1].deepCopy();
            returnHomePositions[2].joints[1] = -55;
            returnHomePositions[2].joints[2] = 60;
            returnHomePositions[0].addRelativeToJoints();
            returnHomePositions[1].addRelativeToJoints();
            returnHomePositions[2].addRelativeToJoints();

            simulateStep(TurnOffPosition, returnHomePositions[0], false);
            ChangeSlidersValue(returnHomePositions[0]);
            await Task.Delay(2000);
            simulateStep(returnHomePositions[0], returnHomePositions[1], false);
            ChangeSlidersValue(returnHomePositions[1]);
            await Task.Delay(2000);
            simulateStep(returnHomePositions[1], returnHomePositions[2], false);
            ChangeSlidersValue(returnHomePositions[2]);
            await Task.Delay(3000);
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(128);
            frame.buffer[2] = (byte)(0);
            frame.buffer[3] = (byte)(0);
            frame.buffer[4] = (byte)(0);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)('x');
            frame.buffer[9] = (byte)('x');
            frame.text = new string(frame.encodeMessage());

            SendMessage_action(frame);

        }

        private void Control_rawTurnOffManipulator(object sender, RoutedEventArgs e)
        {

            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(128);
            frame.buffer[2] = (byte)(0);
            frame.buffer[3] = (byte)(0);
            frame.buffer[4] = (byte)(0);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)('x');
            frame.buffer[9] = (byte)('x');
            frame.text = new string(frame.encodeMessage());

            SendMessage_action(frame);

        }


        private void Control_CloseGripperBtn_Click(object sender, RoutedEventArgs e)
        {
            isClosing = true;
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(157);
            frame.buffer[2] = (byte)(1);
            frame.buffer[3] = (byte)('x');
            frame.buffer[4] = (byte)('x');
            frame.buffer[5] = (byte)('x');
            frame.buffer[6] = (byte)('x');
            frame.buffer[7] = (byte)('x');
            frame.buffer[8] = (byte)('x');
            frame.buffer[9] = (byte)('x');
            frame.text = new string(frame.encodeMessage());

            SendMessage_action(frame);
        }

        private void Control_idkGripperBtn_Click(object sender, RoutedEventArgs e)
        {
            isOpening = false;
            isClosing = false;
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(157);
            frame.buffer[2] = (byte)(0);
            frame.buffer[3] = (byte)('x');
            frame.buffer[4] = (byte)('x');
            frame.buffer[5] = (byte)('x');
            frame.buffer[6] = (byte)('x');
            frame.buffer[7] = (byte)('x');
            frame.buffer[8] = (byte)('x');
            frame.buffer[9] = (byte)('x');
            frame.text = new string(frame.encodeMessage());

            SendMessage_action(frame);
        }

        private void Control_OpenGripperBtn_Click(object sender, RoutedEventArgs e)
        {
            isOpening = true;
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(157);
            frame.buffer[2] = (byte)(2);
            frame.buffer[3] = (byte)('x');
            frame.buffer[4] = (byte)('x');
            frame.buffer[5] = (byte)('x');
            frame.buffer[6] = (byte)('x');
            frame.buffer[7] = (byte)('x');
            frame.buffer[8] = (byte)('x');
            frame.buffer[9] = (byte)('x');
            frame.text = new string(frame.encodeMessage());

            SendMessage_action(frame);
        }

        private void Control_ZeroPosition(object sender, RoutedEventArgs e)
        {
            Position previousPosition = actualPosition.deepCopy();
            actualPosition.addRelative0(relativeZeros);
            float[] zeroAngles = new float[6];
            for (int i = 0; i < 6; i++)
                zeroAngles[i] = 0;
            Position returnHomePosition;
            returnHomePosition = previousPosition.deepCopy();
            returnHomePosition.update(zeroAngles);
            returnHomePosition.addRelativeToJoints();

            simulateStep(previousPosition, returnHomePosition, false);
            ChangeSlidersValue(returnHomePosition);
        }
        //////////////////////////////////////////////////////////////////////////////////
        //////                                  Sekwencje
        //////////////////////////////////////////////////////////////////////////////////

        private void HistoryTab_opened(object sender, ContextMenuEventArgs e)
        {
            if (History_sequenceCombobox.SelectedIndex == 0)
            {
                //LoadSequenceToList(history);
            }
        }
        private void LoadSequenceToList(Sequence sequence)
        {
            //History_SequenceListBox.Items.Clear();

            History_SequenceListBox.ItemsSource = null;
            History_SequenceListBox.ItemsSource = sequence.sequence;
        }
        private void simulateStep(Position firstPosition, Position secondPosition, bool simulateOnly)
        {


            CreateVisualization_action(secondPosition);

            if (!simulateOnly)
            {

                SendPosition_action(secondPosition);
            }
        }
        private async void simulateSequence(Sequence sequence, bool simulateOnly)
        {
            simulatingSequence = !simulatingSequence;
            if (simulatingSequence) {
                if (loadedSequence != null)
                {
                    History_simulateSequence.Content = "Zatrzymaj sekwencję";
                    for (int i = 0; i < loadedSequence.length(); i++)
                    {
                        if (selectedPositionInSequence + 1 < loadedSequence.length())
                        {
                            simulateStep(loadedSequence.sequence[selectedPositionInSequence], loadedSequence.sequence[selectedPositionInSequence + 1], simulateOnly);
                            await Task.Delay(loadedSequence.sequence[selectedPositionInSequence].Duration * 1000);
                            selectedPositionInSequence++;
                            History_SequenceListBox.SelectedIndex = selectedPositionInSequence;
                        }
                        if (!simulatingSequence)
                        {
                            History_simulateSequence.Content = "Symuluj sekwencję";

                            break;
                        }

                    }
                }
                else
                    simulatingSequence = false; 
            }
            else
            {
                simulatingSequence = false;
                History_simulateSequence.Content = "Symuluj sekwencję";
            }
        }
        private void positionDuration_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (History_SequenceListBox.SelectedItem is Position selectedPosition)
            {
                TextBox textBox = (TextBox)sender;
                selectedPosition.Duration = Convert.ToInt32(textBox.Text);
            }
        }
        private void History_sequenceCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (History_sequenceCombobox.SelectedItem is Sequence selectedSequence)
            {
                loadedSequence = selectedSequence.deepCopy();
                LoadSequenceToList(loadedSequence);
                History_newSequenceName.Text = loadedSequence.name;
                
            }
        }
        private void History_SequenceListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedSequencePosition = History_SequenceListBox.SelectedItem as Position;
            if (!turnOffSequencePositionChanging && selectedSequencePosition != null)
            {
                CreateVisualization_action(selectedSequencePosition);
                actualPosition = selectedSequencePosition.deepCopy();
                ChangeSlidersValue(actualPosition);
            }
            selectedPositionInSequence = History_SequenceListBox.SelectedIndex;

        }
        private void History_SequencesComboBox_DropDownOpened(object sender, EventArgs e)
        {
            History_sequenceCombobox.ItemsSource = null;
            sequenceManager.LoadFromFile(sequencePath);
            if (sequenceManager.Sequences == null)
            {
                sequenceManager.Sequences = new List<Sequence>();
                sequenceManager.Sequences.Add(history);
            }
            else if (!sequenceManager.Sequences.Any(seq => seq.name == "History"))
                sequenceManager.Sequences.Add(history);
            History_sequenceCombobox.ItemsSource = sequenceManager.Sequences;
        }
        private void History_SaveSequenceButton_Click(object sender, RoutedEventArgs e)
        {
            if (loadedSequence != null)
            {
                Sequence newSequence = loadedSequence.deepCopy();
                newSequence.name = History_newSequenceName.Text;
                sequenceManager.Sequences.Add(newSequence);
                sequenceManager.SaveToFile(sequencePath);
                sequenceManager.Sequences.Add(history);
            }
        }
        private void History_nextPosition_Click(object sender, RoutedEventArgs e)
        {
            if (loadedSequence != null)
                if (selectedPositionInSequence + 1 < loadedSequence.length())
                {
                    simulateStep(loadedSequence.sequence[selectedPositionInSequence], loadedSequence.sequence[selectedPositionInSequence + 1], true);
                    selectedPositionInSequence++;
                    History_SequenceListBox.SelectedIndex = selectedPositionInSequence;
                }
        }
        private void History_earlierPosition_Click(object sender, RoutedEventArgs e)
        {
            if (loadedSequence != null)
                if (selectedPositionInSequence - 1 >=0)
                {
                    simulateStep(loadedSequence.sequence[selectedPositionInSequence], loadedSequence.sequence[selectedPositionInSequence - 1], true);
                    selectedPositionInSequence--;
                    History_SequenceListBox.SelectedIndex = selectedPositionInSequence;
                }
        }
        private void History_simulateSequence_Click(object sender, RoutedEventArgs e)
        {
            if (loadedSequence != null)
                simulateSequence(loadedSequence,true);
        }
        private void History_savePosition_Click(object sender, RoutedEventArgs e)
        {
            if (loadedSequence != null)
            {
                int _temp = selectedPositionInSequence;
                loadedSequence.addPosition(actualPosition.deepCopy());
                LoadSequenceToList(loadedSequence);
                selectedPositionInSequence = _temp;

            }
        }
        private void History_deletePosition_Click(object sender, RoutedEventArgs e)
        {
            if (loadedSequence != null)
            {
                loadedSequence.removePosition(selectedPositionInSequence);
                selectedPositionInSequence = (selectedPositionInSequence-1>=0?selectedPositionInSequence-1:0);
                LoadSequenceToList(loadedSequence);
            }
        }
        private void History_sendSequence_Click(object sender, RoutedEventArgs e)
        {
            if (loadedSequence != null)
                simulateSequence(loadedSequence, false);
        }
        private void History_sendPosition_Click(object sender, RoutedEventArgs e)
        {
            if (loadedSequence != null)
            {
                var selectedSequencePosition = History_SequenceListBox.SelectedItem as Position;
                SendPosition_action(selectedSequencePosition);
            }
        }
        private void History_turnOffChangingPosition_Click(object sender, RoutedEventArgs e)
        {
            if(turnOffSequencePositionChanging==false)
            { 
                History_turnOffChangingPosition.Content = "Włącz zmianę pozycji";
                turnOffSequencePositionChanging = true;
            }
            else
            {
                History_turnOffChangingPosition.Content = "Wyłącz zmianę pozycji";
                turnOffSequencePositionChanging = false;
            }
        }

        private void History_newSequence_Click(object sender, RoutedEventArgs e)
        {
            if (loadedSequence != null)
            {
                List<Position> newPositions = new List<Position>();
                newPositions.Add(actualPosition);
                Sequence newSequence = new Sequence(History_newSequenceName.Text,newPositions);  
                sequenceManager.Sequences.Add(newSequence);
                sequenceManager.SaveToFile(sequencePath);
                sequenceManager.Sequences.Add(history);
            }
        }
        ///////////////////////////////////////////////////////////////
        /////                       Xbox
        ///////////////////////////////////////////////////////////////
        private void OnXboxPadStateChanged(object sender, State state)
        {
            Dispatcher.Invoke(() =>
            {
                UpdateManipulatorPosition_Xbox(state);
            });

        }
       
        private void UpdateManipulatorPosition_Xbox(State state)
        {
            if ((GamepadButtonFlags.Start & XboxPad.Instance.GetPressedButtons()) != 0)
            {
                if (!isTimerRunning)
                {
                    timer.Start();
                    isTimerRunning = true;
                    usingXboxPad = true;
                    XboxControlBus.SendXboxModeChanged(1);
                    Control_turnOnManipulator(null, new RoutedEventArgs());
                    
                }
                else
                {
                    timer.Stop();
                    isTimerRunning = false;
                    usingXboxPad = false;
                    XboxControlBus.SendXboxModeChanged(0);
                }
            }
            if ((GamepadButtonFlags.Y & XboxPad.Instance.GetPressedButtons()) != 0)
            {  
                    timer.Stop();
                    isTimerRunning = false;
                    usingXboxPad = false;
                    Control_turnOffManipulator(null, new RoutedEventArgs());
                
            }
            if (usingXboxPad) { 
            if ((GamepadButtonFlags.DPadUp & XboxPad.Instance.GetPressedButtons()) != 0)
            {
                isClosing = true;
                Message frame = new Message();
                frame.buffer[0] = (byte)('#');
                frame.buffer[1] = (byte)(157);
                frame.buffer[2] = (byte)(1);
                frame.buffer[3] = (byte)('x');
                frame.buffer[4] = (byte)('x');
                frame.buffer[5] = (byte)('x');
                frame.buffer[6] = (byte)('x');
                frame.buffer[7] = (byte)('x');
                frame.buffer[8] = (byte)('x');
                frame.buffer[9] = (byte)('x');
                frame.text = new string(frame.encodeMessage());

                SendMessage_action(frame);
            }
            if ((GamepadButtonFlags.DPadRight & XboxPad.Instance.GetPressedButtons()) != 0)
            {
                isOpening = false;
                isClosing = false;
                Message frame = new Message();
                frame.buffer[0] = (byte)('#');
                frame.buffer[1] = (byte)(157);
                frame.buffer[2] = (byte)(0);
                frame.buffer[3] = (byte)('x');
                frame.buffer[4] = (byte)('x');
                frame.buffer[5] = (byte)('x');
                frame.buffer[6] = (byte)('x');
                frame.buffer[7] = (byte)('x');
                frame.buffer[8] = (byte)('x');
                frame.buffer[9] = (byte)('x');
                frame.text = new string(frame.encodeMessage());
                SendMessage_action(frame);
            }
            if ((GamepadButtonFlags.DPadDown & XboxPad.Instance.GetPressedButtons()) != 0)
            {
                isOpening = true;
                Message frame = new Message();
                frame.buffer[0] = (byte)('#');
                frame.buffer[1] = (byte)(157);
                frame.buffer[2] = (byte)(2);
                frame.buffer[3] = (byte)('x');
                frame.buffer[4] = (byte)('x');
                frame.buffer[5] = (byte)('x');
                frame.buffer[6] = (byte)('x');
                frame.buffer[7] = (byte)('x');
                frame.buffer[8] = (byte)('x');
                frame.buffer[9] = (byte)('x');
                frame.text = new string(frame.encodeMessage());

                SendMessage_action(frame);
            }
            if ((GamepadButtonFlags.A & XboxPad.Instance.GetPressedButtons()) != 0)
            {
                Control_ZeroPosition(null, new RoutedEventArgs());
            }
            }
        }
        private void OnXboxControlModeChanged(int value)
        {
            if(value ==1 && !isTimerRunning)
            {
                timer.Start();
                isTimerRunning = true;
                usingXboxPad = true;
            } if(value == 0)
            {
                timer.Stop();
                isTimerRunning = false;
                usingXboxPad = false;
            }
        }
        private void Control_turnOnGamePad(object sender, RoutedEventArgs e)
        {
            if (!isTimerRunning)
            {
                timer.Start();
                isTimerRunning = true;
                usingXboxPad = true;

                XboxControlBus.SendXboxModeChanged(1);
            }
            else
            {
                timer.Stop();
                isTimerRunning = false;
                usingXboxPad = false;

                XboxControlBus.SendXboxModeChanged(0);
            }    
        }
        private async Task SendManipulatorFrames_Xbox()
        {
            Gamepad _gamepad = _XboxPad.GetCurrentState().Gamepad;
            if (Math.Abs((int)_gamepad.LeftThumbX) > Gamepad.LeftThumbDeadZone)
               await UpdateDofPositionXbox(5, -mapXboxThumb(_gamepad.LeftThumbX));
            if (Math.Abs((int)_gamepad.LeftThumbY) > Gamepad.LeftThumbDeadZone)
                await UpdateDofPositionXbox(2, 0.6f*mapXboxThumb(_gamepad.LeftThumbY));
            if (Math.Abs((int)_gamepad.RightThumbX) > Gamepad.RightThumbDeadZone)
                await UpdateDofPositionXbox(4, mapXboxThumb(_gamepad.RightThumbX));
            if (Math.Abs((int)_gamepad.RightThumbY) > Gamepad.RightThumbDeadZone)
                await UpdateDofPositionXbox(3, -mapXboxThumb(_gamepad.RightThumbY));
            if(Math.Abs((int)_gamepad.RightTrigger) >Gamepad.TriggerThreshold && Math.Abs((int)_gamepad.LeftTrigger) < Gamepad.TriggerThreshold)
                await UpdateDofPositionXbox(1, -100f * mapXboxThumb(_gamepad.RightTrigger));
            if (Math.Abs((int)_gamepad.LeftTrigger) > Gamepad.TriggerThreshold && Math.Abs((int)_gamepad.RightTrigger) < Gamepad.TriggerThreshold)
                await UpdateDofPositionXbox(1,100f*mapXboxThumb(_gamepad.LeftTrigger));
            if((GamepadButtonFlags.LeftShoulder & XboxPad.Instance.GetPressedButtons()) != 0)
                await UpdateDofPositionXbox(6, -1f);
            if ((GamepadButtonFlags.RightShoulder& XboxPad.Instance.GetPressedButtons()) != 0)
                await UpdateDofPositionXbox(6, 1f);
            Dispatcher.Invoke(() =>
            {
                Position actPosition = new Position(returnAnglesFromJoints(joints, true));
                actPosition.addRelative0(relativeZeros);
                SendPosition_action(actPosition);
            });

        }
        private Task UpdateDofPositionXbox(int dof, float delta)
        {
           
            if(!initialization)
            {
                activeJointChange = dof;
                activeSenderSlider = _JointSliders[dof - 1];
                
                joints[activeJointChange - 1].angle = CalculateToBounds(activeJointChange - 1, joints[activeJointChange - 1].angle + delta);
                if (joints[activeJointChange - 1].angle + delta > joints[activeJointChange - 1].angleMax && joints[activeJointChange - 1].angle == joints[activeJointChange - 1].angleMax)
                {
                    XboxPad.Instance.VibrateGamepad(0, 0.1f);
                    
                }
                else if (joints[activeJointChange - 1].angle + delta < joints[activeJointChange - 1].angleMin && joints[activeJointChange - 1].angle == joints[activeJointChange - 1].angleMin)
                {
                    XboxPad.Instance.VibrateGamepad(0.1f, 0);
                }
                else
                    XboxPad.Instance.StopVibration();
                Dispatcher.Invoke(() =>
                {
                    angleTextbox.Text = joints[activeJointChange - 1].angle.ToString("0.00");
                        if (activeSenderSlider is Slider slider)
                    slider.Value = joints[activeJointChange - 1].angle;
                   
                });
               
            }
            return Task.CompletedTask;
        }
        private float mapXboxThumb(int value)
        {

            return (float)value / 32768f;
        }


        ////////Kinematyka odwrotna 
        ///
        private void UseInverseKinematics(float[] startAngle,float[] destination, int numberOfPoints)
        {
            Position InversePosition=actualPosition.deepCopy();
            InversePosition.addRelative0(relativeZeros);

            float[] startPoint = inverseKinematics.ToolPosition(startAngle);
            float deltaX = destination[0] - startPoint[0];
            float deltaY = destination[1] - startPoint[1];
            float deltaZ = destination[2] - startPoint[2];
            float[] midPoint = new float[6]; 

            float[] InverseKinematicsResult = new float[6];
            for (int i = 0; i < 6; i++)
            {
                InverseKinematicsResult[i] = startAngle[i];
                InversePosition.joints[i] = startAngle[i];
            }
            

            for (int i=1; i<= numberOfPoints; i++)
            {
                float t = (float)i / (float)(numberOfPoints - 1);
                midPoint[0] = startPoint[0] + t * deltaX;
                midPoint[1] = startPoint[1] + t * deltaY;
                midPoint[2] = startPoint[2] + t * deltaZ;
                midPoint[3] = 0f;
                midPoint[4] = 0f;
                midPoint[5] = 0f;

               
                    InverseKinematicsResult = inverseKinematics.inverseKinematics6DOF(InverseKinematicsResult, midPoint);
                    InversePosition.update(InverseKinematicsResult);
                    InversePosition.addRelativeToJoints();
               
      
                CreateVisualization_action(InversePosition);
                ChangeSlidersValue(InversePosition);
               
            }


        }
        
             
         
       
        private void Inverse_X_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            

/*
            float[] InverseAnglesResult = new float[6];
            float[] zeroAngles = new float[6];
            for (int i = 0; i < 6; i++)
                zeroAngles[i] = 0;
            Position previousPosition = actualPosition.deepCopy();
            actualPosition.addRelative0(relativeZeros);
            InverseAnglesResult = inverseKinematics.inverseKinematics6DOF(InverseAnglesResult, destination);
           
           
            
            Position InversePosition;
            InversePosition = previousPosition.deepCopy();
            InversePosition.update(InverseAnglesResult);
           InversePosition.addRelativeToJoints();
*/
        }
        private void Inverse_Y_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
        private void Inverse_Z_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private async void inverseKinematics_Btn_Click(object sender, RoutedEventArgs e)
        {
            ///Control_ZeroPosition(null, null);
            float[] destination = new float[6];
            float[] zero = new float[6];
            zero= actualPosition.joints;
            //actualPosition.addRelative0(relativeZeros);
            destination[0] = (float)Inverse_XSlider.Value + 700f;
            destination[1] = 0;
            destination[2] = 700f;
            destination[3] = 0f;
            destination[4] = 0f;
            destination[5] = 0f;
         //   for (int i = 0; i < 6; i++)
         //      zero[i] = zero[i]*180f/(float)Math.PI;
             UseInverseKinematics(zero, destination, 100);
        }
    }
}
