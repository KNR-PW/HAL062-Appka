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
        public event Action Dequeue;


        public laboratoriumForm()
        {
            InitializeComponent();
        }

        private void customButton1_Click(object sender, EventArgs e)
        {
            Dequeue?.Invoke();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public void Updateee(string  text)
        {
            textBox1.Text = text;
        }
    }
}
