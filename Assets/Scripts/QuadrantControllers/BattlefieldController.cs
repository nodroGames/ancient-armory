using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using RpgDB;

namespace AncientArmory
{
    public sealed class BattlefieldController : ControllerBase
    {
        [Header("---Battlefield Controller---")]
        public List<GameObject> WaitingLine;
        List<GameObject> FrontLine;
        List<GameObject> MiddleLine;
        List<GameObject> BackLine;
        bool inCycle;
        protected override void Start()
        {
            cooldownDelay = 2;
        }

        void Update()
        {
            if (!inCycle && WaitingLine.Count != 0)
                StartTimerCycle();
        }


        //
        // Timer Cycle Functions
        //

        public override void OnReadyIconPressed()
        {
            base.OnReadyIconPressed();
            inCycle = true;
            GameObject nextMerc = WaitingLine[0];
            infoPromptControllerInstance.LoadInfo(nextMerc.GetComponent<MercController>());
        }

        public override void OnRightButton()//accept
        {
            Debug.Log("Send to Ranged!", this);
            SetDefenderInList(WaitingLine[0], BackLine, FrontLine);
            readyIcon.SetActive(false);
        }

        public override void OnLeftButton()
        {
            Debug.Log("Send to Melee!", this);
            SetDefenderInList(WaitingLine[0], FrontLine, BackLine);
            readyIcon.SetActive(false);
        }

        private void OnRecruitingComplete()
        {
            Debug.Log("Recruiting Complete! Increment up!", this);
            timerController.onTimerComplete.RemoveListener(OnRecruitingComplete);
            StartCoroutine(ShowCompleteWindow()); //show message to player
            //TimerCycle(); //or do so immediately
        }

        
        // 
        // Merc Control Functions
        //
        void SetDefenderInList(GameObject merc, List<GameObject> desiredList, List<GameObject> alternateList)
        {
            if (desiredList.Count > 6)
                desiredList.Add(merc);
            else if (MiddleLine.Count > 6)
                MiddleLine.Add(merc);
            else if (alternateList.Count > 6)
                alternateList.Add(merc);
        }
    }
}
