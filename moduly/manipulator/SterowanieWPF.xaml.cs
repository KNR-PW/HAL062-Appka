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
            public double angle = 0;
            public double angleMin = -180;
            public double angleMax = 180;
            public double rotPointX = 0;
            public double rotPointY = 0;
            public double rotPointZ = 0;
            public double rotAxisX = 0;
            public double rotAxisY = 0;
            public double rotAxisZ = 0;
            public double defaultAngle = 0;
            public double lastAngle = 0;
            public double relative0 = 0; //to jest wartosc, ktora odpowiada za zmiane katow zerowych w modelu 3D na katy zerowe w mainboardzie manipulatora
            public Joint(double min, double max, double angle, double relative0)
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



        Joint[] joints = new Joint[6];
        Position actualPositon = null; 
        double[] angles = new double[6];
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
        private int id = 0;


        public SterowanieWPF()
        {
            InitializeComponent();
            joints[0] = new Joint(-90,90,0,0);
            joints[1] = new Joint(-60,90,0,50);
            joints[2] = new Joint(-60,-60,0,-60);
            joints[3] = new Joint(-90,90,0,180);
            joints[4] = new Joint(-90,60,0,10);
            joints[5] = new Joint(-360,360,0,90);
            addValues.Add("-5.0", -5.0);
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
            for(int i =0; i < 6; i++)
                UpdateSlidersValues(joints[i], JointSliders[i], JointLabels[i]);


            actualPositon = new Position(returnAnglesFromJoints(joints));
            positionsHistory.Add(actualPositon);
            history = new Sequence("History", positionsHistory);
            sequencesList.Add(history);
            initialization = false;
           
        }

        private void UpdateSlidersValues(Joint joint, Slider slider, Label label)
        {
            slider.Minimum = joint.angleMin;
            slider.Maximum=joint.angleMax;
            slider.Maximum = joint.angle;
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
                joints[Convert.ToInt32(parentGrid.Tag.ToString())-1].angle = slider.Value;
                slider.Background = Brushes.LightGray;
                    angleTextbox.Text = slider.Value.ToString("0.00");
                    Label associatedLabel = FindLabelInGrid(parentGrid);
                if (associatedLabel != null)
                {
                    associatedLabel.Content = slider.Value.ToString("0.00");
                }
                    SendVisualizationPosition();
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

        private double CalculateToBounds(int jointID, double newValue)
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
                joints[activeJointChange - 1].angle = CalculateToBounds(activeJointChange - 1, joints[activeJointChange - 1].angle + addValues[btn.Tag.ToString()]);
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
            //actualPositon.update(returnAnglesFromJoints(joints));
            Position actPosition = new Position(returnAnglesFromJoints(joints));
            positionsHistory.Add(actPosition);
            //tutaj to do wysylania zrobic
            SendPosition_action(actPosition);
            actPosition.id = ++id;
            if (sequenceCombobox.SelectedItem is Sequence selectedSequence)
            {
                CollectionViewSource.GetDefaultView(SequenceListBox.ItemsSource).Refresh();
            }
        }

        private void JointControlPanel(int activeJoint)
        {
            DOFcontrolText.Text = "DOF "+activeJoint.ToString();
            angleTextbox.Text = joints[activeJoint-1].angle.ToString("0.00");

        }

        double[] returnAnglesFromJoints(Joint[] joints)
        {
            double[] ans = new double[joints.Length];
            int id = 0;
            foreach(Joint joint in joints)
                ans[id++] = joint.angle + joint.relative0;
            return ans;
        }

        private void SaveSequenceButton_Click(object sender, RoutedEventArgs e)
        {
          //  string json = JsonConvert.SerializeObject(this, Formatting.Indented);
         //   System.IO.File.WriteAllText(filePath, json);

        }
        private void SequencesComboBox_DropDownOpened(object sender, EventArgs e)
        {
            // string json = System.IO.File.ReadAllText(filePath);
            ///return JsonConvert.DeserializeObject<Sequence>(json);
            //
            sequenceCombobox.ItemsSource = sequencesList;
        }
        

        private void HistoryTab_opened(object sender, ContextMenuEventArgs e)
        {
            if(sequenceCombobox.SelectedIndex == 0)
            {
                //LoadSequenceToList(history);

            }

        }

        private void LoadSequenceToList(Sequence sequence)
        {
            
            SequenceListBox.Items.Clear();
           
            SequenceListBox.ItemsSource = sequence.sequence;


        }
        private void RefreshPositionListInList(Sequence sequence)
        {
            

        }
        private void sequenceCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(sequenceCombobox.SelectedItem is Sequence selectedSequence)
            {
                LoadSequenceToList(selectedSequence);
            }
        }

        private void SequenceListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedSequence = SequenceListBox.SelectedItem as Position;
            CreateVisualization_action(selectedSequence);
        }

        private void positionDuration_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (SequenceListBox.SelectedItem is Position selectedPosition)
            {
                TextBox textBox = (TextBox)sender;
                selectedPosition.Duration = Convert.ToInt32( textBox.Text);
            }
        }


        private void TurnOnManipulator_Click(object sender, RoutedEventArgs e)
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
    }
}
