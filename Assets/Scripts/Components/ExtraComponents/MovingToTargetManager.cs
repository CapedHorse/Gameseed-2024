using UnityEngine;

namespace Components.ExtraComponents
{
    public class MovingToTargetManager : MonoBehaviour
    {
        [SerializeField] MovingToTargetComponent[] movingComps;

        public void StopMovingAll(bool stop)
        {
            foreach (var moving in movingComps)
            {
                moving.StopMoving(stop);
            }
        }
    }
}
