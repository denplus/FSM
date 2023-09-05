using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;
using UnityEngine.UI;
using ViewPresentation.Presenter;

namespace ViewPresentation.View.HuntingGroupUI
{
    public class HuntingGroupView : MonoBehaviour
    {
        [SerializeField] private List<HuntingGroupGridCellView> gridCells;

        [Header("Buttons"), SerializeField] private Button buy;
        [SerializeField] private Button play;

        public bool IsAnyFreeCell => gridCells.Any(x => !x.IsOccupied);

        private HuntingGroupPresenter presenter;

        private void Start()
        {
            presenter = new HuntingGroupPresenter(this);

            HuntingGroupGridCellView.MergeCell += HuntingGroupGridCellOnMergeCell;

            buy.onClick.AddListener(BuyClick);
            play.onClick.AddListener(PlayClick);
        }

        public void SpawnPlayerUnits(List<UnitInfo> units)
        {
            // TODO: use object pooling

            foreach (HuntingGroupGridCellView cell in gridCells)
                cell.ClearData();

            for (int i = 0; i < units?.Count; i++)
            {
                if (i < gridCells.Count)
                {
                    gridCells[i].SetData(units[i]);
                }
            }
        }

        private void BuyClick()
        {
            presenter.OnBuyClick();
        }

        private void PlayClick()
        {
            presenter.OnPlayClick();
        }

        private void HuntingGroupGridCellOnMergeCell(HuntingGroupGridCellView removeCell,
            HuntingGroupGridCellView updateCell)
        {
            presenter.MergeCells(removeCell, updateCell);
        }

        private void OnDestroy()
        {
            buy.onClick.RemoveListener(BuyClick);
            play.onClick.RemoveListener(PlayClick);

            HuntingGroupGridCellView.MergeCell -= HuntingGroupGridCellOnMergeCell;
        }
    }
}