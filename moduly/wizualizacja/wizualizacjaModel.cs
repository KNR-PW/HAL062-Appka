using HAL062app.moduly.komunikacja;
using System.Collections.Concurrent;

namespace HAL062app.moduly.wizualizacja
{
    public class wizualizacjaModel : IMessageObserver
    {
        komunikacja.KomunikacjaModel komunikacjaModel;
        private ConcurrentQueue<Message> receivedQueue;
        private objectConfig _objectConfig;
        private engine _engine;

        public wizualizacjaModel(KomunikacjaModel komunikacja)
        {
            receivedQueue = new ConcurrentQueue<Message>();
            this.komunikacjaModel = komunikacja;
            _objectConfig = new objectConfig();
            _engine = new engine(_objectConfig);
        }

        public void ReceiveMessage(Message message)
        {
            receivedQueue.Enqueue(message); // to powoduje, ze wiadomosc z komunikacji trafia do tej queue

        }










    }

}
