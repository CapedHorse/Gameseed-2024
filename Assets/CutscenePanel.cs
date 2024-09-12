using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CutscenePanel : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void Show()
    {
        animator.SetTrigger("Show");
    }
}
