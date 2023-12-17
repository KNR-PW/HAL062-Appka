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
using Newtonsoft.Json;

namespace HAL062app.moduly.manipulator
{
    /// <summary>
    /// Logika interakcji dla klasy SterowanieWPF.xaml
    /// </summary>
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
        List<Sequence> sequencesList = new List<Sequence>();
        List<string> sequenceNames = new List<string>();
        string filePath = "sequenceList.txt";
        private int _id = 0;
        private float[] relativeZeros = new float[6]; //Dla modelu wgrywanego z plików kąty są inne, niż te dla manipulatora w łaziku. Tutaj przechowujemy różnicę tych kątów, aby przy wysyłaniu do łazika ten kąt odjąć
        Sequence loadedSequence; 
        int selectedPositionInSequence = 0; //Która pozycja z sekwencji aktualnie jest wyświetlana
        bool turnOffSequencePositionChanging = false; //Klikanie w pozycję na historii w sekwencji zmienia/ nie zmienia wizualizacji
        Position settingPosition = null; //to jest zmienna przechowująca aktualne położenie, wykorzystywane w zapisywaniu w sekcji sekwencji
        //manager sekwencji - zapisywanie do pliku
        private SequenceManager sequenceManager = null;


        public SterowanieWPF()
        {
            InitializeComponent(); //to tutaj zmienia się maksymalne, minimalne i startowe kąty dla symulacji 
            joints[0] = new Joint(-90,90,0,0); 
            joints[1] = new Joint(-60,90,0,50);
            joints[2] = new Joint(-60,70,0,-60);
            joints[3] = new Joint(-90,90,0,180);
            joints[4] = new Joint(-90,60,0,10);
            joints[5] = new Joint(-360,360,0,90);
            addValues.Add("-5.0", -5.0); //to odpowiada tylko za przyciski do szczegółowej zmiany kąta, nic ważnego
            addValues.Add("5.0", 5.0);
            addValues.Add("-1.0", -1.0);
            addValues.Add("1.0", 1.0);
            addValues.Add("-0.5", -0.5);
            addValues.Add("0.5", 0.5);
            addValues.Add("-0.1", -0.1);
            addValues.Add("0.1", 0.1);
            foreach (var sequence in sequencesList)
            {
                sequenceNames.Add(sequence.name);
            }

            Slider[] JointSliders = {Joint1Slider,Joint2Slider, Joint3Slider, Joint4Slider, Joint5Slider, Joint6Slider };
            Label[] JointLabels = { Joint1Label, Joint2Label, Joint3Label, Joint4Label, Joint5Label, Joint6Label };
            _JointSliders = JointSliders;
            _JointLabels = JointLabels;
            for (int i = 0; i < 6; i++)
            {
                UpdateSlidersValues(joints[i], _JointSliders[i], _JointLabels[i]);
                relativeZeros[i] = joints[i].relative0;
            }

            actualPosition = new Position(returnAnglesFromJoints(joints));
            actualPosition.Duration = 5;
            positionsHistory.Add(actualPosition);
            history = new Sequence("History", positionsHistory);
            sequencesList.Add(history);
            initialization = false;


          //  sequenceManager= new SequenceManager();
            //sequenceManager.LoadFromFile("sequences.json");
        }

        private void UpdateSlidersValues(Joint joint, Slider slider, Label label)
        {
            slider.Minimum = joint.angleMin;
            slider.Maximum= joint.angleMax;
            slider.Value = joint.angle;
            label.Content = joint.angle;
        }

