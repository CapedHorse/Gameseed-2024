using System;
using Core;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI.Panel
{
    public class LevelSelectionPanel : UIPanel
    {
        [SerializeField] private PlayerInput levelSelectInput;
        [SerializeField] private LevelSelectButton[] levelSelectButtons;
        [SerializeField] private Button[] levelButtons;

        private void Start()
        {
            ToggleLevelInput(false);
        }

        public void ToggleLevelInput(bool inputOn)
        {
            if(inputOn)
                levelSelectInput.ActivateInput();
            else
                levelSelectInput.DeactivateInput();
        }

        public override void ShowPanel()
        {
            base.ShowPanel();

            for (int i = 0; i < GameManager.instance.playerData.levelDatas.Count; i++)
            {
                int levelId = i;
                LevelData levelData = GameManager.instance.playerData.levelDatas[levelId];

                if (levelData.unlocked)
                {
                    levelSelectButtons[levelId].SetUnlocked(levelData);
                    levelSelectButtons[levelId].AddListenerToButton(() =>
                    {
                        GameManager.instance.StartLevel(levelId);
                    });
                }
                else
                {
                    levelSelectButtons[levelId].SetLocked();
                }
            }

            FinishShowPanel();
        }

        protected override void FinishShowPanel()
        {
            base.FinishShowPanel();
        }

        public override void HidePanel()
        {
            base.HidePanel();
            
            FinishHidePanel();
        }

        protected override void FinishHidePanel()
        {
            base.FinishHidePanel();
        }
    }
}