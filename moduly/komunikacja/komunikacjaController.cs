using InTheHand.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace HAL062app.moduly.komunikacja
{
    public class komunikacjaController
    {
        private komunikacjaForm display;
        private komunikacjaModel model;
        private Dictionary<string, Form> modules;

        public komunikacjaController(Dictionary<string, Form> moduleForms, komunikacjaModel model)
        {
            modules = moduleForms;
            this.model = model;
           
            if (modules.TryGetValue("Komunikacja", out Form form))
            {
                display = form as komunikacjaForm;

                if (display != null)
                {

                    display.SendTerminalMsg += ReceiveTerminalMsg;
                    model.UpdateLogTerminal += UpdateTerminal;

                    
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

        private void ReceiveTerminalMsg(Message msg)
        {
            
           
            model.SendPrivateMessage(msg);

        }
        private void UpdateTerminal(List<Message> logs) {
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
            if (status == false&&model.isTelnetConnected())
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
