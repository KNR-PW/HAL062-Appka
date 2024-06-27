using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Media3D;

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
      
        private bool isMoving = false;
        private Timer timer;
        private int readInterval = 100;
        private int timerInterval = 25;
        private int elapsedTime = 0;
        private bool isRunning = false;

        private bool isKeyboardPressedVertically = false;
        private bool isKeyboardPressedHorizontally = false;
        private int horizontalKeyboardDelta = 4;
        private int verticalKeyboardDelta = 10;
        private int returnKeyboardDelta= 10;
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

           horizontalKeyboardTextbox.Text = horizontalKeyboardDelta.ToString();
           verticalKeyboardTextbox.Text = verticalKeyboardDelta.ToString();
            returnKeyboardTextbox.Text = returnKeyboardDelta.ToString();

        }

        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = timerInterval;
            timer.Tick += Timer_Tick;
            timer.Start();
            TimerTickTextbox.Text = timerInterval.ToString();
            IntervalOfSendingTextbox.Text = readInterval.ToString();
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
            if(!isDragging)
          returnToZeroJoystickPosition();

        }


        private void joystickPictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(Brushes.Gray, joystickPictureBox.ClientSize.Width / 2 - limitRadius, joystickPictureBox.ClientSize.Height / 2 - limitRadius, 2*limitRadius, 2*limitRadius);
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
               // joystickPosition.X = joystickPictureBox.ClientSize.Width / 2 ;
               // joystickPosition.Y = joystickPictureBox.ClientSize.Height / 2 ;
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

        private void UpdateJoystick()
        {
            ForwardSpeedTrack.Maximum = (int)forwardSpeedMaxLimit;
            ForwardSpeedTrack.Minimum = (int)-forwardSpeedMaxLimit;
            TurningSpeedTrack.Maximum = (int)turningSpeedMaxLimit;
            TurningSpeedTrack.Minimum = (int)-turningSpeedMaxLimit;

            forwardSpeed = -((float)joystickPosition.Y - (float)joystickPictureBox.ClientSize.Height/2.0f) / (float)(limitRadius-joystickRadius);
            turningSpeed = ((float)joystickPosition.X - (float)joystickPictureBox.ClientSize.Width/2.0f) / (float)(limitRadius-joystickRadius);
            ForwardSpeedTrack.Value = (int)forwardSpeed;
            TurningSpeedTrack.Value = (int)turningSpeed;
        
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
            if(isRunning)
            sendMotorDataToController_Action(data);

        }

      

        private void podwozieForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            int deltaX = horizontalKeyboardDelta;
            int deltaY = verticalKeyboardDelta;
            int height = joystickPictureBox.ClientSize.Height;
            int width = joystickPictureBox.ClientSize.Width;

            bool isUpPressed = Keyboard.IsKeyDown(Key.Up);
            bool isDownPressed = Keyboard.IsKeyDown(Key.Down);
            bool isLeftPressed = Keyboard.IsKeyDown(Key.Left);
            bool isRightPressed = Keyboard.IsKeyDown(Key.Right);

            if (isUpPressed)
            {
                isKeyboardPressedVertically = true;
                joystickPosition.Y -= deltaY; // Przesuń joystick w górę
            }
            if (isDownPressed)
            {
                isKeyboardPressedVertically = true;
                joystickPosition.Y += deltaY; // Przesuń joystick w dół
            }
            if (isLeftPressed)
            {
                isKeyboardPressedHorizontally = true;
                joystickPosition.X -= deltaX; // Przesuń joystick w lewo
            }
            if (isRightPressed)
            {
                isKeyboardPressedHorizontally = true;
                joystickPosition.X += deltaX; // Przesuń joystick w prawo
            }

            if (joystickPosition.X - width / 2 >= 0)
                joystickPosition.X = Math.Min(joystickPosition.X, width / 2 + pitagoras(limitRadius, joystickPosition.Y - height / 2) - joystickRadius);
            else
                joystickPosition.X = Math.Max(joystickPosition.X, width / 2 - pitagoras(limitRadius, (height / 2) - joystickPosition.Y) + joystickRadius);

            if (joystickPosition.Y - height / 2 >= 0)
                joystickPosition.Y = Math.Min(joystickPosition.Y, height / 2 + pitagoras(limitRadius, joystickPosition.X - width / 2) - joystickRadius);
            else
                joystickPosition.Y = Math.Max(joystickPosition.Y, height / 2 - pitagoras(limitRadius, (width / 2) - joystickPosition.X) + joystickRadius);
            joystickPictureBox.Refresh();
          


        }

        

        private void podwozieForm_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            bool isUpPressed = Keyboard.IsKeyDown(Key.Up);
            bool isDownPressed = Keyboard.IsKeyDown(Key.Down);
            bool isLeftPressed = Keyboard.IsKeyDown(Key.Left);
            bool isRightPressed = Keyboard.IsKeyDown(Key.Right);
            if ( !isLeftPressed && !isRightPressed)
                isKeyboardPressedHorizontally = false;
            if (!isUpPressed && !isDownPressed)
                isKeyboardPressedVertically = false;
            joystickPictureBox.Refresh();
           
        }


    

        private void startJoystickButton_Click_1(object sender, EventArgs e)
        {
            joystickPictureBox_MouseUp(MouseButtons.Left, new System.Windows.Forms.MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
            if (startJoystickButton.Text == "Start")
            {
                isRunning = true;
                startJoystickButton.Text = "Stop";
                startJoystickButton.BackColor = Color.FromArgb(192, 0, 0);
            }
            else
            {
                isRunning = false;
                startJoystickButton.Text = "Start";
                startJoystickButton.BackColor = Color.FromArgb(0, 192, 0);

            }
        }

        private void SetFrequencyButton_Click(object sender, EventArgs e)
        {
            if (int.TryParse(TimerTickTextbox.Text, out int tickValue) && int.TryParse(IntervalOfSendingTextbox.Text, out int sendValue) && int.TryParse(returnKeyboardTextbox.Text, out int returnDelta) && int.TryParse(horizontalKeyboardTextbox.Text, out int horizontalDelta) && int.TryParse(verticalKeyboardTextbox.Text, out int verticalDelta))
            {
                if (tickValue <= 0 || sendValue <= 0 || returnDelta <= 0 || horizontalDelta <= 0 || verticalDelta <= 0)
                {
                    errorTextbox.Text = "Wartości powinny być dodatnie";

                }
                else if (tickValue >= sendValue)
                {
                    errorTextbox.Text = "Wartość interwału zegara powinna być mniejsza od okresu wysyłania";
                }
                else
                {
                    errorTextbox.Text = "Ustawiono!";
                    horizontalKeyboardDelta = horizontalDelta;
                    verticalKeyboardDelta = verticalDelta;
                    returnKeyboardDelta = returnDelta;
                    timer.Stop();
                    timerInterval = tickValue;
                    readInterval = sendValue;
                    isRunning = false;
                    startJoystickButton.Text = "Start";
                    startJoystickButton.BackColor = Color.FromArgb(0, 192, 0);
                    InitializeTimer();


                }

            }
            else
            {
                errorTextbox.Text = "Wartości powinny być liczbami całkowitymi";
            } 
        }


        

        private void returnToZeroJoystickPosition()
        {
            int changeDelta = returnKeyboardDelta;
            int xZero = joystickPictureBox.ClientSize.Width / 2;
            int yZero = joystickPictureBox.ClientSize.Height / 2;
            if (!isKeyboardPressedHorizontally)
            {
                if (joystickPosition.X > xZero + changeDelta || joystickPosition.X < xZero - changeDelta)
                {
                    if (joystickPosition.X > xZero + changeDelta)
                        joystickPosition.X -= changeDelta;
                    else
                        joystickPosition.X += changeDelta;
                }
                else
                    joystickPosition.X = xZero;
            }
            if (!isKeyboardPressedVertically)
            {
                if (joystickPosition.Y > yZero + changeDelta || joystickPosition.Y < yZero - changeDelta)
                {
                    if (joystickPosition.Y > yZero + changeDelta)
                        joystickPosition.Y -= changeDelta;
                    else
                        joystickPosition.Y += changeDelta;
                }
                else
                    joystickPosition.Y = yZero;
            }
            joystickPictureBox.Refresh();
      
        }





    }
}
