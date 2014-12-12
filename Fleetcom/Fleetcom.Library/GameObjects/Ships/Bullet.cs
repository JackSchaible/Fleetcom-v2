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
        public Bullet(Texture2D texture, Vector2 position, Vector2 vector)
        {
            Sprite = new Sprite(texture, position, Sprite.OriginModes.Center);
            ShouldRemove = false;

            _vector = vector;
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
