using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GoldFarm.Components
{
    public class EnterCollisionComponent : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private UnityEvent<GameObject> _action;
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(_tag))
            {

                _action?.Invoke(other.gameObject);
            }
        }
    }
}

