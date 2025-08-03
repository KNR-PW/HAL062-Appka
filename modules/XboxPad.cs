using SharpDX;
using SharpDX.XInput;
using System;
using System.Threading;

namespace HAL062app
{

    public static class XboxControlBus
    {
        public static Action<int> XboxControlMode;

        public static void SendXboxModeChanged(int value)
        {
            XboxControlMode?.Invoke(value);
        }
    }


    public class XboxPad
    {
        private static readonly Lazy<XboxPad> instance = new Lazy<XboxPad>(() => new XboxPad());

        public static XboxPad Instance => instance.Value;
        public event EventHandler<State> ControllerStateChanged;
        public bool IsXboxPadOn => controller.IsConnected;
        private Controller controller;
        private ControllerState prevState;
        int disconnectedCounter = 0;
        private CancellationTokenSource tokenSource = new CancellationTokenSource();

        private XboxPad()
        {
            controller = new Controller(UserIndex.One);
            if (controller.IsConnected)
            {
                prevState = new ControllerState(controller.GetState());
                StartMonitoring();
            }
           
        }

        private void StartMonitoring()
        {
            var monitoringThread = new Thread(MonitorController);
            monitoringThread.IsBackground = true;
            monitoringThread.Start();
        }

        private void MonitorController()
        {
            var token = tokenSource.Token;

            while (!token.IsCancellationRequested)
            {
                try
                {
                    if (controller.IsConnected)
                    {
                        var state = controller.GetState();

                        if (prevState.HasChanged(state))
                        {
                            Volatile.Read(ref ControllerStateChanged)?.Invoke(this, state);

                            prevState = new ControllerState(state);
                        }
                        disconnectedCounter = 0;
                    }
                    else
                    {
                        disconnectedCounter++;
                        if (disconnectedCounter > 50)
                        {
                            controller = new Controller(UserIndex.One); // próbuj połączyć na nowo
                            disconnectedCounter = 0;
                        }
                    }

                } catch (SharpDXException) {}
                Thread.Sleep(100);
            }
        }
        public void StopMonitoring()
        {
            tokenSource.Cancel();
        }

        public void VibrateGamepad(float leftMotor, float rightMotor)
        {
            if (controller.IsConnected)
            {
                ushort leftMotorSpeed = (ushort)(leftMotor * ushort.MaxValue);
                ushort rightMotorSpeed = (ushort)(rightMotor * ushort.MaxValue);

                Vibration vibration = new Vibration
                {
                    LeftMotorSpeed = leftMotorSpeed,
                    RightMotorSpeed = rightMotorSpeed
                };

                controller.SetVibration(vibration);

            }

        }
        public GamepadButtonFlags GetPressedButtons()
        {
            if (controller.IsConnected)
            {
                var state = controller.GetState().Gamepad;
                return state.Buttons;
            }

            return GamepadButtonFlags.None;
        }
        public void StopVibration()
        {
            if (controller.IsConnected)
            {
                Vibration stopVibration = new Vibration();
                controller.SetVibration(stopVibration);
            }
        }
        public State GetCurrentState()
        {
            return controller.GetState();
        }
    }

    public class ControllerState
    {
        public GamepadButtonFlags Buttons;
        public short LeftThumbX;
        public short LeftThumbY;
        public short RightThumbX;
        public short RightThumbY;
        public byte LeftTrigger;
        public byte RightTrigger;

        public ControllerState(State state)
        {
            Buttons = state.Gamepad.Buttons;
            LeftThumbX = state.Gamepad.LeftThumbX;
            LeftThumbY = state.Gamepad.LeftThumbY;
            RightThumbX = state.Gamepad.RightThumbX;
            RightThumbY = state.Gamepad.RightThumbY;
            LeftTrigger = state.Gamepad.LeftTrigger;
            RightTrigger = state.Gamepad.RightTrigger;
        }



        public bool HasChanged(State newState)
        {
            return Buttons != newState.Gamepad.Buttons ||
                   LeftThumbX != newState.Gamepad.LeftThumbX ||
                   LeftThumbY != newState.Gamepad.LeftThumbY ||
                   RightThumbX != newState.Gamepad.RightThumbX ||
                   RightThumbY != newState.Gamepad.RightThumbY ||
                   LeftTrigger != newState.Gamepad.LeftTrigger ||
                   RightTrigger != newState.Gamepad.RightTrigger;
        }


        

    }
}
