using System.Collections.Generic;
using System.Windows.Forms;

namespace HAL062app.moduly.podwozie
{

    public class DrivePresenter
    {

        private readonly IPodwozieModel _model;
        private readonly IPodwozieView _view;

        private Dictionary<string, Form> _modules;

        public DrivePresenter(Dictionary<string, Form> moduleForms, IPodwozieModel model)
        {
            _modules = moduleForms;
            this._model = model;

            if (_modules.TryGetValue("Podwozie", out Form form))
            {
                _view = form as IPodwozieView;

                if (_view != null)
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

                    _view.SendMotorDataToController_Action += SendMotorDataToModel_Action;
                    _view.SendMessage_Action += SendMessageToRobot;
                }
            }

        }
      
        private void SendMotorDataToModel_Action(MotorData data)
        {
            _model.SendVelocityToRobot(data);
        }

        private void SendMessageToRobot(Message msg)
        {
            _model.SendMessageToRobot(msg);

        }

    }
}
