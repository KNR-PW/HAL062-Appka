using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Text;
using System.Runtime.InteropServices;


namespace HAL062app.moduly.laboratorium
{
    
    interface ILaboratoryView
    {
        event Action<Message> SendFrame_Action;
        event Action<int> GetTubeInfo_Action;
        event Action<bool> RotateRevolver_Action;
       
        Func<int, string> GetTubeDescription_Func { get; set; }
    }

    


    public partial class LaboratoryForm : Form, ILaboratoryView
    {
        public event Action<Message> SendFrame_Action;
        public event Action<int> GetTubeInfo_Action;
        public event Action<bool> RotateRevolver_Action;

        public Func<int, string> GetTubeDescription_Func { get; set; }
        private int probesCount = 11;
        Revolver revolver;
        public LaboratoryForm()
        {
            InitializeComponent();

            ProbesPanel.Paint += ProbesPanel_Paint;

            for (int i = 0; i < probesCount; i++)
            {
               


            }
            ProbesPanel.Resize += ProbesPanel_Resize;
            revolver = new Revolver(probesCount, ProbesPanel, pos=> GetTubeDescription_Func?.Invoke(pos) ?? "Brak opisu");
        
        }

        private void ProbesPanel_Resize1(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void laboratoriumForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }



       






        private void ProbesPanel_Paint(object sender, PaintEventArgs e)
        {


            /*
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int centerX = ProbesPanel.Width / 2;
            int centerY = ProbesPanel.Height / 2;
            int radius = 160;
            int probes = 10;

            for (int i = 0; i < probes; i++)
            {

                double angle = 2* Math.PI * i / probes;
                int x = (int) (centerX + radius * Math.Cos(angle));
                int y = (int) (centerY + radius * Math.Sin(angle));


                RichTextBox tb = new RichTextBox();
                tb.Width = 120;
                tb.Height = 120;
                tb.BackColor = Color.Black;
                tb.ForeColor = Color.Lime;
                tb.Text = "Probe " + i.ToString() ;
                tb.Location = new Point(x - tb.Width/2, y - tb.Height/2);

                ProbesPanel.Controls.Add(tb);
            }
            positionTextBoxesInPanel();
            */
        }


        private void ProbesPanel_Resize(object sender, EventArgs e)
        {
        
            revolver.UpdatePanel();
        }

        public enum ID : byte
        {
            Reagent = 201,
            Module = 202,
            DrillMove = 203,
            Drill = 204,
            Probe = 205
        }
        public enum ModuleCommand : byte
        {
            Up = 1,
            Stop = 0,
            Down = 2
        }


        private void SendCommand(ID id, byte param1 = 0, byte param2 = 0, byte param3 = 0, byte param4 = 0,
                         byte param5 = 0, byte param6 = 0, byte param7 = 0, byte param8 = 0, byte param9 = 0)
        {
            var frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)id;
            frame.buffer[2] = param1;
            frame.buffer[3] = param2;
            frame.buffer[4] = param3;
            frame.buffer[5] = param4;
            frame.buffer[6] = param5;
            frame.buffer[7] = param6;
            frame.buffer[8] = param7;
            frame.buffer[9] = param9; // jeśli 9 ma znaczenie, inaczej dopasuj
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action?.Invoke(frame);
        }

      
      
        private void ModulUpBtn_Click(object sender, EventArgs e)
        {     
            SendCommand(ID.Module, (byte)ModuleCommand.Up);
        }

        private void ModulStopBtn_Click(object sender, EventArgs e)
        {
            SendCommand(ID.Module, (byte)ModuleCommand.Stop);
        }

        private void ModulDownBtn_Click(object sender, EventArgs e)
        {
            SendCommand(ID.Module, (byte)ModuleCommand.Down);
        }
        private void Lab_preparat_A_btn_Click(object sender, EventArgs e)
        {
            SendCommand(ID.Reagent, 1);
        }

        private void Lab_preparat_B_btn_Click(object sender, EventArgs e)
        {
            SendCommand(ID.Reagent, 2);
        }

