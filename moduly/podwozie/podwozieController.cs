using HAL062app.moduly.laboratorium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HAL062app.moduly.podwozie
{
    public class podwozieController
    {
        private podwozieForm display;
        private podwozieModel model;
        private Dictionary<string, Form> modules;

        public podwozieController(Dictionary<string, Form> moduleForms, podwozieModel model)
        {
            modules = moduleForms;
            this.model = model;

            if (modules.TryGetValue("Podwozie", out Form form))
            {
                display = form as podwozieForm;

                if (display != null)
                {


                  //  model.wywolaj += UpdateTextBox1;
                //    display.DequeueAction += dequeue;
                 //   display.MessageAction += sendMessageToKomunikacja;

                }
            }

        }

        private void sendMessageToKomunikacja(Message msg)
        {
            model.SendMessageToKomunikacja(msg);

        }
        
    }
}
