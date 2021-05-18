using System;
using Xunit;
using TradingClasses;

namespace TestsForTradingMonitor
{
    public class PositionDictionaryTests
    {
        [Fact]
        public void CreateNewPositionTesting() {
            Settings settings = SettingsHandler.GetSettings("develop");
            PositionDictionary dict = new PositionDictionary(settings, "finam");

            Assert.Equal("finam", dict.Source);
            Assert.Equal("developData", dict.PathToDataDirectory);
            Assert.Equal("finam.json", dict.NameOfDataFile);
            Assert.Equal("developData/finam.json", dict.PathToDataFile);
            Assert.False(dict.IsChanged);

        }

        [Fact]
        public void TestAddingNewElement() {
            Settings settings = SettingsHandler.GetSettings("develop");
            PositionDictionary dict = new PositionDictionary(settings, "test");

            // Проверка перед началом внесения изменений в словарь с данными
            Assert.Equal("developData/test.json", dict.PathToDataFile);
            Assert.False(dict.IsChanged);

            // Проведение изменений
            dict.Clear();
            Assert.False(dict.Contains("testKey"));

            // Проверка результатов изменений
            dict["testKey"] = "testValue";
            Assert.True(dict.IsChanged);
            Assert.True(dict.Contains("testKey"));
            Assert.Equal(1, dict.Count());

            // Сохранение изменений
            dict.SaveData();
            Assert.False(dict.IsChanged);

            // Создание нового объекта для проверки корректности чтения и записи данных
            PositionDictionary dict2 = new PositionDictionary(settings, "test");

            // Тестирование чтения записи
            Assert.Equal(1, dict2.Count());
            Assert.True(dict2.Contains("testKey"));
            Assert.Equal("testValue", dict["testKey"]);
        }
    }
}
