using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace HAL062app.moduly.komunikacja
{
    public class komunikacjaController
    {
        private komunikacjaForm display;
        private komunikacjaModel model;
        private Dictionary<string, Form> modules;

        public komunikacjaController(Dictionary<string, Form> moduleForms, komunikacjaModel model)
        {
            modules = moduleForms;
            this.model = model;
           
            if (modules.TryGetValue("Komunikacja", out Form form))
            {
                display = form as komunikacjaForm;

                if (display != null)
                {

                    display.SendTerminalMsg += ReceiveTerminalMsg;
                    model.UpdateLogTerminal += UpdateTerminal;
                }
            }
        }

        private void ReceiveTerminalMsg(Message msg)
        {
            
           
            model.SendPrivateMessage(msg);

        }
        private void UpdateTerminal(List<Message> logs) {
            display.UpdateTerminal(logs);

        }
       
    }


}
