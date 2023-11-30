using GoldFarm.Creatures;
using UnityEngine;


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
