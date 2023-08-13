using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;
using System.Windows.Forms;
using HAL062app.moduly.komunikacja;
using System.Reflection;

namespace HAL062app.moduly.laboratorium
{
   
   
    public class laboratoriumModel : MainChannelObserver
    {
        public event Action<string> wywolaj;


        komunikacja.komunikacjaModel komunikacjaModel;
        private ConcurrentQueue<Message> receivedQueue;

        public laboratoriumModel(komunikacjaModel komunikacja)
        {
            receivedQueue = new ConcurrentQueue<Message>();
            this.komunikacjaModel = komunikacja;
        }

        public void MainChannel(Message message)
        {
            receivedQueue.Enqueue(message);
            
        }
        public void wyswietlKolejnaWiadomosc()
        {

            bool success = receivedQueue.TryDequeue(out Message message);
            if (success)
                wywolaj?.Invoke(message.text);
            else
                wywolaj?.Invoke("Brak wiadomosci w kolejce");
        }
        public void SendMessageToKomunikacja(Message message)
        {
            message.author = 2;
            komunikacjaModel.SendMMessageToHALService(message);

        }
     
       
    }
}
