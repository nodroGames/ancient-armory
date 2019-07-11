using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AncientArmory
{
    public class EnemyArmyController : MonoBehaviour
    {
        private const int reinforcementsPollsSecondsDelay = 1;

        [SerializeField]
        private ArmyController armyController;

        /// <summary>
        /// How many soldiers does the Enemy want to have on their side? Call reinforcements to fill to this amount.
        /// </summary>
        [SerializeField]
        private int desiredCombatantsOnField = 3;
        
        private float nextReinforcementsTime;

        private Coroutine coroutine_reinforcements;

        // Start is called before the first frame update
        void Start()
        {
            coroutine_reinforcements = StartCoroutine(HandleReinforcements());
        }

        // Update is called once per frame
        void Update()
        {

        }

        private IEnumerator HandleReinforcements()
        {
            while (true)
            {
                if(Time.time > nextReinforcementsTime)
                {
                    CallReinforcements();
                }

                yield return new WaitForSeconds(reinforcementsPollsSecondsDelay);
            }
        }

        private void CallReinforcements()
        {
            if (armyController.GetAliveCombatants() < desiredCombatantsOnField)//check for available space
            {
                //spawn more enemies!
            }
        }
    }
}