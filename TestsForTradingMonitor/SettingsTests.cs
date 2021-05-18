using System;
using Xunit;
using TradingClasses;

namespace TestsForTradingMonitor
{
    public class SettingsTests
    {
        [Fact]
        // Метод для тестирования класса с настройками
        public void TestCheckSettingsClass() {
            Settings settings = new Settings();
            settings.DataDirectory = "Data";
            Assert.Equal("Data", settings.DataDirectory);
        }
    }

    public class SettingsHandlerTests {
        [Fact]
        // Метод для тестирования настроек для разработки
        public void TestDevelopSettings() {
            Settings developSettings = SettingsHandler.GetSettings("develop");

            AssertEqual("developData", developerSettings.DataDirectory);
        }

        [Fact]
        // Метод для тестирования пользовательских настроек
        public void TestUserSettings() {
            Settings userSettings = SettingsHandler.GetSettings("production");

            AssertEqual("data", userSettings.DataDirectory);
        }

        [Fact]
        // Метод для тестирования настроек по умолчанию
        public void TestDefaultSettings() {
            Settings defaultSettings = SettingsHandler.GetSettings("default");

            AssertEqual("data", defaultSettings.DataDirectory);
        }

        [Fact]
        // Метод для тестирования сохранения и чтения настроек
        public void TestChangingUserSettings() {
            Settings userSettings = SettingsHandler.GetSettings("production");

            Assert.Equal("data", userSettings.DataDirectory);

            userSettings.DataDirectory = "userData";
            userSettings.Save();

            // Создаем новый экземпляр настроек для проверки сохранения данных
            userSettings = SettingsHandler("production");
            Assert.Equal("userData", userSettings.DataDirectory);

            userSettings.DataDirectory = "data";
            userSettings.Save();

        }
    }

}
