using UnityEngine;

namespace DescentII
{
    public class EnemyGetHitState : EnemyBaseState<BaseGetHitStrategy>
    {
        public EnemyGetHitState(Creature enemy, Animator animator) 
            : base(enemy, animator) { }
    }
}
