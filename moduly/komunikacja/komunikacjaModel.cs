using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Windows.Forms;

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

     
        
    }
}
