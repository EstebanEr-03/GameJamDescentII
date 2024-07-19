using UnityEngine;
using Utilities;

namespace DescentII
{
    public interface IDetectionStrategy
    {
        bool Execute(Transform player, Transform detector, CountdownTimer timer);
    }
}
