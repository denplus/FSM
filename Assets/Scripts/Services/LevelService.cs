using Core;
using Data;
using Services.Interfaces;
using UnityEngine;

namespace Services
{
    public class LevelService : ILevelService
    {
        public int CurrentLevel { get; private set; }

        public LevelService(int level)
        {
            CurrentLevel = level;
        }

        public void IncreaseLevel()
        {
            CurrentLevel++;
            CurrentLevel = (int) Mathf.Repeat(CurrentLevel, DIContainer.GetService<GameSettings>().levels.Count);
        }
    }
}