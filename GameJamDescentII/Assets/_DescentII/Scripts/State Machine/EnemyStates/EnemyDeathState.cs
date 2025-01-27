﻿using UnityEngine;

namespace DescentII
{
    public class EnemyDeathState : EnemyBaseState<BaseDeathStrategy>
    {
        public EnemyDeathState(Creature enemy, Animator animator)
            : base(enemy, animator) { }

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("On DeathState");
        }
    }
}
