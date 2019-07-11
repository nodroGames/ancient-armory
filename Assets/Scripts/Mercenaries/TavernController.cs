using UnityEngine;
using System.Timers; 
using System.Collections;
using System.Collections.Generic;
using RpgDB;

namespace AncientArmory
{
    public sealed class TavernController : MercenaryController
    {
        public int MercsSpawned;
        public GameObject MercPrefab;
        void Start(){
            setup();
            spawnMerc(Battlefield.transform);
            spawnMerc(Battlefield.transform);
        }

        // 
        //
        // Helper Functions

        void spawnMerc(Transform location) {
            GameObject newMerc;
            if (Tavern.transform.childCount == 0) // if pool is empty
            {
                // instantiate prefab at spawn position
                newMerc = Instantiate(MercPrefab, location);
                GameDatabase.Classes.CreateCharacter(newMerc, "Soldier", ++MercsSpawned, GameDatabase.Extensions);
                // attach character script
            }
            else // if pool has mercs
            {
                // Relocate to spawn position
                newMerc = getPoolContents(Tavern)[0];
                newMerc.SetActive(true);
                newMerc.transform.parent = location.transform;
            }
            // Set merc level and increase MercsSpawned counter
            Character merc = newMerc.GetComponent<Character>();
            merc.Level = MercsSpawned;
            // merc.Left_Hand = GameDatabase.Weapons.GetByName("Bow");
            // TODO: assign stats based on the number of MercsSpawned
        }
    }
}