using System;
using System.Collections.Generic;
using Data;

namespace Services.Interfaces
{
    public interface IPlayerUnitService
    {
        public event Action<List<UnitInfo>> UnitsUpdated;

        public List<UnitInfo> PlayerUnits { get; }
        public int PlayerPrice { get; }

        public UnitInfo AddStartUnitAndSave();
        void UpgradeUnit(UnitInfo removeUnit, UnitInfo updateUnit);
    }
}