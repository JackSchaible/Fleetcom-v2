using System;
using System.Collections.Generic;
using Fleetcom.Library.Content;
using Fleetcom.Library.Controls;
using Fleetcom.Library.GameObjects.Ships;
using Fleetcom.Library.Graphics.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fleetcom.Library.Graphics.UI.Player
{
    public class WeaponsMenu : IGameObject
    {
        private const int TotalFrames = 30;

        private int _currentFrame;
        private float _downSpeed;

        
        private Sprite _menuSprite;
        private List<Sprite> _weapons;

        private MenuStates _state;

        private enum MenuStates
        {
            Opened,
            Opening,
            Closed,
            Closing
        }

        public WeaponsMenu(ContentManager manager)
        {
            _menuSprite = new Sprite(manager.TextureContent[Keys.UI.Player.WeaponsMenu], new Vector2(-1, -150), Sprite.OriginModes.TopLeft);
            _weapons = new List<Sprite>();

            _state = MenuStates.Closed;

            _currentFrame = 0;
            _downSpeed = (float)(_menuSprite.Texture.Height / (TotalFrames * 0.8));

            Controller.RightShoulderHeld += OpenMenu;
        }

        public void Initialize(Ship ship)
        {
            foreach (var item in ship.WeaponArrays)
            {
                //TODO: Add icon to weaponarray class
            }
        }
        
        public void Update(GameTime gameTime)
        {
            switch (_state)
            {
                case MenuStates.Closing:
                    break;

                case MenuStates.Closed:
                    _currentFrame = 0;
                    break;

                case MenuStates.Opening:
                    //Fix easing
                    if (_currentFrame < (TotalFrames * 0.8))
                        _menuSprite.Position = new Vector2(-1, (_currentFrame * _downSpeed) - _menuSprite.Texture.Height);
                    else
                        _menuSprite.Position = new Vector2(-1, (_downSpeed * (_currentFrame - TotalFrames)) - _menuSprite.Texture.Height);

                    if (_menuSprite.Position.Y >= -1)
                    {
                        _menuSprite.Position = new Vector2(-1, -1);
                        _state = MenuStates.Opened;
                    }
                    
                    _currentFrame++;
                    break;

                case MenuStates.Opened:
                    _currentFrame = 0;
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _menuSprite.Draw(spriteBatch);
        }

        public void OpenMenu()
        {
            _state = MenuStates.Opening;
        }

        public void CloseMenu()
        {
            
        }
    }
}
