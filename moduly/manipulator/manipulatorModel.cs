using HAL062app.moduly.komunikacja;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL062app.moduly.manipulator
{
    public class manipulatorModel : MainChannelObserver
    {
        komunikacja.komunikacjaModel komunikacjaModel;
        private ConcurrentQueue<Message> receivedQueue;

        public manipulatorModel(komunikacjaModel komunikacja)
        {
            receivedQueue = new ConcurrentQueue<Message>();
            this.komunikacjaModel = komunikacja;
        }
        public void MainChannel(Message message)
        {
            receivedQueue.Enqueue(message);

        }

        public void SendMessageToKomunikacja(Message message)
        {
            message.author = 4;
            komunikacjaModel.SendMMessageToHALService(message);

        }

    }
}
