using HAL062app.moduly.komunikacja;
using System.Collections.Concurrent;
using System.Windows.Documents;
using System.Collections.Generic;

namespace HAL062app.moduly.laboratorium
{


    public interface ILaboratoryModel
    {
        void SendMessageToRobot(Message message);
        void sendFrame(Message frame);
        string GetTubeInfo(int pos);
        void RotateRevolver(bool clockwise = true);
    }


    public class Tube
    {

        public int tubeID { get; }
        private int globalPosition; //pozycja względem kamery, 0 - na dole, 1 - nad, 2 - nad itd.
        bool isEmpty = true;
        bool isLocked = false;
        public string description { get; set; }
        public List<Reagent> reagents { get; set; }
        public string[] modifications { get; set; }
        public Tube(int _tubeID)
        {
            this.tubeID = _tubeID;
            this.reagents = new List<Reagent>();
        }

        public void AddReagent(Reagent reagent)
        {       
           reagents.Add(reagent);
           isEmpty = false;
          // CreateDescription();
        }

        public void CreateDescription(int globalPositon)
        {
            description = $"ID: {tubeID},\n" +
                $"{(isLocked?"Zamknięte":"Otwarte")}\n" +
                $"Zawartość: Puste";
        }
    };

    public class Reagent
        {
        public string name { get; set; }
        public string description { get; set; }
        public Reagent(string _name, string _description, string[] _properties)
        {
            this.name = _name;
            this.description = _description;
        }
       
    };

  
    public class Rewolwer
    {
        private int numberOfTubes = 11;
        private Tube[] tubes;
        private int[] tubesPosition;
        public Rewolwer(int _numberOfTubes)
        {
            this.numberOfTubes = _numberOfTubes;
            this.tubesPosition = new int[numberOfTubes];
            this.tubes = new Tube[_numberOfTubes];
            for (int i = 0; i < numberOfTubes; i++)
            {
                     tubes[i] = new Tube(i);
                     tubesPosition[i] = i;
            }


        }


        public void RotateTube(bool clockwise = true)
        {
            for (int i = 0; i < numberOfTubes; i++)
            {
                tubesPosition[i] = (tubesPosition[i] + (clockwise == false ? 1 : -1) + numberOfTubes) % numberOfTubes;
            }
        }

        public string GetTubeInfo(int pos)
        {
            Tube t = tubes[tubesPosition[pos]];
            t.CreateDescription(pos);
            return t.description;
        }
    };


    public class LaboratoryModel : IMessageObserver, ILaboratoryModel
    {



        komunikacja.CommunicationModel communicationModel;
        private ConcurrentQueue<Message> receivedQueue;
        Rewolwer rewolwer;
        public LaboratoryModel(CommunicationModel komunikacja)
        {
            receivedQueue = new ConcurrentQueue<Message>();
            this.communicationModel = komunikacja;
            rewolwer = new Rewolwer(11);

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


        public string GetTubeInfo(int pos)
        {
            return rewolwer.GetTubeInfo(pos);
        }

        public void RotateRevolver(bool clockwise = true)
        {
            rewolwer.RotateTube(clockwise);
        }


    }
}
