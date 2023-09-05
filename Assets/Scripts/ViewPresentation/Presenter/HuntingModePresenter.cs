using System.Collections.Generic;
using Core;
using Data;
using Services.Interfaces;
using UnityEngine.SceneManagement;
using Utils;
using ViewPresentation.GamePlay;

namespace ViewPresentation.Presenter
{
    public class HuntingModePresenter
    {
        private readonly HuntingModeView huntingModeView;

        public HuntingModePresenter(HuntingModeView view)
        {
            huntingModeView = view;

            huntingModeView.SpawnPlayerUnit(DIContainer.GetService<IPlayerUnitService>().PlayerUnits);

            List<HuntUnitSettings> huntList =
                DIContainer.GetService<GameSettings>().levels[DIContainer.GetService<ILevelService>().CurrentLevel]
                    .huntUnitInitSettings;
            huntingModeView.SpawnHuntUnit(huntList);
        }

        public void PlayerHasKillTheHuntUnit(int huntUnitPricePerKill)
        {
            DIContainer.GetService<IMoneyService>().AddBalance(huntUnitPricePerKill);
        }

        public void LevelIsCompleted()
        {
            DIContainer.GetService<ILevelService>().IncreaseLevel();
            DIContainer.GetService<IPersistentService>().Save();
            SceneManager.LoadScene(Consts.SceneNames.HuntingGroup);
        }
    }
}