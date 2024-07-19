using Cinemachine;
using KBCore.Refs;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace DescentII
{
    public class PlayerController : ValidatedMonoBehaviour
    {
        [Header("References")]
        [SerializeField, Self] Rigidbody rb;
        [SerializeField, Self] Animator animator;
        [SerializeField, Self] GroundChecker groundChecker;
        [SerializeField, Self] Health health;
        [SerializeField, Anywhere] CinemachineFreeLook freeLookCam;
        [SerializeField, Anywhere] InputReader input;

        [Header("Movement Settings")]
        [SerializeField] float moveSpeed = 6f;
        [SerializeField] float rotationSpeed = 15f;
        [SerializeField] float smoothTime = 0.2f;

        [Header("Jump Settings")]
        [SerializeField] float JumpForce = 10f;
        [SerializeField] float jumpDuration = 0.5f;
        [SerializeField] float jumpCooldown = 0f;
        [SerializeField] float gravityMultiplier = 1f;

        [Header("Run Settings")]
        [SerializeField] float runForce = 10f;
        [SerializeField] float runDuration = 1f;
        [SerializeField] float runCooldown = 0f;

        [Header("Attack Settings")]
        [SerializeField] float attackCooldown = 0.5f;
        [SerializeField] float attackDistance = 2f;
        [SerializeField] int damageAmount = 10;

        [Header("Receiving Damage Settings")]
        [SerializeField] float receiveDamageDuration = 0.5f;

        const float ZeroF = 0f;

        Transform mainCam;

        float currentSpeed;
        float velocity;
        float jumpVelocity;
        float runVelocity;

        Vector3 movement;

        List<Timer> timers;
        CountdownTimer jumpTimer;
        CountdownTimer jumpCooldownTimer;
        CountdownTimer runTimer;
        CountdownTimer runCooldownTimer;
        CountdownTimer attackTimer;
        CountdownTimer recieveDamageTimer;

        StateMachine stateMachine;

        // Animator parameters
        static readonly int Speed = Animator.StringToHash("Speed");

        void Awake()
        {
            mainCam = Camera.main.transform;
            freeLookCam.Follow = transform;
            freeLookCam.LookAt = transform;
            // Invoke event when observed transform is teleported, adjusting freeLookCam's position accordingly
            freeLookCam.OnTargetObjectWarped(
                transform,
                transform.position - freeLookCam.transform.position - Vector3.forward
                );

            rb.freezeRotation = true;

            SetUpTimers();
            SetUpStateMachine();

        }

        private void SetUpStateMachine()
        {
            // State Machine
            stateMachine = new StateMachine();

            // Declare States
            var locomotionState = new LocomotionState(this, animator);
            var jumpState = new JumpState(this, animator);
            var runState = new RunState(this, animator);
            var attackState = new AttackState(this, animator);
            var receiveDamageState = new ReceiveDamageState(this, animator);
            var deathState = new DeathState(this, animator);

            // Define Transitions
            At(locomotionState, jumpState, new FuncPredicate(() => jumpTimer.IsRunning));
            At(locomotionState, runState, new FuncPredicate(() => runTimer.IsRunning));
            At(locomotionState, attackState, new FuncPredicate(() => attackTimer.IsRunning));

            Any(receiveDamageState, new FuncPredicate(() => recieveDamageTimer.IsRunning));
            Any(deathState, new FuncPredicate(() => health.IsDead));

            Any(locomotionState, new FuncPredicate(() => ReturnToLocomationState()));

            // Set initial state
            stateMachine.SetState(locomotionState);
        }

        private bool ReturnToLocomationState()
        {
            return groundChecker.IsGrounded 
                && !attackTimer.IsRunning
                && !jumpTimer.IsRunning 
                && !runTimer.IsRunning
                && !recieveDamageTimer.IsRunning
                && !health.IsDead;
        }

        private void SetUpTimers()
        {
            // Setup timers
            jumpTimer = new CountdownTimer(jumpDuration);
            jumpCooldownTimer = new CountdownTimer(jumpCooldown);

            runTimer = new CountdownTimer(runDuration);
            runCooldownTimer = new CountdownTimer(runCooldown);

            attackTimer = new CountdownTimer(attackCooldown);

            recieveDamageTimer = new CountdownTimer(receiveDamageDuration);

            jumpTimer.OnTimeStart += () => jumpVelocity = JumpForce;
            jumpTimer.OnTimeStop += () => jumpCooldownTimer.Start();

            runTimer.OnTimeStart += () => runVelocity = runForce;
            runTimer.OnTimeStop += () => runCooldownTimer.Start();

            attackTimer.OnTimeStop += () => attackTimer.Stop();

            recieveDamageTimer.OnTimeStop += () => recieveDamageTimer.Stop();

            timers = new List<Timer>(capacity: 6) {
                jumpTimer, jumpCooldownTimer, runTimer, runCooldownTimer, attackTimer, recieveDamageTimer};
        }

        void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
        void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);

        void Start() => input.EnablePlayerActions();

        void OnEnable()
        {
            input.Jump += OnJump;
            input.Run += OnRun;
            input.Attack += OnAttack;
            health.RecieveDamage += OnRecieveDamage;
        }

        void OnDisable()
        {
            input.Jump -= OnJump;
            input.Run -= OnRun;
            input.Attack -= OnAttack;
            health.RecieveDamage -= OnRecieveDamage;
        }

        void Update()
        {
            movement = new Vector3(input.Direction.x, 0f, input.Direction.y);
            stateMachine.Update();

            HandleTimers();
            UpdateAnimator();
        }

        void FixedUpdate()
        {
            stateMachine.FixedUpdate();

            HandleRun();
        }

        private void OnRecieveDamage()
        {
            recieveDamageTimer.Start();
        }

        private void OnAttack()
        {
            if (!attackTimer.IsRunning)
            {
                attackTimer.Start();
            }
        }

        public void Attack()
        {
            Vector3 attackPos = transform.position + transform.forward;
            Collider[] hitEnemies = Physics.OverlapSphere(attackPos, attackDistance);

            foreach (var enemy in hitEnemies)
            {
                // Debug.Log(enemy.name);
                if (enemy.CompareTag("Creature"))
                {
                    enemy.GetComponent<Health>().TakeDamage(damageAmount);
                }
            }
        }

        void OnJump(bool performed)
        {
            if (performed && !jumpTimer.IsRunning && !jumpCooldownTimer.IsRunning && groundChecker.IsGrounded)
            {
                jumpTimer.Start();
            }
            else if (!performed && jumpTimer.IsRunning)
            {
                jumpTimer.Stop();
            }
        }

        void OnRun(bool performed)
        {
            if (performed && !runTimer.IsRunning && !runCooldownTimer.IsRunning)
            {
                runTimer.Start();
            }
            else if (!performed && runTimer.IsRunning)
            {
                runTimer.Stop();
            }
        }

        public void HandleRun()
        {
            if (!runTimer.IsRunning)
            {
                runVelocity = ZeroF;
                runTimer.Stop();
                return;
            }
        }

        public void HandleJump()
        {
            // If not jumping and grounded, keep jump velocity at 0
            if (!jumpTimer.IsRunning && groundChecker.IsGrounded)
            {
                jumpVelocity = ZeroF;
                jumpTimer.Stop();
                return;
            }

            // If jumping or falling burst of velocity
            if (!jumpTimer.IsRunning)
            {
                // Gravity takes over
                jumpVelocity += Physics.gravity.y * gravityMultiplier * Time.fixedDeltaTime;
            }

            // Apply velocity
            rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);
        }

        void HandleTimers()
        {
            foreach (var timer in timers)
            {
                timer.Tick(Time.deltaTime);
            }
        }

        public void UpdateAnimator()
        {
            animator.SetFloat(Speed, currentSpeed);
        }

        public void HandleMovement()
        {
            // Rotate movement direction to match camera rotation
            var adjustedDirection = Quaternion.AngleAxis(mainCam.eulerAngles.y, Vector3.up) * movement;
            if (adjustedDirection.magnitude > ZeroF)
            {
                HandleRotation(adjustedDirection);
                HandleHorizontalMovement(adjustedDirection);
                SmoothSpeed(adjustedDirection.magnitude);
            }
            else
            {
                SmoothSpeed(ZeroF);
                // Reset horizontal velocity for a snappy stop
                rb.velocity = new Vector3(ZeroF, rb.velocity.y, ZeroF);
            }
        }

        void HandleHorizontalMovement(Vector3 direction)
        {
            // UpdatePosition player
            Vector3 velocity = direction * ((moveSpeed + runVelocity) * Time.fixedDeltaTime);
            rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
        }

        void HandleRotation(Vector3 direction)
        {   // Adjust rotation to match movement direction
            var targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.LookAt(transform.position + direction);
        }


        void SmoothSpeed(float value)
        {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, value, ref velocity, smoothTime);
        }
    }
}
