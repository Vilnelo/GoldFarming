using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GoldFarm.Components
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private bool _invertXScale;

        [ContextMenu("Switch")]
        public void Spawn()
        {
            var instantiate = Instantiate(_prefab, _target.position, Quaternion.identity);
            var scale = _target.lossyScale;
            scale.x *= _invertXScale ? -1 : 1;
            
            instantiate.transform.localScale = scale;

        }
    }
}

