using Audio;
using DG.Tweening;
using UI.Panel;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Pause_Settings
{
    public class SettingsPanel : UIPanel

    {
        [SerializeField] private Image parentFadeImage;
        [SerializeField] private Transform tweenedBG;
        [SerializeField] private float tweenTime = 0.25f;

        [SerializeField] private Slider sfxVolumeSlider, bgmVolumeSlider;

        public override void ShowPanel()
        {
            parentFadeImage.DOFade(0, 0).SetUpdate(true);
            tweenedBG.DOScale(0, 0).SetUpdate(true);
            
            float curSFXVol = 0;
            AudioBGMManager.instance.SFXAudiMixer.audioMixer.GetFloat("SFX Volume", out curSFXVol);
            sfxVolumeSlider.value = curSFXVol;
            
            float curBGMVol = 0;
            AudioBGMManager.instance.BGMAudiMixer.audioMixer.GetFloat("BGM Volume", out curBGMVol);
            bgmVolumeSlider.value = curBGMVol;
            
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