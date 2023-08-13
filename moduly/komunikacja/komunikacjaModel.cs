using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Net;
using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System.Reflection;


/*  Tutaj znajdziemy cały model komunikacji pomiędzy modułami laboratorium, manipulator itp. a łazikiem. 
 *  Każdy moduł posiadać będzie umiejętność wysyłania i odbierania wiadomości tylko do/z modułu komunikacji.
 *  Wiadomością będzie ramka, która posiada ID.
 *  ID #00 oznacza, że ramka ma zostać wysłana do łazika
 *  ID #01~99 przypisana jest dla każdego modułu, czyli jeśli łazik wyśle wiadomość typową dla laboratorium, to wiadomość
 *  dostanie ID #04 i wiadomość zostanie wysłana na ogólny kanał, który odbiera każdy moduł, ale wszystkie moduły odrzucą nie swoje ID
 *  
 *  Jak wygląda samo magazynowanie wiadomości
 *  Każdy moduł posiada kolejkę, do której wrzucane są wiadomości i po kolei czytane.
 *  Wyjątkiem jest moduł komunikacji, gdzie kolejki są dwie. Jedna, to ta odbierająca wiadomości od aplikacji
 *  Druga, to ta odbierająca info od łazika. 
 
 * Większość kodu została napisana przez błogosławione narzędzie programistyczne - ChatGPT
 * 
 * 
 * Na początku mamy całe zarządzanie wiadomościami, mainChannel itd
 * 
 * Potem 
 * uart
 * ethernet - protokół telnet
 * bluetooth - korzystamy z biblioteki nuget 32feet
 */
namespace HAL062app.moduly.komunikacja
{




    public class komunikacjaModel
    {
        private List<MainChannelObserver> observers = new List<MainChannelObserver>();    //lista obserwujących modułów
        private List<Message> logMessages = new List<Message>();


        private ConcurrentQueue<Message> messageQueue;
        private CancellationTokenSource tokenSource;
        private Task receivedTask;
       
        public event Action<List<Message>> UpdateLogTerminal;
        //UART
        public event Action<string[]> SendUARTdetectedPorts_action;
        //Telnet
        public event Action<bool> isEthernetConnected_action;

        //Bluetooth
        public event Action<List<string>> SendBluetoothdetectedDevices_action;
        public event Action<bool> IsBluetoothConnected_action;
        public delegate void MessageReceivedEventHandler(string message);
      
        private Task listeningBluetoothTask;


        //uart
        private SerialPort UART;
        private string[] UartPorts = SerialPort.GetPortNames();
        private bool UartOn = false;

        //telnet
        private TcpClient tcpClient = new TcpClient();
        private byte[] buffer = new byte[2048];
        private bool ethernetOn = false;
        private StreamReader tcpReader;
        private StreamWriter tcpWriter;
        private NetworkStream telnetStream;
        private CancellationTokenSource TelnetCancellationTokenSource;

        //bluetooth
        private BluetoothClient bluetoothClient;
        private BluetoothDeviceInfo[] bluetoothDeviceInfos = new BluetoothDeviceInfo[0];
        private bool BluetoothOn = false;



        public komunikacjaModel()
        {

            messageQueue = new ConcurrentQueue<Message>();
          
            if (isBluetoothOn())
            { bluetoothClient = new BluetoothClient(); }

        }

        public void Subscribe(MainChannelObserver observer)
        {
            observers.Add(observer);
        }

        public void SendTerminalMessage(string text)
        {
            Message msg = new Message();
            msg.text = text;
            msg.author = 420;
            msg.receiver = 69;
            logMessages.Add(msg);
            UpdateLogTerminal(logMessages);
        }
        public void SendPrivateMessage(Message message) //Funkcja odpowiedzialna za wyslanie pierwszej wiadomosci w kolejce na kanal glowny
        {
            
            PushMessageMainChannel(message);
            SendMMessageToHALService(message);
            //dorobic zwracanie informacji, jesli kolejka pusta
        }
        public void SendMMessageToHALService(Message message)
        {

            messageQueue.Enqueue(message);
            logMessages.Add(message);
            UpdateLogTerminal(logMessages);
           
            if (BluetoothOn)
                Task.Run(async () => await SendBluetoothMessage(message));
            if (isTelnetConnected())
                Task.Run(async () => await SendTelnetMessage(message));
        }
        private void PushMessageMainChannel(Message message) //Ta funkcja powiadamia wszystkie moduly i wysyla wiadomosc
        {
            foreach (var observer in observers)
            {
                observer.MainChannel(message);
            }
        }


        

     
       



