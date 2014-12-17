using System;
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
    public class Player : DrawableGameComponent
    {
        public enum PlayerState
        {
            Flying,
            WeaponsMenu
        }
        public event Events.StateChanged<PlayerState> StateChanged;
        
        private Ship _playerShip;
        private WeaponsMenu _menu;

        private SpriteBatch _spriteBatch;
        private Game _game;
        
        private PlayerState _currentPlayerState;
        private PlayerState _lastPlayerState;
        
        public Player(Game game)
            : base(game)
        {
            _game = game;
            _currentPlayerState = PlayerState.Flying;
        }

        public override void Initialize()
        {
            _spriteBatch = new SpriteBatch(_game.GraphicsDevice);

            Controller.RightShoulderHeld += OpenWeaponMenu;
            Controller.RightShoulderPressed += ControllerRightShoulderPressed;
            Controller.RightTriggerPressed += Controller_RightTriggerPressed;

            base.Initialize();
        }


        protected override void LoadContent()
        {
            //TODO: Replace with getting player's currently selected ship
            _playerShip = new F302(((Game1)Game).ContentManager, new Vector2(300, 300));
            _playerShip.Initialize();

            _menu = new WeaponsMenu(((Game1)Game).ContentManager);            

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            switch (_currentPlayerState)
            {
                case PlayerState.Flying:
                    GetRotation();
                    GetMovement();
                    GetStrafe();
                    break;

                case PlayerState.WeaponsMenu:
                    GetRotation();
                    GetMovement();
                    _menu.Update(gameTime);
                    break;
            }

            _playerShip.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            _playerShip.Draw(_spriteBatch);

            switch (_currentPlayerState)
            {
                case PlayerState.WeaponsMenu:
                    _menu.Draw(_spriteBatch);
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
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
        private void OpenWeaponMenu()
        {
            _lastPlayerState = _currentPlayerState;
            _currentPlayerState = PlayerState.WeaponsMenu;

            if (StateChanged != null)
                StateChanged(_currentPlayerState, _lastPlayerState);

            _menu.OpenMenu();
        }
        private void ControllerRightShoulderPressed()
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
        #endregion
    }
}