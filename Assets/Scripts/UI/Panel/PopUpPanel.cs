using System.Collections;
using System.Collections.Generic;
using UI.Panel;
using UnityEngine;

public class PopUpPanel : UIPanel
{
    [SerializeField] Animator popUpAnimator;
    
    public override void ShowPanel()
    {
        popUpAnimator.SetTrigger("Show");
        base.ShowPanel();
    }

    public override void HidePanel()
    {
        popUpAnimator.SetTrigger("Hide");
        base.HidePanel();
    }

    public override void FinishShowPanel()
    {
        base.FinishShowPanel(); 
            
    }

    protected override void FinishHidePanel()
    {
        base.FinishHidePanel();
    }
}
