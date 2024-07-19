using KBCore.Refs;
using UnityEngine;

namespace DescentII
{
    public class RangeCreature : HostileCreature
    {
        [SerializeField] GameObject projectile;
        [SerializeField] Transform shootPoint;

        protected new void Start()
        {
            base.Start();

            attackStrategy = new RangeAttackStrategy(transform, agent, attackDetector.Player,
                projectile, shootPoint, damage, attackCooldown);
            attackState.SetStrategy(attackStrategy);
        }
    }
}