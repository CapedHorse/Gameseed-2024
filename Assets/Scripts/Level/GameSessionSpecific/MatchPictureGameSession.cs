using Components.Objects.SpecificObjects.MatchPicture;
using UnityEngine;

namespace Level.GameSessionSpecific
{
    public class MatchPictureGameSession : GameSession
    {
        [SerializeField] private Transform memorizedParent;
        [SerializeField] private Transform answersParent;
        [SerializeField] private MatchPictureBox[] optionBoxes; 
        [SerializeField] private MatchPictureBox correctAnswer;

        public void CheckCorrectAnswer()
        {
            foreach (var option in optionBoxes)
            {
                if (option.IsEntered)
                {
                    if (option == correctAnswer)
                    {
                        GameSessionSuccess();
                        return;
                    }
                }
            }
            
            GameSessionFailed();
        }
    }
}