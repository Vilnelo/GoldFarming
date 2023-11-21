using System.Collections;
using UnityEngine;

namespace GoldFarm.Components
{
    public class CoinDropComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _goldCoinPrefab;
        [SerializeField] private GameObject _silverCoinPrefab;
        [SerializeField] private int _minCoins = 1;
        [SerializeField] private int _maxCoins = 5;
        [SerializeField] private float _coinSpawnForce = 5f;
        [SerializeField] private float _goldProbability = 0.7f;

        public void SpawnCoins()
        {
            int numberOfCoins = Random.Range(_minCoins, _maxCoins + 1);

            for (int i = 0; i < numberOfCoins; i++)
            {
                GameObject coinPrefab = (Random.value < _goldProbability) ? _goldCoinPrefab : _silverCoinPrefab;
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
}