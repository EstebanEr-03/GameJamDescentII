﻿using UnityEngine;

namespace DescentII
{
    public abstract class BaseState : IState
    {
        protected readonly PlayerController player;
        protected readonly Animator animator;

        protected static readonly int LocomotionHash = Animator.StringToHash("Locomotion");
        protected static readonly int JumpHash = Animator.StringToHash("Jump");
        protected static readonly int RunHash = Animator.StringToHash("Run");
        protected static readonly int AttackHash = Animator.StringToHash("Attack");
        protected static readonly int ReceiveDamageHash = Animator.StringToHash("ReceiveDamage");
        protected static readonly int DieHash = Animator.StringToHash("Die");

        protected const float crossFadeDuration = 0.1f;

        protected BaseState(PlayerController player, Animator animator)
        {
            this.player = player;
            this.animator = animator;
        }

        public virtual void FixedUpdate()
        {
            //throw new System.NotImplementedException();
        }

        public virtual void OnEnter()
        {
            //throw new System.NotImplementedException();
        }

        public virtual void OnExit()
        {
            // Debug.Log("Exiting State");
        }

        public virtual void Update()
        {
            //throw new System.NotImplementedException();
        }
    }
}