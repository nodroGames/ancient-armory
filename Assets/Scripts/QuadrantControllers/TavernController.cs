using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RpgDB;

namespace AncientArmory
{
    public sealed class TavernController : ControllerBase
    {
        // Required game objects:
        // readyIcon
        // infoPromptControllerInstance

        public int MercsSpawned;
        public GameObject MercPrefab;
        List<GameObject> DeadPool;
        GameObject Armory;
        GameObject Tavern;

        GameObject newMerc;

        void Start()
        {
            base.Start();
            cooldownDelay = 10;
            cooldownMessage = "cooldownMessage";
            Armory = GameObject.FindGameObjectWithTag("ArmoryController");
            Battlefield = GameObject.FindGameObjectWithTag("BattlefieldController");
            Tavern = GameObject.FindGameObjectWithTag("TavernController");

            StartTimerCycle();
        }



        /// <summary>
        /// Called by Button.
        /// </summary>
        public override void OnReadyIconPressed()
        {
            base.OnReadyIconPressed();
            newMerc = SpawnMerc();
            newMerc.GetComponent<Character>();
            infoPromptControllerInstance.LoadInfo("lookie, merc details");
        }

        /// <summary>
        /// Called by Button.
        /// </summary>
        public override void OnRightButton()//accept
        {
            Debug.Log("Send to Armory!", this);
            SendToArmory();
            readyIcon.SetActive(false);
            // deduct money
        }


        /// <summary>
        /// Called by Button.
        /// </summary>
        public override void OnLeftButton()
        {
            Debug.Log("Send to Battlefield!", this);
            SendToBattlefield();
            StartTimerCycle();//get a different one
            readyIcon.SetActive(false);
            // deduct money
        }

        /// <summary>
        /// Called by Timer Event.
        /// </summary>
        private void OnRecruitingComplete()
        {
            Debug.Log("Recruiting Complete! Increment up!", this);
            currentResearch.OnResearchComplete();
            timerController.onTimerComplete.RemoveListener(OnResearchComplete);
            StartCoroutine(ShowCompleteWindow());//show message to player
            //TimerCycle();//or do so immediately
        }

        void SendToArmory()
        {
            // 
        }

        void SendToBattlefield()
        {
            battlefieldControllerInstance.WaitingLine.Add(newMerc);
            newMerc.transform.parent = Battlefield.transform;
        }

        GameObject SpawnMerc()
        {
            Character merc;
            if (Tavern.transform.childCount == 0) // if pool is empty
            {
                // instantiate prefab at spawn position
                newMerc = Instantiate(MercPrefab, Tavern.transform);
                // Create new character
                mercCharacter = GameDatabase.Classes.CreateCharacter(newMerc, "Soldier", 1, GameDatabase.Extensions);
                mercCharacter.Level = ++MercsSpawned;
                assignStats(merc);
            }
            else // if pool has mercs
            {
                // Relocate to spawn position
                newMerc = DeadPool[0];
                newMerc.SetActive(true);
                newMerc.transform.parent = Tavern.transform;
                mercCharacter = newMerc.GetComponent<Character>();
                // Set level of existing character
                mercCharacter.Level = ++MercsSpawned;
                assignStats(mercCharacter);
            }
            return newMerc;
        }

        void assignStats(Character merc)
        {
            
        }
    }
}