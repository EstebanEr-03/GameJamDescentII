﻿using UnityEngine;

namespace DescentII
{
    public class EnemyMobilityState : EnemyBaseStrategyState<BaseMobilityStrategy>
    {
        public EnemyMobilityState(Creature enemy, Animator animator) 
            : base(enemy, animator) { }
    }
}
