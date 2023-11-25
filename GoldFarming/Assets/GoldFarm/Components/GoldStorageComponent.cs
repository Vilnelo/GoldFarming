using GoldFarm.Model;
using UnityEngine;

namespace GoldFarm.Components
{
    public class GoldStorageComponent : MonoBehaviour
    {
        [SerializeField] private int _goldLimit;
        [SerializeField] private ParticleSystem _hitParticlesGold;

        private GameSession _session;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
        }
        public void CountGold(int _gold)
        {
            if (_gold < 0 && _session.Data.Coins > 0)
            {
                SpawnCoins();
            }
            if (_session.Data.Coins + _gold >= _goldLimit)
            {
                _session.Data.Coins = _goldLimit;
            } else if(_session.Data.Coins + _gold < 0) {
                _session.Data.Coins = 0;
            } else
            {
                _session.Data.Coins += _gold;
            }
            Debug.Log(message:"Монет:");
            Debug.Log(_session.Data.Coins);
        }

        private void SpawnCoins()
        {
            var numCoinsToDispose = Mathf.Min(_session.Data.Coins, 5);

            var burst = _hitParticlesGold.emission.GetBurst(0);
            burst.count = numCoinsToDispose;
            _hitParticlesGold.emission.SetBurst(0, burst);
            _hitParticlesGold.gameObject.SetActive(true);
            _hitParticlesGold.Play();
        }

    }
}

