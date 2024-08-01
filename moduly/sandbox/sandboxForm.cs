using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HAL062app.moduly.sandbox
{
    public partial class sandboxForm : Form
    {

        public event Action<Message> sendFrame_action;
        public sandboxForm()
        {
            InitializeComponent();
        }
        
        private void SendFrameBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.text = frameTextBox.Text;
            frame.author = 6;
            frame.receiver = 0;
            sendFrame_action(frame);

        }

        private void CreateFrameBtn_Click(object sender, EventArgs e)
        {
            byte[] frameByte = new byte[10];
            frameByte[0] = (byte)PrefixTextBox.Text[0];
            frameByte[1]= (byte)numericID.Value;

            bool[] x = { dataCheckBox1.Checked, dataCheckBox2.Checked, dataCheckBox3.Checked, dataCheckBox4.Checked, dataCheckBox5.Checked, dataCheckBox6.Checked, dataCheckBox7.Checked, dataCheckBox8.Checked };
            int[] numeric = { (int)numericData1.Value, (int)numericData2.Value, (int)numericData3.Value, (int)numericData4.Value, (int)numericData5.Value, (int)numericData6.Value, (int)numericData7.Value, (int)numericData8.Value };
            for(int i = 2; i<10;  i++)
            {
                if (x[i-2])
                    frameByte[i] = (byte)'x';
                else
                    frameByte[i] = (byte)numeric[i-2];
                

            }
            Message frame = new Message();
            frame.buffer = frameByte;
            
            frameTextBox.Text = new string(frame.encodeMessage());
        }

        private void ClearFrameBtn_Click(object sender, EventArgs e)
        {
            numericID.Value = 0;
            numericData1.Value = 0;
            numericData2.Value = 0;
            numericData3.Value = 0;
            numericData4.Value = 0;
            numericData5.Value = 0;
            numericData6.Value = 0;
            numericData7.Value = 0;
            numericData8.Value = 0;

            dataCheckBox1.Checked = true;
            dataCheckBox2.Checked = true;
            dataCheckBox3.Checked = true;
            dataCheckBox4.Checked = true;
            dataCheckBox5.Checked = true;
            dataCheckBox6.Checked = true;
            dataCheckBox7.Checked = true;
            dataCheckBox8.Checked = true;
            frameTextBox.Text = "";

        }

        private void sandboxForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
