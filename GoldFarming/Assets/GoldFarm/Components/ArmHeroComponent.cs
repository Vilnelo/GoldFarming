using GoldFarm.Components;
using GoldFarm.Creatures;
using UnityEngine;

namespace Assets.GoldFarm.Components
{
    public class ArmHeroComponent : MonoBehaviour
    {
        [SerializeField] private int _swordCount = 1;
        public void ArmHero(GameObject go)
        {
            var hero = go.GetComponent<Hero>();
            if (hero != null)
            {
                hero.ArmHero();
                var swords = hero.GetComponent<SwordStorageComponent>();
                swords?.CountSwords(_swordCount);
            }
        }
    }
}