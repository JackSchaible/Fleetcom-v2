using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fleetcom.Library.Graphics.Sprites
{
    public class Sprite : IGameObject
    {
        public enum OriginModes
        {
            Center,
            TopLeft
        }

        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Offset { get; set; }
        public Rectangle Source { get; set; }
        public Color Tint { get; set; }
        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }
        public float Scale { get; set; }
        public SpriteEffects SpriteEffects { get; set; }
        public float LayerDepth { get; set; }

        public Vector2 TopLeft
        {
            get
            {
                var result = new Vector2();

                switch (_originMode)
                {
                    case OriginModes.TopLeft:
                        result = Position;
                        break;

                    case OriginModes.Center:
                        result = new Vector2(
                            Position.X - (Texture.Width / 2.0f),
                            Position.Y - (Texture.Height / 2.0f)
                            );
                        break;
                }

                return result;
            }
        }

        private OriginModes _originMode;

        public Sprite()
        {
            
        }

        public Sprite(Texture2D texture, Vector2 position, OriginModes originMode)
        {
            Texture = texture;
            Position = position;
            Source = new Rectangle(0, 0, texture.Width, texture.Height);
            Tint = Color.White;
            Rotation = 0;
            Scale = 1.0f;
            SpriteEffects = SpriteEffects.None;
            LayerDepth = 1.0f;

            switch (originMode)
            {
                case OriginModes.TopLeft:
                    Origin = Vector2.Zero;
                    break;

                case OriginModes.Center:
                    Origin = new Vector2(
                        Texture.Width / 2.0f,
                        Texture.Height / 2.0f);
                    break;                
            }

            _originMode = originMode;
        }
        
        public Sprite(Texture2D texture, Vector2 position, float rotation)
        {
            Texture = texture;
            Position = position;
            Source = new Rectangle(0, 0, texture.Width, texture.Height);
            Tint = Color.White;
            Rotation = rotation;
            Scale = 1.0f;
            SpriteEffects = SpriteEffects.None;
            LayerDepth = 1.0f;
            Origin = new Vector2(
                        Texture.Width / 2.0f,
                        Texture.Height / 2.0f);
        }

        public Sprite(Texture2D texture, Vector2 position, Rectangle source, Color tint, float rotation, Vector2 origin, float scale, SpriteEffects spriteEffects, float layerDepth)
        {
            Texture = texture;
            Position = position;
            Source = source;
            Tint = tint;
            Rotation = rotation;
            Origin = origin;
            Scale = scale;
            SpriteEffects = spriteEffects;
            LayerDepth = layerDepth;
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Source, Color.White, Rotation, Origin, Scale, SpriteEffects, LayerDepth);
        }

        public virtual void Draw(SpriteBatch spriteBatch, float transparency)
        {
            spriteBatch.Draw(Texture, Position, Source, Color.White * transparency, Rotation, Origin, Scale, SpriteEffects, LayerDepth);
        }
    }
}
