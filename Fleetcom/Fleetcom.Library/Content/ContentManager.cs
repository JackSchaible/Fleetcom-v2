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
#if DEBUG
            TextureContent.Add(Keys.Ships.DebugAimer, _game.Content.Load<Texture2D>("Graphics/Ships/Debug Aimer"));
#endif

            #endregion
            #region UI
            #region Player
            TextureContent.Add(Keys.UI.Player.WeaponsMenu, _game.Content.Load<Texture2D>("Graphics/UI/Player/WeaponsMenu"));
            #endregion
            #region Input
            #region Controller
            TextureContent.Add(Keys.UI.Input.Controller.A, _game.Content.Load<Texture2D>("Graphics/UI/Input/Controller/360_A"));
            TextureContent.Add(Keys.UI.Input.Controller.B, _game.Content.Load<Texture2D>("Graphics/UI/Input/Controller/360_B"));
            TextureContent.Add(Keys.UI.Input.Controller.X, _game.Content.Load<Texture2D>("Graphics/UI/Input/Controller/360_X"));
            TextureContent.Add(Keys.UI.Input.Controller.Y, _game.Content.Load<Texture2D>("Graphics/UI/Input/Controller/360_Y"));
            TextureContent.Add(Keys.UI.Input.Controller.Back, _game.Content.Load<Texture2D>("Graphics/UI/Input/Controller/360_Back_Alt"));
            TextureContent.Add(Keys.UI.Input.Controller.Start, _game.Content.Load<Texture2D>("Graphics/UI/Input/Controller/360_Start_Alt"));
            TextureContent.Add(Keys.UI.Input.Controller.LeftShoulder, _game.Content.Load<Texture2D>("Graphics/UI/Input/Controller/360_LB"));
            TextureContent.Add(Keys.UI.Input.Controller.LeftTrigger, _game.Content.Load<Texture2D>("Graphics/UI/Input/Controller/360_LT"));
            TextureContent.Add(Keys.UI.Input.Controller.LeftStick, _game.Content.Load<Texture2D>("Graphics/UI/Input/Controller/360_Left_Stick"));
            TextureContent.Add(Keys.UI.Input.Controller.RightShoulder, _game.Content.Load<Texture2D>("Graphics/UI/Input/Controller/360_RB"));
            TextureContent.Add(Keys.UI.Input.Controller.RightTrigger, _game.Content.Load<Texture2D>("Graphics/UI/Input/Controller/360_RT"));
            TextureContent.Add(Keys.UI.Input.Controller.RightStick, _game.Content.Load<Texture2D>("Graphics/UI/Input/Controller/360_Right_Stick"));
            TextureContent.Add(Keys.UI.Input.Controller.DPadUp, _game.Content.Load<Texture2D>("Graphics/UI/Input/Controller/360_Dpad_Up"));
            TextureContent.Add(Keys.UI.Input.Controller.DPadDown, _game.Content.Load<Texture2D>("Graphics/UI/Input/Controller/360_Dpad_Down"));
            TextureContent.Add(Keys.UI.Input.Controller.DPadLeft, _game.Content.Load<Texture2D>("Graphics/UI/Input/Controller/360_Dpad_Left"));
            TextureContent.Add(Keys.UI.Input.Controller.DPadRight, _game.Content.Load<Texture2D>("Graphics/UI/Input/Controller/360_Dpad_Right"));
            #endregion
            #endregion
            #endregion
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
