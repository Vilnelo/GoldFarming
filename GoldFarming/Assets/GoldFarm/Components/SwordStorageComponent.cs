using GoldFarm.Model;
using UnityEngine;

namespace GoldFarm.Components
{
    public class SwordStorageComponent : MonoBehaviour
    {
        [SerializeField] private int _swordLimit;

        private GameSession _session;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
        }

        public void CountSwords(int swordCount)
        {
            if (_session.Data.Swords + swordCount < 0)
            {
                _session.Data.Swords = 0;
            } else if (_session.Data.Swords + swordCount >= _swordLimit)
            {
                _session.Data.Swords = _swordLimit;
            } else
            {
                _session.Data.Swords += swordCount;
            }
        }
    }
}