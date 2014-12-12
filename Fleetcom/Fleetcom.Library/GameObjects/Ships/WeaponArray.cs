using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Fleetcom.Library.Graphics.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fleetcom.Library.GameObjects.Ships
{
    public class WeaponArray
    {
        public enum FireMethods
        {    
            All_At_Once,
            One_At_A_Time
        }

        public float Rotation { get; set; }
        public Vector2 Position { get; set; }
        public List<Weapon> Weapons { get; set; }
        public Sprite AimGuide { get; set; }
        public FireMethods FireMethod { get; set; }
        public TimeSpan FireRate { get; set; }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                foreach (var item in Weapons)
                    item.IsSelected = value;

                _isSelected = value;
            }
        }
        private bool _isSelected;

        private int _currentWeapon;
        private int _weaponArraySize;
        private TimeSpan _lastFiredGameTime;
        public WeaponArray(Sprite aimGuide, FireMethods fireMethod)
        {
            _isSelected = false;
            Weapons = new List<Weapon>();
            AimGuide = aimGuide;
            FireMethod = fireMethod;
        }

        public void Initialize()
        {
            if (Weapons.Any())
                _currentWeapon = 0;

            _weaponArraySize = Weapons.Count;
            _lastFiredGameTime = new TimeSpan(0);
        }

        public void Fire(GameTime gameTime, Vector2 shipPosition, float shipRotation)
        {
            if (!_isSelected)
                return;

            switch (FireMethod)
            {
                case FireMethods.All_At_Once:
                    foreach (var weapon in Weapons)
                            weapon.Fire(gameTime, shipPosition, shipRotation);
                    break;

                case FireMethods.One_At_A_Time:
                    if (_lastFiredGameTime - gameTime.TotalGameTime > FireRate)
                    {
                        _lastFiredGameTime = gameTime.TotalGameTime;

                        if (_currentWeapon + 1 > _weaponArraySize)
                            _currentWeapon = 0;
                        else
                            _currentWeapon += 1;
                    }

                    Weapons[_currentWeapon].Fire(gameTime, shipPosition, shipRotation);
                    break;

                default:
                    return;

            }
        }

        public void Update(GameTime gameTime)
        {
            if (_isSelected)
            {
                AimGuide.Position = Position;
                AimGuide.Rotation = Rotation;

                AimGuide.Update(gameTime);
            }

            foreach (var weapon in Weapons)
                weapon.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_isSelected)
                AimGuide.Draw(spriteBatch);

            foreach(var weapon in Weapons)
                weapon.Draw(spriteBatch);
        }
    }
}
