using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HAL062app.moduly.manipulator
{
    public class SequenceManager
    {
        public List<Sequence> Sequences { get; set; }

        public SequenceManager()
        {
            Sequences = new List<Sequence>();
        }
        public void SaveToFile(string path)
        {

            var updatedSequences = new List<Sequence>();

            foreach (var sequence in Sequences)
            {

                var existingSequence = updatedSequences.FirstOrDefault(seq => seq.name == sequence.name);

                if (existingSequence != null)
                {

                    existingSequence.sequence = sequence.sequence;
                }
                else
                {
                    updatedSequences.Add(new Sequence(sequence.name, sequence.sequence));
                }
            }
            updatedSequences.Remove(updatedSequences.Find(seq => seq.name == "History"));
            // Podmieniamy oryginalną listę zaktualizowaną listą
            Sequences = updatedSequences;

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(Sequences, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(path, json);

        }
        public void LoadFromFile(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                Sequences = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Sequence>>(json);
            }
        }

    }
}
