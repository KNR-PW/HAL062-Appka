using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace HAL062app.moduly.komunikacja
{
    public class CommunicationPresenter
    {
       
        private Dictionary<string, Form> modules;


        private readonly ICommunicationModel _model;
        private readonly ICommunicationView _view;
        public CommunicationPresenter(Dictionary<string, Form> moduleForms, ICommunicationModel model)
        {
            modules = moduleForms;
            this._model = model;
            if (modules.TryGetValue("Komunikacja", out Form form))
            {
                _view = form as ICommunicationView;
               
                if (_view != null)
                {

                    _view.SendTerminalMsg += ReceiveTerminalMsg;
                    _model.UpdateLogTerminal += UpdateTerminal;

                    CommunicationStatistics.Instance.StatsUpdated_action += CommunicationStatisticsUpdate;
                   
                    _view.ConnectTelnet_action += ConnectTelnet;
                    _view.EthernetStatus_action += EthernetStatus;
                    _view.disconnectTelnet_action += DisconnectTelnet;
                    
                    _view.RefreshBluetoothDevices_action += RequestBluetoothDevices;
                    _view.ConnectBluetooth_action += ConnectBluetooth;
                    
                    _view.DisconnectBluetooth_action += DisconnectBluetooth;
                    _view.BluetoothStatusRequest += BluetoothStatus;

                    _model.isEthernetConnected_action += EthernetConnected;
                    _model.OnBluetoothDevicesFound += UpdateBluetoothDevicesComboBox;
                    _model.IsBluetoothConnected_action += BluetoothConnected;
                    _view.WatchdogEnable_action += EnableWatchdog;
                    _view.ClearQueue_action += ClearQueue;
                }
            }
        }
        private void CommunicationStatisticsUpdate(int SentMessages_Count, int ReceivedMessages_Count, int BufforFillLevel_Count)
        {
            _view.UpdateStatistics(SentMessages_Count,ReceivedMessages_Count, BufforFillLevel_Count);

        }

        private void ReceiveTerminalMsg(Message msg)
        {


            _model.SendMessageToObservers(msg);

        }
        private void UpdateTerminal(List<Message> logs)
        {
            _view.UpdateTerminal(logs);

        }
       
        //Telnet/SSH
        private void ConnectTelnet(string ip, int port)
        {
            Task task = _model.ConnectTelnet(ip, port);

        }
        private void EthernetStatus(bool status)
        {
            _view.EthernetStatus(status);
            if (status == false && _model.IsTelnetConnected())
            {
                _model.TelnetDisconnect();
            }
        }
        private void DisconnectTelnet()
        {
            _model.TelnetDisconnect();

        }
        private void EthernetConnected(bool status)
        {
            _view.EthernetConnected(status);
        }

        //Bluetooth
        private void UpdateBluetoothDevicesComboBox(List<string> devices)
        {
            _view.RefreshBluetoothDevices(devices);
        }
        private void RequestBluetoothDevices()
        {
            Task task = _model.RefreshBluetoothDevices();
        }
        private void ConnectBluetooth(string deviceName)
        {
            _model.ConnectBluetooth(deviceName);

        }
        private void BluetoothConnected(bool connected)
        {
            _view.BluetoothConnected(connected);
        }
        private void DisconnectBluetooth()
        {
            _model.DisconnectBluetooth();
        }
        private void BluetoothStatus(bool bluetoothSwitch)
        {
            if (bluetoothSwitch && _model.IsBluetoothOn())
            {
                _view.BluetoothStatus(_model.IsBluetoothOn());

            }
            else
            {
                if (_model.IsBluetoothOn())
                {

                    _model.DisconnectBluetooth();
                }
                else
                    _model.SendTerminalMessage("Bluetooth nie został włączony");
                _view.BluetoothStatus(false);

            }


        }
        private void EnableWatchdog(bool enable, int interval)
        {
                _model.ChangeWatchdogState(enable, interval);
        }
        private void ClearQueue()
        {
            _model.ClearQueue();
        }
    }


}
