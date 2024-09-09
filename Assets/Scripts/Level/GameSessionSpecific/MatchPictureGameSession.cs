using System.Collections;
using Components.Objects.SpecificObjects.MatchPicture;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Level.GameSessionSpecific
{
    public class MatchPictureGameSession : GameSession
    {
        [SerializeField] private Transform memorizedParent;
        [SerializeField] private Transform answersParent;
        [SerializeField] private MatchPictureBox[] optionBoxes; 
        [SerializeField] private MatchPictureBox correctAnswer;
        [SerializeField] float memorizeTime = 4f;

        public UnityEvent onMemorizedShownEvent, onAnswersShownEvent;
        
        public void CheckCorrectAnswer()
        {
            foreach (var option in optionBoxes)
            {
                if (option.IsEntered)
                {
                    if (option == correctAnswer)
                    {
                        option.CheckedAnswer(true);
                        GameSessionSuccess();
                        return;
                    }
                    
                    option.CheckedAnswer(false);
                    break;
                }
            }
            
            GameSessionRetry();
        }

        public override void StartGame()
        {
            base.StartGame();
            StartCoroutine(InitiateMemorizing());

        }

        IEnumerator InitiateMemorizing()
        {
            answersParent.gameObject.SetActive(false);
            memorizedParent.DOScale(0, 0).SetUpdate(true);
            memorizedParent.gameObject.SetActive(true);
            memorizedParent.DOScale(Vector2.one, 0.25f).SetUpdate(true);
            onMemorizedShownEvent.Invoke();
            yield return new WaitForSeconds(memorizeTime);
            memorizedParent.DOScale(0, 0.25f).SetUpdate(true);
            answersParent.DOMoveX(12f, 0).SetUpdate(true);
            answersParent.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.25f);
            answersParent.DOMoveX(0, 0.25f).SetUpdate(true);
            onAnswersShownEvent.Invoke();
        }

        public override void TimerRunsOut()
        {
            base.TimerRunsOut();
            CheckCorrectAnswer();
        }
    }
}