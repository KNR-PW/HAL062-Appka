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
        
        public manipulatorForm()
        {
            InitializeComponent();
            manipulatorWPF1.test += testa;
            sterowanieWPF1.SendPosition_action += SendPosition;
            sterowanieWPF1.CreateVisualization_action += CreateVisualization;
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            manipulatorWPF1.StartInverseKinematics(sender, e);
            
        }
       
        private void testa (int a)
        {
           

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




    }
}
