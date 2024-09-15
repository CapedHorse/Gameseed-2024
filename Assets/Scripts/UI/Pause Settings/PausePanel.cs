using DG.Tweening;
using UI.Panel;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Pause_Settings
{
    public class PausePanel : UIPanel
    {
        [SerializeField] private Image parentFadeImage;
        [SerializeField] private Transform tweenedBG;
        [SerializeField] private float tweenTime = 0.25f;

        public override void ShowPanel()
        {
            parentFadeImage.DOFade(0, 0).SetUpdate(true);
            tweenedBG.DOScale(0, 0).SetUpdate(true);
            base.ShowPanel();
            parentFadeImage.DOFade(1, tweenTime).SetUpdate(true);
            tweenedBG.DOScale(1, tweenTime).SetUpdate(true).onComplete = FinishShowPanel;
        }

        public override void HidePanel()
        {
            base.HidePanel();
            parentFadeImage.DOFade(0, tweenTime).SetUpdate(true);
            tweenedBG.DOScale(0, tweenTime).SetUpdate(true).onComplete = FinishHidePanel;
        }
    }
}