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
        private int forwardSpeed = 0;
        private int turningSpeed = 0;
        private int forwardSpeedMaxLimit = 10;
        private int turningSpeedMaxLimit = 5;
        int limitRadius = 160;

        public Action<motorData> sendMotorDataToController_Action;

        public podwozieForm()
        {
            InitializeComponent();
            
            
            
            joystickPosition.X = joystickPictureBox.ClientSize.Width/2;
            joystickPosition.Y = joystickPictureBox.ClientSize.Height/2;
            ForwardSpeedTrack.Maximum = forwardSpeedMaxLimit;
            ForwardSpeedTrack.Minimum = -forwardSpeedMaxLimit;
            TurningSpeedTrack.Maximum = turningSpeedMaxLimit;
            TurningSpeedTrack.Minimum = -turningSpeedMaxLimit;
            joystickPictureBox.Refresh();



        }

      
    

        private void joystickPictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(Brushes.Gray, joystickPictureBox.Width / 2 - limitRadius, joystickPictureBox.Height / 2 - limitRadius, 2*limitRadius, 2*limitRadius);
            e.Graphics.FillEllipse(Brushes.White, joystickPosition.X - joystickRadius, joystickPosition.Y-joystickRadius, 2*joystickRadius, 2*joystickRadius);
        }

        private void joystickPictureBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left) { 
            isDragging = true;
            lastMousePosition = e.Location;
            UpdateJoystick();
            }
        }

        private void joystickPictureBox_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
                joystickPosition.X = joystickPictureBox.ClientSize.Width / 2 ;
                joystickPosition.Y = joystickPictureBox.ClientSize.Height / 2 ;
                joystickPictureBox.Refresh();
                lastMousePosition = e.Location;
                UpdateJoystick();
            }
        }

        private int pitagoras(int c, int b)
        {
            return (int)Math.Sqrt(Math.Abs(c * c - b * b));
        }
        async private void joystickPictureBox_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if(isDragging)
            {
                int deltaX = e.X - lastMousePosition.X;
                int deltaY = e.Y - lastMousePosition.Y;
                joystickPosition.X += deltaX;
                joystickPosition.Y += deltaY;
                int height = joystickPictureBox.ClientSize.Height;
                int width = joystickPictureBox.ClientSize.Width;
                
                textBox3.Text = joystickPosition.X.ToString();
                RFStextbox.Text = joystickPosition.Y.ToString();

                if (joystickPosition.X - width / 2 >= 0)
                    joystickPosition.X = Math.Min(joystickPosition.X, width / 2 + pitagoras(limitRadius, joystickPosition.Y - height/2) - joystickRadius);
                else
                    joystickPosition.X = Math.Max(joystickPosition.X, width/2 - pitagoras(limitRadius,  (height / 2)- joystickPosition.Y) + joystickRadius);

                if (joystickPosition.Y - height / 2 >= 0)
                    joystickPosition.Y = Math.Min(joystickPosition.Y, height / 2  + pitagoras(limitRadius, joystickPosition.X- width / 2) - joystickRadius);
                else
                    joystickPosition.Y = Math.Max(joystickPosition.Y, height / 2  - pitagoras(limitRadius, (width / 2) - joystickPosition.X) + joystickRadius);

                joystickPictureBox.Refresh();
             
                UpdateJoystick();
                lastMousePosition = e.Location;

            }
        }

        async private void UpdateJoystick()
        {
            ForwardSpeedTrack.Maximum = forwardSpeedMaxLimit;
            ForwardSpeedTrack.Minimum = -forwardSpeedMaxLimit;
            TurningSpeedTrack.Maximum = turningSpeedMaxLimit;
            TurningSpeedTrack.Minimum = -turningSpeedMaxLimit;

            forwardSpeed = -forwardSpeedMaxLimit*(joystickPosition.Y - joystickPictureBox.ClientSize.Height/2) / limitRadius;
            turningSpeed = turningSpeedMaxLimit*(joystickPosition.X - joystickPictureBox.ClientSize.Width/2) / limitRadius;
            ForwardSpeedTrack.Value = forwardSpeed;
            TurningSpeedTrack.Value = turningSpeed;
            await Task.Delay(200);
            sendSpeeds(forwardSpeed,turningSpeed);

        }
        private void sendSpeeds(int fspeed, int tspeed)
        {
            float coeff = 100.0f / 6.45f;
            int rightSpeed = (int)(coeff * (6.45f * fspeed + 2.97f * tspeed));
            int leftSpeed = (int)(coeff * (6.45f * fspeed - 2.97f * tspeed));
            motorData data = new motorData(rightSpeed, 0, rightSpeed, leftSpeed, 0, leftSpeed);
            sendMotorDataToController_Action(data);

        }
    }

    


}
