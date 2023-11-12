using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace GoldFarm.Components
{
    public class GoldCountComponent : MonoBehaviour
    {
        [SerializeField] private Hero _hero;
        [SerializeField] private int _coinsCount;

        public void GoldCount(GameObject _target)
        {
            var _gold = _target.GetComponent<GoldStorageComponent>();
            _gold?.CountGold(_coinsCount);
        }
    }
}
