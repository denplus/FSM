using System;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ViewPresentation.View.HuntingGroupUI
{
    public class HuntingGroupGridCellView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private TMP_Text unitName;

        public static event Action<HuntingGroupGridCellView, HuntingGroupGridCellView> MergeCell = delegate { };
        public bool IsOccupied { get; private set; }
        public UnitInfo UnitData { get; private set; }

        private int selectedCellId;
        private HuntingGroupGridCellView selectedCell;

        public void SetData(UnitInfo unit)
        {
            UnitData = unit;
            
            unitName.text = unit.Type.ToString();
            IsOccupied = true;
        }
        
        public void ClearData()
        {
            UnitData = null;
            
            unitName.text = "";
            IsOccupied = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            selectedCellId = eventData.pointerCurrentRaycast.gameObject.GetInstanceID();
            selectedCell = eventData.pointerCurrentRaycast.gameObject.GetComponent<HuntingGroupGridCellView>();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            GameObject raycastObject = eventData.pointerCurrentRaycast.gameObject;

            if (raycastObject && selectedCellId != raycastObject.GetInstanceID())
            {
                selectedCellId = Int32.MinValue;

                HuntingGroupGridCellView dropCellView =
                    eventData.pointerCurrentRaycast.gameObject.GetComponent<HuntingGroupGridCellView>();

                if (dropCellView != null && dropCellView.IsOccupied)
                    MergeCell?.Invoke(selectedCell, dropCellView);
            }
        }
    }
}