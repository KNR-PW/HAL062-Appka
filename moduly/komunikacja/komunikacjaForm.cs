﻿using InTheHand.Net.Sockets;
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
        public event Action DisconnectUart_action;
        //telnet
        public event Action<string, int> ConnectTelnet_action;
        public event Action disconnectTelnet_action;
        public event Action<bool> EthernetStatus_action;
        private bool EthernetStatusBoolean = false;

        //Bluetooth
        public event Action RefreshBluetoothDevices_action;
        public event Action<string> ConnectBluetooth_action;
        public event Action DisconnectBluetooth_action;
        public event Action<bool> BluetoothStatusRequest;
        private bool BluetoothStatusBoolean = false;

        public komunikacjaForm()
        {
            InitializeComponent();
            UartBaudRateCombo.Items.AddRange(baudRates.Select(x => x.ToString()).ToArray());
            UartBaudRateCombo.SelectedIndex = 9;
        }

        private void komunikacjaForm_Load(object sender, EventArgs e)
        {
            RefreshUartPorts_action();
            ConnectBluetoothBtn.BackColor = Color.FromArgb(192, 192, 192);
            BluetoothRefreshBtn.BackColor = Color.FromArgb(192, 192, 192);
            EthernetStatus(false);
        }

        public void UpdateTerminal(List<Message> logs)
        {
           // TerminalBox.Items.Clear(); // przy wiekszej ilosci wiadomosci bedzie sie zawieszac, trzeba dodac opcje filtrowania po adresie i wtedy clear uzywac
          //  foreach (var message in logs)
         //   {

          //      TerminalBox.Invoke(new MethodInvoker(() => TerminalBox.Items.Add(message.time.ToString("HH:mm:ss") + ": " + message.author + "->" + message.receiver + ": " + message.text)));
          //  }
           
            Message lastMsg = logs.Last();
            TerminalBox.Invoke(new MethodInvoker(() => TerminalBox.Items.Add(lastMsg.time.ToString("HH:mm:ss") + ": " + lastMsg.author + "->" + lastMsg.receiver + ": " + lastMsg.text)));
            TerminalBox.TopIndex = TerminalBox.Items.Count -1;
        }
        private void TerminalBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.TerminalBox.SelectedItem != null) 
            Clipboard.SetDataObject(this.TerminalBox.SelectedItem.ToString());

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
            UartPortCombo.Items.Clear();
            UartPortCombo.Items.AddRange(ports.Select(x => x.ToString()).ToArray());

        }

        private void UartRefreshBtn_click(object sender, EventArgs e)
        {
            RefreshUartPorts_action();
        }

        private void ConnectUartBtn_Click(object sender, EventArgs e)
        {
            if (ConnectUartBtn.Text == "Połącz")
            {
                if (UartPortCombo.SelectedItem != null)
                {
                    ConnectUartBtn.BackColor = Color.FromArgb(192,0, 0);
                    ConnectUart_action(UartPortCombo.SelectedItem.ToString(), baudRates[UartBaudRateCombo.SelectedIndex]);
                    ConnectUartBtn.Text = "Rozłącz";
                }
                else
                    ConnectUart_action("-1", baudRates[UartBaudRateCombo.SelectedIndex]);
            } else
            {
                ConnectUartBtn.BackColor = Color.FromArgb(0, 192, 0);
                ConnectUartBtn.Text = "Połącz";
                DisconnectUart_action();

            }
        }
        //////////////////////////////////////////////////
        ///
        ///                 Telnet/SSH 
        ///
        //////////////////////////////////////////////////
        private void ReceivingToggleBtnEthernet_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void EthernetConnectBtn_Click(object sender, EventArgs e)
        {
            if (EthernetStatusBoolean)
            {
                if (EthernetConnectBtn.Text == "Połącz")
                {
                    if (IPtextbox.TextLength > 0)
                    {

                        EthernetConnectBtn.BackColor = Color.FromArgb(192, 0, 0);
                        EthernetConnectBtn.Text = "Rozłącz";
                        ConnectTelnet_action(IPtextbox.Text, (int)EthernetPort.Value);

                    }
                    else
                        ConnectTelnet_action("", 0);

                }
                else
                {
                    EthernetConnectBtn.BackColor = Color.FromArgb(0, 192, 0);
                    EthernetConnectBtn.Text = "Połącz";
                    disconnectTelnet_action();

                }
            }
        }
        private void EthernetSwitch_CheckedChanged(object sender, EventArgs e)
        {
            EthernetStatus_action(!EthernetStatusBoolean);
        }

        public void EthernetStatus(bool status)
        {
            EthernetStatusBoolean = status;
            EthernetSwitch.Checked = status;
            if(!status) 
                EthernetConnectBtn.BackColor = Color.FromArgb(192, 192, 192);
            else
                EthernetConnectBtn.BackColor = Color.FromArgb(0, 192, 0);
        }

        public void EthernetConnected(bool status)
        {
            if(status)
            {
                EthernetConnectBtn.BackColor = Color.FromArgb(192, 0, 0);
                EthernetConnectBtn.Text = "Rozłącz";

            }
            else
            {
                EthernetConnectBtn.BackColor = Color.FromArgb(0, 192, 0);
                EthernetConnectBtn.Text = "Połącz";
            }


        }
        //////////////////////////////////////////////////
        ///
        ///                 Bluetooth
        ///
        //////////////////////////////////////////////////

        private void BluetoothRefreshBtn_Click(object sender, EventArgs e)
        {
            if (BluetoothStatusBoolean)
            {
                BluetoothRefreshBtn.Text = "Szukanie";
                RefreshBluetoothDevices_action();
            }
        }

        public void RefreshBluetoothDevices(List<string> devices)
        {
            if (BluetoothStatusBoolean)
            {
                BluetoothRefreshBtn.Text = "Odśwież";
                BluetoothDevicesComboBox.Items.Clear();


                foreach (string device in devices)
                {

                    BluetoothDevicesComboBox.Items.Add(device);
                }
            }

        }

        private void ConnectBluetoothBtn_Click(object sender, EventArgs e)
        {
            if (BluetoothStatusBoolean)
            {
                if (ConnectBluetoothBtn.Text == "Połącz")
                {
                    if (BluetoothDevicesComboBox.SelectedItem != null)
                    {

                        ConnectBluetooth_action(BluetoothDevicesComboBox.SelectedItem.ToString());

                    }
                    else
                        ConnectBluetooth_action("-1_err");
                }
                else
                {
                    ConnectBluetoothBtn.BackColor = Color.FromArgb(0, 192, 0);
                    ConnectBluetoothBtn.Text = "Połącz";
                    DisconnectBluetooth_action();

                }
            }
        }
        public void BluetoothConnected(bool connected)
        {
            if (connected)
            {
                ConnectBluetoothBtn.BackColor = Color.FromArgb(192, 0, 0);
                ConnectBluetoothBtn.Text = "Rozłącz";
            } else
            {
                ConnectBluetoothBtn.BackColor = Color.FromArgb(0, 192, 0);
                ConnectBluetoothBtn.Text = "Połącz";
            }

        }

        private void BluetoothSwitch_CheckedChanged(object sender, EventArgs e) // Tutaj zajmujemy się tym, żeby przełączenie włącznika bluetooth działało 
        {
            BluetoothStatusRequest(!BluetoothStatusBoolean);
        }
        public void BluetoothStatus(bool status)
        {
            BluetoothStatusBoolean = status;
            BluetoothSwitch.Checked = status;
            if(!status)
            {
                ConnectBluetoothBtn.BackColor = Color.FromArgb(192, 192, 192);
                BluetoothRefreshBtn.BackColor = Color.FromArgb(192, 192, 192);
                BluetoothRefreshBtn.Text = "Odśwież";
                BluetoothDevicesComboBox.Items.Clear();
                BluetoothDevicesComboBox.SelectedIndex = -1;
                BluetoothDevicesComboBox.Texts = "";

            }else
            {
                ConnectBluetoothBtn.BackColor = Color.FromArgb(0, 192, 0);
                BluetoothRefreshBtn.BackColor = Color.FromArgb(192, 0, 0);
            }

        }

        private void komunikacjaForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
    }
    
}
