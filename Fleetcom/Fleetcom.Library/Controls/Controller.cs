using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Fleetcom.Library.Controls
{
    public static class Controller
    {
        #region Properties
        public static bool Enabled { get; set; }
        public static bool IsConnected { get { return _gamePadState.IsConnected; } }
        public static bool A { get { return _gamePadState.IsButtonDown(Buttons.A); } }
        public static bool B { get { return _gamePadState.IsButtonDown(Buttons.B); } }
        public static bool X { get { return _gamePadState.IsButtonDown(Buttons.X); } }
        public static bool Y { get { return _gamePadState.IsButtonDown(Buttons.Y); } }
        public static bool Back { get { return _gamePadState.IsButtonDown(Buttons.Back); } }
        public static bool Start { get { return _gamePadState.IsButtonDown(Buttons.Start); } }
        public static bool RightShoulder { get { return _gamePadState.IsButtonDown(Buttons.RightShoulder); } }
        public static bool LeftShoulder { get { return _gamePadState.IsButtonDown(Buttons.LeftShoulder); } }
        public static float RightTrigger { get { return _gamePadState.Triggers.Right; } }
        public static float LeftTrigger { get { return _gamePadState.Triggers.Left; } }
        public static bool LeftStickButton { get { return _gamePadState.IsButtonDown(Buttons.LeftStick); } }
        public static Vector2 LeftStick { get { return _gamePadState.ThumbSticks.Left; } }
        public static bool RightStickButton { get { return _gamePadState.IsButtonDown(Buttons.RightStick); } }
        public static Vector2 RightStick { get { return _gamePadState.ThumbSticks.Right; } }
        public static GamePadDPad DPad { get { return _gamePadState.DPad; } }

        #region Timing & Flags
        private static float _aTimer;
        private static bool _aHeldFlag;
        private static float _bTimer;
        private static bool _bHeldFlag;
        private static float _xTimer;
        private static bool _xHeldFlag;
        private static float _yTimer;
        private static bool _yHeldFlag;
        private static float _backTimer;
        private static bool _backHeldFlag;
        private static float _startTimer;
        private static bool _startHeldFlag;
        private static float _rightShoulderTimer;
        private static bool _rightShoulderHeldFlag;
        private static float _leftShoulderTimer;
        private static bool _leftShoulderHeldFlag;
        private static float _leftStickTimer;
        private static bool _leftStickHeldFlag;
        private static float _rightStickTimer;
        private static bool _rightStickHeldFlag;
        private static float _dPadUpTimer;
        private static bool _dPadUpHeldFlag;
        private static float _dPadDownTimer;
        private static bool _dPadDownHeldFlag;
        private static float _dPadLeftTimer;
        private static bool _dPadLeftHeldFlag;
        private static float _dPadRightTimer;
        private static bool _dPadRightHeldFlag;
        #endregion

        private static GamePadDeadZone _deadZone;
        private static GamePadState _gamePadState;
        private static GamePadState _previousGamePadState;
        private const float HoldTimespan = 0.5f;
        private const float TriggerThreshold = 0.5f;

        #endregion

        #region Events
        public static event Events.ButtonPressed APressed;
        public static event Events.ButtonHeld AHeld;
        public static event Events.ButtonPressed BPressed;
        public static event Events.ButtonHeld BHeld;
        public static event Events.ButtonPressed XPressed;
        public static event Events.ButtonHeld XHeld;
        public static event Events.ButtonPressed YPressed;
        public static event Events.ButtonHeld YHeld;
        public static event Events.ButtonPressed BackPressed;
        public static event Events.ButtonHeld BackHeld;
        public static event Events.ButtonPressed StartPressed;
        public static event Events.ButtonHeld StartHeld;
        public static event Events.ButtonPressed RightShoulderPressed;
        public static event Events.ButtonHeld RightShoulderHeld;
        public static event Events.ButtonPressed LeftShoulderPressed;
        public static event Events.ButtonHeld LeftShoulderHeld;
        public static event Events.ButtonPressed LeftStickPressed;
        public static event Events.ButtonHeld LeftStickHeld;
        public static event Events.ButtonPressed RightStickPressed;
        public static event Events.ButtonHeld RightStickHeld;
        public static event Events.ButtonPressed DPadUpPressed;
        public static event Events.ButtonHeld DPadUpHeld;
        public static event Events.ButtonPressed DPadDownPressed;
        public static event Events.ButtonHeld DPadDownHeld;
        public static event Events.ButtonPressed DPadLeftPressed;
        public static event Events.ButtonHeld DPadLeftHeld;
        public static event Events.ButtonPressed DPadRightPressed;
        public static event Events.ButtonHeld DPadRightHeld;
        public static event Events.ButtonPressed LeftTriggerPressed;
        public static event Events.ButtonPressed RightTriggerPressed;
        #endregion

        public static void Inititalize(GamePadDeadZone deadZone)
        {
            _deadZone = deadZone;

            _gamePadState = GamePad.GetState(PlayerIndex.One, _deadZone);
            _previousGamePadState = _gamePadState;
        }

        public static void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                _previousGamePadState = _gamePadState;
                _gamePadState = GamePad.GetState(PlayerIndex.One);

                #region Events
                //A
                if (_gamePadState.IsButtonDown(Buttons.A))
                    _aTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_aTimer > HoldTimespan)
                {
                    _aHeldFlag = true;

                    if (AHeld != null)
                        AHeld();
                }

                if (_gamePadState.IsButtonUp(Buttons.A))
                {
                    if (_aHeldFlag)
                    {
                        _aTimer = 0;
                        _aHeldFlag = false;
                    }
                    else if (_previousGamePadState.IsButtonDown(Buttons.A)
                        && APressed != null)
                        APressed();
                }


                //B
                if (_gamePadState.IsButtonDown(Buttons.B))
                    _bTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_bTimer > HoldTimespan)
                {
                    _bHeldFlag = true;

                    if (BHeld != null)
                        BHeld();
                }

                if (_gamePadState.IsButtonUp(Buttons.B))
                {
                    if (_bHeldFlag)
                    {
                        _bTimer = 0;
                        _bHeldFlag = false;
                    }
                    else if (_previousGamePadState.IsButtonDown(Buttons.B)
                        && BPressed != null)
                        BPressed();
                }


                //X
                if (_gamePadState.IsButtonDown(Buttons.X))
                    _xTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_xTimer > HoldTimespan)
                {
                    _xHeldFlag = true;

                    if (XHeld != null)
                        XHeld();
                }

                if (_gamePadState.IsButtonUp(Buttons.X))
                {
                    if (_xHeldFlag)
                    {
                        _xTimer = 0;
                        _xHeldFlag = false;
                    }
                    else if (_previousGamePadState.IsButtonDown(Buttons.X)
                        && XPressed != null)
                        XPressed();
                }


                //Y
                if (_gamePadState.IsButtonDown(Buttons.Y))
                    _yTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_yTimer > HoldTimespan)
                {
                    _yHeldFlag = true;

                    if (YHeld != null)
                        YHeld();
                }

                if (_gamePadState.IsButtonUp(Buttons.Y))
                {
                    if (_yHeldFlag)
                    {
                        _yTimer = 0;
                        _yHeldFlag = false;
                    }
                    else if (_previousGamePadState.IsButtonDown(Buttons.Y)
                        && YPressed != null)
                        YPressed();
                }


                //Back
                if (_gamePadState.IsButtonDown(Buttons.Back))
                    _backTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_backTimer > HoldTimespan)
                {
                    _backHeldFlag = true;

                    if (BackHeld != null)
                        BackHeld();
                }

                if (_gamePadState.IsButtonUp(Buttons.Back))
                {
                    if (_backHeldFlag)
                    {
                        _backTimer = 0;
                        _backHeldFlag = false;
                    }
                    else if (_previousGamePadState.IsButtonDown(Buttons.Back)
                        && BackPressed != null)
                        BackPressed();
                }


                //Start
                if (_gamePadState.IsButtonDown(Buttons.Start))
                    _startTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_startTimer > HoldTimespan)
                {
                    _startHeldFlag = true;

                    if (StartHeld != null)
                        StartHeld();
                }

                if (_gamePadState.IsButtonUp(Buttons.Start))
                {
                    if (_startHeldFlag)
                    {
                        _startTimer = 0;
                        _startHeldFlag = false;
                    }
                    else if (_previousGamePadState.IsButtonDown(Buttons.Start)
                        && StartPressed != null)
                        StartPressed();
                }


                //LeftShoulder
                if (_gamePadState.IsButtonDown(Buttons.LeftShoulder))
                    _leftShoulderTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_leftShoulderTimer > HoldTimespan)
                {
                    _leftShoulderHeldFlag = true;

                    if (LeftShoulderHeld != null)
                        LeftShoulderHeld();
                }

                if (_gamePadState.IsButtonUp(Buttons.LeftShoulder))
                {
                    if (_leftShoulderHeldFlag)
                    {
                        _leftShoulderTimer = 0;
                        _leftShoulderHeldFlag = false;
                    }
                    else if (_previousGamePadState.IsButtonDown(Buttons.LeftShoulder)
                        && LeftShoulderPressed != null)
                        LeftShoulderPressed();
                }


                //RightShoulder
                if (_gamePadState.IsButtonDown(Buttons.RightShoulder))
                    _rightShoulderTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_rightShoulderTimer > HoldTimespan)
                {
                    _rightShoulderHeldFlag = true;

                    if (RightShoulderHeld != null)
                        RightShoulderHeld();
                }

                if (_gamePadState.IsButtonUp(Buttons.RightShoulder))
                {
                    if (_rightShoulderHeldFlag)
                    {
                        _rightShoulderTimer = 0;
                        _rightShoulderHeldFlag = false;
                    }
                    else if (_previousGamePadState.IsButtonDown(Buttons.RightShoulder)
                        && RightShoulderPressed != null)
                        RightShoulderPressed();
                }


                //LeftStick
                if (_gamePadState.IsButtonDown(Buttons.LeftStick))
                    _leftStickTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_leftStickTimer > HoldTimespan)
                {
                    _leftStickHeldFlag = true;

                    if (LeftStickHeld != null)
                        LeftStickHeld();
                }

                if (_gamePadState.IsButtonUp(Buttons.LeftStick))
                {
                    if (_leftStickHeldFlag)
                    {
                        _leftStickTimer = 0;
                        _leftStickHeldFlag = false;
                    }
                    else if (_previousGamePadState.IsButtonDown(Buttons.LeftStick)
                        && LeftStickPressed != null)
                        LeftStickPressed();
                }


                //RightStick
                if (_gamePadState.IsButtonDown(Buttons.RightStick))
                    _rightStickTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_rightStickTimer > HoldTimespan)
                {
                    _rightStickHeldFlag = true;

                    if (RightStickHeld != null)
                        RightStickHeld();
                }

                if (_gamePadState.IsButtonUp(Buttons.RightStick))
                {
                    if (_rightStickHeldFlag)
                    {
                        _rightStickTimer = 0;
                        _rightStickHeldFlag = false;
                    }
                    else if (_previousGamePadState.IsButtonDown(Buttons.RightStick)
                        && RightStickPressed != null)
                        RightStickPressed();
                }


                //DPadUp
                if (_gamePadState.IsButtonDown(Buttons.DPadUp))
                    _dPadUpTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_dPadUpTimer > HoldTimespan)
                {
                    _dPadUpHeldFlag = true;

                    if (DPadUpHeld != null)
                        DPadUpHeld();
                }

                if (_gamePadState.IsButtonUp(Buttons.DPadUp))
                {
                    if (_dPadUpHeldFlag)
                    {
                        _dPadUpTimer = 0;
                        _dPadUpHeldFlag = false;
                    }
                    else if (_previousGamePadState.IsButtonDown(Buttons.DPadUp)
                        && DPadUpPressed != null)
                        DPadUpPressed();
                }


                //DPadDown
                if (_gamePadState.IsButtonDown(Buttons.DPadDown))
                    _dPadDownTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_dPadDownTimer > HoldTimespan)
                {
                    _dPadDownHeldFlag = true;

                    if (DPadDownHeld != null)
                        DPadDownHeld();
                }

                if (_gamePadState.IsButtonUp(Buttons.DPadDown))
                {
                    if (_dPadDownHeldFlag)
                    {
                        _dPadDownTimer = 0;
                        _dPadDownHeldFlag = false;
                    }
                    else if (_previousGamePadState.IsButtonDown(Buttons.DPadDown)
                        && DPadDownPressed != null)
                        DPadDownPressed();
                }


                //DPadLeft
                if (_gamePadState.IsButtonDown(Buttons.DPadLeft))
                    _dPadLeftTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_dPadLeftTimer > HoldTimespan)
                {
                    _dPadLeftHeldFlag = true;

                    if (DPadLeftHeld != null)
                        DPadLeftHeld();
                }

                if (_gamePadState.IsButtonUp(Buttons.DPadLeft))
                {
                    if (_dPadLeftHeldFlag)
                    {
                        _dPadLeftTimer = 0;
                        _dPadLeftHeldFlag = false;
                    }
                    else if (_previousGamePadState.IsButtonDown(Buttons.DPadLeft)
                        && DPadLeftPressed != null)
                        DPadLeftPressed();
                }


                //DPadRight
                if (_gamePadState.IsButtonDown(Buttons.DPadRight))
                    _dPadRightTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_dPadRightTimer > HoldTimespan)
                {
                    _dPadRightHeldFlag = true;

                    if (DPadRightHeld != null)
                        DPadRightHeld();
                }

                if (_gamePadState.IsButtonUp(Buttons.DPadRight))
                {
                    if (_dPadRightHeldFlag)
                    {
                        _dPadRightTimer = 0;
                        _dPadRightHeldFlag = false;
                    }
                    else if (_previousGamePadState.IsButtonDown(Buttons.DPadRight)
                        && DPadRightPressed != null)
                        DPadRightPressed();
                }

                if (_gamePadState.Triggers.Right >= TriggerThreshold
                    && RightTriggerPressed != null)
                    RightTriggerPressed();

                if (_gamePadState.Triggers.Left >= TriggerThreshold
                    && LeftTriggerPressed != null)
                    LeftTriggerPressed();
                #endregion
            }
        }
    }
}
