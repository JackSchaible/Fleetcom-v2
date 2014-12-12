namespace Fleetcom.Library.Content
{
    public static class Keys
    {
        public static class Ships
        {
#if DEBUG
            public const string DebugAimer = "DebugAimer";
#endif

            public static class Ancient
            {
                public static class Jumper
                {
                    public const string Main = "Jumper_Main";
                }
            }

            public static class Human
            {
                public static class F302
                {
                    public const string Main = "F302_Main";

                    public static class Engines
                    {
                        public const string Glow = "F302_Glow";
                    }

                    public static class Weapons
                    {
                        public static class AimGuides
                        {
                            public const string Missile = "F302_AimGuide_Missile";
                            public const string Railgun = "F302_AimGuide_Railgun";
                        }

                        public static class Projectiles
                        {
                            public const string Railgun_Round = "F302_Bullet";
                        }
                    }
                }
            }
        }

        public enum Fonts
        {
#if DEBUG
            Debug
#endif
        }
    }
}
