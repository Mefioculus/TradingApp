using System;

namespace TradingClasses
{
    public class Briefcase
    {
        // Данный класс предназначен для хранения данных о текущем инвестиционном портфеле пользователя
        
        #region Properties

        public double AccountBalance { get; private set; } = 0.0;
        public CollectionOfActives Actives { get; private set;} 

        #endregion Properties
    }
}
