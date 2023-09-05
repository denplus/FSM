using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class UnitSettings
    {
        [HideInInspector] public string name;
        public UnitTypes type;
        public int health;
        public int attack;
    }

    [Serializable]
    public class HuntUnitSettings
    {
        public string name;
        public int health;
        public int pricePerKill;
    }
}