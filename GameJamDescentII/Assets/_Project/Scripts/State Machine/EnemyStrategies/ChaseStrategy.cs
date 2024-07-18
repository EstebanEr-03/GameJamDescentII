using UnityEngine;
using UnityEngine.AI;

namespace DescentII
{
    public class ChaseStrategy : BaseMobilityStrategy
    {
        readonly Transform player;

        public ChaseStrategy(NavMeshAgent agent, Transform player) : base(agent)
        {
            this.player = player;
        }

        public override void UpdateAnimator(Animator animator)
        {
            animator.CrossFade(AnimatorUtil.Run, AnimatorUtil.Duration);
        }

        public override void Update()
        {
            agent.SetDestination(player.position);
        }
    }
}
