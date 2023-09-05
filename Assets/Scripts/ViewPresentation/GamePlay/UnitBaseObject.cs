using System.Collections;
using Data;
using UnityEngine;

namespace ViewPresentation.GamePlay
{
    public abstract class UnitBaseObject : MonoBehaviour
    {
        protected abstract IEnumerator Move(Vector3 point);
    }
}