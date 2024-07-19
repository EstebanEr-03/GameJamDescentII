using UnityEngine;
using Utilities;

namespace DescentII
{
    public class ConeDetectionStrategy : IDetectionStrategy
    {
        private readonly float detectionRadius;
        private readonly float detectionAngle;

        public ConeDetectionStrategy(float detectionAngle, float detectionRadius)
        {
            this.detectionAngle = detectionAngle;
            this.detectionRadius = detectionRadius;
        }

        public bool Execute(Transform player, Transform detector, CountdownTimer timer)
        {
            if (timer.IsRunning) return false;

            var directionToPlayer = player.position - detector.position;
            var angleToPlayer = Vector3.Angle(directionToPlayer, detector.forward);

            // If the player is not within the detection angle + outer radius (aka the cone in front of the enemy)
            // or is within the inner radius, return false
            if (!(angleToPlayer < detectionAngle / 2f) || !(directionToPlayer.magnitude < detectionRadius))
                return false;

            timer.Start();
            return true;
        }
    }

    public class RadiusDetectionStrategy : IDetectionStrategy
    {
        private readonly float detectionRadius;
        protected Vector3 directionToPlayer;

        public RadiusDetectionStrategy(float detectionRadius)
        {
            this.detectionRadius = detectionRadius;
        }

        public bool Execute(Transform player, Transform detector, CountdownTimer timer)
        {
            if (timer.IsRunning) return false;

            if (player.position.y < detector.position.y) return false;

            directionToPlayer = player.position - detector.position;

            // If the player is not within the outer radius (aka the cone in front of the enemy)
            if (!(directionToPlayer.magnitude < detectionRadius))
                return false;

            timer.Start();
            return true;
        }
    }
}
