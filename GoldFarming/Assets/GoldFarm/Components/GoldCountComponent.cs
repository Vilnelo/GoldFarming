using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace GoldFarm.Components
{
    public class GoldCountComponent : MonoBehaviour
    {
        [SerializeField] private int _coinsCount;
        private Hero _hero;

        public void GoldCount()
        {
            _hero = FindObjectOfType<Hero>();
            var _gold = _hero.GetComponent<GoldStorageComponent>();
            _gold?.CountGold(_coinsCount);
        }
    }
}
