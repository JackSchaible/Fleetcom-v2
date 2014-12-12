using System.Threading;

namespace Fleetcom.Library.Settings
{
    internal interface ISettings
    {
        event FileSaved OnFileSaved;
        event FileLoaded OnFileLoaded;

        void RestoreDefaults();
        void Save();
        void Load();
    }
}
