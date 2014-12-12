using System;
using System.Collections.Generic;
using Fleetcom.Library.Graphics.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fleetcom.Library.GameObjects.Ships
{
    public class Weapon
    {
        public enum ProjectileTypes
        {
            Bullet,
            Missile
        }



        public Vector2 OffsetFromShip { get; set; }
        public bool IsSelected { get; set; }
        public Sprite WeaponSprite { get; set; }
        public Texture2D Projectile { get; set; }
        public ProjectileTypes ProjectileType { get; set; }

        private List<IGameObject> _projectiles;
        private readonly TimeSpan _fireRate;
        private TimeSpan _lastFiredOn;
        public Weapon(bool isSelected, Sprite weaponSprite, Vector2 offsetFromShip, Texture2D projectile, float speed, ProjectileTypes projectileType, TimeSpan fireRate)
        {
            IsSelected = isSelected;
            WeaponSprite = weaponSprite;
            OffsetFromShip = offsetFromShip;
            Projectile = projectile;
            ProjectileType = projectileType;

            _fireRate = fireRate;
            _lastFiredOn = new TimeSpan(0);
            _projectiles = new List<IGameObject>();
        }

        public void Fire(GameTime gameTime, Vector2 shipPosition, float shipRotation)
        {
            if (gameTime.TotalGameTime - _lastFiredOn < _fireRate) return;

            var pos = Vector2.Transform(OffsetFromShip, Matrix.CreateRotationZ(shipRotation));
            pos += shipPosition;

            switch (ProjectileType)
            {
                case ProjectileTypes.Bullet:
                    _projectiles.Add(new Sprite(Projectile, pos, shipRotation));
                    break;
            }

            _lastFiredOn = gameTime.TotalGameTime;
        }

        public void Update(GameTime gameTime)
        {
            if (WeaponSprite != null)
                WeaponSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (WeaponSprite != null)
                WeaponSprite.Draw(spriteBatch);

            foreach(var projectile in _projectiles)
                projectile.Draw(spriteBatch);
        }
    }
}
