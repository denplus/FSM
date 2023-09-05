using System;
using System.Collections;
using Data;
using TMPro;
using UnityEngine;

namespace ViewPresentation.GamePlay
{
    public class PlayerUnitObject : UnitBaseObject
    {
        [SerializeField] private LineRenderer moveLine;
        [SerializeField] private TMP_Text textField;
        [SerializeField] private float moveSpeed = 10f;

        public event Action UnitEndMove = delegate { };

        private const float CloseDistanceMove = 0.5f;
        private const float CloseDistanceAttack = 5f;

        private Camera cameraMain;

        private const float MaxRaycastDistance = 100f;
        private Coroutine moveCoroutine;
        private bool isMoving;

        private HuntUnitObject huntUnitObject;

        private UnitInfo unitData;

        private void Start()
        {
            cameraMain = Camera.main;
            moveLine.SetPosition(0, transform.position);
        }

        public void Update()
        {
            if (!isMoving)
                RaycastLine();
        }

        public void SetData(UnitInfo data)
        {
            unitData = data;
            textField.text = $"{data.Type} attack:\n{data.Attack}"; // TODO: localize
        }

        public void ToggleActiveState(bool isOn)
        {
            moveLine.enabled = isOn;
            isMoving = !isOn;
        }

        private void RaycastLine()
        {
            Ray ray = cameraMain.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, MaxRaycastDistance))
            {
                huntUnitObject = hit.collider.gameObject.GetComponentInParent<HuntUnitObject>();
                Vector3 hitPoint = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                moveLine.SetPosition(1, hitPoint);

                if (Input.GetMouseButtonDown(0))
                {
                    isMoving = true;

                    if (moveCoroutine != null)
                        StopCoroutine(moveCoroutine);
                    moveCoroutine = StartCoroutine(Move(hitPoint));

                    moveLine.enabled = false;
                }
            }
        }

        protected override IEnumerator Move(Vector3 point)
        {
            while (Vector3.Distance(transform.position, point) > CloseDistanceMove)
            {
                Vector3 position = transform.position;
                Vector3 direction = point - position;
                position += direction.normalized * (Time.deltaTime * moveSpeed);
                transform.position = position;

                yield return null;
            }

            isMoving = false;
            moveLine.SetPosition(0, transform.position);

            TryAttack();

            UnitEndMove?.Invoke();
        }

        private void TryAttack()
        {
            if (huntUnitObject 
                && Vector3.Distance(transform.position, huntUnitObject.transform.position) < CloseDistanceAttack)
            {
                huntUnitObject.TakeDamage(unitData?.Attack ?? 0);
            }
        }
    }
}