using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Components.Objects.SpecificObjects.Boss
{
    public class AttackWarningSign : MonoBehaviour
    {
        [SerializeField] private bool warnXPos = true;
        [SerializeField] private bool warnYPos = true;

        public void Warn(Transform target, float warnTime, UnityAction actionAfterWarn)
        {
            gameObject.SetActive(true);
            StartCoroutine(StartWarning(target, warnTime, actionAfterWarn));
        }

        private IEnumerator StartWarning(Transform target, float warnTime, UnityAction actionAfterWarn)
        {
            transform.position = new Vector2(
                warnXPos ? target.position.x : transform.position.x, 
                warnYPos ? target.position.y : transform.position.y);
            yield return new WaitForSeconds(warnTime);
            gameObject.SetActive(false);
            actionAfterWarn?.Invoke();
        }
    }
}