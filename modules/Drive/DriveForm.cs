using HAL062app.CustomControls;
using SharpDX.XInput;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Input;
using static HAL062app.ControllerState;

namespace HAL062app.moduly.podwozie
{

    public interface IPodwozieView
    {
        event Action<MotorData> SendMotorDataToController_Action;
        event Action<Message> SendMessage_Action;
    }




    public partial class DriveForm : Form, IPodwozieView
    {
        private System.Drawing.Point joystickPosition = System.Drawing.Point.Empty;
        bool isDragging = false;
        private System.Drawing.Point lastMousePosition = System.Drawing.Point.Empty;
        private int joystickRadius = 30;
        private float velocity = 0;
        private float angularVelocity = 0;
        private float velocityMaxLimit = 9;
        private float angularVelocityMaxLimit = 3;
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
        private int returnKeyboardDelta = 10;
        public event Action<MotorData> SendMotorDataToController_Action;
        public event Action<Message> SendMessage_Action;

        private bool usingXboxPad = false;
        private XboxPad _XboxPad;
        private System.Drawing.Point lastMouseXbox = System.Drawing.Point.Empty;

        private FPVCamera fpvCamera;

        public DriveForm()
        {
            InitializeComponent();
            InitializeTimer();
            this.KeyPreview = true;

            _XboxPad = XboxPad.Instance;
            _XboxPad.ControllerStateChanged += OnXboxPadStateChanged;
            XboxControlBus.XboxControlMode += OnXboxControlModeChanged;

            joystickPosition.X = joystickPictureBox.ClientSize.Width / 2;
            joystickPosition.Y = joystickPictureBox.ClientSize.Height / 2;

            ForwardVelocityTrack.Maximum = (int)velocityMaxLimit;
            ForwardVelocityTrack.Minimum = (int)-velocityMaxLimit;

            joystickPictureBox.Refresh();

            horizontalKeyboardTextbox.Text = horizontalKeyboardDelta.ToString();
            verticalKeyboardTextbox.Text = verticalKeyboardDelta.ToString();
            returnKeyboardTextbox.Text = returnKeyboardDelta.ToString();


            fpvCamera = new FPVCamera(3,frame=> SendMessage_Action.Invoke(frame));
            fpvCamera.AddButtons(0, cameraFPV11btn, cameraFPV12btn, cameraFPV13btn);
            fpvCamera.AddButtons(1, cameraFPV21btn, cameraFPV22btn, cameraFPV23btn);
            fpvCamera.AddButtons(2, cameraFPV31btn, cameraFPV32btn, cameraFPV33btn);

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
                UpdateJoystick();
                elapsedTime = 0;

            }
            if (!isDragging)
                returnToZeroJoystickPosition();

        }


