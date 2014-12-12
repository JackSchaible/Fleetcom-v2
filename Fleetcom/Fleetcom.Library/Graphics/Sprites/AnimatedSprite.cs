using Microsoft.Xna.Framework;

namespace Fleetcom.Library.Graphics.Sprites
{
    public class AnimatedSprite : Sprite
    {
        public int SheetSizeX { get; set; }
        public int SheetSizeY { get; set; }
        public int FrameRate { get; set; }
        
        private int _frameCount;
        private int _currentFrameX;
        private int _currentFrameY;

        public AnimatedSprite(int sheetSizeX, int sheetSizeY, int frameRate)
        {
            SheetSizeX = sheetSizeX;
            SheetSizeY = sheetSizeY;
            FrameRate = frameRate;

            Source = new Rectangle(0, 0, sheetSizeX, sheetSizeY);

            _frameCount = 0;
            _currentFrameX = 0;
            _currentFrameY = 0;
        }

        public override void Update(GameTime gameTime)
        {
            if (_frameCount == FrameRate)
            {
                _frameCount = 0;

                Source = new Rectangle(_currentFrameX*Source.Width, _currentFrameY*Source.Height, Source.Width,
                    Source.Height);

                _currentFrameX++;

                if (_currentFrameX == SheetSizeX)
                {
                    _currentFrameX = 0;
                    _currentFrameY++;

                    if (_currentFrameY == SheetSizeY)
                        _currentFrameY = 0;
                }
            }

            _frameCount++;
            base.Update(gameTime);
        }
    }
}