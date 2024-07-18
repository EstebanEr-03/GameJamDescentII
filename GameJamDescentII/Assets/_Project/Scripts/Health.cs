using UnityEngine;
using UnityEngine.Events;

namespace DescentII
{
    public class Health : MonoBehaviour
    {
        [SerializeField] int maxHealth = 100;
        [SerializeField] FloatEventChannel playerHealthChannel;

        public event UnityAction RecieveDamage = delegate { };

        [SerializeField] int currentHealth;

        public bool IsDead => currentHealth <= 0;

        void Awake()
        {
            currentHealth = maxHealth;
        }

        void Start()
        {
            PublishHealthPercentage();
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            RecieveDamage.Invoke();
            PublishHealthPercentage();
        }

        public void Heal(int health)
        {
            currentHealth = Mathf.Min(currentHealth + health, maxHealth);
            PublishHealthPercentage();
        }

        void PublishHealthPercentage()
        {
            playerHealthChannel?.Invoke(currentHealth / (float)maxHealth);
        }

        [ContextMenu("Take Damage")]
        public void TakeDamageTest() => TakeDamage(10);

        [ContextMenu("Heal by 10")]
        public void HealTest() => Heal(10);

        [ContextMenu("Kill Unit")]
        public void KillUnit() => TakeDamage(currentHealth);
    }
}
