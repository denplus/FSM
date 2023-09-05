using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/Game", order = 0)]
    public class GameSettings : ScriptableObject
    {
        public int initMoneyCount;
        public int initUnitPrice; // For simplicity all units have same price
        public List<UnitSettings> unitsInitSettings;
        public List<LevelInfo> levels;
       
        public UnitInfo GetUnitDataByType(UnitTypes type)
        {
            UnitSettings unit = unitsInitSettings.FirstOrDefault(x => x.type == type);
            return unit == null ? new UnitInfo(UnitTypes.Fox, 0, 0) : new UnitInfo(unit.type, unit.attack, unit.health);
        }

        private void OnValidate()
        {
            foreach (var unit in unitsInitSettings)
                unit.name = unit.type.ToString();
        }
    }
}