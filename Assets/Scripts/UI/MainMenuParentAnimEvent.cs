using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainMenuParentAnimEvent : MonoBehaviour
{

    public UnityEvent onEventInvoked;

    public void InvokeEvent()
    {
        onEventInvoked.Invoke();
    }
}
