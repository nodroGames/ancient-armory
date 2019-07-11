using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AncientArmory
{
    public class ArmyController : MonoBehaviour
    {
        public const int maxSoldierCount = 18;

        [Header("---Battlefield Positions---")]
        [SerializeField]
        private Transform[] battlePositions;

        [SerializeField]
        private Transform newSoldierSpawnPoint;

        [Header("---Soliders---")]
        [SerializeField]
        private int currentSoldierLimit = 3;

        [SerializeField]
        private MercController[] mercsOnBattlefield;

        private int aliveSoldiersCount = 0;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnSoldierDied()
        {

        }

        public int GetAliveCombatants()
        {
            return aliveSoldiersCount;
        }

        public void RaiseSoldierLimit(int increment)
        {
            currentSoldierLimit += increment;
            currentSoldierLimit = Mathf.Clamp(currentSoldierLimit, 3, maxSoldierCount);
        }

        //public Transform GetOpenPosition()
        //{
        //    foreach(var position in battlePositions)
        //    {
        //        if()
        //    }
        //}
    }
}
