using System;

namespace Data
{
    [Serializable]
    public class UnitInfo
    {
        public UnitTypes Type { get; }
        public int Health { get; }
        public int Attack { get; }

        public UnitInfo(UnitTypes type, int health, int attack)
        {
            Type = type;
            Health = health;
            Attack = attack;
        }
    }
}