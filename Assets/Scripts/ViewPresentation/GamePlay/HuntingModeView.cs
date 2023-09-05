using System.Collections.Generic;
using System.Threading.Tasks;
using Data;
using TMPro;
using UnityEngine;
using ViewPresentation.Presenter;
using HuntSettings = Data.HuntUnitSettings;
using Random = UnityEngine.Random;

namespace ViewPresentation.GamePlay
{
    public class HuntingModeView : MonoBehaviour
    {
        [SerializeField] private PlayerUnitObject playerUnitPrefab;
        [SerializeField] private HuntUnitObject huntUnitPrefab;
        [SerializeField] private CameraFollow cameraFollow;

        [SerializeField] private TMP_Text victoryText;

        private const float RandomCoefficient = 10f;

        private HuntingModePresenter presenter;

        private readonly List<PlayerUnitObject> playerUnitObjectOnScene = new();
        private readonly List<HuntUnitObject> huntUnitObjectOnScene = new();

        private int victoryHuntCount = 0;
        private int currentPlayerMoveIndex = 0;

        private void Start()
        {
            presenter = new HuntingModePresenter(this);
        }

        public void SpawnPlayerUnit(List<UnitInfo> playerUnits)
        {
            for (int i = 0; i < playerUnits.Count; i++)
            {
                PlayerUnitObject playerUnitObject =
                    Instantiate(playerUnitPrefab, GetRandomPosition(), Quaternion.identity);
                playerUnitObject.SetData(playerUnits[i]);
                playerUnitObject.ToggleActiveState(i == 0);

                if (i == 0)
                    cameraFollow.SetTarget(playerUnitObject.transform);

                playerUnitObjectOnScene.Add(playerUnitObject);

                playerUnitObject.UnitEndMove += UnitOnSceneOnEndMove;
            }
        }

        public void SpawnHuntUnit(List<HuntSettings> huntUnits)
        {
            foreach (HuntSettings unit in huntUnits)
            {
                HuntUnitObject huntUnitObject = Instantiate(huntUnitPrefab, GetRandomPosition(), Quaternion.identity);
                huntUnitObject.SetData(new HuntInfo(unit.name, unit.health, unit.pricePerKill));

                huntUnitObjectOnScene.Add(huntUnitObject);

                huntUnitObject.HuntIsDead += OnHuntIsDead;
            }
        }

        private async void OnHuntIsDead(HuntInfo huntUnit)
        {
            presenter.PlayerHasKillTheHuntUnit(huntUnit.PricePerKill);
            victoryHuntCount++;

            if (victoryHuntCount >= huntUnitObjectOnScene.Count)
            {
                await ShowVictoryText();
                presenter.LevelIsCompleted();
            }
        }

        private async Task ShowVictoryText()
        {
            const int seconds = 4 * 1000;
            
            victoryText.gameObject.SetActive(true);
            await Task.Delay(seconds);
            victoryText.gameObject.SetActive(false);
        }

        private Vector3 GetRandomPosition() =>
            new(RandomCoefficient * Random.insideUnitCircle.x,
                1f,
                RandomCoefficient * Random.insideUnitCircle.y);

        private void UnitOnSceneOnEndMove()
        {
            currentPlayerMoveIndex++;
            currentPlayerMoveIndex = (int) Mathf.Repeat(currentPlayerMoveIndex, playerUnitObjectOnScene.Count);

            for (int i = 0; i < playerUnitObjectOnScene.Count; i++)
            {
                bool isActive = currentPlayerMoveIndex == i;

                if (isActive)
                    cameraFollow.SetTarget(playerUnitObjectOnScene[i].transform);

                playerUnitObjectOnScene[i].ToggleActiveState(isActive);
            }
        }

        private void OnDestroy()
        {
            foreach (PlayerUnitObject unitOnScene in playerUnitObjectOnScene)
            {
                if (unitOnScene)
                    unitOnScene.UnitEndMove -= UnitOnSceneOnEndMove;
            }

            foreach (HuntUnitObject huntUnitOnScene in huntUnitObjectOnScene)
            {
                if (huntUnitOnScene)
                    huntUnitOnScene.HuntIsDead -= OnHuntIsDead;
            }
        }
    }
}