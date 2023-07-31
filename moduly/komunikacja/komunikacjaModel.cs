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
 * bluetooth
 * ethernet - protokół telnet
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
        public event Action<Message> privateMessageReceivedAction; 
        public event Action<List<Message>> UpdateLogTerminal;
        //UART
        public event Action<string[]> SendUARTdetectedPorts_action;
        


        //uart
        private SerialPort UART;
        private string[] UartPorts = SerialPort.GetPortNames();

        //telnet
        private TcpClient tcpClient = new TcpClient();
        private NetworkStream stream;
        private byte[] buffer = new byte[2048];
        public komunikacjaModel( )
        {
         
            messageQueue = new ConcurrentQueue<Message>();
            tokenSource = new CancellationTokenSource();
            receivedTask = Task.Run(() => ReceiveMessages(tokenSource.Token));
            
           
        }

        public void Subscribe(MainChannelObserver observer)
        {
            observers.Add(observer);
        }

        
        public void SendPrivateMessage(Message message) //Funkcja odpowiedzialna za wyslanie pierwszej wiadomosci w kolejce na kanal glowny
        {
          
            PushMessageMainChannel(message);
            ReceivedMessageService(message);
            //dorobic zwracanie informacji, jesli kolejka pusta
        }
        public void ReceivedMessageService(Message message)
        {
            messageQueue.Enqueue(message);
            logMessages.Add(message);
            UpdateLogTerminal(logMessages);

        }
        private void PushMessageMainChannel(Message message) //Ta funkcja powiadamia wszystkie moduly i wysyla wiadomosc
        {
            foreach(var observer in observers)
            {
                observer.MainChannel(message);
            }
        }
        
        
        public async Task<Message> ReceiveMessage()
        {
            while(true)
            {
                tokenSource.Token.ThrowIfCancellationRequested();
                if(messageQueue.TryDequeue(out Message message))
                {
                    return message;
                }
                await Task.Delay(100);
            }

        }
       
        private void ReceiveMessages(CancellationToken cancellationToken)
        {
            Task.Run(async () => { while (!cancellationToken.IsCancellationRequested)
                {


                    await Task.Delay(100);
                    Message message = new Message()
                    {
                        text = "tutaj odbieranie po necie"
                    };
                    messageQueue.Enqueue(message);
                }
            }, cancellationToken);
        

        }
        public void StopReceiving()
        {
            tokenSource.Cancel();
        }



        //////////////////////////////////////////////////
        ///
        ///                 UART
        ///
        //////////////////////////////////////////////////
       


        public void ConnectUART (string portName, int baudRate)
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
            }else
            {
                msg.text = "Brak wybranego portu";
                logMessages.Add(msg);
                UpdateLogTerminal(logMessages);
            }
        }
        public void DisconnectUART ()
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
            Message msg = new Message();
            msg.author = 420;
            msg.receiver = 69;
            try
            {
                tcpClient = new TcpClient();
                await tcpClient.ConnectAsync(ipAddress, port);
                stream = tcpClient.GetStream();
                msg.text = "Połączono przez Telnet.";
            }
            catch (Exception ex)
            {
                msg.text = "Błąd połączenia: " + ex.Message;
                // Dodaj obsługę błędów połączenia
            }
            logMessages.Add(msg);
            UpdateLogTerminal(logMessages);
        }

        public async Task<string> ReceiveMessageTelnet()
        {
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            return message;
        }
    }
}
