using UnityEngine;
using UnityEngine.AI;

namespace DescentII
{
    public class WanderStrategy : BaseMobilityStrategy
    {
        readonly float wanderRadius;
        readonly Vector3 startPoint;

        public WanderStrategy(NavMeshAgent agent, Vector3 startPoint,
            float wanderRadius) : base(agent)
        {
            this.wanderRadius = wanderRadius;
            this.startPoint = startPoint;
        }

        public override void UpdateAnimator(Animator animator)
        {
            animator.CrossFade(AnimatorUtil.Walk, AnimatorUtil.Duration);
        }

        public override void Update()
        {
            if (HasReachDestination())
            {
                var randomDirection = UnityEngine.Random.insideUnitSphere * wanderRadius;
                randomDirection += startPoint;
                NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, wanderRadius, 1);
                var finalPosition = hit.position;
                agent.SetDestination(finalPosition);
            }
        }

        public bool HasReachDestination()
        {
            return !agent.pathPending
                && agent.remainingDistance <= agent.stoppingDistance
                && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f);
        }
    }
}
