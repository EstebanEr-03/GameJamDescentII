using UnityEngine;
using UnityEngine.AI;
using Utilities;

namespace DescentII
{
    public abstract class EnemyBaseStrategy
    {
        public abstract void OnEnter();
        public abstract void UpdateAnimator(Animator animator);
        public abstract void Update();
    }

    public abstract class BaseMobilityStrategy : EnemyBaseStrategy
    {
        protected NavMeshAgent agent;

        public BaseMobilityStrategy(NavMeshAgent agent)
        {
            this.agent = agent;
        }
    }

    public abstract class BaseAttackStrategy : EnemyBaseStrategy
    {
        protected Transform creature;
        protected Transform target;
        protected float rotationSpeed = 5f;
        protected int damage;
        protected CountdownTimer attackTimer;
        protected NavMeshAgent agent;

        public BaseAttackStrategy(Transform creature, NavMeshAgent agent, Transform target, int damage, float cooldown)
        {
            this.creature = creature;
            this.target = target;
            this.damage = damage;
            attackTimer = new CountdownTimer(cooldown);
            this.agent = agent;
        }

        public override void OnEnter()
        {
            Debug.Log("On Attack: Stop!");
            agent.SetDestination(creature.transform.position);
        }

        public override void Update()
        {
            RotateTowardsTarget();
        }

        protected void RotateTowardsTarget()
        {
            Vector3 direction = (target.position - creature.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            creature.rotation = Quaternion.Slerp(creature.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }

    public abstract class BaseGetHitStrategy : EnemyBaseStrategy
    {
        public BaseGetHitStrategy() { }
    }

    public abstract class BaseDeathStrategy : EnemyBaseStrategy
    {
        protected GameObject creature;
        protected CountdownTimer deathTimer;
        protected NavMeshAgent agent;

        public BaseDeathStrategy(GameObject creature, NavMeshAgent agent, float duration)
        {
            this.creature = creature;
            deathTimer = new CountdownTimer(duration);
            deathTimer.Start();
            deathTimer.OnTimeStop += () => KillCreature();
            this.agent = agent;
        }

        public override void OnEnter()
        {
            Debug.Log("On Death: Stop!");
            agent.SetDestination(creature.transform.position);
        }

        protected abstract void KillCreature();
    }

}
