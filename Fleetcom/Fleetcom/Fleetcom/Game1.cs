using Fleetcom.GameComponents;
using Fleetcom.Library.Content;
using Fleetcom.Library.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Keyboard = Fleetcom.Library.Controls.Keyboard;
using Keys = Fleetcom.Library.Content.Keys;

namespace Fleetcom
{
    public class Game1 : Game
    {
        #region Properties
        #region GameComponents

        public readonly ContentManager ContentManager;
        private InGame _inGameComponent;
        #endregion

        private GraphicsDeviceManager _graphicsManager;
        private GameStates _gameState;
#if DEBUG
        private SpriteBatch _spriteBatch;
#endif
        #endregion

        public Game1()
        {
            Content.RootDirectory = "Content";

            _graphicsManager = new GraphicsDeviceManager(this);

            _gameState = GameStates.InGanme;

            _inGameComponent = new InGame(this);
            ContentManager = new ContentManager(this);

            Components.Add(ContentManager);
            Components.Add(_inGameComponent);

            //_graphicsManager.PreferredBackBufferWidth = 1920;
            //_graphicsManager.PreferredBackBufferHeight = 1080;
            //_graphicsManager.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            Controller.Enabled = true;
            Controller.BackPressed += Exit;
            Controller.Inititalize(GamePadDeadZone.IndependentAxes);

            Keyboard.Enabled = true;
            Keyboard.EscapePressed += Exit;

            _inGameComponent.Enabled = true;
            _inGameComponent.Visible = true;


#if DEBUG
            _spriteBatch = new SpriteBatch(GraphicsDevice);
#endif
            base.Initialize();
        }

        protected override void LoadContent()
        {
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            Controller.Update(gameTime);
            Keyboard.Update();
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
#if DEBUG
            _spriteBatch.Begin();
            _spriteBatch.DrawString(ContentManager.FontContent[Keys.Fonts.Debug], (1 / gameTime.ElapsedGameTime.TotalSeconds).ToString(),
                Vector2.Zero, Color.Red);
            _spriteBatch.End();
#endif
            base.Draw(gameTime);
        }

        internal void ChangeState(GameStates newGameState)
        {
            switch (newGameState)
            {
                case GameStates.InGanme:
                    _inGameComponent.Enabled = true;
                    _inGameComponent.Visible = true;
                    break;
            }
        }
    }
}
