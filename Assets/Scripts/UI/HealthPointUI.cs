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
            healthFillUI.gameObject.SetActive(false);
        }
    }
}