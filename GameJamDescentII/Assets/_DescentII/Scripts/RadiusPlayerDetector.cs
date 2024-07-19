using UnityEngine;

namespace DescentII
{

    public class RadiusPlayerDetector : PlayerDetector
    {
        [SerializeField] protected float detectionRadius = 10f;

        protected new void Start()
        {
            base.Start();
            SetDetectionStrategy(new RadiusDetectionStrategy(detectionRadius));
        }

        void OnDrawGizmos ()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
    }
}
