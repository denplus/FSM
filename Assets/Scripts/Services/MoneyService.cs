using System;
using Services.Interfaces;

namespace Services
{
    public class MoneyService : IMoneyService
    {
        public int CurrentMoneyCount { get; private set; }

        public event Action<int> NewMoneyBalance = delegate { };

        public MoneyService(int money)
        {
            CurrentMoneyCount = money;
        }

        public bool TrySpendMoney(int price)
        {
            if (CurrentMoneyCount >= price)
            {
                CurrentMoneyCount -= price;
                NewMoneyBalance?.Invoke(CurrentMoneyCount);
                return true;
            }

            return false;
        }

        public void AddBalance(int huntUnitPricePerKill)
        {
            CurrentMoneyCount += huntUnitPricePerKill;
        }
    }
}