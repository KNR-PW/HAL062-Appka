using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace HAL062app.moduly.komunikacja
{
    public partial class komunikacjaForm : Form
    {
        //Action

        public event Action<Message> SendTerminalMsg;


        // Uart
        public event Action RefreshUartPorts_action;
        public event Action<string,int> ConnectUart_action;

        public komunikacjaForm()
        {
            InitializeComponent();
            UartBaudRateCombo.Items.AddRange(baudRates.Select(x => x.ToString()).ToArray());
            UartBaudRateCombo.SelectedIndex = 9;
        }

        private void komunikacjaForm_Load(object sender, EventArgs e)
        {

        }

        public void UpdateTerminal(List<Message> logs)
        {
            TerminalBox.Items.Clear(); // przy wiekszej ilosci wiadomosci bedzie sie zawieszac, trzeba dodac opcje filtrowania po adresie i wtedy clear uzywac
            foreach (var message in logs)
            {

                TerminalBox.Invoke(new MethodInvoker(() => TerminalBox.Items.Add(message.time.ToString("HH:mm:ss") + ": " + message.author + "->" + message.receiver + ": " + message.text)));
            }
            TerminalBox.TopIndex = TerminalBox.Items.Count - 1;



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
                e.Handled = true;
                e.SuppressKeyPress = true;
                SendTerminalMsg?.Invoke(msg);
            }
        }



        //////////////////////////////////////////////////
        ///
        ///                 UART
        ///
        //////////////////////////////////////////////////
        private int[] baudRates = { 110, 300, 1200, 2400, 4800, 9600, 19200, 38400, 57600, 115200 };
        public void UpdatePorts(string[] ports)
        {
            UartPortCombo.Items.AddRange(ports.Select(x => x.ToString()).ToArray());

        }

        private void UartRefreshBtn_click(object sender, EventArgs e)
        {
            RefreshUartPorts_action();
        }

        private void ConnectUartBtn_Click(object sender, EventArgs e)
        {
            ConnectUart_action(UartPortCombo.SelectedItem.ToString(), baudRates[UartBaudRateCombo.SelectedIndex]);
        }
    }
}
