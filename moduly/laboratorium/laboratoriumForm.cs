using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xceed.Wpf.AvalonDock.Controls;

namespace HAL062app.moduly.laboratorium
{
    public partial class laboratoriumForm : Form
    {
        public event Action<Message> SendFrame_Action;
        private List<RichTextBox> TextBoxes = new List<RichTextBox>();
        private int probesCount = 11;

        public laboratoriumForm()
        {
            InitializeComponent();
           
            ProbesPanel.Paint += ProbesPanel_Paint;

            for(int i = 0; i < probesCount; i++)
            {
                RichTextBox tb = new RichTextBox();
                tb.Width = 120;
                tb.Height = 50;
                tb.BackColor = Color.Black;
                tb.ForeColor = Color.Lime;
                tb.Text = "Probe " + (i+1).ToString();
                ProbesPanel.Controls.Add(tb);
                TextBoxes.Add(tb);


            }
            ProbesPanel.Resize += ProbesPanel_Resize;
            positionTextBoxesInPanel();
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
            positionTextBoxesInPanel();
        }

        private void positionTextBoxesInPanel()
        {
            int centerX = ProbesPanel.Width / 2;
            int centerY = ProbesPanel.Height / 2;
            int radiusY = ProbesPanel.Height / 2 - 40;
            int radiusX = ProbesPanel.Width / 2 - 80;


            for (int i = 0; i < probesCount; i++)
            {

                double angle = 2 * Math.PI * i / probesCount;
                int x = (int)(centerX + radiusX * Math.Cos(angle + Math.PI));
                int y = (int)(centerY + radiusY * Math.Sin(angle + Math.PI));


                RichTextBox tb = TextBoxes[i];
                tb.Location = new Point(x - tb.Width / 2, y - tb.Height / 2);

              
            }
        }
        private void ModulUpBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(202);
            frame.buffer[2] = (byte)(1);
            frame.buffer[3] = (byte)(0);
            frame.buffer[4] = (byte)(0);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }

