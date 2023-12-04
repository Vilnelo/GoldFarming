using System.Collections;
using UnityEngine;

namespace GoldFarm.Creatures
{
    public class GroundPatrol : Patrol
    {
        [SerializeField] private LayerCheck _isOnGround;
        [SerializeField] private LayerCheck _isItWall;
        [SerializeField] private LayerCheck _isItSpikes;

        private Creature _creature;
        private int _destination = 1;

        private void Awake()
        {
            _creature = GetComponent<Creature>();
        }
        public override IEnumerator DoPatrol()
        {
            while (enabled)
            {
                if (!_isOnGround.IsTouchingLayer || _isItWall.IsTouchingLayer || _isItSpikes.IsTouchingLayer)
                {
                    _destination *= -1;
                } 

                var direction = transform.position * _destination;
                direction.y = 0;
                _creature.SetDirection(direction.normalized);

                yield return null;
            }
        }
    }
}