        private void joystickPictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(Brushes.Gray, joystickPictureBox.ClientSize.Width / 2 - limitRadius, joystickPictureBox.ClientSize.Height / 2 - limitRadius, 2 * limitRadius, 2 * limitRadius);
            e.Graphics.FillEllipse(Brushes.White, joystickPosition.X - joystickRadius, joystickPosition.Y - joystickRadius, 2 * joystickRadius, 2 * joystickRadius);
        }

        private void joystickPictureBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            joystickPictureBox.Focus();
            if (e.Button == MouseButtons.Left)
            {
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
                joystickPictureBox.Refresh();
                lastMousePosition = e.Location;

            }
        }

        private int GetDistanceFromCenterPoint(int c, int b)
        {
            return (int)Math.Sqrt(Math.Abs(c * c - b * b));
        }
        async private void joystickPictureBox_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            joystickPictureBox.Focus();
            if (isDragging && !usingXboxPad)
            {


                int deltaX = e.X - lastMousePosition.X;
                int deltaY = e.Y - lastMousePosition.Y;
                joystickPosition.X += deltaX;
                joystickPosition.Y += deltaY;

                int height = joystickPictureBox.ClientSize.Height;
                int width = joystickPictureBox.ClientSize.Width;



                if (joystickPosition.X - width / 2 >= 0)
                    joystickPosition.X = Math.Min(joystickPosition.X, width / 2 + GetDistanceFromCenterPoint(limitRadius, joystickPosition.Y - height / 2) - joystickRadius);
                else
                    joystickPosition.X = Math.Max(joystickPosition.X, width / 2 - GetDistanceFromCenterPoint(limitRadius, (height / 2) - joystickPosition.Y) + joystickRadius);

                if (joystickPosition.Y - height / 2 >= 0)
                    joystickPosition.Y = Math.Min(joystickPosition.Y, height / 2 + GetDistanceFromCenterPoint(limitRadius, joystickPosition.X - width / 2) - joystickRadius);
                else
                    joystickPosition.Y = Math.Max(joystickPosition.Y, height / 2 - GetDistanceFromCenterPoint(limitRadius, (width / 2) - joystickPosition.X) + joystickRadius);

                joystickPictureBox.Refresh();



                lastMousePosition = e.Location;

            }
        }

        private void UpdateJoystick()
        {

            ForwardVelocityTrack.Maximum = (int)velocityMaxLimit;
            ForwardVelocityTrack.Minimum = (int)-velocityMaxLimit;


            velocity = -((float)joystickPosition.Y - (float)joystickPictureBox.ClientSize.Height / 2.0f) / (float)(limitRadius - joystickRadius);
            angularVelocity = ((float)joystickPosition.X - (float)joystickPictureBox.ClientSize.Width / 2.0f) / (float)(limitRadius - joystickRadius);
            ForwardVelocityTrack.Value = (int)velocity;


            CalculateVelocity(velocity, angularVelocity);

        }
        private void CalculateVelocity(float velocity, float angularVelocity)
        {
            // rozstaw kół = 0.92 = 2 * 2.97 / 6.45
            // Funkcja oblicza prędkości kół na podstawie prędkości liniowej i kątowej
            // predkosc skalowana jest w zakresie 0-100
            float coeff = 100.0f / 6.45f;

            int rightVelocity = (int)(coeff * (6.45f * velocity + 2.97f * angularVelocity));
            int leftVelocity = (int)(coeff * (6.45f * velocity - 2.97f * angularVelocity));
            LFStextbox.Text = leftVelocity.ToString();
            LBStextbox.Text = leftVelocity.ToString();
            RFStextbox.Text = rightVelocity.ToString();
            RBStextbox.Text = rightVelocity.ToString();
            MotorData data = new MotorData(rightVelocity, leftVelocity, 0, 0, 0, 0);
            if (isRunning)
                SendMotorDataToController_Action(data);

        }



        private void PodwozieForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
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
                joystickPosition.X = Math.Min(joystickPosition.X, width / 2 + GetDistanceFromCenterPoint(limitRadius, joystickPosition.Y - height / 2) - joystickRadius);
            else
                joystickPosition.X = Math.Max(joystickPosition.X, width / 2 - GetDistanceFromCenterPoint(limitRadius, (height / 2) - joystickPosition.Y) + joystickRadius);

            if (joystickPosition.Y - height / 2 >= 0)
                joystickPosition.Y = Math.Min(joystickPosition.Y, height / 2 + GetDistanceFromCenterPoint(limitRadius, joystickPosition.X - width / 2) - joystickRadius);
            else
                joystickPosition.Y = Math.Max(joystickPosition.Y, height / 2 - GetDistanceFromCenterPoint(limitRadius, (width / 2) - joystickPosition.X) + joystickRadius);
            joystickPictureBox.Refresh();



        }



        private void PodwozieForm_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            bool isUpPressed = Keyboard.IsKeyDown(Key.Up);
            bool isDownPressed = Keyboard.IsKeyDown(Key.Down);
            bool isLeftPressed = Keyboard.IsKeyDown(Key.Left);
            bool isRightPressed = Keyboard.IsKeyDown(Key.Right);
            if (!isLeftPressed && !isRightPressed)
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
                startJoystickButton.ButtonStyle = CustomControls.CustomButton.ButtonStyles.Red;
            }
            else
            {
                isRunning = false;
                startJoystickButton.Text = "Start";
                startJoystickButton.ButtonStyle = CustomControls.CustomButton.ButtonStyles.Green;

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
                    startJoystickButton.ButtonStyle = CustomControls.CustomButton.ButtonStyles.Green;
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
            if (!usingXboxPad)
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

        private void OnXboxControlModeChanged(int value)
        {
            if (value == 0)
            {
                isRunning = true;
                startJoystickButton.Text = "Stop";
                startJoystickButton.ButtonStyle = CustomControls.CustomButton.ButtonStyles.Red;
                usingXboxPad = true;
            }
            if (value == 1)
            {
                isRunning = false;
                startJoystickButton.Text = "Start";
                startJoystickButton.ButtonStyle = CustomControls.CustomButton.ButtonStyles.Green;
                usingXboxPad = false;
            }



        }
        private void OnXboxPadStateChanged(object sender, State state)
        {
            Invoke(new MethodInvoker(delegate
            {
                UpdateJoystick_Xbox(state);

            }));

        }




        private void UpdateJoystick_Xbox(State state)
        {
            joystickPictureBox_XboxPadStateChange(state);
        }

        private void ConnectWithXboxPadBtn_Click(object sender, EventArgs e)
        {
            if (!usingXboxPad)
            {
                usingXboxPad = true;
                ConnectWithXboxPadBtn.Text = "Wyłącz";
            }
            else
            {
                usingXboxPad = false;
                ConnectWithXboxPadBtn.Text = "Włącz";

            }
        }
        async private void joystickPictureBox_XboxPadStateChange(State state)
        {

            if (!isDragging && usingXboxPad)
            {


                int height = joystickPictureBox.ClientSize.Height;
                int width = joystickPictureBox.ClientSize.Width;

                int gamePadX = 0;
                int gamePadY = 0;
            
                if (Math.Abs((int)state.Gamepad.LeftThumbX) > 8689)
                    gamePadX = (int)((float)100 * (float)state.Gamepad.LeftThumbX / 32768f);
                if ((int)state.Gamepad.RightTrigger > Gamepad.TriggerThreshold && (int)state.Gamepad.LeftTrigger < Gamepad.TriggerThreshold)
                    gamePadY = -(int)((float)100 * (float)state.Gamepad.RightTrigger / 255f);
                if ((int)state.Gamepad.LeftTrigger > Gamepad.TriggerThreshold && (int)state.Gamepad.RightTrigger < Gamepad.TriggerThreshold)
                    gamePadY = (int)((float)100 * (float)state.Gamepad.LeftTrigger / 255f);

                gamePadX = gamePadX * limitRadius / 100;
                gamePadY = gamePadY * limitRadius / 120;
                joystickPosition.X = gamePadX + width / 2;
                joystickPosition.Y = gamePadY + height / 2;

                if (joystickPosition.X - width / 2 >= 0)
                    joystickPosition.X = Math.Min(joystickPosition.X, width / 2 + GetDistanceFromCenterPoint(limitRadius, joystickPosition.Y - height / 2) - joystickRadius);
                else
                    joystickPosition.X = Math.Max(joystickPosition.X, width / 2 - GetDistanceFromCenterPoint(limitRadius, (height / 2) - joystickPosition.Y) + joystickRadius);

                if (joystickPosition.Y - height / 2 >= 0)
                    joystickPosition.Y = Math.Min(joystickPosition.Y, height / 2 + GetDistanceFromCenterPoint(limitRadius, joystickPosition.X - width / 2) - joystickRadius);
                else
                    joystickPosition.Y = Math.Max(joystickPosition.Y, height / 2 - GetDistanceFromCenterPoint(limitRadius, (width / 2) - joystickPosition.X) + joystickRadius);

                joystickPictureBox.Refresh();


            }
        }

        private void ArmRoverBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(22);
            frame.buffer[2] = (byte)(1);
            frame.buffer[3] = (byte)(1);
            frame.buffer[4] = (byte)(1);
            frame.buffer[5] = (byte)('x');
            frame.buffer[6] = (byte)('x');
            frame.buffer[7] = (byte)('x');
            frame.buffer[8] = (byte)('x');
            frame.buffer[9] = (byte)('x');
            frame.text = new string(frame.encodeMessage());
            SendMessage_Action(frame);
        }

        private void PodwozieForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void camera_rotate_L_btn_Click(object sender, EventArgs e)
        {
            Camera_track.Value = 25;
            Camera_track_Leave(sender, e);
        }

        private void camera_rotate_R_btn_Click(object sender, EventArgs e)
        {
            Camera_track.Value = 75;
            Camera_track_Leave(sender, e);
        }

        private void camera_rotate_F_btn_Click(object sender, EventArgs e)
        {
            Camera_track.Value = 50;
            Camera_track_Leave(sender, e);
        }

        private void camera_rotate_B_btn_Click(object sender, EventArgs e)
        {
            Camera_track.Value = 100;
            Camera_track_Leave(sender, e);
        }





        private void Camera_track_Leave(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(5);
            frame.buffer[2] = (byte)((int)Camera_track.Value);
            frame.buffer[3] = (byte)('x');
            frame.buffer[4] = (byte)('x');
            frame.buffer[5] = (byte)('x');
            frame.buffer[6] = (byte)('x');
            frame.buffer[7] = (byte)('x');
            frame.buffer[8] = (byte)('x');
            frame.buffer[9] = (byte)('x');
            frame.text = new string(frame.encodeMessage());
            SendMessage_Action(frame);
        }

        private void DriveForm_VisibleChanged(object sender, EventArgs e)
        {
            isRunning = false;
            startJoystickButton.Text = "Start";
            startJoystickButton.ButtonStyle = CustomControls.CustomButton.ButtonStyles.Green;

        }

        private void DriveForm_Resize(object sender, EventArgs e)
        {
            isRunning = false;
            startJoystickButton.Text = "Start";
            startJoystickButton.ButtonStyle = CustomControls.CustomButton.ButtonStyles.Green;

        }

        private void cameraFPV11btn_Click(object sender, EventArgs e)
        {

        }
    }

    public class FPVCamera
    {
        private int[] channels;
        private CustomButton[][] buttons;
        private readonly Action<Message> sendFrameAction;

        public FPVCamera(int _channels, Action<Message> sendFrameAction)
        {
            this.channels = new int[_channels];
            this.buttons = new CustomButton[_channels][];
            for (int i = 0; i < _channels; i++)
            {
                channels[i] = 0;
                buttons[i] = new CustomButton[3];
            }

            this.sendFrameAction = sendFrameAction;
        }


        public void AddButtons(int channel, CustomButton btn1, CustomButton btn2, CustomButton btn3)
        {
            if (channel < 0 || channel >= channels.Length)
                throw new ArgumentOutOfRangeException(nameof(channel), "Kanał poza zakresem");
            buttons[channel][0] = btn1;
            buttons[channel][1] = btn2;
            buttons[channel][2] = btn3;
            btn1.Click += (sender, e) => SetChannel(channel, 0);
            btn2.Click += (sender, e) => SetChannel(channel, 1);
            btn3.Click += (sender, e) => SetChannel(channel, 2);
        }

        public void SetChannel(int channel, int value)
        {
            if (channel < 0 || channel >= channels.Length)
                throw new ArgumentOutOfRangeException(nameof(channel), "Kanał poza zakresem");

            channels[channel] = value;
            UpdateButtons(channel);
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(45);
            frame.buffer[2] = (byte)(channels[0]+1);
            frame.buffer[3] = (byte)(channels[1]+1);
            frame.buffer[4] = (byte)(channels[2]+1);
            frame.buffer[5] = (byte)('x');
            frame.buffer[6] = (byte)('x');
            frame.buffer[7] = (byte)('x');
            frame.buffer[8] = (byte)('x');
            frame.buffer[9] = (byte)('x');
            frame.text = new string(frame.encodeMessage());

            sendFrameAction(frame);
        }

        private void UpdateButtons(int channel)
        {
            for (int i = 0; i < buttons[channel].Length; i++)
            {
                if (buttons[channel][i] != null)
                {
                    if (channels[channel] == i)
                        buttons[channel][i].ButtonStyle = CustomControls.CustomButton.ButtonStyles.Green;
                    else
                        buttons[channel][i].ButtonStyle = CustomControls.CustomButton.ButtonStyles.Off;
                }
            }
        }




    }
}
