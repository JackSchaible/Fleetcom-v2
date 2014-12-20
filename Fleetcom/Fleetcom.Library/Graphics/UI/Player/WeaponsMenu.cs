using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Fleetcom.Library.Content;
using Fleetcom.Library.Controls;
using Fleetcom.Library.GameObjects.Ships;
using Fleetcom.Library.Graphics.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Events = Fleetcom.Library.GameComponents.Events;

namespace Fleetcom.Library.Graphics.UI.Player
{
    public class WeaponsMenu : IGameObject
    {
        public enum MenuStates
        {
            Opened,
            Opening,
            Closed,
            Closing
        }
        public MenuStates State;
        public event Events.StateChanged<MenuStates> StateChanged;

        private MenuStates _previousMenuState;

        private const int TotalFrames = 30;
        private const int StateChangeDelay = 120;

        private int _delayTimer;
        private int _currentFrame;
        private float _downSpeed;

        
        private Sprite _menuSprite;
        private List<Sprite> _weapons;
        
        public WeaponsMenu(ContentManager manager)
        {
            _menuSprite = new Sprite(manager.TextureContent[Keys.UI.Player.WeaponsMenu], new Vector2(-1, -150), Sprite.OriginModes.TopLeft);
            _weapons = new List<Sprite>();

            State = MenuStates.Closed;
            _previousMenuState = MenuStates.Closed;

            _delayTimer = 0;
            _currentFrame = 0;
            _downSpeed = _menuSprite.Texture.Height / TotalFrames;

            StateChanged += OnStateChanged;
        }

        public void Initialize(Ship ship)
        {
            Controller.RightShoulderHeld += Controller_RightShoudlerHeld;

            foreach (var item in ship.WeaponArrays)
            {
                //TODO: Add icon to weaponarray class
            }
        }
        
        public void Update(GameTime gameTime)
        {
            
            switch (State)
            {
                case MenuStates.Closing:
                    _menuSprite.Position = new Vector2(-1, -((_currentFrame * _downSpeed)));

                    _currentFrame++;

                    if (_menuSprite.Position.Y <= -150)
                    {
                        _menuSprite.Position = new Vector2(-1, -150);

                        _previousMenuState = State;
                        State = MenuStates.Closed;

                        StateChanged(State, _previousMenuState);

                        _currentFrame = 0;
                    }
                    break;

                case MenuStates.Closed:
                    break;

                case MenuStates.Opening:
                    _menuSprite.Position = new Vector2(-1, (_currentFrame * _downSpeed) - _menuSprite.Texture.Height);
                    _currentFrame++;

                    if (_menuSprite.Position.Y >= -1)
                    {
                        _menuSprite.Position = new Vector2(-1, -1);

                        _previousMenuState = State;
                        State = MenuStates.Opened;

                        StateChanged(State, _previousMenuState);

                        _currentFrame = 0;
                    }
                    break;

                case MenuStates.Opened:
                    //Handle weapon switching
                    break;
            }

            if (_delayTimer > 0)
                _delayTimer--;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _menuSprite.Draw(spriteBatch);
        }

        private void OnStateChanged(MenuStates newState, MenuStates oldState)
        {
            switch (newState)
            {
                case MenuStates.Opening:
                case MenuStates.Closing:
                    Controller.RightShoulderHeld -= SwitchState;
                    break;

                case MenuStates.Opened:
                case MenuStates.Closed:
                    Controller.RightShoulderHeld += SwitchState;
                    break;
            }
        }

        private void SwitchState()
        {
            if (_delayTimer != 0)
                return;

            _delayTimer = StateChangeDelay;
            switch (State)
            {
                case MenuStates.Closed:
                    _previousMenuState = State;
                    State = MenuStates.Opening;
                    StateChanged(State, _previousMenuState);
                    break;

                case MenuStates.Opened:
                    _previousMenuState = State;
                    State = MenuStates.Closing;
                    StateChanged(State, _previousMenuState);
                    break;
            }
        }

        private void Controller_RightShoudlerHeld()
        {
            Controller.RightShoulderHeld -= Controller_RightShoudlerHeld;

            _previousMenuState = State;
            State = MenuStates.Opening;

            _delayTimer = StateChangeDelay;

            StateChanged(State, _previousMenuState);
        }
    }
}
