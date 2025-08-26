using System;
using System.Collections.Generic;
using System.Text;
using static HAL062app.moduly.laboratorium.LaboratoryForm;

namespace HAL062app.moduly
{


    public class Message
    {
        public string text { get; set; }
        public int receiver { get; set; }
        public int author { get; set; }
        public int ID { get; set; }
        public byte[] buffer { get; set; } = new byte[10];

        public DateTime time { get; set; }

        public Message()
        {
            time = TimeProvider.GetCurrentTime();
        }

        public void terminalMessage(int author, int receiver, string text)
        {
            this.author = author;
            this.receiver = receiver;
            this.text = text;
            time = TimeProvider.GetCurrentTime();

        }
        public void decodeMessage(string msg)
        {
            time = TimeProvider.GetCurrentTime();
            text = msg;
        }

        public void CreateFrame(ID id, byte param1 = 0, byte param2 = 0, byte param3 = 0, byte param4 = 0,
                        byte param5 = 0, byte param6 = 0, byte param7 = 0, byte param8 = 0, byte param9 = 0)
        {
           
            this.buffer[0] = (byte)('#');
            this.buffer[1] = (byte)id;
            this.buffer[2] = param1;
            this.buffer[3] = param2;
            this.buffer[4] = param3;
            this.buffer[5] = param4;
            this.buffer[6] = param5;
            this.buffer[7] = param6;
            this.buffer[8] = param7;
            this.buffer[9] = param9; // jeśli 9 ma znaczenie, inaczej dopasuj
            this.text = new string(this.encodeMessage());
            
        }

        public char[] encodeMessage()
        {
            char[] data = new char[19];
            int k = 0;
            for (int i = 0; i < buffer.Length; i++)
            {
                if (buffer[i] == '#')
                {
                    data[k] = (char)buffer[i];
                    k++;
                }
                else
                {
                    string hex = String.Format("{0:x2}", buffer[i]);
                    byte[] bytes = Encoding.ASCII.GetBytes(hex);
                    if (hex == "78") 
                    {
                        data[k] = 'x';
                        k++;
                        data[k] = 'x';
                        k++;
                    }
                    else
                    {
                        data[k] = (char)bytes[0];
                        k++;
                        data[k] = (char)bytes[1];
                        k++;
                    }
                }
            }
            int x = 9 - buffer.Length;
            if (x > 0)
                for (int j = x; j > -1; j--)
                {
                    data[k] = 'x';
                    k++;
                    data[k] = 'x';
                    k++;
                }
            for (int i = 0; i < data.Length; i++)
                data[i] = char.ToUpper(data[i]);
            return data;
        }

        public byte[] CreateFrame(params string[] dataStrings)
        {
            byte[] ans = new byte[2];
            if (dataStrings.Length != 10)
            {
                throw new ArgumentException("Tablica danych musi zawierać dokładnie 10 elementów.");
            }

            List<byte> frame = new List<byte>();
            frame.Add((byte)dataStrings[0].ToCharArray()[0]);
            return ans;
        }
    }
}
