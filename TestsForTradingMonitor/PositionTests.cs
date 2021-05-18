using System;
using TradingClasses;
using Xunit;

namespace TestsForTradingMonitor
{
    public class PositionTests
    {
        [Fact]
        public void SberPositionTests() {

        }

        [Fact]
        public void FinamCreatingTest() {
            FinamPosition test = new FinamPosition("vtb");
            
            // Проверка позиции на правильность.
            // Для того, чтобы позиция была корректной, она должна содержать
            // Значение даты на текущий момента, значение цены, а так же значение валюты

            // Проверяем, что значения отличаются от значений по умолчанию
            Assert.NotEqual(0.0, test.CurrentCost);
            Assert.NotEqual(string.Empty, test.Currency);
            // Проверяем, что дата соответстует дате проведения теста (без времени)
            Assert.Equal(DateTime.Now.Date, test.LastUpdate.Date);
            

        }
    }
}
