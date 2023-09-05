using System;
using System.Collections.Generic;
using Core;
using Data;
using Services;
using Services.Interfaces;
using UnityEngine.SceneManagement;
using Utils;
using ViewPresentation.View.HuntingGroupUI;

namespace ViewPresentation.Presenter
{
    public class HuntingGroupPresenter : IDisposable
    {
        private readonly HuntingGroupView huntingGroupView;
        private readonly IPlayerUnitService playerUnitService;

        public HuntingGroupPresenter(HuntingGroupView view)
        {
            huntingGroupView = view;

            playerUnitService = DIContainer.GetService<IPlayerUnitService>();

            playerUnitService.UnitsUpdated += OnUnitsUpdated;

            huntingGroupView.SpawnPlayerUnits(playerUnitService.PlayerUnits);
        }

        public void OnBuyClick()
        {
            if (huntingGroupView.IsAnyFreeCell)
            {
                if (DIContainer.GetService<IMoneyService>().TrySpendMoney(playerUnitService.PlayerPrice))
                    DIContainer.GetService<IPlayerUnitService>().AddStartUnitAndSave();
            }
        }

        public void MergeCells(HuntingGroupGridCellView removeCell, HuntingGroupGridCellView updateCell)
        {
            DIContainer.GetService<IPlayerUnitService>().UpgradeUnit(removeCell.UnitData, updateCell.UnitData);
        }

        public void OnPlayClick()
        {
            if (playerUnitService.PlayerUnits.Count > 0)
            {
                // TODO: add scene load service
                SceneManager.LoadScene(Consts.SceneNames.HuntingMode);
            }
        }

        public void Dispose()
        {
            playerUnitService.UnitsUpdated -= OnUnitsUpdated;
        }

        private void OnUnitsUpdated(List<UnitInfo> list)
        {
            huntingGroupView.SpawnPlayerUnits(list);
        }
    }
}