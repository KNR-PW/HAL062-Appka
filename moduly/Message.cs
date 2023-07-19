using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;


namespace HAL062app.moduly
{


    public class Message
    {
        public string text { get; set; }
        public int receiver { get; set; }
        public int author { get; set; }

        public DateTime time { get;  }

        public Message() { 
        time = TimeProvider.GetCurrentTime();
        }
    }
}
