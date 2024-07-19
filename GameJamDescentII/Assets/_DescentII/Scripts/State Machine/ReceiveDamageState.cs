using UnityEngine;

namespace DescentII
{
    public class ReceiveDamageState : BaseState
    {
        public ReceiveDamageState(PlayerController player, Animator animator) : base(player, animator) { }

        public override void OnEnter()
        {
            // Debug.Log("Entering Receive Damage State");
            animator.CrossFade(ReceiveDamageHash, crossFadeDuration);
        }

        public override void FixedUpdate()
        {
            player.HandleMovement();
        }
    }
}