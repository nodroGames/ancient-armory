using UnityEngine;
using System.Timers; 
using System.Collections;
using System.Collections.Generic;
using RpgDB;

namespace AncientArmory
{
    public abstract class PoolController : MonoBehaviour
    {
        public GameDatabase GameDatabase;
        public GameObject Battlefield;
        public GameObject Armory;
        public GameObject Tavern;

        public void Start(){
            setup();
        }

        // 
        //
        // Helper Functions

        public void setup() {
            GameDatabase = GameObject.Find("GameDatabase").GetComponent<RpgDB.GameDatabase>();
            Battlefield = GameObject.Find("BattlefieldController");
            // Armory = GameObject.Find("ArmoryController");
            Tavern = GameObject.Find("TavernController");
            // Research = GameObject.Find("ResearchController");
        }

        public void putMercInPool(GameObject merc, GameObject pool) {
            merc.SetActive(false);
            merc.transform.parent = pool.transform;
        }

        public List<GameObject> getPoolContents(GameObject pool) {
            List<GameObject> mercList = new List<GameObject>();
            foreach(Transform child in pool.transform)
            {
                mercList.Add(child.gameObject);
            }
            return mercList;
        }
    }
}