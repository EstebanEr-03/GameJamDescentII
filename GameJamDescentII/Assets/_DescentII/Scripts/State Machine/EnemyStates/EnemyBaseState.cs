using UnityEngine;

namespace DescentII
{
    public abstract class EnemyBaseState<T> : IState where T : EnemyBaseStrategy
    {
        public Animator GetAnimator() => animator;
        public Transform GetCreature() => enemy.transform;

        protected readonly Creature enemy;
        protected readonly Animator animator;

        protected T strategy;

        protected EnemyBaseState(Creature enemy, Animator animator)
        {
            this.enemy = enemy;
            this.animator = animator;
        }

        public void SetStrategy(T strategy)
        {
            this.strategy = strategy;
        }

        public virtual void OnEnter()
        {
            strategy.OnEnter();
            strategy.UpdateAnimator(animator);
        }

        public virtual void OnExit() { }

        public virtual void FixedUpdate() { }

        public virtual void Update() => strategy.Update();
    }
}
