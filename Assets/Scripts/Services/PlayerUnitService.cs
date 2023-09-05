using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Data;
using Services.Interfaces;

namespace Services
{
    public class PlayerUnitService : IPlayerUnitService
    {
        private readonly List<UnitInfo> playerUnits;
        private readonly GameSettings gameSettings;

        public event Action<List<UnitInfo>> UnitsUpdated = delegate { };

        public List<UnitInfo> PlayerUnits => playerUnits;
        public int PlayerPrice => gameSettings.initUnitPrice;

        public PlayerUnitService(List<UnitInfo> units, GameSettings settings)
        {
            playerUnits = units;
            gameSettings = settings;
        }

        public UnitInfo AddStartUnitAndSave()
        {
            return AddUnitAndSave(UnitTypes.Fox);
        }

        private UnitInfo AddUnitAndSave(UnitTypes unitType)
        {
            UnitInfo unitData = gameSettings.GetUnitDataByType(unitType);
            playerUnits.Add(unitData);

            DIContainer.GetService<IPersistentService>().Save();

            UnitsUpdated?.Invoke(playerUnits);

            return unitData;
        }

        public void UpgradeUnit(UnitInfo removeUnit, UnitInfo updateUnit)
        {
            var maxUnitType = Enum.GetValues(typeof(UnitTypes)).Cast<UnitTypes>().Count() - 1;
            bool canUpgradeUnit = maxUnitType > (int) updateUnit.Type && maxUnitType > (int) removeUnit.Type;

            if (canUpgradeUnit)
            {
                playerUnits.Remove(updateUnit);
                playerUnits.Remove(removeUnit);

                UnitTypes nextType = (UnitTypes) ((int) updateUnit.Type + 1);
                UnitInfo unitData = gameSettings.GetUnitDataByType(nextType);
                playerUnits.Add(unitData);

                DIContainer.GetService<IPersistentService>().Save();

                UnitsUpdated?.Invoke(playerUnits);
            }
        }
    }
}