        private void Lab_preparat_C_btn_Click(object sender, EventArgs e)
        {
            SendCommand(ID.Reagent, 3);
        }

        private void Lab_preparat_D_btn_Click(object sender, EventArgs e)
        {
            SendCommand(ID.Reagent, 4);
        }

        private void Lab_preparat_E_btn_Click(object sender, EventArgs e)
        {
            SendCommand(ID.Reagent, 5);
        }

        private void Lab_preparat_stop_btn_Click(object sender, EventArgs e)
        {
            SendCommand(ID.Reagent, 0);
        }

        private void DrillMoveUpBtn_Click(object sender, EventArgs e)
        {

            SendCommand(ID.DrillMove, 2, (byte)DrillMovePWM_numeric.Value);
        }

        private void DrillMoveStopBtn_Click(object sender, EventArgs e)
        {
            SendCommand(ID.DrillMove, 0, (byte)DrillMovePWM_numeric.Value);
        }

        private void DrillMoveDownBtn_Click(object sender, EventArgs e)
        {
            SendCommand(ID.DrillMove, 1, (byte)DrillMovePWM_numeric.Value);
        }

        private void DrillDownBtn_Click(object sender, EventArgs e)
        {
            SendCommand(ID.Drill, (byte)(75 - DrillPWM_numeric.Value));
        }

        private void DrillStopBtn_Click(object sender, EventArgs e)
        {
            SendCommand(ID.Drill, (byte)(75));
        }

        private void DrillUpBtn_Click(object sender, EventArgs e)
        {
            SendCommand(ID.Drill, (byte)(75 + DrillPWM_numeric.Value));
        }

        private void FullProbeLeftBtn_Click(object sender, EventArgs e)
        {
            SendCommand(ID.Probe, 3);
            RotateRevolver_Action(false);
            revolver.UpdatePanel();
        }

        private void TenStepsProbeLeftBtn_Click(object sender, EventArgs e)
        {
            SendCommand(ID.Probe, 3,2);
        }

        private void OneStepsProbeLeftBtn_Click(object sender, EventArgs e)
        {
            SendCommand(ID.Probe, 3,1);
        }

        private void ProbeStopBtn_Click(object sender, EventArgs e)
        {
            SendCommand(ID.Probe, 0);
        }

        private void OneStepsProbeRightBtn_Click(object sender, EventArgs e)
        {
            SendCommand(ID.Probe, 3, 1, 1);
        }

        private void TenStepsProbeRightBtn_Click(object sender, EventArgs e)
        {
            SendCommand(ID.Probe, 3, 2, 1);
        }

        private void FullProbeRightBtn_Click(object sender, EventArgs e)
        {
            SendCommand(ID.Probe, 3,0,1);
            RotateRevolver_Action(true);
            revolver.UpdatePanel();
        }

        private void MixerOnBtn_Click(object sender, EventArgs e)
        {
            revolver.UpdatePanel();
        }

        private void ProbeUp10stepsBtn_Click(object sender, EventArgs e)
        {
            SendCommand(ID.Probe,2,2);
        }

        private void ProbeUp1stepBtn_Click(object sender, EventArgs e)
        {
            SendCommand(ID.Probe, 2,1,0);
        }

        private void ProbeClosingStopBtn_Click(object sender, EventArgs e)
        {
            SendCommand(ID.Probe, 0,2);
        }

        

        private void ProbeDown10stepsBtn_Click(object sender, EventArgs e)
        {
            SendCommand(ID.Probe, 2,2,1);
        }

        private void InfProbeLeftBtn_Click(object sender, EventArgs e)
        {
            SendCommand(ID.Probe, 3,4);
        }

        private void InfProbeRightBtn_Click(object sender, EventArgs e)
        {
            SendCommand(ID.Probe, 3,4,1);
        }

        private void ProbeDownFullBtn_Click(object sender, EventArgs e)
        {
            SendCommand(ID.Probe, 2,4,1);
        }

        private void ProbeUpFullBtn_Click(object sender, EventArgs e)
        {
            SendCommand(ID.Probe, 2,4);
        }

