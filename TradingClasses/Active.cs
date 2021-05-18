using System;
using TradingInterfaces;

namespace TradingClasses
{
    public class Active
    {
        #region Properties

        public double CostOfBuying { get; private set; }
        public ITradingPosition Securities { get; private set; }
        public DateTime DateOfBuying { get; private set; }
        public double AbsoluteProfit { get; private set; }
        public double PercentProfit { get; private set; }
        public int Quantity { get; private set; }

        #endregion Properties


        #region Constructors

        public Active (ITradingPosition position, double cost, DateTime date) {
            Securities = position;
            CostOfBuying = cost;
            DateOfBuying = date;
        }
        public Active (ITradingPosition position) : this(position, position.CurrentCost, DateTime.Now) {
        }

        #endregion Constructors

        #region Methods

        #region UpdateActive()

        public void Update() {
            // Метод для обновления данных по текущему состоянию позиции
            Securities.UpdateCost();
            AbsoluteProfit = CostOfBuying - Securities.CurrentCost;
            PercentProfit = (AbsoluteProfit * 100)/CostOfBuying;
        }

        public void Add(int quantity) {
        }
        
        public override string ToString() {
            return string.Empty;
        }

        #endregion Update()

        #endregion Methods


    }
}
