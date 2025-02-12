using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using HelixToolkit.Wpf;

namespace HAL062app.moduly.manipulator
{


    public partial class manipulatorForm : Form
    {

        public Action<Position> sendPositionToController_Action;
        public Action<Message> sendFrameToController_Action;
        
        public manipulatorForm()
        {
           
            InitializeComponent();
            sterowanieWPF1.SendMessage_action += SendFrame;
            sterowanieWPF1.SendPosition_action += SendPosition;
            sterowanieWPF1.CreateVisualization_action += CreateVisualization;
            sterowanieWPF1.ChangeSpherePosition_action += ChangeSpherePosition;
            sterowanieWPF1.SendXYZPositon_action += SendXYZPositon;
        }
       
        
        private void SendXYZPositon(Position position)
        {
            sendPositionToController_Action(position);
        }

        private void SendPosition(Position position)
        {

            manipulatorWPF1.ForwardKinematics(position.joints);
            sendPositionToController_Action(position);

        }
        private void CreateVisualization(Position position)
        {
            manipulatorWPF1.ForwardKinematics(position.joints);

        }
        private void SendFrame(Message msg)
        {
            sendFrameToController_Action(msg);

        }

        private void manipulatorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
        private void ChangeSpherePosition(float[] xyz, int ID)
        {
            manipulatorWPF1.UpdateSphere(xyz, ID);
        }
    }
}
