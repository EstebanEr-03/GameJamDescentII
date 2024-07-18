using UnityEngine;

namespace DescentII
{
    public class DeathStrategy : BaseDeathStrategy
    {
        public DeathStrategy(GameObject creature, float duration) : base(creature, duration) { }

        public override void UpdateAnimator(Animator animator)
        {
            animator.CrossFade(AnimatorUtil.Death, AnimatorUtil.Duration);
        }

        public override void Update()
        {
            deathTimer.Tick(Time.deltaTime);
        }

        protected override void KillCreature()
        {
            Object.Destroy(creature);
        }
    }

    public class DropeableStrategy : BaseDeathStrategy
    {
        protected GameObject dropeable;
        protected Vector3 spawnPosition;

        public DropeableStrategy(GameObject creature, GameObject dropeable,
            float duration) : base(creature, duration)
        {
            spawnPosition = creature.transform.position;
            this.dropeable = dropeable;
        }

        public override void UpdateAnimator(Animator animator)
        {
            animator.CrossFade(AnimatorUtil.Death, AnimatorUtil.Duration);
        }

        public override void Update()
        {
            deathTimer.Tick(Time.deltaTime);
        }

        protected override void KillCreature()
        {
            Object.Destroy(creature);
            Object.Instantiate(dropeable, spawnPosition, Quaternion.identity);
        }
    }
}
