using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RpgDB;

namespace AncientArmory
{
    public sealed class TavernController : PoolController
    {
        public int MercsSpawned;
        public GameObject MercPrefab;

        void Start()
        {
            base.Start();
            setup();
            InitiateRecruiting();
        }

        void InitiateRecruiting()
        {
            GameObject merc = spawnMerc();
        }

        // 
        //
        // Helper Functions

        GameObject spawnMerc()
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