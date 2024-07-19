using UnityEngine;

namespace DescentII
{
    public class LocomotionState : BaseState
    {
        public LocomotionState(PlayerController player, Animator animator) : base(player, animator) { }

        public override void OnEnter()
        {
            //Debug.Log("Entering Locomotion State");
            animator.CrossFade(LocomotionHash, crossFadeDuration);
        }

        public override void FixedUpdate()
        {
            player.HandleMovement();
        }
    }
}