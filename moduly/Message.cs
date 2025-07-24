using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;

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

        public Message() { 
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


        public char[] encodeMessage()
        {
            char[] data  = new char[19];
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
                    }                }
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
