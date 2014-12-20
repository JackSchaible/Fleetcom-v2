using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fleetcom.GameComponents
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class InGame : DrawableGameComponent
    {
        private SpriteBatch _spriteBatch;
        private Player _player;
        private Game1 _game;

        public InGame(Game game)
            : base(game)
        {
            _game = (Game1)game;
            _player = new Player(_game);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            _spriteBatch = new SpriteBatch(_game.GraphicsDevice);
            _player.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _player.LoadContent();

            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            _player.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            _player.Draw(_spriteBatch);

            _spriteBatch.End();
        }
    }
}
