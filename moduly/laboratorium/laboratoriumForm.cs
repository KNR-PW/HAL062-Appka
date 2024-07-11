using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HAL062app.moduly.laboratorium
{
    public partial class laboratoriumForm : Form
    {
        public event Action<Message> SendFrame_Action;

        public laboratoriumForm()
        {
            InitializeComponent();
        }

        private void ModulUpBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();    
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(194);
            frame.buffer[2] = (byte)(3);
            frame.buffer[3] = (byte)(0);
            frame.buffer[4] = (byte)(0);
            frame.buffer[5] = (byte)(50);
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
            frame.buffer[1] = (byte)(194);
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

        private void ModulDownBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(194);
            frame.buffer[2] = (byte)(3);
            frame.buffer[3] = (byte)(0);
            frame.buffer[4] = (byte)(0);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(50);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }
        private void WiertloUpBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(194);
            frame.buffer[2] = (byte)(3);
            frame.buffer[3] = (byte)(0);
            frame.buffer[4] = (byte)(0);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(50);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }
        private void WiertloStopBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(194);
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
        private void WiertloDownBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(194);
            frame.buffer[2] = (byte)(3);
            frame.buffer[3] = (byte)(0);
            frame.buffer[4] = (byte)(0);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(50);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }

        private void SilnikOnBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(194);
            frame.buffer[2] = (byte)(3);
            frame.buffer[3] = (byte)(50);
            frame.buffer[4] = (byte)(0);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }

        private void SilnikStopBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(194);
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

        private void SilnikReverseBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(194);
            frame.buffer[2] = (byte)(3);
            frame.buffer[3] = (byte)(0);
            frame.buffer[4] = (byte)(50);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }
    }
}
