using UnityEngine;
using UnityEngine.AI;

namespace DescentII
{
    public class MeleeAttackStrategy : BaseAttackStrategy
    {
        protected Health targetHealth;

        public MeleeAttackStrategy(Transform creature, NavMeshAgent agent, Transform target, Health targetHealth,
            int damage, float cooldown) : base(creature, agent, target, damage, cooldown)
        {
               this.targetHealth = targetHealth;
        }

        public override void UpdateAnimator(Animator animator)
        {
            animator.CrossFade(AnimatorUtil.Attack, AnimatorUtil.Duration);
        }

        public override void Update()
        {
            base.Update();

            attackTimer.Tick(Time.deltaTime);

            if (attackTimer.IsRunning) return;

            attackTimer.Start();
            targetHealth.TakeDamage(damage);
        }
    }
}
