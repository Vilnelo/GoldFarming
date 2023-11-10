using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GoldFarm.Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onDie;

        public void ApplyDamage (int damageValue)
        {
            _health -= damageValue;
            Debug.Log(_health);
            _onDamage?.Invoke();
            if(_health <= 0)
            {
                _onDie?.Invoke();
            }
        }

    }
}

