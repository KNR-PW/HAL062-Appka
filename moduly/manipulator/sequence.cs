using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Tutaj trzymać będziemy wszystkie sekwencje ruchów manipulatora 
 * 
 * Każda pozycja posiada 6 kątów (do każdego DOFa) oraz liczbę int, gdzie jednostką jest 100ms
 * Kąty opisane są za pomocą stopni 
 * Docelowo każda sekwencja zapisana będzie w pliku txt, który pozwoli na tworzenie nowych kombinacji i zostawianie ich na przyszłość
 * 
 */



namespace HAL062app.moduly.manipulator
{
    public class Sequence
    {
        public string name { get; set; }
        

       public List<Position> sequence {get; set; }
        [JsonConstructor]
        public Sequence(string name, List<Position> sequence)
        {
            this.name = name;
            this.sequence = sequence;
        }
        
        public override string ToString()
        {
            return name;
        }
        public void addPositionAtIndex(Position position, int index)
        {
            position.id = sequence.Count;
            sequence.Add(position);
            //refreshIndexing();
        }
        private void refreshIndexing()
        {
            for(int i = 0; i < sequence.Count; i++)
            {
                this.sequence[i].id = i;
            }
        }
        public void addPosition(Position position)
        {
            position.id = sequence.Count;

            sequence.Add(position);

           
        }
        public void removePosition(int sequencePosition)
        {
            if (sequencePosition >= 0 && sequencePosition < sequence.Count)
            {
                sequence.RemoveAt(sequencePosition);
                refreshIndexing();
            }
        }

       public int length()
        {
            return sequence.Count;
        }
        public Sequence deepCopy()
        {
            Sequence copy = new Sequence("copy", new List<Position>());
            copy.sequence = this.sequence;
            copy.name = this.name;
            return copy;
        }
       
    }
}
