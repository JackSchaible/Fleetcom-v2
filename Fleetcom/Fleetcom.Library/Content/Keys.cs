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

        public static class UI
        {
            public static class Player
            {
                public const string WeaponsMenu = "Player_Menu";
            }

            public static class Input
            {
                public static class Controller
                {
                    public const string A = "360A";
                    public const string B = "360B";
                    public const string X = "360X";
                    public const string Y = "360Y";
                    public const string Back = "360Back";
                    public const string Start = "360Start";
                    public const string LeftShoulder = "360LShoulder";
                    public const string LeftTrigger = "360LTrigger";
                    public const string RightShoulder = "360RShoulder";
                    public const string RightTrigger = "360RTrigger";
                    public const string DPadUp = "360DPadUp";
                    public const string DPadDown = "360DPadDown";
                    public const string DPadLeft = "360DPadLeft";
                    public const string DPadRight = "360DPadRight";
                    public const string LeftStick = "360LStick";
                    public const string RightStick = "360RStick";
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
