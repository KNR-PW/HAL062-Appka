
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HAL062app.moduly.laboratorium
{
    public class laboratoriumController
    {
        private laboratoriumForm display;
        private laboratoriumModel model;
        private Dictionary<string, Form> modules;

        public laboratoriumController(Dictionary<string, Form> moduleForms, laboratoriumModel model)
        {
            modules = moduleForms;
            this.model = model;

            if (modules.TryGetValue("Laboratorium", out Form form))
            {
                display = form as laboratoriumForm;

                if (display != null)
                {

                    //    LaboratoriumF.Button1Click += SendMessageHandler;
                    model.wywolaj += UpdateTextBox1;
                    display.Dequeue += dequeue;
                }
            }
        }

        private void SendMessageHandler(String text)
        {
            

        }
        private void dequeue()
        {
            model.wyswietlKolejnaWiadomosc();
                
        }
        
        private void UpdateTextBox1(string msg)
        {
            display.Updateee(msg);
        }

    }
}
