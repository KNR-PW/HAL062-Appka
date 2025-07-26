using HAL062app.moduly.komunikacja;
using System.Collections.Concurrent;

namespace HAL062app.moduly.sandbox
{
    public class SandboxModel : IMessageObserver
    {
        komunikacja.KomunikacjaModel komunikacjaModel;
        private ConcurrentQueue<Message> receivedQueue;

        public SandboxModel(KomunikacjaModel komunikacja)
        {
            receivedQueue = new ConcurrentQueue<Message>();
            this.komunikacjaModel = komunikacja;
        }
        public void ReceiveMessage(Message message)
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
