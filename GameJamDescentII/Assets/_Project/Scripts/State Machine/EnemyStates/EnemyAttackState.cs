using UnityEngine;

namespace DescentII
{
    public class EnemyAttackState : EnemyBaseStrategyState<BaseAttackStrategy>
    {
        public EnemyAttackState(Creature enemy, Animator animator) 
            : base(enemy, animator) {}
    }
}
