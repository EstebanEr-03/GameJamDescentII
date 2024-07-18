using UnityEngine;
using Utilities;

namespace DescentII
{
    public abstract class PlayerDetector : MonoBehaviour
    {
        [SerializeField] protected float detectionCooldown = 1f;

        protected CountdownTimer detectionTimer;
        protected IDetectionStrategy detectionStrategy;

        public Transform Player { get; protected set; }
        public Health PlayerHealth { get; protected set; }

        void Awake()
        {
            Player = GameObject.FindGameObjectWithTag("Player").transform;
            PlayerHealth = Player.GetComponent<Health>();
        }

        protected void Start()
        {
            detectionTimer = new CountdownTimer(detectionCooldown);
        }

        void Update() => detectionTimer.Tick(Time.deltaTime);

        protected void SetDetectionStrategy(IDetectionStrategy strategy)
        {
            detectionStrategy = strategy;
        }

        public void SoyUnMetodo() { }

        public bool CanDetectPlayer()
        {
            return detectionTimer.IsRunning || detectionStrategy.Execute(Player, transform, detectionTimer);
        }
    }
}