        //////////////////////////////////////////////////
        ///
        ///                 UART
        ///
        //////////////////////////////////////////////////



        public void ConnectUART(string portName, int baudRate)
        {
            Message msg = new Message();
            msg.author = 420;
            msg.receiver = 69;

            if (portName != "-1")
            {
                try
                {
                    UART = new SerialPort(portName, baudRate);
                    UART.Open();
                    msg.text = "UART - Połączono z portem " + portName.ToString();
                    logMessages.Add(msg);
                    UpdateLogTerminal(logMessages);

                }
                catch (Exception ex)
                {

                    Message error = new Message();
                    msg.text = "UART - Błąd połączenia: " + ex.Message;

                    logMessages.Add(msg);
                    UpdateLogTerminal(logMessages);

                }
            }
            else
            {
                msg.text = "Brak wybranego portu";
                logMessages.Add(msg);
                UpdateLogTerminal(logMessages);
            }
        }
        public void DisconnectUART()
        {
            Message msg = new Message();
            msg.author = 420;
            msg.receiver = 69;

            try
            {

                UART.Close();
                msg.text = "UART - Rozłączono z portem";
                logMessages.Add(msg);
                UpdateLogTerminal(logMessages);

            }
            catch (Exception ex)
            {

                Message error = new Message();
                msg.text = "UART - Błąd podczas rozłączania: " + ex.Message;

                logMessages.Add(msg);
                UpdateLogTerminal(logMessages);

            }

        }
        public void RefreshPortsUART()
        {
            UartPorts = SerialPort.GetPortNames();
            if (UartPorts == null || UartPorts.Length == 0)
            {
                Message msg = new Message();
                msg.author = 420;
                msg.receiver = 69;
                msg.text = "Brak dostępnych portów COM";
                logMessages.Add(msg);
                UpdateLogTerminal(logMessages);
            }
            SendUARTdetectedPorts_action(UartPorts);
        }



        //////////////////////////////////////////////////
        ///
        ///                 Telnet
        ///
        //////////////////////////////////////////////////


        public async Task ConnectTelnet(string ipAddress, int port)
        {
            
            try
            {
                tcpClient = new TcpClient();
                await tcpClient.ConnectAsync(ipAddress, port);
                telnetStream = tcpClient.GetStream();
                tcpReader = new StreamReader(telnetStream, Encoding.ASCII);
                tcpWriter = new StreamWriter(telnetStream, Encoding.ASCII);
                ethernetOn = true;
                SendTerminalMessage("Połączono przez Telnet.");
                StartListeningTelnet();
            
            }
            catch (Exception ex)
            {
                isEthernetConnected_action(false);
                SendTerminalMessage("Błąd połączenia: " + ex.Message);
                // Dodaj obsługę błędów połączenia
            }
            
        }

