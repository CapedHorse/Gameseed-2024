using UnityEngine;

namespace DefaultNamespace
{
    public class CameraShakeRequester : MonoBehaviour
    {
        public void RequestMediumShake()
        {
            CameraShaker.instance.ShakeCameraMedium();
        }
        
        public void RequestHardShake()
        {
            CameraShaker.instance.ShakeCameraHard();
        }
    }
}