        private void ProbeDown1stepBtn_Click(object sender, EventArgs e)
        {

        }

      
        private string GetTubeDescription(int pos)
        {
            GetTubeInfo_Action(pos);
            return null;
            
        }

    }
    public class Revolver
    {

        private readonly int _tubeCount;
        private string[] tubeDescriptions;
        private Panel _revolverPanel;
        private RichTextBox[] TextBoxes;
        Func<int, string> getTubeDescription;

        private static string[] reagents = new string[5] { "Preparat A", "Preparat B", "Preparat C", "Preparat D", "Preparat E"};
        private string[] modifier = new string[11] { $"Spektrometr {reagents[4]}", "", "","","Zamykanie", "", "Wiertło", reagents[0], reagents[1] + "\nMieszadło", reagents[2], reagents[3]};



        public Revolver(int tubeCount, Panel panel, Func<int, string> getTubeDescriptionFunc)
        {
            _tubeCount = tubeCount;
            tubeDescriptions = new string[_tubeCount];
            _revolverPanel = panel;
            TextBoxes = new RichTextBox[_tubeCount];
            getTubeDescription = getTubeDescriptionFunc;
            for (int i = 0; i < _tubeCount; i++)
            {
                

                RichTextBox tb = new RichTextBox();
                tb.Width = 120;
                tb.Height = 50;
                tb.BackColor = Color.Black;
                tb.ForeColor = Color.Lime;
                tb.Text = "Probe " + (i).ToString();
                TextBoxes[i] = tb;
               // _revolverPanel.Controls.Add(TextBoxes[i]);
                
            }

            UpdatePanel();
        }
        public Func<int, string> GetTubeDescriptionFunc
        {
            set => getTubeDescription = value;
        }

        private void UpdateTextBox(int ID)
        {

            tubeDescriptions[ID] = getTubeDescription(ID);
            TextBoxes[ID].Text = tubeDescriptions[ID];



        }

        public void UpdatePanel ()
        {
        int centerX = _revolverPanel.Width / 2;
        int centerY = _revolverPanel.Height / 2;
        int radiusY = (int)(0.8f * (float)_revolverPanel.Height / 2f);
        int radiusX = (int)(0.8f * (float)_revolverPanel.Width / 2f);

                _revolverPanel.Controls.Clear();

            for (int i = 0; i<_tubeCount; i++)
            {

                double angle = 2 * Math.PI * i / _tubeCount;
                int x = (int)(centerX + radiusX * Math.Cos(angle + Math.PI));
                int y = (int)(centerY + radiusY * Math.Sin(angle + Math.PI));

                UpdateTextBox(i);
                RichTextBox tb = TextBoxes[i];

            //    tb.Location = new Point(x - tb.Width / 2, y - tb.Height / 2);

                Panel container = new Panel();
                container.Width = tb.Width + 10;
                container.Height = tb.Height + 50;
                container.BackColor = Color.Transparent;
                container.Location = new Point(x - container.Width / 2, y - container.Height / 2);
                container.BorderStyle = BorderStyle.FixedSingle;

                // Label (tekst nad RichTextBoxem)
                Label label = new Label();
                label.Text = $"#{i}: {modifier[i]}";
                label.ForeColor = Color.White;
                label.BackColor = Color.Transparent;
                label.Font = new Font("Arial", 10, FontStyle.Bold);
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Location = new Point(0, 0);
                label.Width = container.Width;
                label.Height = 40;

                // Ustawienia RichTextBox
                tb.Location = new Point(5, container.Height - tb.Height - 5);
                tb.ReadOnly = true;

                // Czyścimy stare kontrolki jeśli już są
                if (!container.Controls.Contains(tb))
                    container.Controls.Add(tb);
                container.Controls.Add(label);

                // Dodaj do głównego panelu
                _revolverPanel.Controls.Add(container);



            }
}






        private class Tube
        {
            public int Position { get; set; }
            public string Description { get; set; }
            public Tube(int position, string description)
            {
                Position = position;
                Description = description;
            }
            public void UpdateDescription()
            {
              
            }
        }




    }
}
