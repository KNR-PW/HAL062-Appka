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
                    /*
                     * Tutaj dzieje sie cala magia związana z połączeniem modelu i widoku
                     * Jeżeli chcemy, żeby kliknięcie przycisku wywołało funkcję FunkcjaA w modelu, to:
                     * poniżej tworzymy funkcję private void WykonajFunkcjeA(TypZmiennej nazwaZmiennej){
                     * model.FunkcjaA(Jakies dane)
                     * }
                     * 
                     * W form tworzymy event
                     * public event Action<TypZmiennej> FunkcjaAAction
                     * 
                     * w funkcji odpowiedzialnej za wywolanie np. klikniecie przycisku wstawiamy funkcję
                     * która wywoła FunkcjaAAction
                     * FunkcjaAAction?.Invoke(dane)
                     * 
                     * W tej klamerce tutaj wstawiamy display.FunkcjaAAction += WykonajFunkcjeA
                     * 
                     * no a w modelu musimy tylko zrobic
                     * 
                     * public void FunkcjaA(TypZmiennej nazwaZmiennej)
                     * {
                     * cokolwiek, co ma ta funkcja robic
                     * 
                     * }
                     * 
                     * 
                     * W drugą stronę analogicznie, ale na odwrót 
                     */

                    display.sendMotorDataToController_Action += sendMotorDataToModel_Action;
                    display.sendMessage_Action += sendMessageToKomunikacja;

                }
            }

        }
        private void sendMotorDataToModel_Action (motorData data)
        {
            model.sendSpeed(data);
        }

        private void sendMessageToKomunikacja(Message msg)
        {
            model.SendMessageToKomunikacja(msg);

        }
        
    }
}
