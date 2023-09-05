using System;

namespace Data
{
    [Serializable]
    public class HuntInfo
    {
        public string Name { get; }
        public int Health { get; }
        public int PricePerKill { get; }

        public HuntInfo(string name, int health, int pricePerKill)
        {
            Name = name;
            Health = health;
            PricePerKill = pricePerKill;
        }
    }
}