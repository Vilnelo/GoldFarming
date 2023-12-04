using System;
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
        [SerializeField] private UnityEvent _onHeal;
        [SerializeField] private UnityEvent<int> _onChange;

        public void ApplyDamage (int damageValue)
        {
            if (_health <= 0) return;
            _health += damageValue;
            _onChange?.Invoke(_health);

            if (damageValue >= 0)
            {
                _onHeal?.Invoke();
            } else
            {
                
                if (_health <= 0)
                {
                    _onDie?.Invoke();
                }
                else
                {
                    _onDamage?.Invoke();
                }
                
            }
        }

        public void SetHealth(int health)
        {
            _health = health;
        }
    }
}

