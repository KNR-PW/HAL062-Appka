using System.Collections.Generic;
using System.Windows.Forms;

namespace HAL062app.moduly.laboratorium
{
    public class LaboratoryPresenter
    {
        private ILaboratoryModel _model;
        private ILaboratoryView _view;
  
        private Dictionary<string, Form> modules;

        public LaboratoryPresenter(Dictionary<string, Form> moduleForms, ILaboratoryModel model)
        {
            modules = moduleForms;
            this._model = model;

            if (modules.TryGetValue("Laboratorium", out Form form))
            {
                _view = form as LaboratoryForm;

                if (_view != null)
                {

                    _view.SendFrame_Action += SendMessageToRobot;
                    _view.GetTubeDescription_Func += GetTubeInfo;
                    //_view.GetTubeInfo_Action += GetTubeInfo;
                    _view.RotateRevolver_Action += RotateRevolver;
                    _view.GetTubeDescription_Func += GetTubeInfo;
                  

                }
            }
        }

        private void SendMessageToRobot(Message msg)
        {
            _model.SendMessageToRobot(msg);

        }

        private string GetTubeInfo(int pos)
        {
            //return "aaaaa";
            return _model.GetTubeInfo(pos);
        }
        private void RotateRevolver(bool cw)
        {
            _model.RotateRevolver(cw);
        }

    }
}
