using UnityEngine;

namespace DescentII
{
    public class ConePlayerDetector : RadiusPlayerDetector
    {
        [SerializeField] float detectionAngle = 60f; // Cone in front of enemy
        [SerializeField] int segments = 18;

        protected new void Start()
        {
            base.Start();
            SetDetectionStrategy(new ConeDetectionStrategy(detectionAngle, detectionRadius));
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;

            Gizmos.DrawWireSphere(transform.position, detectionRadius);

            float a = Mathf.Sin(detectionAngle / 2 * Mathf.Deg2Rad) * detectionRadius;
            float b = Mathf.Cos(detectionAngle / 2 * Mathf.Deg2Rad) * detectionRadius;
            Vector3 target = transform.position + transform.forward * b;
            target.y += a;
            Gizmos.DrawLine(transform.position, target);

            // Calculate cone directions
            Vector3 forwardConeDirection = Quaternion.Euler(0, detectionAngle / 2, 0) * transform.forward * detectionRadius;
            Vector3 backwardConeDirection = Quaternion.Euler(0, -detectionAngle / 2, 0) * transform.forward * detectionRadius;

            // Draw lines to represent the cone
            Gizmos.DrawLine(transform.position, transform.position + forwardConeDirection);
            Gizmos.DrawLine(transform.position, transform.position + backwardConeDirection);
        }
    }
}
