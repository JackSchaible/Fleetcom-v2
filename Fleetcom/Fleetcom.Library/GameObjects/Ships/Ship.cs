using System;
using System.Collections.Generic;
using System.Linq;
using Fleetcom.Library.Controls;
using Fleetcom.Library.Graphics.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fleetcom.Library.GameObjects.Ships
{
    public abstract class Ship : IGameObject
    {
        #region Enums
        public enum Directions
        {
            Left,
            Right,
            Stop
        }
        #endregion

        #region Behavior Variables
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        #endregion
        #region Ship Performance
        #region Rotative Motion
        public Directions CurrentDirection { get; set; }
        public float CurrentMaxTurnRate { get; set; }
        public float MaxTurnRate { get; set; }

        protected float TurnRateAcceleration { get; set; }

        private float _currentTurnRate;
        #endregion
        #region Forward Motion
        public float MaxSpeed { get; protected set; }
        public float TargetSpeed { get; set; }

        protected float AccelerationRate { get; set; }

        private float _currentSpeed { get; set; }
        #endregion
        #region Strafing Motion
        public Directions StrafeDirection { get; set; }
        public float MaxManeuveringSpeed { get; protected set; }
        public float TargetManeuveringSpeed { get; set; }

        protected float ManeuveringAccelerationRate { get; set; }

        private Directions _lastStrafeDirection;
        private float _currentManeuveringSpeed { get; set; }
        private Vector2 _currentManeuveringVelocity { get; set; }
        #endregion
        #endregion

        protected Sprite MainShip { get; set; }
        protected List<Sprite> Engines { get; set; }

        #region WeaponArrays
        public int CurrentWeaponArray { get; set; }
        public int WeaponArraySize { get; set; }
        public List<WeaponArray> WeaponArrays { get; set; }
        #endregion
        #region Animation
        private float _forwardMoveSpeed;
        #endregion
        protected List<Sprite> AuxiliarySystems { get; set; }

        private GameTime CurrentGameTime { get; set; }

        public void Initialize()
        {
            CurrentWeaponArray = 0;

            if (WeaponArrays.Any())
                WeaponArrays[CurrentWeaponArray].IsSelected = true;

            foreach(var weaponArray in WeaponArrays)
                weaponArray.Initialize();
        }

        public virtual void Update(GameTime gameTime)
        {
            CurrentGameTime = gameTime;

            Strafe();
            Rotate();
            Move();

            MainShip.Position = Position;
            MainShip.Rotation = Rotation;

            if (MainShip is AnimatedSprite)
                MainShip.Update(gameTime);


            foreach (var engine in Engines)
            {
                engine.Position = Position;
                engine.Rotation = Rotation;

                if (engine is AnimatedSprite)
                    engine.Update(gameTime);
            }

            foreach (var weaponArray in WeaponArrays)
            {
                weaponArray.Position = Position;
                weaponArray.Rotation = Rotation;

                weaponArray.Update(gameTime);
            }

            foreach (var auxSystem in AuxiliarySystems)
            {
                auxSystem.Position = Position;
                auxSystem.Rotation = Rotation;

                if (auxSystem is AnimatedSprite)
                    auxSystem.Update(gameTime);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            MainShip.Draw(spriteBatch);

            var engineOpacity = (_forwardMoveSpeed/10);

            foreach (var engine in Engines)
                engine.Draw(spriteBatch, engineOpacity);

            foreach (var weapon in WeaponArrays)
                weapon.Draw(spriteBatch);

            foreach (var auxSystem in AuxiliarySystems)
                auxSystem.Draw(spriteBatch);
        }

        public void FireWeapons()
        {
            WeaponArrays[CurrentWeaponArray].Fire(CurrentGameTime, MainShip.Position, Rotation);
        }

        public void ChangeWeapons(int weaponIndex)
        {
            if (weaponIndex > WeaponArraySize)
                return;

            WeaponArrays[CurrentWeaponArray].IsSelected = false;
            WeaponArrays[weaponIndex].IsSelected = true;
            CurrentWeaponArray = weaponIndex;
        }

        private void Strafe()
        {
            if (TargetManeuveringSpeed == 0 || StrafeDirection == Directions.Stop)
            {
                if (_currentManeuveringSpeed > 0)
                    if (_currentManeuveringSpeed - ManeuveringAccelerationRate < 0)
                        _currentManeuveringSpeed = 0;
                    else
                        _currentManeuveringSpeed -= ManeuveringAccelerationRate;
                else if (_currentManeuveringSpeed < 0)
                    if (_currentManeuveringSpeed + ManeuveringAccelerationRate > 0)
                        _currentManeuveringSpeed = 0;
                    else
                        _currentManeuveringSpeed += ManeuveringAccelerationRate;
            }
            else
            {
                if (_currentManeuveringSpeed < TargetManeuveringSpeed)
                {
                    if (_currentManeuveringSpeed + ManeuveringAccelerationRate > TargetManeuveringSpeed)
                        _currentManeuveringSpeed = TargetManeuveringSpeed;
                    else
                        _currentManeuveringSpeed += ManeuveringAccelerationRate;
                }
                else
                {
                    if (_currentManeuveringSpeed - ManeuveringAccelerationRate < TargetManeuveringSpeed)
                        _currentManeuveringSpeed = TargetManeuveringSpeed;
                    else
                        _currentManeuveringSpeed -= ManeuveringAccelerationRate;
                }
            }

            var velocity = new Vector2((float)Math.Cos(Rotation),
                    (float)Math.Sin(Rotation));
            velocity.Normalize();

            Position += velocity * _currentManeuveringSpeed;

            if (StrafeDirection != Directions.Stop)
                _lastStrafeDirection = StrafeDirection;
        }

        private void Rotate()
        {
            if (CurrentDirection == Directions.Stop)
            {
                if (_currentTurnRate == 0)
                    return;

                if (_currentTurnRate > 0)
                    if (_currentTurnRate - TurnRateAcceleration < 0)
                        _currentTurnRate = 0;
                    else
                        _currentTurnRate -= TurnRateAcceleration;
                else
                    if (_currentTurnRate + TurnRateAcceleration > 0)
                        _currentTurnRate = 0;
                    else
                        _currentTurnRate += TurnRateAcceleration;
            }
            else
            {
                if (CurrentDirection == Directions.Right)
                {
                    if (_currentTurnRate < CurrentMaxTurnRate)
                        if (_currentTurnRate + TurnRateAcceleration > CurrentMaxTurnRate)
                            _currentTurnRate = CurrentMaxTurnRate;
                        else
                            _currentTurnRate += TurnRateAcceleration;
                }
                else if (CurrentDirection == Directions.Left)
                {
                    if (_currentTurnRate > -CurrentMaxTurnRate)
                        if (_currentTurnRate - TurnRateAcceleration < -CurrentMaxTurnRate)
                            _currentTurnRate = -CurrentMaxTurnRate;
                        else
                            _currentTurnRate -= TurnRateAcceleration;
                }
            }

            Rotation += _currentTurnRate;
        }

        private void Move()
        {
            if (TargetSpeed == 0)
            {
                if (_currentSpeed > 0)
                    _currentSpeed -= AccelerationRate;
                else if (_currentSpeed - AccelerationRate < 0)
                    _currentSpeed = 0;
            }
            else
            {
                if (_currentSpeed < TargetSpeed)
                    if (_currentSpeed + AccelerationRate > TargetSpeed)
                        _currentSpeed = TargetSpeed;
                    else
                        _currentSpeed += AccelerationRate;
            }

            var rot = Rotation - (Math.PI / 2);
            var velocity = new Vector2((float)Math.Cos(rot),
                (float)Math.Sin(rot));
            velocity.Normalize();

            _forwardMoveSpeed = (velocity * _currentSpeed).Length();

            if (_forwardMoveSpeed > 10)
                _forwardMoveSpeed = 10;

            Position += velocity * _currentSpeed;
        }
    }
}