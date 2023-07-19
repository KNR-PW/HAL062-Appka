using HAL062app.moduly.podwozie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HAL062app.moduly.manipulator
{
    public class manipulatorController
    {
        private manipulatorForm display;
        private manipulatorModel model;
        private Dictionary<string, Form> modules;

        public manipulatorController(Dictionary<string, Form> moduleForms, manipulatorModel model)
        {
            modules = moduleForms;
            this.model = model;

            if (modules.TryGetValue("Manipulator", out Form form))
            {
                display = form as manipulatorForm;

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
