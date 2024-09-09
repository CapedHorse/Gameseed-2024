using System;
using UnityEngine;
using UnityEngine.UI;

namespace Components.Objects.SpecificObjects.MatchPicture
{
    public class TemperatureFillStretcher : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Transform point;

        private void Update()
        {
            slider.value = point.transform.localPosition.y;
        }
    }
}