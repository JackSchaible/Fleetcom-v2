using System;
using System.Collections.Generic;
using System.Linq;
using Fleetcom.Library.Graphics.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fleetcom.Library.GameObjects.Ships
{
    public class Bullet : IProjectile
    {
        public Sprite Sprite { get; set; }
        public List<ProjectileExpiryOptions.ProjectileExpiryOption> ExpiryOptions { get; set; }
        public bool ShouldRemove { get; set; }

        private readonly TimeSpan _timeToRemoveAt;
        private readonly float _distanceToRemoveAt;
        private readonly Vector2 _vector;

        #region Constructors
        public Bullet(Texture2D texture, Vector2 position, float rotation, float speed)
        {
            Sprite = new Sprite(texture, position, rotation);
            ShouldRemove = false;

            var rot = rotation - (Math.PI / 2);
            var velocity = new Vector2((float)Math.Cos(rot),
                (float)Math.Sin(rot));
            velocity.Normalize();
            _vector = velocity * speed;

            ExpiryOptions = new List<ProjectileExpiryOptions.ProjectileExpiryOption>();
            ExpiryOptions.Add(new ProjectileExpiryOptions.Distance(230));
        }
        #endregion

        public void Update(GameTime gameTime)
        {
            ShouldRemove = ExpiryOptions.Any(option => option.ShouldRemove);

            Sprite.Position += _vector;

            Sprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch);
        }
    }
}
