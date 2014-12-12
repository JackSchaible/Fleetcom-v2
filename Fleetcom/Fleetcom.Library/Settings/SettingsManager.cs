using System;
using System.IO;
using System.Xml.Serialization;

namespace Fleetcom.Library.Settings
{
    public static class SettingsManager
    {
        public static ControlSettings ControlSettings
        {
            get
            {
                if (_controlSettings == null)
                {
                    lock (ControlSettingsLock)
                    {
                        if (_controlSettings == null)
                        {
                            var path = Folder + ControlSettings.Filename;
                            ControlSettings temp;

                            if (File.Exists(path))
                                temp = Load<ControlSettings>(path);
                            else
                            {
                                temp = new ControlSettings();
                                Save(temp, path);
                            }

                            _controlSettings = temp;
                            return temp;
                        }
                        else
                            return _controlSettings;
                    }
                }
                else
                    return _controlSettings;
            }
        }

        private static string Folder
        {
            get
            {
                if (!Directory.Exists(Environment.SpecialFolder.ApplicationData + "Fleetcom/Settings/"))
                    Directory.CreateDirectory(Environment.SpecialFolder.ApplicationData + "Fleetcom/Settings/");

                return Environment.SpecialFolder.ApplicationData + "Fleetcom/Settings/";
            }
        }

        private volatile static ControlSettings _controlSettings;
        private static readonly object ControlSettingsLock = new object();

        private static void Save<T>(T file, string filePath)
        {
            if (file.GetType().IsSerializable)
            {
                using (var stream = new FileStream(filePath, FileMode.CreateNew))
                {
                    new XmlSerializer(file.GetType()).Serialize(stream, file);
                }
            }
            else
            {
                throw new ArgumentException("file must be serializable", "file");
            }
        }

        private static T Load<T>(string filePath)
        {
            if (typeof(T).IsSerializable)
            {
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    return (T)new XmlSerializer(typeof(T)).Deserialize(stream);
                }
            }

            throw new ArgumentException("file must be serializable", "file");
        }
    }
}
