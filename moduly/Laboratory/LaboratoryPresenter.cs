using System.Collections.Generic;
using System.Windows.Forms;

namespace HAL062app.moduly.laboratorium
{
    public class LaboratoryPresenter
    {
        private LaboratoryForm display;
        private LaboratoryModel model;
        private Dictionary<string, Form> modules;

        public LaboratoryPresenter(Dictionary<string, Form> moduleForms, LaboratoryModel model)
        {
            modules = moduleForms;
            this.model = model;

            if (modules.TryGetValue("Laboratorium", out Form form))
            {
                display = form as LaboratoryForm;

                if (display != null)
                {

                    display.SendFrame_Action += SendMessageToRobot;

                }
            }
        }

        private void SendMessageToRobot(Message msg)
        {
            model.SendMessageToRobot(msg);

        }

        private string GetDescription(int pos)
        {
            return "a";
        }


    }
}
