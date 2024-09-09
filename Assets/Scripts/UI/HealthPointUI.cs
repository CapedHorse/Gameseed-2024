using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthPointUI : MonoBehaviour
    {
        [SerializeField] private Image healthFillUI;

        private bool _hasDecreased;

        public void DecreaseHealth()
        {
            if (_hasDecreased)
                return;

            _hasDecreased = true;
            
            transform.DOShakeRotation(0.5f, Vector3.one * 10, 150).SetUpdate(true);
            healthFillUI.DOFade(0, 0.75f).SetUpdate(true).onComplete = () =>
            {
                healthFillUI.gameObject.SetActive(false);
            };
        }
    }
}