        private void ModulStopBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(202);
            frame.buffer[2] = (byte)(0);
            frame.buffer[3] = (byte)(0);
            frame.buffer[4] = (byte)(0);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }

        private void ModulDownBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(202);
            frame.buffer[2] = (byte)(2);
            frame.buffer[3] = (byte)(0);
            frame.buffer[4] = (byte)(0);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }
        private void Lab_preparat_A_btn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(201);
            frame.buffer[2] = (byte)(1);
            frame.buffer[3] = (byte)(0);
            frame.buffer[4] = (byte)(0);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }

        private void Lab_preparat_B_btn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(201);
            frame.buffer[2] = (byte)(2);
            frame.buffer[3] = (byte)(0);
            frame.buffer[4] = (byte)(0);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }

        private void Lab_preparat_C_btn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(201);
            frame.buffer[2] = (byte)(3);
            frame.buffer[3] = (byte)(0);
            frame.buffer[4] = (byte)(0);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }

        private void Lab_preparat_D_btn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(201);
            frame.buffer[2] = (byte)(4);
            frame.buffer[3] = (byte)(0);
            frame.buffer[4] = (byte)(0);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }

        private void Lab_preparat_E_btn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(201);
            frame.buffer[2] = (byte)(5);
            frame.buffer[3] = (byte)(0);
            frame.buffer[4] = (byte)(0);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }

        private void Lab_preparat_stop_btn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(201);
            frame.buffer[2] = (byte)(0);
            frame.buffer[3] = (byte)(0);
            frame.buffer[4] = (byte)(0);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }

        private void DrillMoveUpBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(203);
            frame.buffer[2] = (byte)(2);
            frame.buffer[3] = (byte)(DrillMovePWM_numeric.Value);
            frame.buffer[4] = (byte)(0);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }

        private void DrillMoveStopBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(203);
            frame.buffer[2] = (byte)(0);
            frame.buffer[3] = (byte)(DrillMovePWM_numeric.Value);
            frame.buffer[4] = (byte)(0);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }

        private void DrillMoveDownBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(203);
            frame.buffer[2] = (byte)(1);
            frame.buffer[3] = (byte)(DrillMovePWM_numeric.Value);
            frame.buffer[4] = (byte)(0);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }

        private void DrillDownBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(204);
            frame.buffer[2] = (byte)(75 - DrillPWM_numeric.Value);
            frame.buffer[3] = (byte)(0);
            frame.buffer[4] = (byte)(0);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }

        private void DrillStopBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(204);
            frame.buffer[2] = (byte)(75);
            frame.buffer[3] = (byte)(0);
            frame.buffer[4] = (byte)(0);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }

        private void DrillUpBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(204);
            frame.buffer[2] = (byte)(75 + DrillPWM_numeric.Value);
            frame.buffer[3] = (byte)(0);
            frame.buffer[4] = (byte)(0);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }

        private void FullProbeLeftBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(205);
            frame.buffer[2] = (byte)(3);
            frame.buffer[3] = (byte)(0);
            frame.buffer[4] = (byte)(0);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }

        private void TenStepsProbeLeftBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(205);
            frame.buffer[2] = (byte)(3);
            frame.buffer[3] = (byte)(2);
            frame.buffer[4] = (byte)(0);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }

        private void OneStepsProbeLeftBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(205);
            frame.buffer[2] = (byte)(3);
            frame.buffer[3] = (byte)(1);
            frame.buffer[4] = (byte)(0);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }

        private void ProbeStopBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(205);
            frame.buffer[2] = (byte)(0);
            frame.buffer[3] = (byte)(0);
            frame.buffer[4] = (byte)(0);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }

        private void OneStepsProbeRightBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(205);
            frame.buffer[2] = (byte)(3);
            frame.buffer[3] = (byte)(1);
            frame.buffer[4] = (byte)(1);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }

        private void TenStepsProbeRightBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(205);
            frame.buffer[2] = (byte)(3);
            frame.buffer[3] = (byte)(2);
            frame.buffer[4] = (byte)(1);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }

        private void FullProbeRightBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(205);
            frame.buffer[2] = (byte)(3);
            frame.buffer[3] = (byte)(0);
            frame.buffer[4] = (byte)(1);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }

        private void MixerOnBtn_Click(object sender, EventArgs e)
        {

        }

        private void ProbeUp10stepsBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(205);
            frame.buffer[2] = (byte)(2);
            frame.buffer[3] = (byte)(2);
            frame.buffer[4] = (byte)(0);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }

        private void ProbeUp1stepBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(205);
            frame.buffer[2] = (byte)(2);
            frame.buffer[3] = (byte)(1);
            frame.buffer[4] = (byte)(0);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }

        private void ProbeClosingStopBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(205);
            frame.buffer[2] = (byte)(0);
            frame.buffer[3] = (byte)(2);
            frame.buffer[4] = (byte)(0);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }

        private void tableLayoutPanel10_Paint(object sender, PaintEventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(205);
            frame.buffer[2] = (byte)(2);
            frame.buffer[3] = (byte)(1);
            frame.buffer[4] = (byte)(1);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }

        private void ProbeDown10stepsBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(205);
            frame.buffer[2] = (byte)(2);
            frame.buffer[3] = (byte)(2);
            frame.buffer[4] = (byte)(1);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }

        private void InfProbeLeftBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(205);
            frame.buffer[2] = (byte)(3);
            frame.buffer[3] = (byte)(4);
            frame.buffer[4] = (byte)(0);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }

        private void InfProbeRightBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(205);
            frame.buffer[2] = (byte)(3);
            frame.buffer[3] = (byte)(4);
            frame.buffer[4] = (byte)(1);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }

        private void ProbeDownFullBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(205);
            frame.buffer[2] = (byte)(2);
            frame.buffer[3] = (byte)(4);
            frame.buffer[4] = (byte)(1);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }

        private void ProbeUpFullBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(205);
            frame.buffer[2] = (byte)(2);
            frame.buffer[3] = (byte)(4);
            frame.buffer[4] = (byte)(0);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }

        private void ProbeDown1stepBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
