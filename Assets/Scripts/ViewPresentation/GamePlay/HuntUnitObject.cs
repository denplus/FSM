using System;
using System.Collections;
using System.Threading.Tasks;
using Data;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

namespace ViewPresentation.GamePlay
{
    public class HuntUnitObject : UnitBaseObject
    {
        [SerializeField] private TMP_Text textField;
        [SerializeField] private float moveSpeed = 10f;

        public event Action<HuntInfo> HuntIsDead = delegate { };

        private Coroutine moveCoroutine;
        private bool isMoving;

        private HuntInfo unitData;

        private int health;

        public void Update()
        {
            if (!isMoving)
            {
                isMoving = true;

                if (moveCoroutine != null)
                    StopCoroutine(moveCoroutine);
                moveCoroutine = StartCoroutine(Move(GetNextPoint()));
            }
        }
        
        public void SetData(HuntInfo data)
        {
            unitData = data;
            textField.text = $"{data.Name} health:\n{data.Health}"; // TODO: localize
            health = data.Health;
        }

        public void TakeDamage(int power)
        {
            health -= power;
            if (health > 0)
            {
                textField.text = $"Deer health:\n{health}"; // TODO: localize
            }
            else
            {
                HuntIsDead?.Invoke(unitData);
                Destroy(gameObject);
            }
        }

        private Vector3 GetNextPoint() =>
            new(Random.Range(5, 10), transform.position.y, Random.Range(5, 10));

        protected override IEnumerator Move(Vector3 point)
        {
            const float closeDistance = 0.5f;

            while (Vector3.Distance(transform.position, point) > closeDistance)
            {
                Vector3 position = transform.position;
                Vector3 direction = point - position;
                position += direction.normalized * (Time.deltaTime * moveSpeed);
                transform.position = position;

                yield return null;
            }

            isMoving = false;
        }
    }
}