using System;
using System.Collections.Generic;
using Fleetcom.Library.Content;
using Fleetcom.Library.Graphics.Sprites;
using Microsoft.Xna.Framework;

namespace Fleetcom.Library.GameObjects.Ships.Human
{
    public class F302 : Ship
    {
        public F302(ContentManager content, Vector2 position)
        {
            MainShip = new Sprite(content.TextureContent[Keys.Ships.Human.F302.Main], position, Sprite.OriginModes.Center);
            Position = position;

            Engines = new List<Sprite>();
            WeaponArrays = new List<WeaponArray>();
            AuxiliarySystems = new List<Sprite>();

            #region WeaponArrays
            var missileAimGuide = new Sprite(content.TextureContent[Keys.Ships.Human.F302.Weapons.AimGuides.Missile],
                position, Sprite.OriginModes.Center);
            var missiles = new WeaponArray(missileAimGuide, WeaponArray.FireMethods.One_At_A_Time);
            missiles.Weapons.Add(new Weapon(false, null, Vector2.Zero, null, ShipStats.F302.Weapons.Missiles.ProjectileSpeed,
                Weapon.ProjectileTypes.Missile, ShipStats.F302.Weapons.Missiles.FireRate));
            missiles.Weapons.Add(new Weapon(false, null, Vector2.Zero, null, ShipStats.F302.Weapons.Missiles.ProjectileSpeed,
                Weapon.ProjectileTypes.Missile, ShipStats.F302.Weapons.Missiles.FireRate));
            missiles.FireRate = new TimeSpan(0, 0, 0, 1);
            WeaponArrays.Add(missiles);

            var railgunAimGuide = new Sprite(content.TextureContent[Keys.Ships.Human.F302.Weapons.AimGuides.Railgun],
                position, Sprite.OriginModes.Center);
            var railguns = new WeaponArray(railgunAimGuide, WeaponArray.FireMethods.All_At_Once);

            railguns.Weapons.Add(new Weapon(false, null, new Vector2(1,-11),
                content.TextureContent[Keys.Ships.Human.F302.Weapons.Projectiles.Railgun_Round], ShipStats.F302.Weapons.Railgun.ProjectileSpeed,
                Weapon.ProjectileTypes.Bullet, ShipStats.F302.Weapons.Railgun.FireRate));
            railguns.Weapons.Add(new Weapon(false, null, new Vector2(1,-11),
                content.TextureContent[Keys.Ships.Human.F302.Weapons.Projectiles.Railgun_Round], ShipStats.F302.Weapons.Railgun.ProjectileSpeed,
                Weapon.ProjectileTypes.Bullet, ShipStats.F302.Weapons.Railgun.FireRate));

            WeaponArrays.Add(railguns);
            railguns.FireRate = new TimeSpan(0, 0, 0, 0, 10);
            WeaponArraySize = 1;
            #endregion

            #region Performance
            MaxTurnRate = ShipStats.F302.MaxTurnRate;
            TurnRateAcceleration = ShipStats.F302.TurnRateAcceleration;

            MaxSpeed = ShipStats.F302.MaxSpeed;
            AccelerationRate = ShipStats.F302.AccelerationRate;

            MaxManeuveringSpeed = ShipStats.F302.MaxManeuveringSpeed;
            ManeuveringAccelerationRate = ShipStats.F302.ManeuveringSpeedAcceleration;
            #endregion
        }
    }
}
