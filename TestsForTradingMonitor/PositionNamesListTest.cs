using System;
using Xunit;
using TradingClasses;

namespace TestsForTradingMonitor
{
    public class UnitTest1
    {
        [Fact]
        public void CreatingPositionNamesListTest()
        {
            Settings appSettings = SettingsHandler.GetSettings("develop");
            PositionNamesList list = new PositionNamesList(appSettings);

            Assert.Equal("developData/PositionList/listOfPosition.json", list.PathToListInJson);
        }

        [Fact]
        public void AccessingPositionNamesListTest() {
            //TODO Написать код для проверки методов оберток

        }

        [Fact]
        public void OpenAndWritePositionNamesListTest() {
            //TODO написать под для проверки создания и сохранения новых списком с позициями
        }
    }
}
