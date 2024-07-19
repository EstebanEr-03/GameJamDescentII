using UnityEngine;
using UnityEngine.AI;

namespace DescentII
{
    public class DeathStrategy : BaseDeathStrategy
    {
        public DeathStrategy(GameObject creature, NavMeshAgent agent, float duration) : base(creature, agent, duration) { }

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

        public DropeableStrategy(GameObject creature, NavMeshAgent agent, GameObject dropeable,
            float duration) : base(creature, agent, duration)
        {
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
            spawnPosition = creature.transform.position;
            Object.Destroy(creature);
            Object.Instantiate(dropeable, spawnPosition, Quaternion.identity);
        }
    }
}
