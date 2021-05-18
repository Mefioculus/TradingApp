using System;

namespace TradingInterfaces
{
    public interface ITradingPosition
    {
        #region Properties

        string Name { get; }
        double CurrentCost { get; }
        string Currency { get; }
        DateTime LastUpdate { get; }

        #endregion Properties

        #region Methods

        void UpdateCost();
        string ToString();

        #endregion Methods
    }
}
