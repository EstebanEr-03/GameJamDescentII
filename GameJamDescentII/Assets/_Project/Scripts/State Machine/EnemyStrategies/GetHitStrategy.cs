using UnityEngine;

namespace DescentII
{
    public class GetHitStrategy : BaseGetHitStrategy
    {
        public GetHitStrategy() : base() { }

        public override void UpdateAnimator(Animator animator)
        {
            animator.CrossFade(AnimatorUtil.GetHit, AnimatorUtil.Duration);
        }

        public override void Update() { }
    }
}
