using HAL062app.moduly.komunikacja;
using HAL062app.moduly.laboratorium;
using HAL062app.moduly.manipulator;
using HAL062app.moduly.podwozie;
using HAL062app.moduly.sandbox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace HAL062app
{
    public partial class mainForm : Form
    {
        private TimerManager _timerManager;
        private Dictionary<string, Form> modules = new Dictionary<string, Form>();
        private String currentModule;
        public mainForm()
        {
            InitializeComponent();
            _timerManager = new TimerManager(5, 50);
            _timerManager.Start();
            _timerManager.TimerIntervalService += OnTimerStartRequested;
            EventAggregator.Instance.TimerStartRequested += OnTimerStartRequested;
            EventAggregator.Instance.TimerStartRequested += OnTimerStopRequested;


            modules.Add("Komunikacja", new moduly.komunikacja.komunikacjaForm(_timerManager));
            modules.Add("Laboratorium", new moduly.laboratorium.laboratoriumForm());
            modules.Add("Podwozie", new moduly.podwozie.podwozieForm(_timerManager));
            modules.Add("Manipulator", new moduly.manipulator.manipulatorForm());
            modules.Add("Sandbox", new moduly.sandbox.sandboxForm());


            moduly.komunikacja.komunikacjaModel komunikacjaM = new moduly.komunikacja.komunikacjaModel();
            moduly.laboratorium.laboratoriumModel laboratoriumM = new moduly.laboratorium.laboratoriumModel(komunikacjaM);
            moduly.podwozie.podwozieModel podwozieM = new moduly.podwozie.podwozieModel(komunikacjaM);
            moduly.manipulator.manipulatorModel manipulatorM = new moduly.manipulator.manipulatorModel(komunikacjaM);
            moduly.sandbox.sandboxModel sandboxM = new moduly.sandbox.sandboxModel(komunikacjaM);


            moduly.komunikacja.komunikacjaController komunikacjaC = new moduly.komunikacja.komunikacjaController(modules, komunikacjaM);
            moduly.laboratorium.laboratoriumController laboratoriumC = new moduly.laboratorium.laboratoriumController(modules, laboratoriumM);
            moduly.podwozie.podwozieController podwozieC = new moduly.podwozie.podwozieController(modules, podwozieM);
            moduly.manipulator.manipulatorController manipulatorC = new moduly.manipulator.manipulatorController(modules, manipulatorM);
            moduly.sandbox.sandboxController sandboxC = new moduly.sandbox.sandboxController(modules, sandboxM);
            
            komunikacjaM.Subscribe(laboratoriumM);
            komunikacjaM.Subscribe(podwozieM);
            komunikacjaM.Subscribe(manipulatorM);
            komunikacjaM.Subscribe(sandboxM);

        }
        private void mainForm_Load(object sender, EventArgs e)
        {
            ShowModule("Podwozie");
            ShowModule("Manipulator");
            ShowModule("Sandbox");
            ShowModule("Laboratorium");
            ShowModule("Komunikacja");

        }

        private void ShowModule(string moduleName)
        {
            if(modules.ContainsKey(moduleName))
            {
                if(currentModule!= null)
                {
                    modules[currentModule].Hide();
                }
                ContextPanel.Controls.Clear();
                currentModule = moduleName;
                modules[moduleName].FormBorderStyle = FormBorderStyle.None;
                modules[moduleName].Dock = DockStyle.Fill;
                modules[moduleName].TopLevel = false;
                ContextPanel.Controls.Add(modules[moduleName]);
                modules[moduleName].Show();
            }

        }
        private void OnTimerStartRequested(object sender, EventArgs e)
        {
            //_timerManager = new TimerManager(5, 15);
            _timerManager.Start();
            
        }
        private void OnTimerStopRequested(object sender, EventArgs e)
        {
           
                _timerManager.Stop();
            
        }
        private void MainPageButton_Click(object sender, EventArgs e)
        {

        }

        private void CommunicationButton_Click(object sender, EventArgs e)
        {
            ShowModule("Komunikacja");
        }

        private void ChassisButton_Click(object sender, EventArgs e)
        {
            ShowModule("Podwozie");
        }

        private void ManipulatorButton_Click(object sender, EventArgs e)
        {
            ShowModule("Manipulator");
        }

        private void LaboButton_Click(object sender, EventArgs e)
        {
            ShowModule("Laboratorium");
        }

        private void customButton6_Click(object sender, EventArgs e)
        {
            ShowModule("Sandbox");
        }

    }
}
