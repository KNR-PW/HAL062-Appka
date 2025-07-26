using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace HAL062app.moduly.komunikacja
{
    public class KomunikacjaPresenter
    {
        private KomunikacjaForm display;
        private KomunikacjaModel model;
        private Dictionary<string, Form> modules;

        public KomunikacjaPresenter(Dictionary<string, Form> moduleForms, KomunikacjaModel model)
        {
            modules = moduleForms;
            this.model = model;

            if (modules.TryGetValue("Komunikacja", out Form form))
            {
                display = form as KomunikacjaForm;

                if (display != null)
                {

                    display.SendTerminalMsg += ReceiveTerminalMsg;
                    model.UpdateLogTerminal += UpdateTerminal;

                    CommunicationStatistics.Instance.StatsUpdated_action += CommunicationStatisticsUpdate;
                    //Telnet/SSH
                    display.ConnectTelnet_action += ConnectTelnet;
                    display.EthernetStatus_action += EthernetStatus;
                    display.disconnectTelnet_action += DisconnectTelnet;
                    model.isEthernetConnected_action += EthernetConnected;
                    //Bluetooth
                    model.SendBluetoothdetectedDevices_action += UpdateBluetoothDevicesComboBox;
                    display.RefreshBluetoothDevices_action += RequestBluetoothDevices;
                    display.ConnectBluetooth_action += ConnectBluetooth;
                    model.IsBluetoothConnected_action += BluetoothConnected;
                    display.DisconnectBluetooth_action += DisconnectBluetooth;
                    display.BluetoothStatusRequest += BluetoothStatus;
                }
            }
        }
        private void CommunicationStatisticsUpdate(int SentMessages_Count, int ReceivedMessages_Count, int BufforFillLevel_Count)
        {
            display.UpdateStatistics(SentMessages_Count,ReceivedMessages_Count, BufforFillLevel_Count);

        }

        private void ReceiveTerminalMsg(Message msg)
        {


            model.SendPrivateMessage(msg);

        }
        private void UpdateTerminal(List<Message> logs)
        {
            display.UpdateTerminal(logs);

        }
       
        //Telnet/SSH
        private void ConnectTelnet(string ip, int port)
        {
            Task task = model.ConnectTelnet(ip, port);

        }
        private void EthernetStatus(bool status)
        {
            display.EthernetStatus(status);
            if (status == false && model.isTelnetConnected())
            {
                model.TelnetDisconnect();
            }
        }
        private void DisconnectTelnet()
        {
            model.TelnetDisconnect();

        }
        private void EthernetConnected(bool status)
        {
            display.EthernetConnected(status);
        }

        //Bluetooth
        private void UpdateBluetoothDevicesComboBox(List<string> devices)
        {
            display.RefreshBluetoothDevices(devices);
        }
        private void RequestBluetoothDevices()
        {
            Task task = model.RefreshBluetoothDevices();
        }
        private void ConnectBluetooth(string deviceName)
        {
            model.ConnectBluetooth(deviceName);

        }
        private void BluetoothConnected(bool connected)
        {
            display.BluetoothConnected(connected);
        }
        private void DisconnectBluetooth()
        {
            model.DisconnectBluetooth();
        }
        private void BluetoothStatus(bool bluetoothSwitch)
        {
            if (bluetoothSwitch && model.isBluetoothOn())
            {
                display.BluetoothStatus(model.isBluetoothOn());

            }
            else
            {
                if (model.isBluetoothOn())
                {

                    model.DisconnectBluetooth();
                }
                else
                    model.SendTerminalMessage("Bluetooth nie został włączony");
                display.BluetoothStatus(false);

            }


        }


    }


}
