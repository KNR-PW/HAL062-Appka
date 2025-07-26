using System.Collections.Generic;
using System.Windows.Forms;

namespace HAL062app.moduly.laboratorium
{
    public class LaboratoriumPresenter
    {
        private LaboratoriumForm display;
        private LaboratoriumModel model;
        private Dictionary<string, Form> modules;

        public LaboratoriumPresenter(Dictionary<string, Form> moduleForms, LaboratoriumModel model)
        {
            modules = moduleForms;
            this.model = model;

            if (modules.TryGetValue("Laboratorium", out Form form))
            {
                display = form as LaboratoriumForm;

                if (display != null)
                {

                    display.SendFrame_Action += SendMessageToKomunikacja;

                }
            }
        }

        private void SendMessageToKomunikacja(Message msg)
        {
            model.SendMessageToKomunikacja(msg);

        }

        private string GetDescription(int pos)
        {
            return "a";
        }


    }
}
