using UnityEngine;

namespace DescentII
{
    public static class AnimatorUtil
    {
        private static readonly int walkHash = Animator.StringToHash("Walk");
        private static readonly int runHash = Animator.StringToHash("Run");
        private static readonly int attackHash = Animator.StringToHash("Attack");
        private static readonly int shootHash = Animator.StringToHash("Shoot");
        private static readonly int getHitHash = Animator.StringToHash("GetHit");
        private static readonly int deathHash = Animator.StringToHash("Die");

        private const float crossFadeDuration = 0.1f;

        public static int Walk => walkHash;
        public static int Run => runHash;
        public static int Attack => attackHash;
        public static int Shoot => shootHash;
        public static int GetHit => getHitHash;
        public static int Death => deathHash;

        public static float Duration => crossFadeDuration;
    }
}
