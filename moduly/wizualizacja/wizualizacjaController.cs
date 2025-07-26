using System.Collections.Generic;
using System.Windows.Forms;

namespace HAL062app.moduly.wizualizacja
{
    public class wizualizacjaController
    {
        private wizualizacjaForm display;
        private wizualizacjaModel model;
        private Dictionary<string, Form> modules;

        public wizualizacjaController(Dictionary<string, Form> moduleForms, wizualizacjaModel model)
        {
            modules = moduleForms;
            this.model = model;

            if (modules.TryGetValue("Podwozie", out Form form))
            {
                display = form as wizualizacjaForm;

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

                    // display.SendMotorDataToController_Action += SendMotorDataToModel_Action;
                    //  display.SendMessage_Action += SendMessageToKomunikacja;

                }
            }

        }
    }
}
