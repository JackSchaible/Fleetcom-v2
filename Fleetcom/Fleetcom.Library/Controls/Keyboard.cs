using Microsoft.Xna.Framework.Input;

namespace Fleetcom.Library.Controls
{
    public static class Keyboard
    {
        #region Properties
        public static bool Enabled { get; set; }
        public static bool Esc { get { return _keyboardState.IsKeyDown(Keys.Escape); } }
        #endregion

        #region Events

        public static Events.ButtonPressed EscapePressed;
        #endregion

        private static KeyboardState _keyboardState;
        private static KeyboardState _previousKeyboardState;

        public static void Update()
        {
            if (Enabled)
            {
                _keyboardState = Microsoft.Xna.Framework.Input.Keyboard.GetState();

                if (_keyboardState.IsKeyDown(Keys.Escape) && _previousKeyboardState.IsKeyUp(Keys.Escape)
                    && EscapePressed != null)
                    EscapePressed();

                _previousKeyboardState = _keyboardState;
            }
        }
    }
}
