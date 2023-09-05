using System;

namespace Services.Interfaces
{
    public interface IMoneyService
    {
        int CurrentMoneyCount { get; }

        event Action<int> NewMoneyBalance;

        public bool TrySpendMoney(int price);
        void AddBalance(int huntUnitPricePerKill);
    }
}