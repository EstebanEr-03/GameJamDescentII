using UnityEngine;

namespace DescentII
{
    public class RangeCreature : HostileCreature
    {
        [SerializeField] GameObject projectile;

        protected new void Start()
        {
            base.Start();

            attackStrategy = new RangeAttackStrategy(transform, attackDetector.Player,
                projectile, transform, damage, attackCooldown);
            attackState.SetStrategy(attackStrategy);
        }
    }
}