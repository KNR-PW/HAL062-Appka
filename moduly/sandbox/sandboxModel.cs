using HAL062app.moduly.komunikacja;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL062app.moduly.sandbox
{
    public class sandboxModel : MainChannelObserver
    {
        komunikacja.komunikacjaModel komunikacjaModel;
        private ConcurrentQueue<Message> receivedQueue;

        public sandboxModel(komunikacjaModel komunikacja)
        {
            receivedQueue = new ConcurrentQueue<Message>();
            this.komunikacjaModel = komunikacja;
        }
        public void MainChannel(Message message)
        {
            receivedQueue.Enqueue(message); // to powoduje, ze wiadomosc z komunikacji trafia do tej queue

        }

        public void SendMessageToKomunikacja(Message message)
        {
            message.author = 400;
            komunikacjaModel.SendMMessageToHALService(message);

        }

    }
}
