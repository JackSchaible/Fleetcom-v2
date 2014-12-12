using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fleetcom.Library.Content
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ContentManager : GameComponent
    {
        #region Enums
        #endregion

        public readonly Dictionary<string, Texture2D> TextureContent;
        public readonly Dictionary<Keys.Fonts, SpriteFont> FontContent;

        private Game _game;

        public ContentManager(Game game)
            : base(game)
        {
            _game = game;
            TextureContent = new Dictionary<string, Texture2D>();
            FontContent = new Dictionary<Keys.Fonts, SpriteFont>();
        }

        public override void Initialize()
        {
            #region Textures
            #region Ships
            #region Ancient
            TextureContent.Add(Keys.Ships.Ancient.Jumper.Main, _game.Content.Load<Texture2D>("Graphics/Ships/Ancient/Jumper/Jumper"));
            #endregion
            #region Human
            TextureContent.Add(Keys.Ships.Human.F302.Main, _game.Content.Load<Texture2D>("Graphics/Ships/Human/F-302/F-302"));
            TextureContent.Add(Keys.Ships.Human.F302.Engines.Glow, _game.Content.Load<Texture2D>("Graphics/Ships/Human/F-302/Engine Glow"));
            TextureContent.Add(Keys.Ships.Human.F302.Weapons.AimGuides.Missile, _game.Content.Load<Texture2D>("Graphics/Ships/Human/F-302/Missile Aim Guide"));
            TextureContent.Add(Keys.Ships.Human.F302.Weapons.AimGuides.Railgun, _game.Content.Load<Texture2D>("Graphics/Ships/Human/F-302/Railgun Aim Guide"));
            TextureContent.Add(Keys.Ships.Human.F302.Weapons.Projectiles.Railgun_Round, _game.Content.Load<Texture2D>("Graphics/Ships/Human/F-302/Railgun Round"));
            #endregion
            #endregion
#if DEBUG
            TextureContent.Add(Keys.Ships.DebugAimer, _game.Content.Load<Texture2D>("Graphics/Ships/Debug Aimer"));
#endif
            #endregion
            #region Fonts
#if DEBUG
            FontContent.Add(Keys.Fonts.Debug, _game.Content.Load<SpriteFont>("Graphics/Text/Debug"));
#endif
            #endregion
            base.Initialize();
        }
    }
}