        private void JointGridClicked(object sender, MouseButtonEventArgs e)
        {
            if(sender is Grid grid)
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
            if(!initialization)
            if (sender is Slider slider)
            {
                Grid parentGrid = slider.Tag as Grid;
                JointGridClicked(parentGrid, null);
                joints[Convert.ToInt32(parentGrid.Tag.ToString())-1].angle = (float)slider.Value;
                slider.Background = Brushes.LightGray;
                    angleTextbox.Text = slider.Value.ToString("0.00");
                    Label associatedLabel = FindLabelInGrid(parentGrid);
                if (associatedLabel != null)
                {
                    associatedLabel.Content = slider.Value.ToString("0.00");
                }
                    actualPosition = new Position(returnAnglesFromJoints(joints));
                    SendVisualizationPosition();
                    
                }    

        }
        private void ChangeSlidersValue(Position position)
        {
            for (int i = 0; i < 6; i++)
            {
                _JointSliders[i].Value = position.joints[i] - relativeZeros[i];
                _JointLabels[i].Content =( position.joints[i] - relativeZeros[i]).ToString("0.00");
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
            else if(newValue > joints[jointID].angleMax)
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
            Position Visualization = new Position(returnAnglesFromJoints(joints));
            CreateVisualization_action(Visualization);
        }
        private void SendButton_Click(object sender, EventArgs e)
        {
            joints[activeJointChange - 1].lastAngle = joints[activeJointChange - 1].angle;
            //actualPosition.update(returnAnglesFromJoints(joints));
            Position actPosition = new Position(returnAnglesFromJoints(joints));
            actPosition.addRelative0(relativeZeros);
            actPosition.Duration = 5;
            positionsHistory.Add(actPosition);
            //tutaj to do wysylania zrobic
            SendPosition_action(actPosition);
            actPosition.id = ++_id;
            if (History_sequenceCombobox.SelectedItem is Sequence selectedSequence)
            {
                CollectionViewSource.GetDefaultView(History_SequenceListBox.ItemsSource).Refresh();
            }
        }

        private void JointControlPanel(int activeJoint)
        {
            DOFcontrolText.Text = "DOF "+activeJoint.ToString();
            angleTextbox.Text = joints[activeJoint-1].angle.ToString("0.00");

        }

        float[] returnAnglesFromJoints(Joint[] joints)
        {
            float[] ans = new float[joints.Length];
            int id = 0;
            foreach(Joint joint in joints)
                ans[id++] = joint.angle + joint.relative0;
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
            returnHomePositions[2].joints[1] = -40;
            returnHomePositions[2].joints[2] = 70;
            returnHomePositions[0].addRelativeToJoints();
            returnHomePositions[1].addRelativeToJoints();
            returnHomePositions[2].addRelativeToJoints();

            simulateStep(TurnOffPosition, returnHomePositions[0], false);
            ChangeSlidersValue(returnHomePositions[0]);
            await Task.Delay(5000);
            simulateStep(returnHomePositions[0], returnHomePositions[1], false);
            ChangeSlidersValue(returnHomePositions[1]);
            await Task.Delay(5000);
            simulateStep(returnHomePositions[1], returnHomePositions[2], false);
            ChangeSlidersValue(returnHomePositions[2]);
            await Task.Delay(5000);
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





        //////////////////////////////////////////////////////////////////////////////////
        //////                                  Sekwencje
        //////////////////////////////////////////////////////////////////////////////////

        private void SaveSequenceButton_Click(object sender, RoutedEventArgs e)
        {
            //  string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            //   System.IO.File.WriteAllText(filePath, json);
        }
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
                LoadSequenceToList(selectedSequence);
                loadedSequence = selectedSequence;
            }
        }
        private void History_SequenceListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedSequencePosition = History_SequenceListBox.SelectedItem as Position;
            if (!turnOffSequencePositionChanging && selectedSequencePosition != null)
            {
                CreateVisualization_action(selectedSequencePosition);
                actualPosition = selectedSequencePosition;
                ChangeSlidersValue(actualPosition);
            }
            selectedPositionInSequence = History_SequenceListBox.SelectedIndex;

        }
        private void History_SequencesComboBox_DropDownOpened(object sender, EventArgs e)
        {
            // string json = System.IO.File.ReadAllText(filePath);
            ///return JsonConvert.DeserializeObject<Sequence>(json);
            //
            History_sequenceCombobox.ItemsSource = sequencesList;
        }
        private void History_SaveSequenceButton_Click(object sender, RoutedEventArgs e)
        {
            //  string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            //   System.IO.File.WriteAllText(filePath, json);

        }

        private void simulateStep(Position firstPosition, Position secondPosition, bool simulateOnly)
        {

            
            CreateVisualization_action(secondPosition);

            if (!simulateOnly)
            {
                
                SendPosition_action(secondPosition);
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
        private async void simulateSequence(Sequence sequence, bool simulateOnly)
        {
            if (loadedSequence != null)
                for (int i = 0; i < loadedSequence.length(); i++)
                {
                    if (selectedPositionInSequence + 1 < loadedSequence.length())
                    {
                        simulateStep(loadedSequence.sequence[selectedPositionInSequence], loadedSequence.sequence[selectedPositionInSequence + 1], simulateOnly);
                        await Task.Delay(loadedSequence.sequence[selectedPositionInSequence].Duration * 1000);
                        selectedPositionInSequence++;
                        History_SequenceListBox.SelectedIndex = selectedPositionInSequence;
                    }

                }

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
    }
}
