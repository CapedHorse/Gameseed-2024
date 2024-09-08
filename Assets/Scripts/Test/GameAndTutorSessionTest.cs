using System.Collections;
using Level;
using Tutorial;
using UnityEngine;

namespace Test
{
    public class GameAndTutorSessionTest : MonoBehaviour
    {
        private GameSession testedGameSession;
        private TutorialSession testedGamTutorialSession;
        [SerializeField] private float testingDelayTime;

        private IEnumerator Start()
        {
            testedGameSession = FindObjectOfType<GameSession>();
            testedGamTutorialSession = FindObjectOfType<TutorialSession>();
            
            yield return new WaitForSeconds(testingDelayTime);
            if (testedGameSession)
            {
                FindObjectOfType<GameSessionManager>().InitializeTesting(testedGameSession.name);
                yield return null;
            }

            if (testedGamTutorialSession)
            {
                testedGamTutorialSession.StartTutor();
                yield return null;
            }
        }
    }
}