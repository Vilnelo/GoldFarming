using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoldFarm.Components
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _prefab;

        [ContextMenu("Switch")]
        public void Spawn()
        {
            var instantiate = Instantiate(_prefab, _target.position, Quaternion.identity);
            instantiate.transform.localScale = transform.lossyScale;

        }
    }
}

