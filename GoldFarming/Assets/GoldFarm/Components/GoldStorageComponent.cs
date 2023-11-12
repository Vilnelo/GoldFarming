using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GoldFarm.Components
{
    public class GoldStorageComponent : MonoBehaviour
    {
        [SerializeField] private int _goldLimit;
        [SerializeField] private ParticleSystem _hitParticlesGold;

        private int _goldCount;
        private void Awake()
        {
            _goldCount = 0;
        }

        public void CountGold(int _gold)
        {
            if (_gold < 0 && _goldCount > 0)
            {
                SpawnCoins();
            }
            if (_goldCount + _gold >= _goldLimit)
            {
                _goldCount = _goldLimit;
            } else if(_goldCount + _gold < 0) {
                _goldCount = 0;
            } else
            {
                _goldCount += _gold;
            }
            Debug.Log(message:"Монет:");
            Debug.Log(_goldCount);
        }

        private void SpawnCoins()
        {
            var numCoinsToDispose = Mathf.Min(_goldCount, 5);

            var burst = _hitParticlesGold.emission.GetBurst(0);
            burst.count = numCoinsToDispose;
            _hitParticlesGold.emission.SetBurst(0, burst);
            _hitParticlesGold.gameObject.SetActive(true);
            _hitParticlesGold.Play();
        }

    }
}

