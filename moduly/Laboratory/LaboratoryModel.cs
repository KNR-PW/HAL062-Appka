using HAL062app.moduly.komunikacja;
using System.Collections.Concurrent;

namespace HAL062app.moduly.laboratorium
{

    public class Tube
    {

        public int tubeID { get; }
        private int position; //pozycja na rewolwerze zgodnie z ruchem wskazówek zegara, 0 - na dole
        public string[] modifications { get; set; }
        public Tube(int _tubeID, int _position)
        {
            this.tubeID = _tubeID;
            this.position = _position;
        }

    };


    public class Rewolwer
    {
        private int numberOfTubes = 0;
        private Tube[] tubes;
        private int[] tubesPosition;
        public Rewolwer(int _numberOfTubes)
        {
            this.numberOfTubes = _numberOfTubes;
            this.tubesPosition = new int[numberOfTubes];
            for (int i = 0; i < numberOfTubes; i++)
            {
                //     tubes[i] = new Tube(i, i);
                //   tubesPosition[i] = i;
            }

        }


        public void RotateTube(bool clockwise = true)
        {
            for (int i = 0; i < numberOfTubes; i++)
            {
                tubesPosition[i] = (tubesPosition[i] + (clockwise == true ? 1 : -1)) % numberOfTubes;
            }
        }

        public string GetTubeInfo(int pos)
        {
            Tube t = tubes[tubesPosition[pos]];

            string description = $"ID: {t.tubeID},\n" +
                $"Zawartość: {t.modifications}";


            return description;
        }
    };


    public class LaboratoryModel : IMessageObserver
    {



        komunikacja.CommunicationModel communicationModel;
        private ConcurrentQueue<Message> receivedQueue;
        Rewolwer rewolwer;
        public LaboratoryModel(CommunicationModel komunikacja)
        {
            receivedQueue = new ConcurrentQueue<Message>();
            this.communicationModel = komunikacja;
            rewolwer = new Rewolwer(9);

        }

        public void ReceiveMessage(Message message)
        {
            receivedQueue.Enqueue(message);

        }

        public void SendMessageToRobot(Message message)
        {
            message.author = 3;
            communicationModel.SendMessageToRobot(message);
           // ReceiveMessage(message);

        }
        public void sendFrame(Message frame)
        {
            //SendMessageToRobot(frame);
        }







    }
}
