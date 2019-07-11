using UnityEngine;
using System.Collections;

namespace AncientArmory
{
    public class ResearchController : ControllerBase
    {
        [Header("---ResearchController---")]
        [SerializeField]
        private Research[] researchArray;

        private Research currentResearch;

        protected override void Start()
        {
            base.Start();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            timerController.onTimerComplete.RemoveListener(OnResearchComplete);
        }

        protected override void OnLeftButton()//accept
        {
            base.OnLeftButton();//remove to disable debug

            infoPromptController.ToggleVisuals(false);
            timerController.onTimerComplete.AddListener(OnResearchComplete);
            timerController.StartTimer(currentResearch.secondsToComplete, inProcessMessage);//start researching thing
        }

        protected override void OnRightButton()//reject
        {
            base.OnRightButton();// remove to disable debug
            StartTimerCycle();//get a different one
        }

        private static int SumRandomWeights(Research[] researchArray)
        {
            var weightSum = 0;

            foreach (var research in researchArray)
            {
                weightSum += research.randomWeight;
            }

            return weightSum;
        }

        private static Research GetRandomResearch(Research[] researchArray)
        {
            var randomWeight = Random.Range(0, SumRandomWeights(researchArray));
            Research selectedResearch = null;

            foreach (var research in researchArray)
            {
                if (randomWeight < research.randomWeight)
                {
                    selectedResearch = research;
                    break;
                }
                else
                {
                    randomWeight -= research.randomWeight;
                }
            }

            return selectedResearch;
        }
        
        /// <summary>
        /// Called by Timer Event.
        /// </summary>
        private void OnResearchComplete()
        {
            Debug.Log("Research Complete! Increment up!");
            currentResearch.OnResearchComplete();
            timerController.onTimerComplete.RemoveListener(OnResearchComplete);
            StartCoroutine(ShowCompleteWindow());//show message to player
            //TimerCycle();//or do so immediately
        }

        /// <summary>
        /// Called by Button.
        /// </summary>
        public override void OnReadyIconPressed()
        {
            base.OnReadyIconPressed();
            currentResearch = GetRandomResearch(researchArray);
            infoPromptController.LoadInfo(currentResearch);
        }
    }
}
