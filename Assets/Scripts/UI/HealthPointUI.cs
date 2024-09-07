using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthPointUI : MonoBehaviour
    {
        [SerializeField] private Image healthFillUI;

        public void DecreaseHealth()
        {
            healthFillUI.gameObject.SetActive(false);
        }
    }
}