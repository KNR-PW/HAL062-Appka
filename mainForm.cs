using Hall062app;
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
        public mainForm()
        {
            InitializeComponent();
            timerManager.StartTimer();
            XboxControlBus.XboxControlMode += OnXboxControlModeChanged;
            OnXboxControlModeChanged(-1);

            modules.Add("Komunikacja", new moduly.komunikacja.CommunicationForm());
            modules.Add("Laboratorium", new moduly.laboratorium.LaboratoryForm());
            modules.Add("Podwozie", new moduly.podwozie.DriveForm());
            modules.Add("Manipulator", new moduly.manipulator.manipulatorForm());
            modules.Add("Debug", new moduly.sandbox.SandboxForm());
            

            foreach (var module in modules)
                isPinned.Add(module.Key, true);


            moduly.komunikacja.CommunicationModel communicationModule = new moduly.komunikacja.CommunicationModel();
            moduly.laboratorium.LaboratoryModel laboratoryModule = new moduly.laboratorium.LaboratoryModel(communicationModule);
            moduly.podwozie.PodwozieModel driveModel = new moduly.podwozie.PodwozieModel(communicationModule);
            moduly.manipulator.manipulatorModel manipulatorM = new moduly.manipulator.manipulatorModel(communicationModule);
            moduly.sandbox.SandboxModel sandboxM = new moduly.sandbox.SandboxModel(communicationModule);


            moduly.komunikacja.CommunicationPresenter komunikacjaC = new moduly.komunikacja.CommunicationPresenter(modules, communicationModule);
            moduly.laboratorium.LaboratoryPresenter laboratoriumC = new moduly.laboratorium.LaboratoryPresenter(modules, laboratoryModule);
            moduly.podwozie.DrivePresenter podwozieC = new moduly.podwozie.DrivePresenter(modules, driveModel);
            moduly.manipulator.manipulatorController manipulatorC = new moduly.manipulator.manipulatorController(modules, manipulatorM);
            moduly.sandbox.SandboxPresenter sandboxC = new moduly.sandbox.SandboxPresenter(modules, sandboxM);

            communicationModule.Subscribe(laboratoryModule);
            communicationModule.Subscribe(driveModel);
            communicationModule.Subscribe(manipulatorM);
           


        }
        private void mainForm_Load(object sender, EventArgs e)
        {
            ShowModule("Podwozie");
            ShowModule("Manipulator");
            ShowModule("Debug");
            ShowModule("Laboratorium");
            ShowModule("Komunikacja");

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


        private void CommunicationBtn_Click(object sender, EventArgs e)
        {
            ShowModule("Komunikacja");
            ResetBtnColor();
            CommunicationBtn.BackgroundColor = System.Drawing.Color.FromArgb(255, 0, 120, 215);
        
        }

        private void DriveBtn_Click(object sender, EventArgs e)
        {
            ShowModule("Podwozie");
            ResetBtnColor();
            DriveBtn.BackColor = System.Drawing.Color.FromArgb(255, 0, 120, 215);
        }

        private void ManipulatorBtn_Click(object sender, EventArgs e)
        {
            ShowModule("Manipulator");
            ResetBtnColor();
            ManipulatorBtn.BackgroundColor = System.Drawing.Color.FromArgb(255, 0, 120, 215);
        }

        private void LaboratoryBtn_Click(object sender, EventArgs e)
        {
            ShowModule("Laboratorium");
            ResetBtnColor();
            LaboratoryBtn.BackgroundColor = System.Drawing.Color.FromArgb(255, 0, 120, 215);
        }

        private void SandboxBtn_Click(object sender, EventArgs e)
        {
            ShowModule("Debug");
            ResetBtnColor();
            SandboxBtn.BackgroundColor = System.Drawing.Color.FromArgb(255, 0,120,215);
        }

        private void UnpinBtn_Click(object sender, EventArgs e)
        {
            isPinned[currentModule] = false;
            ShowModule(currentModule);
        }

        private void ResetBtnColor()
        {
            var color = System.Drawing.Color.FromArgb(255, 128,128,128);
            CommunicationBtn.BackColor = color;
            DriveBtn.BackColor = color;
            ManipulatorBtn.BackgroundColor = color;
            LaboratoryBtn.BackgroundColor = color;
            SandboxBtn.BackgroundColor = color;
            

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

       


        private void OnXboxControlModeChanged(int state)
        {
            switch (state)
            {
                case -1:
                    GamePadStatusLabel.Text = "Gamepad: \nwyłączony";
                    break;
                case 0:
                    GamePadStatusLabel.Text = "Gamepad: \n tryb jazdy";
                    break;
                case 1:
                    GamePadStatusLabel.Text = "Gamepad: \n tryb manipulatora";
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
