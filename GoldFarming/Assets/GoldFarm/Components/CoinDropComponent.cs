using GoldFarm.Model;
using System.Collections;
using UnityEngine;

namespace GoldFarm.Components
{
    public class CoinDropComponent : MonoBehaviour
    {
        [SerializeField] private CoinDropPrefab[] _prefabs;
        [SerializeField] private int _minCoins = 1;
        [SerializeField] private int _maxCoins = 5;
        [SerializeField] private float _coinSpawnForce = 5f;

        public void SpawnCoins()
        {
            int numberOfCoins = Random.Range(_minCoins, _maxCoins + 1);

            for (int i = 0; i < numberOfCoins; i++)
            {
                GameObject coinPrefab = GetRandomCoinPrefab();

                if (coinPrefab != null )
                {
                    GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
                    Rigidbody2D coinRb = coin.GetComponent<Rigidbody2D>();

                    if (coinRb != null)
                    {
                        Vector2 force = new Vector2(Random.Range(-1f, 1f), Random.Range(0.5f, 1f)).normalized * _coinSpawnForce;
                        coinRb.AddForce(force, ForceMode2D.Impulse);
                    }
                }
            }
        }

        private GameObject GetRandomCoinPrefab()
        {
            if (_prefabs != null && _prefabs.Length > 0)
            {
                float totalProbability = 0f;

                foreach (var coinData in _prefabs)
                {
                    totalProbability += coinData.DropProbability;
                }

                float randomValue = Random.value * totalProbability;
                float cumulativeProbability = 0f;

                for (int i = 0; i < _prefabs.Length; i++)
                {
                    cumulativeProbability += _prefabs[i].DropProbability;

                    if (randomValue <= cumulativeProbability)
                    {
                        return _prefabs[i].CoinPrefab;
                    }
                }
            }

            return null;
        }
    }
}