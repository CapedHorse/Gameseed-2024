using System;
using DG.Tweening;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public static CameraShaker instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            return;
        }
        
        Destroy(gameObject);
    }

    [SerializeField] private int shakeMediumPower = 5;
    [SerializeField] private int shakeHardPower = 8;
    private Tweener _cameraShakingTween;


    public void ShakeCameraMedium()
    {
        if (_cameraShakingTween != null)
        {
            _cameraShakingTween.Kill(true);
        }
            
        _cameraShakingTween = transform.DOShakePosition(0.2f, Vector3.one * 0.05f, shakeMediumPower);
    }

    public void ShakeCameraHard()
    {
        if (_cameraShakingTween != null)
        {
            _cameraShakingTween.Kill(true);
        }
        
        _cameraShakingTween = transform.DOShakePosition(0.2f, Vector3.one * 0.05f, shakeHardPower);
    }
}