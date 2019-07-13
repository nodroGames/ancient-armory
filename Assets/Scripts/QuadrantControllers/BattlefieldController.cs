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

        protected override void Start()
        {
        }

        void Update()
        {
            // if (Battlefield.transform.childCount != 0)
            // {
            //     if (!InitialLoadComplete && Time.time > twoSecondsFromNow)
            //     {
            //         merc_1 = getPoolContents(Battlefield)[0].GetComponent<Character>();
            //         merc_2 = getPoolContents(Battlefield)[1].GetComponent<Character>();
            //         merc_1.Right_Hand = GameDatabase.Weapons.GetByName("Bow");
            //         merc_2.Right_Hand = GameDatabase.Weapons.GetByName("Longsword");
            //         merc_1.Armor = GameDatabase.Armor.GetByName("Ceremonial plate, troop");
            //         InitialLoadComplete = true;
            //         twoSecondsFromNow = Time.time + 2;
            //     }
            //     if (Time.time > twoSecondsFromNow)
            //     {
            //         merc_1 = merc_1 ?? getPoolContents(Battlefield)[0].GetComponent<Character>();
            //         merc_2 = merc_2 ?? getPoolContents(Battlefield)[1].GetComponent<Character>();
            //         if(merc_1.Hit_Points() > merc_2.Damage_Taken)
            //             ResolveAttack(merc_1, merc_2);
            //         if(merc_2.Hit_Points() > merc_2.Damage_Taken)
            //             ResolveAttack(merc_2, merc_1);
            //     }
            // }
        }

        void SetDefender(GameObject merc, string position)
        {
            switch (position) {
                case "melee":
                    setDefenderInList(merc, FrontLine, BackLine);
                    break;
                case "ranged":
                    setDefenderInList(merc, BackLine, FrontLine);
                    break;
            }
        }

        // 
        //
        // Helper Functions

        void setDefenderInList(GameObject merc, List<GameObject> desiredList, List<GameObject> alternateList)
        {
            if (desiredList.Count > 6)
                desiredList.Add(merc);
            else if (MiddleLine.Count > 6)
                MiddleLine.Add(merc);
            else if (alternateList.Count > 6)
                alternateList.Add(merc);
        }

        int resolveSingleOrDualWeildAttack(Character attacker, int defense)
        {
            int damage = 0;
            if (attacker.Right_Hand.Name != "None")
                damage = attacker.Attack(attacker.Right_Hand, defense);
            if (attacker.Left_Hand.Name != "None" || attacker.Right_Hand.Name == "None")
                damage = attacker.Attack(attacker.Left_Hand, defense);
            return damage;
        }
    }
}
