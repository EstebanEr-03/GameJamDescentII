using UnityEngine;

namespace DescentII
{
    [RequireComponent(typeof(PlayerDetector))]
    public class HostileCreature : Creature
    {
        [SerializeField]
        protected PlayerDetector attackDetector;
        [SerializeField] 
        protected float attackCooldown = 2f;
        [SerializeField]
        protected int damage = 10;

        protected BaseMobilityStrategy chaseStrategy;
        protected BaseAttackStrategy attackStrategy;

        protected EnemyAttackState attackState;

        protected new void Start()
        {
            base.Start();

            chaseStrategy = new ChaseStrategy(agent, chaseDetector.Player);

            attackState = new EnemyAttackState(this, animator);

            At(mobilityState, attackState,
                               new FuncPredicate(() => attackDetector.CanDetectPlayer()));
            At(attackState, mobilityState,
                               new FuncPredicate(() => !attackDetector.CanDetectPlayer()));

            mobilityState.AddStrategy(chaseStrategy,
                               new FuncPredicate(() => chaseDetector.CanDetectPlayer()));
        }
    }
}