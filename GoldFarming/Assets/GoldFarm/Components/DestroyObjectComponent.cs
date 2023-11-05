using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoldFarm.Components
{
    public class DestroyObjectComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _objectToDestroy;
        public void DestroySelf()
        {
            
            Destroy(_objectToDestroy);
        }
    }
}

