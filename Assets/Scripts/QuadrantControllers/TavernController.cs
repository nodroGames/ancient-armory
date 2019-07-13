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

        void Start()
        {
            base.Start();
            cooldownDelay = 10;
            cooldownMessage = "cooldownMessage";
            StartTimerCycle();
        }


        /// <summary>
        /// Called by Button.
        /// </summary>
        public override void OnReadyIconPressed()
        {
            base.OnReadyIconPressed();
            GameObject merc = SpawnMerc();
            merc.GetComponent<Character>();
            infoPromptControllerInstance.LoadInfo("lookie, merc details");
        }

        /// <summary>
        /// Called by Button.
        /// </summary>
        public override void OnRightButton()//accept
        {
            Debug.Log("Send to Armory!", this);
            timerController.onTimerComplete.AddListener(OnRecruitingComplete);
            timerController.StartTimer(currentResearch.secondsToComplete, inProcessMessage);//start researching thing
            readyIcon.SetActive(false);
            //deduct money
        }


        /// <summary>
        /// Called by Button.
        /// </summary>
        public virtual void OnLeftButton()
        {
            Debug.Log("Send to Battlefield!", this);
            StartTimerCycle();//get a different one
            readyIcon.SetActive(false);
            //do the left thing
        }

        /// <summary>
        /// Called by Timer Event.
        /// </summary>
        private void OnRecruitingComplete()
        {
            Debug.Log("Research Complete! Increment up!", this);
            currentResearch.OnResearchComplete();
            timerController.onTimerComplete.RemoveListener(OnResearchComplete);
            StartCoroutine(ShowCompleteWindow());//show message to player
            //TimerCycle();//or do so immediately
        }

        GameObject SpawnMerc()
        {
            GameObject newMerc;
            Character merc;
            if (Tavern.transform.childCount == 0) // if pool is empty
            {
                // instantiate prefab at spawn position
                newMerc = Instantiate(MercPrefab, Tavern.transform);
                // Create new character
                merc = GameDatabase.Classes.CreateCharacter(newMerc, "Soldier", 1, GameDatabase.Extensions);
                merc.Level = MercsSpawned;
            }
            else // if pool has mercs
            {
                // Relocate to spawn position
                newMerc = getPoolContents(Tavern)[0];
                newMerc.SetActive(true);
                newMerc.transform.parent = Tavern.transform;
                merc = newMerc.GetComponent<Character>();
                // Set level of existing character
                merc.Level = MercsSpawned;
            }
            return newMerc;
        }

        void assignStats(Character merc)
        {
            
        }
    }
}