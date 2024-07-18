using UnityEngine;

namespace DescentII
{
    public class MeleeCreature : HostileCreature
    {
        protected new void Start()
        {
            base.Start();

            attackStrategy = new MeleeAttackStrategy(transform, attackDetector.Player,
                attackDetector.PlayerHealth, damage, attackCooldown);
            attackState.SetStrategy(attackStrategy);
        }
    }
}