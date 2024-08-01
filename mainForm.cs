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
/*
 * Zgaduję, że skoro tu jesteś to szukasz jakichkolwiek rzeczy związanych z tym, jak poruszać się po plikach 
 * Zamysłem tego projektu jest wykorzystanie architektury MVC -> Model-View-Controller
 * Każda zakładka, to oddzielna aplikacja, którą łączy ten plik.
 * Jak działa poruszanie danych pomiędzy plikami? Zostało to opisane w podwozieController
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



            modules.Add("Komunikacja", new moduly.komunikacja.komunikacjaForm());
            modules.Add("Laboratorium", new moduly.laboratorium.laboratoriumForm());
            modules.Add("Podwozie", new moduly.podwozie.podwozieForm());
            modules.Add("Manipulator", new moduly.manipulator.manipulatorForm());
            modules.Add("Debug", new moduly.sandbox.sandboxForm());

            foreach (var module in modules)
                isPinned.Add(module.Key, true);
            

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
                } else
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
                isPinned[module.Key]= true;
                modules[module.Key].Hide();
            }
            ShowModule(currentModule);

        }

        private void PinBtn_Click(object sender, EventArgs e)
        {
            isPinned[currentModule] = true;
            ShowModule(currentModule);
        }

        /// 
        /// <param name="sender"></param>
        /// <param name="e"></param>
    }
}
