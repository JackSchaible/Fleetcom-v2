using System;

namespace Fleetcom.Library.Settings
{
    [Serializable]
    public class ControlSettings
    {
        public const string Filename = "ControlSettings.xml";
        public float TriggerThreshold { get; set; }

        #region Defaults
        private const float TriggerThresholdDefault = 0.5f;
        #endregion

        public ControlSettings()
        {
            RestoreDefaults();
        }

        public void RestoreDefaults()
        {
            TriggerThreshold = TriggerThresholdDefault;
        }
    }
}
