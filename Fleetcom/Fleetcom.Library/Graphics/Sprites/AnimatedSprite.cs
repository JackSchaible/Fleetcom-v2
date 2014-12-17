using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fleetcom.Library.Graphics.Sprites
{
    public class AnimatedSprite : Sprite
    {
        public enum AnimationModes
        {
            Continuous,
            PlayOnce,
            StopOnLast
        }

        public int SheetSizeX { get; set; }
        public int SheetSizeY { get; set; }
        public int FrameRate { get; set; }

        public event Events.AnimationFinished AnimationFinished;
        
        private int _frameCount;
        private int _currentFrameX;
        private int _currentFrameY;
        private readonly int _frameSizeX;
        private readonly int _frameSizeY;
        private readonly Vector2 _stretchVector;
        private readonly AnimationModes _mode;

        public AnimatedSprite(Texture2D texture, Vector2 position, OriginModes originMode, AnimationModes mode,
            Vector2 stretchVector, int sheetSizeX, int sheetSizeY, int frameSizeX, int frameSizeY, int frameRate)
            :base (texture, position, originMode)
        {
            SheetSizeX = sheetSizeX;
            SheetSizeY = sheetSizeY;
            FrameRate = frameRate;

            Source = new Rectangle(0, 0, _frameSizeX, _frameSizeY);

            _frameCount = 0;
            _currentFrameX = 0;
            _currentFrameY = 0;
            _frameSizeX = frameSizeX;
            _frameSizeY = frameSizeY;
            _stretchVector = stretchVector;
            _mode = mode;
        }

        public override void Update(GameTime gameTime)
        {
            Console.WriteLine(_currentFrameX);
            if (_frameCount == FrameRate)
            {
                _frameCount = 0;

                Source = new Rectangle(_currentFrameX * _frameSizeX, _currentFrameY * _frameSizeY, _frameSizeX,
                    _frameSizeY);

                _currentFrameX++;

                if (_currentFrameX == SheetSizeX)
                {
                    _currentFrameX = 0;
                    _currentFrameY++;

                    if (_currentFrameY == SheetSizeY)
                        switch (_mode)
                        {
                            case AnimationModes.Continuous:
                                _currentFrameY = 0;
                                break;

                            case AnimationModes.PlayOnce:
                                if (AnimationFinished != null)
                                    AnimationFinished();
                                break;
                        }
                }
            }

            _frameCount++;
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_stretchVector != Vector2.Zero)
                spriteBatch.Draw(Texture, Position, Source, Color.White, Rotation, Origin, _stretchVector, SpriteEffects, LayerDepth);
            else
                spriteBatch.Draw(Texture, Position, Source, Color.White, Rotation, Origin, 1.0f, SpriteEffects, LayerDepth);
        }

        public void ResetSprite()
        {
            _currentFrameX = 0;
            _currentFrameY = 0;
        }
    }
}