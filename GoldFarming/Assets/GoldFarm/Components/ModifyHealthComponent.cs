using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoldFarm.Components
{
    public class ModifyHealthComponent : MonoBehaviour
    {
        [SerializeField] private int _damage;


        public void ApplyDamage(GameObject target)
        {
            var _health = target.GetComponent<HealthComponent>();
            _health?.ApplyDamage(_damage);

        }
    }
}
