using System.Collections.Generic;
using Fleetcom.Library.Content;
using Fleetcom.Library.Graphics.Sprites;
using Microsoft.Xna.Framework;

namespace Fleetcom.Library.GameObjects.Ships.Ancient
{
    public class Jumper : Ship
    {
        //private Sprite PortEngineSprite { get; set; }
        //private Sprite StarboardEngineSprite { get; set; }
       
        public Jumper(ContentManager content, Vector2 position)
        {
            MainShip = new Sprite(content.TextureContent[Keys.Ships.Ancient.Jumper.Main], position, Sprite.OriginModes.Center);
            Position = position;

            //TODO: Get offsets
            var starboardOffset = position;
            var portOffset = position;

            //StarboardEngineSprite = new Sprite(Content.Graphics.Textures[Content.Graphics.TextureNames.Ships_Jumper], starboardOffset);
            //PortEngineSprite = new Sprite(Content.Graphics.Textures[Content.Graphics.TextureNames.Ships_Jumper], portOffset);

            Engines = new List<Sprite>();
            //Engines.Add(StarboardEngineSprite);
            //Engines.Add(PortEngineSprite);

            WeaponArrays = new List<WeaponArray>();

            AuxiliarySystems = new List<Sprite>();

            #region Performance 
            MaxTurnRate = ShipStats.Jumper.MaxTurnRate;
            TurnRateAcceleration = ShipStats.Jumper.TurnRateAcceleration;

            MaxSpeed = ShipStats.Jumper.MaxSpeed;
            AccelerationRate = ShipStats.Jumper.AccelerationRate;

            MaxManeuveringSpeed = ShipStats.Jumper.MaxManeuveringSpeed;
            ManeuveringAccelerationRate = ShipStats.Jumper.ManeuveringSpeedAcceleration;
            #endregion
        }
    }
}
