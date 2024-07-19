using UnityEngine;
using UnityEngine.AI;
using KBCore.Refs;
using Utilities;

namespace DescentII
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(PlayerDetector))]
    public partial class Creature : Entity
    {
        [SerializeField, Self] protected NavMeshAgent agent;
        [SerializeField, Self] protected Health health;
        [SerializeField, Child] protected Animator animator;
        [SerializeField] protected PlayerDetector chaseDetector;
        [SerializeField] GameObject dropeable;
        [SerializeField] float wanderRadius = 10f;
        [SerializeField] float deathDuration = 1f;
        [SerializeField] float getHitDuration = 1f;

        CountdownTimer getHitTimer;
        StateMachine stateMachine;

        protected BaseMobilityStrategy wanderStrategy;
        protected BaseGetHitStrategy getHitStrategy;
        protected BaseDeathStrategy deathStrategy;

        protected EnemyMobilityState mobilityState;
        protected EnemyGetHitState getHitState;
        protected EnemyDeathState deathState;

        void OnValidate() => this.ValidateRefs();

        protected void Awake()
        {
            stateMachine = new StateMachine();
        }

        void OnEnable()
        {
            health.RecieveDamage += OnRecieveDamage;
        }

        void OnDisable()
        {
            health.RecieveDamage -= OnRecieveDamage;
        }

        protected void Start()
        {
            getHitTimer = new CountdownTimer(getHitDuration);

            wanderStrategy = new WanderStrategy(agent, transform.position, wanderRadius);
            getHitStrategy = new GetHitStrategy();
            deathStrategy = new DropeableStrategy(gameObject, agent, dropeable, deathDuration);

            mobilityState = new EnemyMobilityState(this, animator);
            getHitState = new EnemyGetHitState(this, animator);
            deathState = new EnemyDeathState(this, animator);

            Any(getHitState, 
                new FuncPredicate(() => getHitTimer.IsRunning && !health.IsDead));
            At(getHitState, mobilityState, 
                new FuncPredicate(() => !getHitTimer.IsRunning));
            Any(deathState, 
                new FuncPredicate(() => health.IsDead));

            mobilityState.SetStrategy(wanderStrategy,
                new FuncPredicate(() => !chaseDetector.CanDetectPlayer()));
            getHitState.SetStrategy(getHitStrategy);
            deathState.SetStrategy(deathStrategy);

            stateMachine.SetState(mobilityState);
        }

        protected void At(IState from, IState to, IPredicate condition)
            => stateMachine.AddTransition(from, to, condition);
        protected void Any(IState to, IPredicate condition)
            => stateMachine.AddAnyTransition(to, condition);

        private void OnRecieveDamage()
        {
            getHitTimer.Start();
        }

        protected void Update()
        {
            stateMachine.Update();
            getHitTimer.Tick(Time.deltaTime);
        }

        void FixedUpdate()
        {
            stateMachine.FixedUpdate();
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, wanderRadius);
        }

        //IEnumerator DropCollectable()
        //{
        //    yield return new WaitForSeconds(deathDuration);
        //    Instantiate(dropeable, transform.position, Quaternion.identity);
        //    Destroy(gameObject);
        //}
    }
}
