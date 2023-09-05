using System;
using Core;
using Services;
using Services.Interfaces;
using ViewPresentation.View;

namespace ViewPresentation.Presenter
{
    public class MoneyPresenter : IDisposable
    {
        private readonly MoneyView moneyView;
        private readonly IMoneyService moneyService;

        public MoneyPresenter(MoneyView view)
        {
            moneyView = view;

            moneyService = DIContainer.GetService<IMoneyService>();
            moneyService.NewMoneyBalance += OnNewMoneyBalance;

            moneyView.SetMoneyCount(moneyService.CurrentMoneyCount);
        }

        private void OnNewMoneyBalance(int newBalance)
        {
            moneyView.SetMoneyCount(newBalance);
        }

        public void Dispose()
        {
            moneyService.NewMoneyBalance -= OnNewMoneyBalance;
        }
    }
}