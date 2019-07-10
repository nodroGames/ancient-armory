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
        }

        // 
        //
        // Helper Functions

        void spawnMerc(Transform location) {
            MercsSpawned++;
            if (Tavern.transform.childCount == 0) // if pool is empty
            {
                // instantiate prefab at spawn position
                Instantiate(MercPrefab, location);
                // TODO: attach character script
            }
            else // if pool has mercs
            {
                // TODO: relocate to spawn position
                getPoolContents(Tavern)[0].SetActive(true);
                getPoolContents(Tavern)[0].transform.parent = location.transform;
            }
            // TODO: assign stats based on the number of MercsSpawned
        }

    }
}