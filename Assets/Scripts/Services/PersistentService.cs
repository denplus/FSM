using System.Collections.Generic;
using Core;
using Data;
using Services.Interfaces;
using UnityEngine;
using Newtonsoft.Json;

namespace Services
{
    public class PersistentService : IPersistentService
    {
        private readonly GameSettings gameSettings;
        private const string SavedData = "saved_data";

        public PersistentService(GameSettings settings)
        {
            gameSettings = settings;
        }

        public PersistentData Load()
        {
            if (PlayerPrefs.HasKey(SavedData))
                return JsonConvert.DeserializeObject<PersistentData>(PlayerPrefs.GetString(SavedData));

            return new PersistentData()
            {
                Level = 0,
                PlayerUnits = new List<UnitInfo>(),
                CurrentLanguage = "US",
                MoneyCount = gameSettings.initMoneyCount,
            };
        }

        public void Save()
        {
            string data = JsonConvert.SerializeObject(GetSavingData());
            PlayerPrefs.SetString(SavedData, data);
        }

        private PersistentData GetSavingData()
        {
            return new PersistentData
            {
                Level = DIContainer.GetService<ILevelService>().CurrentLevel,
                PlayerUnits = DIContainer.GetService<IPlayerUnitService>().PlayerUnits,
                CurrentLanguage = "US",
                MoneyCount = DIContainer.GetService<IMoneyService>().CurrentMoneyCount,
            };
        }
    }
}