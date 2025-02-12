using HAL062app.moduly.komunikacja;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL062app.moduly.wizualizacja
{
    public class wizualizacjaModel : MainChannelObserver
    {
        komunikacja.komunikacjaModel komunikacjaModel;
        private ConcurrentQueue<Message> receivedQueue;
        private objectConfig _objectConfig;
        private engine _engine;

        public wizualizacjaModel(komunikacjaModel komunikacja)
        {
            receivedQueue = new ConcurrentQueue<Message>();
            this.komunikacjaModel = komunikacja;
            _objectConfig = new objectConfig();
            _engine = new engine(_objectConfig);
        }

        public void MainChannel(Message message)
        {
            receivedQueue.Enqueue(message); // to powoduje, ze wiadomosc z komunikacji trafia do tej queue

        }










    }

}
