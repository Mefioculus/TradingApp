using System;
using System.IO;
using Newtonsoft.Json;

namespace TradingClasses
{
    #region SettingsHandler class
    // Класс, который будет отвечать за действия, связанные с настройками
    public static class SettingsHandler
    {
        #region Properties

        private static string pathToUserSettings = "settings.json";

        #endregion Properties

        #region Constructor


        #endregion Constructor

        #region Methods

        public static Settings GetSettings(string option) {

            Settings appSettings;

            switch (option) {
                case "develop":
                    appSettings = GetDevelopSettings();
                    break;
                case "production":
                    appSettings = GetUserSettings();
                    break;
                case "default":
                    appSettings = GetDefaultSettings();
                    break;
                default:
                    throw new Exception($"'{option}' - unknownSettings");
            }
            return appSettings;
        }

        #region Read and Write methods

        internal static Settings ReadSettings () {
            if (File.Exists(pathToUserSettings)) {
                string jsonString = File.ReadAllText(pathToUserSettings);
                return JsonConvert.DeserializeObject<Settings>(jsonString);
            }
            else {
                Console.WriteLine("Не получилось найти пользовательские настройки");
                Console.WriteLine("В качестве настроек будут выбраны настройки по умолчанию");
                Settings settings = GetDefaultSettings();
                WriteSettings(settings);
                return settings;
            }
        }

        internal static void WriteSettings (Settings settings) {
            string jsonString = JsonConvert.SerializeObject(settings);
            File.WriteAllText(pathToUserSettings, jsonString);
            Console.WriteLine($"Произведена запись текущих настроек в файл\n{pathToUserSettings}");
        }

        #endregion Read and Write methods

        #region Fabric methods

        private static Settings GetDefaultSettings() {
            Settings defaultSettings = new Settings();
            defaultSettings.DataDirectory = "data";
            return defaultSettings;
        }

        private static Settings GetUserSettings() {
            return ReadSettings();
        }

        private static Settings GetDevelopSettings() {
            Settings developSettings = new Settings();
            developSettings.DataDirectory = "developData";
            return developSettings;
        }

        #endregion Fabric methods

        #endregion Methods
    }
    #endregion SettingsHandler class

    #region Settings class
    // Класс для хранения и оперирования настройками
    public class Settings
    {
        public string DataDirectory { get; set; }

        public void Save() {
            SettingsHandler.WriteSettings(this);
        }
    }


    #endregion Settings class
}
