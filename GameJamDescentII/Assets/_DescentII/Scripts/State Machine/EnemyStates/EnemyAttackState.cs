using UnityEngine;

namespace DescentII
{
    public class EnemyAttackState : EnemyBaseStrategyState<BaseAttackStrategy>
    {
        public EnemyAttackState(Creature enemy, Animator animator) 
            : base(enemy, animator) {}

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("On AttackState");
        }
    }
}
