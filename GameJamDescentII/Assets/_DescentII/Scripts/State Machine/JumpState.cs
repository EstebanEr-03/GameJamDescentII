using UnityEngine;

namespace DescentII
{
    public class JumpState : BaseState
    {
        public JumpState(PlayerController player, Animator animator) : base(player, animator) { }

        public override void OnEnter()
        {
            //Debug.Log("Entering Jump State");
            animator.CrossFade(JumpHash, crossFadeDuration);
        }

        public override void FixedUpdate()
        {
            player.HandleJump();
            player.HandleMovement();
        }
    }

    public class DeathState : BaseState
    {
        public DeathState(PlayerController player, Animator animator) : base(player, animator) { }

        public override void OnEnter()
        {
            // Debug.Log("Entering Die State");
            animator.CrossFade(DieHash, crossFadeDuration);
        }
    }
}