using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
