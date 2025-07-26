using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static HAL062app.ControllerState;
/*
 * Zgaduję, że skoro tu jesteś to szukasz jakichkolwiek rzeczy związanych z tym, jak poruszać się po plikach 
 * Zamysłem tego projektu jest wykorzystanie architektury MVC -> Model-View-Controller
 * Każda zakładka, to oddzielna aplikacja, którą łączy ten plik.
 * Jak działa poruszanie danych pomiędzy plikami? Zostało to opisane w PodwozieController
 * Ogólnie w tym pliku polecam nic nie zmieniać, bo popsucie czegokolwiek tu rozwali całą aplikację
 * W tym pliku powinny się znaleźć tylko inicjacje programu i aplikacji.
 * 
 * Starałem się umieszczać instrukcję i wyjaśnienia, jak co działa podczas pisania programu, ale..
 * ale program był pisany ponad 2 lata, ja się uczyłem C# i może być tutaj dużo dziwnych trików
 * 
 * Sama historia programu, to pomysł na przejściówkę na melu. Potem był to plan na inżynierkę, ostatecznie na dyplomie robiłem autonomicznego drona z grantu KNTI 2024
 * 
 * Autor: Dominik Chmielak
 */

namespace HAL062app
{
    public partial class mainForm : Form
    {
        private TimerManager timerManager = new TimerManager();
        private Dictionary<string, Form> modules = new Dictionary<string, Form>();
        private Dictionary<string, bool> isPinned = new Dictionary<string, bool>();

        private String currentModule = "Komunikacja";
        //   moduly.komunikacja.komunikacjaForm komun = new moduly.komunikacja.komunikacjaForm();
        public mainForm()
        {
            InitializeComponent();
            timerManager.StartTimer();
            XboxControlBus.XboxControlMode += OnXboxControlModeChanged;
            OnXboxControlModeChanged(-1);

            modules.Add("Komunikacja", new moduly.komunikacja.KomunikacjaForm());
            modules.Add("Laboratorium", new moduly.laboratorium.LaboratoriumForm());
            modules.Add("Podwozie", new moduly.podwozie.PodwozieForm());
            modules.Add("Manipulator", new moduly.manipulator.manipulatorForm());
            modules.Add("Debug", new moduly.sandbox.SandboxForm());
            // modules.Add("Wizualizacja", new moduly.wizualizacja.wizualizacjaForm());

            foreach (var module in modules)
                isPinned.Add(module.Key, true);


            moduly.komunikacja.KomunikacjaModel komunikacjaM = new moduly.komunikacja.KomunikacjaModel();
            moduly.laboratorium.LaboratoriumModel laboratoriumM = new moduly.laboratorium.LaboratoriumModel(komunikacjaM);
            moduly.podwozie.PodwozieModel podwozieM = new moduly.podwozie.PodwozieModel(komunikacjaM);
            moduly.manipulator.manipulatorModel manipulatorM = new moduly.manipulator.manipulatorModel(komunikacjaM);
            moduly.sandbox.SandboxModel sandboxM = new moduly.sandbox.SandboxModel(komunikacjaM);
            //moduly.wizualizacja.wizualizacjaModel wizualizacjaM = new moduly.wizualizacja.wizualizacjaModel(komunikacjaM);


            moduly.komunikacja.KomunikacjaPresenter komunikacjaC = new moduly.komunikacja.KomunikacjaPresenter(modules, komunikacjaM);
            moduly.laboratorium.LaboratoriumPresenter laboratoriumC = new moduly.laboratorium.LaboratoriumPresenter(modules, laboratoriumM);
            moduly.podwozie.PodwoziePresenter podwozieC = new moduly.podwozie.PodwoziePresenter(modules, podwozieM);
            moduly.manipulator.manipulatorController manipulatorC = new moduly.manipulator.manipulatorController(modules, manipulatorM);
            moduly.sandbox.SandboxPresenter sandboxC = new moduly.sandbox.SandboxPresenter(modules, sandboxM);
            // moduly.wizualizacja.wizualizacjaController wizualizacjaC = new moduly.wizualizacja.wizualizacjaController(modules, wizualizacjaM);

            komunikacjaM.Subscribe(laboratoriumM);
            komunikacjaM.Subscribe(podwozieM);
            komunikacjaM.Subscribe(manipulatorM);
            komunikacjaM.Subscribe(sandboxM);
            //  komunikacjaM.Subscribe(wizualizacjaM);


        }
        private void mainForm_Load(object sender, EventArgs e)
        {
            ShowModule("Podwozie");
            ShowModule("Manipulator");
            ShowModule("Debug");
            ShowModule("Laboratorium");
            ShowModule("Komunikacja");
            //ShowModule("Wizualizacja");

        }

        /// <Sterowanie wyswietlaniem stron>
        /// 
        private void ShowModule(string moduleName)
        {
            if (modules.ContainsKey(moduleName))
            {
                if (currentModule != null)
                {
                    //  modules[currentModule].Hide();
                }
                if (isPinned[moduleName])
                {
                    ContextPanel.Controls.Clear();
                    currentModule = moduleName;
                    modules[moduleName].FormBorderStyle = FormBorderStyle.None;
                    modules[moduleName].Dock = DockStyle.Fill;
                    modules[moduleName].TopLevel = false;
                    ContextPanel.Controls.Add(modules[moduleName]);
                }
                else
                {
                    ContextPanel.Controls.Clear();
                    currentModule = moduleName;
                    modules[moduleName].FormBorderStyle = FormBorderStyle.Sizable;
                    modules[moduleName].TopLevel = true;
                    modules[moduleName].Dock = DockStyle.None;
                    modules[moduleName].Show();
                }
                modules[moduleName].Show();
            }

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
        private void wizualizacjaBtn_Click(object sender, EventArgs e)
        {
            //  ShowModule("Wizualizacja");
        }

        private void customButton6_Click(object sender, EventArgs e)
        {
            ShowModule("Debug");
        }

        private void UnpinBtn_Click(object sender, EventArgs e)
        {
            isPinned[currentModule] = false;
            ShowModule(currentModule);
        }

        private void ResetViewBtn_Click(object sender, EventArgs e)
        {

            foreach (var module in modules)
            {
                isPinned[module.Key] = true;
                modules[module.Key].Hide();
            }
            ShowModule(currentModule);

        }

        private void PinBtn_Click(object sender, EventArgs e)
        {
            isPinned[currentModule] = true;
            ShowModule(currentModule);
        }


        private void OnXboxControlModeChanged(int state)
        {
            switch (state)
            {
                case -1:
                    GamePadStatusLabel.Text = "Gamepad: off";
                    break;
                case 0:
                    GamePadStatusLabel.Text = "Gamepad: \n Drive";
                    break;
                case 1:
                    GamePadStatusLabel.Text = "Gamepad: \n Manipulator angle";
                    break;
                case 2:
                    GamePadStatusLabel.Text = "Gamepad: \n Manipulator 3DOF";
                    break;
                default:
                    GamePadStatusLabel.Text = "Gamepad: \nUnknown";
                    break;
            }

        }



        /// 
        /// <param name="sender"></param>
        /// <param name="e"></param>
    }
}