        public async Task SendTelnetMessage(Message message)
        {
            try
            {
                await tcpWriter.WriteAsync(message.text + "\r\n");
                await tcpWriter.FlushAsync();
            }
            catch (Exception ex)
            {
                SendTerminalMessage("TCP: Błąd podczas wysyłania wiadomości: " + ex.Message);

            }

        }
        private async Task ReceiveTelnetMessage(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    string receivedMessage = await tcpReader.ReadLineAsync();

                    if (!string.IsNullOrEmpty(receivedMessage))
                    {
                        Message msg = new Message();
                        msg.text = receivedMessage;
                        msg.author = 0;
                        messageQueue.Enqueue(msg);
                        logMessages.Add(msg);
                        UpdateLogTerminal(logMessages);
                    }
                }
            }
            catch (Exception ex)
            {
                SendTerminalMessage("Błąd podczas odbierania wiadomości: " + ex.Message);
            }
        }

        private async void StartListeningTelnet()
        {
            TelnetCancellationTokenSource = new CancellationTokenSource();
            try
            {
                await ReceiveTelnetMessage(TelnetCancellationTokenSource.Token);
            }catch (TaskCanceledException)
            {
                SendTerminalMessage("Błąd podczas uruchamiania funkcji ReceiveTelnetMessage: ");
            }

        }

        public void TelnetDisconnect()
        {
            try
            {
               // TelnetCancellationTokenSource?.Cancel();
                tcpReader?.Dispose();
                tcpWriter?.Dispose();
                tcpClient?.Close();
                telnetStream.Close();
                isEthernetConnected_action(false);

                SendTerminalMessage("Rozłączono");
            } catch (Exception ex)
            {
                SendTerminalMessage("Błąd podczas rozłączania: "+ ex.Message);

            }
        }
        public bool isTelnetConnected()
        {
            return tcpClient.Connected;

        }
        //////////////////////////////////////////////////
        ///
        ///                 Bluetooth
        ///
        //////////////////////////////////////////////////



        public bool isBluetoothOn()
        {
            
            return BluetoothRadio.IsSupported;

        }

        public async Task RefreshBluetoothDevices()
        {
            if (BluetoothRadio.IsSupported)
            {
                List<string> bluetoothDeviceNames = new List<string>();
                await Task.Run(() =>
                {
                    
                    using (var bluetoothClient = new BluetoothClient())
                    {

                        bluetoothDeviceInfos = bluetoothClient.DiscoverDevices();
                    }
                    foreach (var deviceInfo in bluetoothDeviceInfos)
                        bluetoothDeviceNames.Add(deviceInfo.DeviceName);

                    if (bluetoothDeviceInfos == null || bluetoothDeviceInfos.Length == 0)
                    {
                        SendTerminalMessage("Brak dostępnych urządzeń Bluetooth");
                    }
                    else
                    {
                        SendTerminalMessage("Wykryto nowe urządzenia Bluetooth");
                        SendBluetoothdetectedDevices_action(bluetoothDeviceNames);
                    }
                });
                
                
            }
            else
            { 
                SendTerminalMessage("Urządzenie nie posiada włączonego Bluetooth");
            }

        }
        public  void ConnectBluetooth(string deviceName)
        {
            if (isBluetoothOn())
            {
                BluetoothDeviceInfo selectedDevice = bluetoothDeviceInfos.FirstOrDefault(b => b.DeviceName == deviceName);
                if (deviceName == "-1_err")
                {
                    SendTerminalMessage("Brak wybranego urządzenia");

                }
                else if (selectedDevice != null)
                {
               
                  
                       SendTerminalMessage("Łączenie...");
                        
                        try
                        {
                            
                            var bluetoothEndPoint = new BluetoothEndPoint(selectedDevice.DeviceAddress, BluetoothService.SerialPort);
                            bluetoothClient = new BluetoothClient();
                            bluetoothClient.Connect(bluetoothEndPoint);
                            SendTerminalMessage("Połączono z " + selectedDevice.DeviceName);
                            IsBluetoothConnected_action(true);
                            startListeningBluetooth();

                        }
                        catch (Exception ex)
                        {
                            SendTerminalMessage("Błąd podczas próby połączenia: " + ex.ToString());
                            IsBluetoothConnected_action(false);
                        }
                       
                    
                }
            }
            else
            {
                SendTerminalMessage("Urządzenie nie posiada włączonego Bluetooth");
            }


        }

        public void DisconnectBluetooth()
        {
            if (bluetoothClient!=null)
            {
                bluetoothClient.Close();
                IsBluetoothConnected_action(false);
                SendTerminalMessage("Rozłączono");
            }
        }
        private async Task ReceiveBluetoothMessage(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    int bytesRead = await bluetoothClient.GetStream().ReadAsync(buffer, 0, buffer.Length);

                    if (bytesRead > 0)
                    {
                        Message msg = new Message();
                        msg.text = System.Text.Encoding.ASCII.GetString(buffer, 0, bytesRead);
                        msg.author = 0;
                        messageQueue.Enqueue(msg);
                        logMessages.Add(msg);
                        UpdateLogTerminal(logMessages);
                    }

                }

            }
            catch (Exception ex)
            {
                SendTerminalMessage("Błąd podczas odbierania wiadomości: " + ex.Message);
            }

        }
        private async Task SendBluetoothMessage(Message msg)
        {
            try
            {
                buffer = System.Text.Encoding.ASCII.GetBytes(msg.text);
                await bluetoothClient.GetStream().WriteAsync(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                SendTerminalMessage("BT: Błąd podczas wysyłania wiadomości: " + ex.Message);
            }
        }

        private async void startListeningBluetooth()
        {
          CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            try
            {
                listeningBluetoothTask = ReceiveBluetoothMessage(cancellationTokenSource.Token);
                await listeningBluetoothTask;
            }
            catch (TaskCanceledException)
            {
                SendTerminalMessage("Błąd podczas uruchamiania zadania 'ReceiveBluetoothMessage'");
            }
            
            
            
            /*
            listeningBluetoothThread = new Thread(() =>
            {
                Task.Run(async () => await ReceiveBluetoothMessage());
            });
            listeningBluetoothThread.Start();
        */}
       



    }
}
