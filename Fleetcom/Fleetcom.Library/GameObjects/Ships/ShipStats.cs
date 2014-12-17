
using System;

namespace Fleetcom.Library.GameObjects.Ships
{
    internal static class ShipStats
    {
        #region Ancient
        public static class Jumper
        {
            public const float MaxTurnRate = 0.1f;
            public const float TurnRateAcceleration = 0.01f;

            public const float MaxSpeed = 10f;
            public const float AccelerationRate = 0.5f;

            public const float MaxManeuveringSpeed = 10f;
            public const float ManeuveringSpeedAcceleration = 0.5f;
        }
        #endregion

        #region Human
        public static class F302
        {
            public const float MaxTurnRate = 0.1f;
            public const float TurnRateAcceleration = 0.01f;

            public const float MaxSpeed = 10f;
            public const float AccelerationRate = 0.5f;

            public const float MaxManeuveringSpeed = 2.5f;
            public const float ManeuveringSpeedAcceleration = 0.25f;

            public static class Weapons
            {
                public static class Railgun
                {
                    public static readonly TimeSpan FireRate = new TimeSpan(0, 0, 0, 0, 1);
                    public const float ProjectileSpeed = 50f;
                }

                public static class Missiles
                {
                    public static readonly TimeSpan FireRate = new TimeSpan(0, 0, 0, 1);
                    public const float ProjectileSpeed = 15f;
                }
            }
        }
        #endregion
    }
}
