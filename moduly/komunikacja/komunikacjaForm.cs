using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HAL062app.moduly.komunikacja
{
    public partial class komunikacjaForm : Form
    {

        public event Action<Message> SendTerminalMsg;
        public komunikacjaForm()
        {
            InitializeComponent();
            
        }

        private void komunikacjaForm_Load(object sender, EventArgs e)
        {

        }

        public void UpdateTerminal(List<Message> logs)
        {
            TerminalBox.Items.Clear(); // przy wiekszej ilosci wiadomosci bedzie sie zawieszac, trzeba dodac opcje filtrowania po adresie i wtedy clear uzywac
            foreach(var message in logs)
            {
                
                TerminalBox.Invoke(new MethodInvoker(() => TerminalBox.Items.Add(message.time.ToString("HH:mm:ss") +": "+ message.author + "->"+ message.receiver +": "+ message.text)));
            }

        }

        private void SendBtn_Click(object sender, EventArgs e)
        {
            Message msg = new Message();
            msg.author = 1;
            msg.text = sendTextBox.Text;
            SendTerminalMsg?.Invoke(msg);
        }

        private void sendTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Message msg = new Message();
                msg.author = 1;
                msg.text = sendTextBox.Text;
               
                SendTerminalMsg?.Invoke(msg);
            }
        }
    }
}
