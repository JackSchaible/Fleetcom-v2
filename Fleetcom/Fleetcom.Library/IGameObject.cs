using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fleetcom.Library
{
    public interface IGameObject
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}