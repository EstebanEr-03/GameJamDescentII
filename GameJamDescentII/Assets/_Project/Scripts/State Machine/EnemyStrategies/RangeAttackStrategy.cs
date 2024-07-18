using UnityEngine;

namespace DescentII
{
    public class RangeAttackStrategy : BaseAttackStrategy
    {
        protected GameObject projectile;
        protected Transform shootPoint;

        public RangeAttackStrategy(Transform creature, Transform target, GameObject projectile,
            Transform shootPoint, int damage, float cooldown) 
            : base(creature, target, damage, cooldown)
        {
            this.projectile = projectile;
            this.shootPoint = shootPoint;
        }

        public override void UpdateAnimator(Animator animator)
        {
            animator.CrossFade(AnimatorUtil.Shoot, AnimatorUtil.Duration);
        }

        public override void Update()
        {
            base.Update();

            attackTimer.Tick(Time.deltaTime);

            if (attackTimer.IsRunning) return;

            attackTimer.Start();
            var direction = (target.position - creature.position).normalized;
            Object.Instantiate(projectile, shootPoint.position, Quaternion.LookRotation(direction));
        }
    }
}
