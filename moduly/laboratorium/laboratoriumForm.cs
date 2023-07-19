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
        public event Action DequeueAction;
        public event Action<Message> MessageAction;

        public laboratoriumForm()
        {
            InitializeComponent();
        }

        private void customButton1_Click(object sender, EventArgs e)
        {
            DequeueAction?.Invoke();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public void Updateee(string  text)
        {
            textBox1.Text = text;
        }

        private void customButton2_Click(object sender, EventArgs e)
        {
            Message msg = new Message();
            msg.text = textBox2.Text;
            MessageAction?.Invoke(msg);
        }
    }
}
