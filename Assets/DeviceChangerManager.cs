using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeviceChangerManager : MonoBehaviour
{
    public static DeviceChangerManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            return;
        }
        
        Destroy(gameObject);
    }
}
