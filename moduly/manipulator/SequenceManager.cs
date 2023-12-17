using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HAL062app.moduly.manipulator
{
    public class SequenceManager
    {
        public List<Sequence> Sequences { get; set; }

        public SequenceManager() {
            Sequences = new List<Sequence>();
        }
        public void SaveToFile(string path)
        {
            string json = JsonConvert.SerializeObject(Sequences);
            File.WriteAllText(path, json);

        }
        public void LoadFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                Sequences = JsonConvert.DeserializeObject<List<Sequence>>(json);
            }
        }

    }
}
