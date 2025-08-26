using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HAL062app.moduly.komunikacja
{

    interface ICommunicationView
    {
        void UpdateTerminal(List<Message> logs);
        void UpdateStatistics(int SentMessages_Count, int ReceivedMessages_Count, int BufforFillLevel_Count);
        void EthernetStatus(bool status);
        void EthernetConnected(bool status);
        void RefreshBluetoothDevices(List<string> devices);
        void BluetoothConnected(bool connected);
        void BluetoothStatus(bool status);

        event Action<Message> SendTerminalMsg;
        event Action<string, int> ConnectTelnet_action;
        event Action disconnectTelnet_action;
        event Action<bool> EthernetStatus_action;
        event Action RefreshBluetoothDevices_action;
        event Action<string> ConnectBluetooth_action;
        event Action DisconnectBluetooth_action;
        event Action<bool> BluetoothStatusRequest;
        event Action<bool, int> WatchdogEnable_action;
        event Action ClearQueue_action; 

    }


    public partial class CommunicationForm : Form, ICommunicationView
    {

        

        //Action
        public event Action<Message> SendTerminalMsg;
        
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
    
        public event Action<bool, int> WatchdogEnable_action;
        public event Action ClearQueue_action;
        public CommunicationForm()
        {
            InitializeComponent();
           
        }

        private void komunikacjaForm_Load(object sender, EventArgs e)
        {
            ConnectBluetoothBtn.ButtonStyle = CustomControls.CustomButton.ButtonStyles.Off;
            BluetoothRefreshBtn.ButtonStyle = CustomControls.CustomButton.ButtonStyles.Off;
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
            TerminalBox.TopIndex = TerminalBox.Items.Count - 1;


           
        }


        public void UpdateStatistics(int SentMessages_Count, int ReceivedMessages_Count, int BufforFillLevel_Count)
        {
            SentMsg_label.Text = "Wysłano: " + SentMessages_Count + " wiadomości";
            ReceivedMsg_label.Text = "Odebrano: " + ReceivedMessages_Count + " wiadomości";
            Buffor_Label.Text = "Zapełnienie buffora: " + BufforFillLevel_Count;
        }

        private void TerminalBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.TerminalBox.SelectedItem != null)
                Clipboard.SetDataObject(this.TerminalBox.SelectedItem.ToString());

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
        ///                 Telnet/SSH 
        ///
        //////////////////////////////////////////////////
        private void ReceivingToggleBtnEthernet_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void EthernetConnectBtn_Click(object sender, EventArgs e)
        {
         
        }
        private void EthernetSwitch_CheckedChanged(object sender, EventArgs e)
        {
            EthernetStatus_action(!EthernetStatusBoolean);
        }

        public void EthernetStatus(bool status)
        {
            EthernetStatusBoolean = status;
            EthernetSwitch.Checked = status;
            if (!status)
                EthernetConnectBtn.ButtonStyle = CustomControls.CustomButton.ButtonStyles.Off;
            else
                EthernetConnectBtn.ButtonStyle = CustomControls.CustomButton.ButtonStyles.Green;
        }

        public void EthernetConnected(bool status)
        {
            if (status)
            {
                EthernetConnectBtn.ButtonStyle = CustomControls.CustomButton.ButtonStyles.Red;
                EthernetConnectBtn.Text = "Rozłącz";

            }
            else
            {
                EthernetConnectBtn.ButtonStyle = CustomControls.CustomButton.ButtonStyles.Green;
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
                    ConnectBluetoothBtn.ButtonStyle = CustomControls.CustomButton.ButtonStyles.Green;
                    ConnectBluetoothBtn.Text = "Połącz";
                    DisconnectBluetooth_action();

                }
            }
        }
        public void BluetoothConnected(bool connected)
        {
            if (connected)
            {
                ConnectBluetoothBtn.ButtonStyle = CustomControls.CustomButton.ButtonStyles.Red;
                ConnectBluetoothBtn.Text = "Rozłącz";
            }
            else
            {
                ConnectBluetoothBtn.ButtonStyle = CustomControls.CustomButton.ButtonStyles.Green;
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
            if (!status)
            {
                BluetoothRefreshBtn.ButtonStyle = CustomControls.CustomButton.ButtonStyles.Off;
                ConnectBluetoothBtn.ButtonStyle = CustomControls.CustomButton.ButtonStyles.Off;
                BluetoothRefreshBtn.Text = "Odśwież";
                BluetoothDevicesComboBox.Items.Clear();
                BluetoothDevicesComboBox.SelectedIndex = -1;
                BluetoothDevicesComboBox.Texts = "";

            }
            else
            {
          
                BluetoothRefreshBtn.ButtonStyle = CustomControls.CustomButton.ButtonStyles.Red;
                ConnectBluetoothBtn.ButtonStyle = CustomControls.CustomButton.ButtonStyles.Green;
            }

        }

        private void komunikacjaForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

      

     
       

        private void watchdogBtn_Click(object sender, EventArgs e)
        {
            if(watchdogBtn.Text == "Włącz cykliczne ramki")
            {
                WatchdogEnable_action(true, (int)watchdogNumeric.Value);
                watchdogBtn.ButtonStyle = CustomControls.CustomButton.ButtonStyles.Red;
                watchdogBtn.Text = "Wyłącz cykliczne ramki";
            }
            else
            {
                WatchdogEnable_action(false, (int)watchdogNumeric.Value);
                watchdogBtn.ButtonStyle = CustomControls.CustomButton.ButtonStyles.Green;
                watchdogBtn.Text = "Włącz cykliczne ramki";
            }
        }

        private void clearQueueBtn_Click(object sender, EventArgs e)
        {
            ClearQueue_action();
        }

        private void SendBtn_Click(object sender, MouseEventArgs e)
        {
            Message msg = new Message();
            msg.author = 1;
            msg.text = sendTextBox.Text;
            SendTerminalMsg?.Invoke(msg);
        }

        private void EthernetConnectBtn_Click(object sender, MouseEventArgs e)
        {
            if (EthernetStatusBoolean)
            {
                if (EthernetConnectBtn.Text == "Połącz")
                {
                    if (IPtextbox.TextLength > 0)
                    {

                        EthernetConnectBtn.ButtonStyle = CustomControls.CustomButton.ButtonStyles.Red;
                        EthernetConnectBtn.Text = "Rozłącz";
                        ConnectTelnet_action(IPtextbox.Text, (int)EthernetPort.Value);

                    }
                    else
                        ConnectTelnet_action("", 0);

                }
                else
                {
                    EthernetConnectBtn.ButtonStyle = CustomControls.CustomButton.ButtonStyles.Green;
                    EthernetConnectBtn.Text = "Połącz";
                    disconnectTelnet_action();

                }
            }
        }
    }

}
