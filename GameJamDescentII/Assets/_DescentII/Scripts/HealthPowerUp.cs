using UnityEngine;

namespace DescentII
{
    public class HealthPowerUp : Dropeable
    {
        [SerializeField] int healRate = 1;
        public Health PlayerHealth { get; protected set; }

        protected new void Awake()
        {
            base.Awake();
            PlayerHealth = Player.GetComponent<Health>();
        }

        protected override void Execute()
        {
            PlayerHealth.Heal(healRate);
            base.Execute();
        }
    }
}
