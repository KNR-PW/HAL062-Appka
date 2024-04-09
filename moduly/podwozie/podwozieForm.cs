using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace HAL062app.moduly.podwozie
{
    public partial class podwozieForm : Form
    {
        private System.Drawing.Point joystickPosition = System.Drawing.Point.Empty;
        bool isDragging = false;
        private System.Drawing.Point lastMousePosition = System.Drawing.Point.Empty;
        private int joystickRadius = 30;
        private float forwardSpeed = 0;
        private float turningSpeed = 0;
        private float forwardSpeedMaxLimit = 9;
        private float turningSpeedMaxLimit = 3;
        int limitRadius = 160;
        Keys LastKeys = Keys.None;
        private bool isMoving = false;
        private Timer timer;
        private int readInterval = 50;
        private int elapsedTime = 0;

        public Action<motorData> sendMotorDataToController_Action;

        public podwozieForm()
        {
            InitializeComponent();
            InitializeTimer();
            this.KeyPreview = true;

            joystickPosition.X = joystickPictureBox.ClientSize.Width/2;
            joystickPosition.Y = joystickPictureBox.ClientSize.Height/2;
            ForwardSpeedTrack.Maximum = (int)forwardSpeedMaxLimit;
            ForwardSpeedTrack.Minimum = (int)-forwardSpeedMaxLimit;
            TurningSpeedTrack.Maximum = (int)turningSpeedMaxLimit;
            TurningSpeedTrack.Minimum = (int)-turningSpeedMaxLimit;
            joystickPictureBox.Refresh();



        }

        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 25;
            timer.Tick += Timer_Tick;
            timer.Start();

        }
        private async void Timer_Tick(object sender, EventArgs e)
        {
            elapsedTime += timer.Interval;

            if (elapsedTime >= readInterval)
            {
                // Wywołaj funkcję aktualizacji joysticka i odczytu prędkości
                UpdateJoystick();
                elapsedTime = 0; // Zresetuj licznik czasu
               // if(isMoving) MoveJoystick(LastKeys);

            }

        }


        private void joystickPictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(Brushes.Gray, joystickPictureBox.Width / 2 - limitRadius, joystickPictureBox.Height / 2 - limitRadius, 2*limitRadius, 2*limitRadius);
            e.Graphics.FillEllipse(Brushes.White, joystickPosition.X - joystickRadius, joystickPosition.Y-joystickRadius, 2*joystickRadius, 2*joystickRadius);
        }

        private void joystickPictureBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            joystickPictureBox.Focus();
            if (e.Button == MouseButtons.Left) { 
            isDragging = true;
            lastMousePosition = e.Location;
        
            }
        }

        private void joystickPictureBox_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            joystickPictureBox.Focus();
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
                joystickPosition.X = joystickPictureBox.ClientSize.Width / 2 ;
                joystickPosition.Y = joystickPictureBox.ClientSize.Height / 2 ;
                joystickPictureBox.Refresh();
                lastMousePosition = e.Location;
               
            }
        }

        private int pitagoras(int c, int b)
        {
            return (int)Math.Sqrt(Math.Abs(c * c - b * b));
        }
        async private void joystickPictureBox_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            joystickPictureBox.Focus();
            if (isDragging)
            {
                int deltaX = e.X - lastMousePosition.X;
                int deltaY = e.Y - lastMousePosition.Y;
                joystickPosition.X += deltaX;
                joystickPosition.Y += deltaY;
                int height = joystickPictureBox.ClientSize.Height;
                int width = joystickPictureBox.ClientSize.Width;
                
              

                if (joystickPosition.X - width / 2 >= 0)
                    joystickPosition.X = Math.Min(joystickPosition.X, width / 2 + pitagoras(limitRadius, joystickPosition.Y - height/2) - joystickRadius);
                else
                    joystickPosition.X = Math.Max(joystickPosition.X, width/2 - pitagoras(limitRadius,  (height / 2)- joystickPosition.Y) + joystickRadius);

                if (joystickPosition.Y - height / 2 >= 0)
                    joystickPosition.Y = Math.Min(joystickPosition.Y, height / 2  + pitagoras(limitRadius, joystickPosition.X- width / 2) - joystickRadius);
                else
                    joystickPosition.Y = Math.Max(joystickPosition.Y, height / 2  - pitagoras(limitRadius, (width / 2) - joystickPosition.X) + joystickRadius);

                joystickPictureBox.Refresh();
             
                lastMousePosition = e.Location;

            }
        }

        private async void UpdateJoystick()
        {
            ForwardSpeedTrack.Maximum = (int)forwardSpeedMaxLimit;
            ForwardSpeedTrack.Minimum = (int)-forwardSpeedMaxLimit;
            TurningSpeedTrack.Maximum = (int)turningSpeedMaxLimit;
            TurningSpeedTrack.Minimum = (int)-turningSpeedMaxLimit;

            forwardSpeed = -((float)joystickPosition.Y - (float)joystickPictureBox.ClientSize.Height/2.0f) / (float)(limitRadius-joystickRadius);
            turningSpeed = ((float)joystickPosition.X - (float)joystickPictureBox.ClientSize.Width/2.0f) / (float)(limitRadius-joystickRadius);
            ForwardSpeedTrack.Value = (int)forwardSpeed;
            TurningSpeedTrack.Value = (int)turningSpeed;
            LMStextbox.Text = forwardSpeed.ToString();
            RMStextbox.Text = turningSpeed.ToString();
            sendSpeeds(forwardSpeed,turningSpeed);

        }
        private void sendSpeeds(float fspeed, float tspeed)
        {
            float coeff = 100.0f / 6.45f;
          
            int rightSpeed = (int)(coeff * (6.45f * fspeed + 2.97f * tspeed));
            int leftSpeed = (int)(coeff * (6.45f * fspeed - 2.97f * tspeed));
            LFStextbox.Text = leftSpeed.ToString();
            LBStextbox.Text = leftSpeed.ToString();
            RFStextbox.Text = rightSpeed.ToString();
            RBStextbox.Text = rightSpeed.ToString();
            motorData data = new motorData(rightSpeed, leftSpeed, 0, 0, 0, 0);
            sendMotorDataToController_Action(data);

        }

       

     
        private void joystickPictureBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            /*
            int step = 5;
            switch (e.KeyCode)
            {
                case Keys.Up:
                    joystickPosition.Y -= step; // Przesuń joystick w górę
                    break;
                case Keys.Down:
                    joystickPosition.Y += step; // Przesuń joystick w dół
                    break;
                case Keys.Left:
                    joystickPosition.X -= step; // Przesuń joystick w lewo
                    break;
                case Keys.Right:
                    joystickPosition.X += step; // Przesuń joystick w prawo
                    break;
            }

            // Sprawdź, czy wciśnięte są klawisze strzałki w górę i lewo jednocześnie
            if (e.KeyCode == Keys.Up && e.KeyCode == Keys.Left)
            {
                joystickPosition.Y -= step; // Przesuń joystick w górę
                joystickPosition.X -= step; // Przesuń joystick w lewo
            }
            joystickPictureBox.Refresh();
            */
        }

        private void podwozieForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            /*switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                    isMoving = true; // Ustawienie flagi, że obrazek jest przesuwany
                    MoveJoystick(e.KeyCode); // Przesuń obrazek
                    break;
            }*/
        }

        private void MoveJoystick(Keys key)
        {
         /*
            int step = 5;
            // Określ kierunek przesunięcia na podstawie wciśniętego klawisza
            switch (key)
            {
                case Keys.Up:
                    joystickPosition.Y = -step; // Przesuń obrazek w górę
                    break;
                case Keys.Down:
                    joystickPosition.Y = step; // Przesuń obrazek w dół
                    break;
                case Keys.Left:
                    joystickPosition.X = -step; // Przesuń obrazek w lewo
                    break;
                case Keys.Right:
                    joystickPosition.X = step; // Przesuń obrazek w prawo
                    break;
            }
            UpdateJoystick();
            joystickPictureBox.Refresh();
            // Przesuń pozycję obrazka

            // Sprawdź, czy obrazek nadal ma być przesuwany
            if (isMoving)
            {
                // Kontynuuj przesuwanie obrazka
                MoveJoystick(key);
            }*/
        }

        private void podwozieForm_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            /*switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                    isMoving = false; // Ustawienie flagi, że obrazek nie jest już przesuwany
                    break;
            }*/
        }
    }

    
    

}
