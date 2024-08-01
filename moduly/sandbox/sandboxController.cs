using HAL062app.moduly.podwozie;
using HAL062app.moduly.sandbox;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace HAL062app.moduly.sandbox
{
    public class sandboxController
    {
        private sandboxForm display;
        private sandboxModel model;
        private Dictionary<string, Form> modules;
        public sandboxController(Dictionary<string, Form> moduleForms, sandboxModel model)
        {
            modules = moduleForms;
            this.model = model;

            if (modules.TryGetValue("Debug", out Form form))
            {
                display = form as sandboxForm;

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
                    display.sendFrame_action += sendMessageToKomunikacja;

                }
            }

        }

        private void sendMessageToKomunikacja(Message msg)
        {
            model.SendMessageToKomunikacja(msg);

        }


    }




}
