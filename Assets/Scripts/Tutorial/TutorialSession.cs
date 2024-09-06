using UnityEngine;
using UnityEngine.Events;

namespace Tutorial
{
    public class TutorialSession : MonoBehaviour
    {
        public UnityEvent onTutorialStarted, onTutorialEnded;
        public void StartTutor()
        {
            onTutorialStarted.Invoke();
        }

        public void EndTutor()
        {
            onTutorialEnded.Invoke();
        }
        
        public void TutorSessionEnded()
        {
            EndTutor();
            TutorialManager.instance.CanEndTutor();
        }

    }
}
