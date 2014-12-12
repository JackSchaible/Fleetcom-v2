using System;
using Fleetcom.Library.Settings;
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
        public static bool RightBumper { get { return _gamePadState.IsButtonDown(Buttons.RightShoulder); } }
        public static bool LeftBumper { get { return _gamePadState.IsButtonDown(Buttons.LeftShoulder); } }
        public static float RightTrigger { get { return _gamePadState.Triggers.Right; } }
        public static float LeftTrigger { get { return _gamePadState.Triggers.Left; } }
        public static bool LeftStickButton { get { return _gamePadState.IsButtonDown(Buttons.LeftStick); } }
        public static Vector2 LeftStick { get { return _gamePadState.ThumbSticks.Left; } }
        public static bool RightStickButton { get { return _gamePadState.IsButtonDown(Buttons.RightStick); } }
        public static Vector2 RightStick { get { return _gamePadState.ThumbSticks.Right; } }
        public static GamePadDPad DPad { get { return _gamePadState.DPad; } }

        private static GamePadDeadZone _deadZone;
        private static GamePadState _gamePadState;
        private static GamePadState _previousGamePadState;
        #endregion

        #region Events
        public static event Events.ButtonPressed APressed;
        public static event Events.ButtonPressed BPressed;
        public static event Events.ButtonPressed XPressed;
        public static event Events.ButtonPressed YPressed;
        public static event Events.ButtonPressed BackPressed;
        public static event Events.ButtonPressed StartPressed;
        public static event Events.ButtonPressed RightBumperPressed;
        public static event Events.ButtonPressed LeftBumperPressed;
        public static event Events.ButtonPressed LeftStickPressed;
        public static event Events.ButtonPressed RightStickPressed;
        public static event Events.ButtonPressed DPadUpPressed;
        public static event Events.ButtonPressed DPadDownPressed;
        public static event Events.ButtonPressed DPadLeftPressed;
        public static event Events.ButtonPressed DPadRightPressed;
        public static event Events.ButtonPressed LeftTriggerPressed;
        public static event Events.ButtonPressed RightTriggerPressed;
        #endregion

        public static void Inititalize(GamePadDeadZone deadZone)
        {
            _deadZone = deadZone;
        }

        public static void Update()
        {
            if (Enabled)
            {
                _gamePadState = GamePad.GetState(PlayerIndex.One, _deadZone);

                #region Events

                if (_gamePadState.IsButtonUp(Buttons.A)
                    && _previousGamePadState.IsButtonDown(Buttons.A)
                    && APressed != null)
                    APressed();

                if (_gamePadState.IsButtonUp(Buttons.B)
                    && _previousGamePadState.IsButtonDown(Buttons.B)
                    && BPressed != null)
                    BPressed();

                if (_gamePadState.IsButtonUp(Buttons.X)
                    && _previousGamePadState.IsButtonDown(Buttons.X)
                    && XPressed != null)
                    XPressed();

                if (_gamePadState.IsButtonUp(Buttons.Y)
                    && _previousGamePadState.IsButtonDown(Buttons.Y)
                    && YPressed != null)
                    YPressed();

                if (_gamePadState.IsButtonUp(Buttons.Back)
                    && _previousGamePadState.IsButtonDown(Buttons.Back)
                    && BackPressed != null)
                    BackPressed();

                if (_gamePadState.IsButtonUp(Buttons.Start)
                    && _previousGamePadState.IsButtonDown(Buttons.Start)
                    && StartPressed != null)
                    StartPressed();

                if (_gamePadState.IsButtonUp(Buttons.RightShoulder)
                    && _previousGamePadState.IsButtonDown(Buttons.RightShoulder)
                    && RightBumperPressed != null)
                    RightBumperPressed();

                if (_gamePadState.IsButtonUp(Buttons.LeftShoulder)
                    && _previousGamePadState.IsButtonDown(Buttons.LeftShoulder)
                    && LeftBumperPressed != null)
                    LeftBumperPressed();

                if (_gamePadState.IsButtonUp(Buttons.RightStick)
                    && _previousGamePadState.IsButtonDown(Buttons.RightStick)
                    && RightStickPressed != null)
                    RightStickPressed();

                if (_gamePadState.IsButtonUp(Buttons.LeftStick)
                    && _previousGamePadState.IsButtonDown(Buttons.LeftStick)
                    && LeftStickPressed != null)
                    LeftStickPressed();

                if (_gamePadState.IsButtonUp(Buttons.DPadLeft)
                    && _previousGamePadState.IsButtonDown(Buttons.DPadLeft)
                    && DPadLeftPressed != null)
                    DPadLeftPressed();

                if (_gamePadState.IsButtonUp(Buttons.DPadRight)
                    && _previousGamePadState.IsButtonDown(Buttons.DPadRight)
                    && DPadRightPressed != null)
                    DPadRightPressed();

                if (_gamePadState.IsButtonUp(Buttons.DPadUp)
                    && _previousGamePadState.IsButtonDown(Buttons.DPadUp)
                    && DPadUpPressed != null)
                    DPadUpPressed();

                if (_gamePadState.IsButtonUp(Buttons.DPadDown)
                    && _previousGamePadState.IsButtonDown(Buttons.DPadDown)
                    && DPadDownPressed != null)
                    DPadDownPressed();

                if (_gamePadState.Triggers.Right >= SettingsManager.ControlSettings.TriggerThreshold
                    && RightTriggerPressed != null)
                    RightTriggerPressed();

                if (_gamePadState.Triggers.Left >= SettingsManager.ControlSettings.TriggerThreshold
                    && LeftTriggerPressed != null)
                    LeftTriggerPressed();
                #endregion

                _previousGamePadState = _gamePadState;
            }
        }
    }
}
