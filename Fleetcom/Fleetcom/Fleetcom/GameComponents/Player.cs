using System;
using Fleetcom.Library;
using Fleetcom.Library.Content;
using Fleetcom.Library.Controls;
using Fleetcom.Library.GameObjects.Ships;
using Fleetcom.Library.GameObjects.Ships.Human;
using Fleetcom.Library.Graphics.Sprites;
using Fleetcom.Library.Graphics.UI.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Events = Fleetcom.Library.GameComponents.Events;

// ReSharper disable CompareOfFloatsByEqualityOperator


namespace Fleetcom.GameComponents
{
    public class Player : IGameObject
    {
        public enum PlayerState
        {
            Flying,
            WeaponsMenu
        }
        public event Events.StateChanged<PlayerState> StateChanged;
        
        private Ship _playerShip;
        private WeaponsMenu _menu;

        private Game1 _game;
        
        private PlayerState _currentPlayerState;
        private PlayerState _lastPlayerState;
        
        public Player(Game1 game)
        {
            _game = game;
            _currentPlayerState = PlayerState.Flying;
        }

        public void Initialize()
        {
            Controller.RightShoulderPressed += Controller_RightShoulderPressed;
            Controller.RightTriggerPressed += Controller_RightTriggerPressed;
        }


        public void LoadContent()
        {
            //TODO: Replace with getting player's currently selected ship
            _playerShip = new F302(_game.ContentManager, new Vector2(300, 300));
            _playerShip.Initialize();

            _menu = new WeaponsMenu(_game.ContentManager);            
            _menu.Initialize(_playerShip);
            _menu.StateChanged += Menu_StateChanged;
        }

        public void Update(GameTime gameTime)
        {
            switch (_menu.State)
            {
                case WeaponsMenu.MenuStates.Closing:
                case WeaponsMenu.MenuStates.Closed:
                    GetRotation();
                    GetMovement();
                    GetStrafe();
                    break;

                case WeaponsMenu.MenuStates.Opening:
                case WeaponsMenu.MenuStates.Opened:
                    GetRotation();
                    GetMovement();
                    break;
            }

            _menu.Update(gameTime);
            _playerShip.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _playerShip.Draw(spriteBatch);
            _menu.Draw(spriteBatch);
        }

        private void GetRotation()
        {
            if (Controller.LeftStick.X == 0)
                _playerShip.CurrentDirection = Ship.Directions.Stop;
            else
            {
                var leftStickX = Controller.LeftStick.X;
                var length = Math.Sqrt((leftStickX * leftStickX) + (Controller.LeftStick.Y * Controller.LeftStick.Y));
                var max = Math.Max(Math.Abs(leftStickX), Math.Abs(Controller.LeftStick.Y));

                if (max > 0)
                    leftStickX *= (float)length / max;

                _playerShip.CurrentMaxTurnRate = Math.Abs(leftStickX) * _playerShip.MaxTurnRate;

                if (Controller.LeftStick.X < 0)
                    _playerShip.CurrentDirection = Ship.Directions.Left;
                else if (Controller.LeftStick.X > 0)
                    _playerShip.CurrentDirection = Ship.Directions.Right;
            }
        }
        private void GetStrafe()
        {
            if (Controller.RightStick.X == 0)
                _playerShip.StrafeDirection = Ship.Directions.Stop;
            else
            {
                var rightStickX = Controller.RightStick.X;
                var length = Math.Sqrt((rightStickX * rightStickX) + (Controller.RightStick.Y * Controller.RightStick.Y));
                var max = Math.Max(Math.Abs(rightStickX), Math.Abs(Controller.RightStick.Y));

                if (max > 0)
                    rightStickX *= (float)length / max;

                _playerShip.TargetManeuveringSpeed = rightStickX * _playerShip.MaxManeuveringSpeed;

                if (rightStickX == 0)
                    _playerShip.StrafeDirection = Ship.Directions.Stop;
                else if (rightStickX < 0)
                    _playerShip.StrafeDirection = Ship.Directions.Left;
                else if (rightStickX > 0)
                    _playerShip.StrafeDirection = Ship.Directions.Right;
            }
        }
        private void GetMovement()
        {
            if (Controller.LeftStick.Y == 0)
                _playerShip.TargetSpeed = 0;
            else
            {
                var leftStickY = Controller.LeftStick.Y;
                var length = Math.Sqrt((Controller.LeftStick.X * Controller.LeftStick.X) + (Controller.LeftStick.Y * Controller.LeftStick.Y));
                var max = Math.Max(Math.Abs(leftStickY), Math.Abs(Controller.LeftStick.Y));

                if (max > 0)
                    leftStickY *= (float)length / max;

                _playerShip.TargetSpeed = leftStickY * _playerShip.MaxSpeed;
            }
        }

        #region Event Handlers
        private void Controller_RightShoulderPressed()
        {
            var nextWeaponArray = _playerShip.CurrentWeaponArray + 1;

            if (nextWeaponArray > _playerShip.WeaponArraySize)
                nextWeaponArray = 0;

            _playerShip.ChangeWeapons(nextWeaponArray);
        }
        private void Controller_RightTriggerPressed()
        {
            _playerShip.FireWeapons();
        }
        private void Menu_StateChanged(WeaponsMenu.MenuStates newState, WeaponsMenu.MenuStates oldState)
        {
            switch (newState)
            {
                case WeaponsMenu.MenuStates.Opened:
                    Console.WriteLine("Player : Unsubscribing from right pressed event");
                    Controller.RightShoulderPressed -= Controller_RightShoulderPressed;
                    break;

                case WeaponsMenu.MenuStates.Closing:
                    Controller.RightShoulderPressed += Controller_RightShoulderPressed;
                    break;
            }
        }
        #endregion
    }
}