using UnityEngine;

namespace Tutorial
{
    public class TutorialSession : MonoBehaviour
    {
        public void TutorSessionEnded()
        {
            TutorialManager.instance.CanEndTutor();
        }
    }
}
