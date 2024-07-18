using UnityEngine;

namespace DescentII
{
    public class AttackState : BaseState
    {
        public AttackState(PlayerController player, Animator animator) : base(player, animator) { }

        public override void OnEnter()
        {
            //Debug.Log("Entering Attack State");
            animator.CrossFade(AttackHash, crossFadeDuration);
            player.Attack();
        }

        public override void FixedUpdate()
        {
            player.HandleMovement();
        }
    }
}