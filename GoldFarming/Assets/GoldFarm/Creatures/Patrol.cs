﻿using System.Collections;
using UnityEngine;

namespace GoldFarm.Creatures
{
    public abstract class Patrol : MonoBehaviour
    {
        public abstract IEnumerator DoPatrol();
    }
}