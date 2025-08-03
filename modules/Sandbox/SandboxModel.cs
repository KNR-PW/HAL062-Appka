using HAL062app.moduly.komunikacja;
using System;
using System.Collections.Concurrent;

namespace HAL062app.moduly.sandbox
{
    public class SandboxModel : IMessageObserver
    {
        komunikacja.CommunicationModel komunikacjaModel;
        private ConcurrentQueue<Message> receivedQueue;
        public event Action<String> updateTextBox_action;

        public SandboxModel(CommunicationModel komunikacja)
        {
            receivedQueue = new ConcurrentQueue<Message>();
            this.komunikacjaModel = komunikacja;
            this.komunikacjaModel.Subscribe(this);
        }
        public void ReceiveMessage(Message message)
        {
            // receivedQueue.Enqueue(message); // to powoduje, ze wiadomosc z komunikacji trafia do tej queue
            updateTextBox_action(message.text);
        }
        public void SendMessageToRobot(Message message)
        {
            message.author = 400;
            komunikacjaModel.SendMessageToRobot(message);

        }

    }
}
