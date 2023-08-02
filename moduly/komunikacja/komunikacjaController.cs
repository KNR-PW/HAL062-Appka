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

                    //Uart
                    model.SendUARTdetectedPorts_action += UpdateUARTComboBox;
                    display.RefreshUartPorts_action += RequestUARTports;
                    display.ConnectUart_action += ConnectUart;
                    display.DisconnectUart_action += DisconnectUart;

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
        //UART
        private void UpdateUARTComboBox(string[] ports)
        {
            display.UpdatePorts(ports);

        }
        private void RequestUARTports()
        {
            model.RefreshPortsUART();
        }
        private void ConnectUart(string portName, int baudRate)
        {
            model.ConnectUART(portName, baudRate);
        }
        private void DisconnectUart() {
            model.DisconnectUART();
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
