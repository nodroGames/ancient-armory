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
        List<Weapon> MeleeWeaponsPile;
        List<Weapon> RangedWeaponsPile;
        List<Armor> ArmorPile;
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

            GameObject merc = WaitingLine[0];
            WaitingLine.RemoveAt(0);

            SetDefenderInList(merc, BackLine, FrontLine);
            CheckForRangedEquipment(merc);
            readyIcon.SetActive(false);
        }

        public override void OnLeftButton()
        {
            Debug.Log("Send to Melee!", this);

            GameObject merc = WaitingLine[0];
            WaitingLine.RemoveAt(0);

            SetDefenderInList(merc, FrontLine, BackLine);
            CheckForMeleeEquipment(merc);
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

        void CheckForMeleeEquipment(GameObject merc)
        {
            MercController controller = merc.GetComponent<MercController>();
            if (MeleeWeaponsPile.Count > 0)
                GetWeaponFromPile("melee", controller);
            if (ArmorPile.Count > 0)
                GetArmorFromPile("heavy_armor", controller);
        }

        void CheckForRangedEquipment(GameObject merc)
        {
            MercController controller = merc.GetComponent<MercController>();
            if (RangedWeaponsPile.Count > 0)
                GetWeaponFromPile("ranged", controller);
            if (ArmorPile.Count > 0)
                GetArmorFromPile("light_armor", controller);
        }

        void GetWeaponFromPile(String type, MercController controller)
        {
            // walk over to pile
            if (type.Contains("melee"))
                controller.weapon = MeleeWeaponsPile[0];
            else
                controller.weapon = RangedWeaponsPile[0];
        }

        void GetArmorFromPile(String type, MercController controller)
        {
            // walk over to pile
            for(int i = 0; i<ArmorPile.Count; i++)
            {
                if (ArmorPile[i].Category == type)
                {
                    controller.SetArmorAndDefense(ArmorPile[i]);
                    return;
                }
            }
            controller.SetArmorAndDefense(ArmorPile[0]);
        }
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
