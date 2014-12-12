using System;
using Fleetcom.Library.Controls;
using Fleetcom.Library.GameObjects.Ships;
using Fleetcom.Library.GameObjects.Ships.Ancient;
using Fleetcom.Library.GameObjects.Ships.Human;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
// ReSharper disable CompareOfFloatsByEqualityOperator


namespace Fleetcom.GameComponents
{
    public class Player : DrawableGameComponent
    {
        private Ship _playerShip;
        private SpriteBatch _spriteBatch;
        private Game _game;

        public Player(Game game)
            : base(game)
        {
            _game = game;
        }

        public override void Initialize()
        {
            _spriteBatch = new SpriteBatch(_game.GraphicsDevice);
            //TODO: Replace with getting player's currently selected ship

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _playerShip = new F302(((Game1)Game).ContentManager, new Vector2(300, 300));
            _playerShip.Initialize();

            Controller.RightBumperPressed += () =>
            {
                var nextWeaponArray = _playerShip.CurrentWeaponArray + 1;

                if (nextWeaponArray > _playerShip.WeaponArraySize)
                    nextWeaponArray = 0;

                _playerShip.ChangeWeapons(nextWeaponArray);
            };

            Controller.RightTriggerPressed += () => _playerShip.FireWeapons();

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            GetRotation();
            GetStrafe();
            GetMovement();

            _playerShip.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            _playerShip.Draw(_spriteBatch);

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
    }
}