using Data;
using Services;
using Services.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Core
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameSettings gameSettings;

        private void Awake()
        {
            InitAllServices();

            // TODO: add scene load service
            SceneManager.LoadScene(Consts.SceneNames.HuntingGroup);
        }

        private void InitAllServices()
        {
            IPersistentService persistentService = new PersistentService(gameSettings);
            DIContainer.Register(persistentService);
            PersistentData data = persistentService.Load();
            
            ILocalizationService localizationService = new LocalizationService(data.CurrentLanguage);
            DIContainer.Register(localizationService);

            ILevelService levelService = new LevelService(data.Level);
            DIContainer.Register(levelService);
            
            IPlayerUnitService playerUnitService = new PlayerUnitService(data.PlayerUnits, gameSettings);
            DIContainer.Register(playerUnitService);
            
            IMoneyService moneyService = new MoneyService(data.MoneyCount);
            DIContainer.Register(moneyService);
            
            DIContainer.Register(gameSettings);
        }
    }
}