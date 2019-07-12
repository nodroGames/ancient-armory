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
            currentResearch = GetRandomResearch(researchArray);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            timerController.onTimerComplete.RemoveListener(OnResearchComplete);
        }

        protected override void ShowInfoPrompt()
        {
            infoPromptControllerInstance.ShowInfoPrompt(this, currentResearch.description, currentResearch.goldCost.ToString());
        }

        /// <summary>
        /// Called by Button.
        /// </summary>
        public override void OnReadyIconPressed()
        {
            base.OnReadyIconPressed();
            currentResearch = GetRandomResearch(researchArray);
            infoPromptControllerInstance.LoadInfo(currentResearch);
        }

        /// <summary>
        /// Begin working on Research.  
        /// </summary>
        public override void OnRightButton()//accept
        {
            Debug.Log("Research Accepted!", this);
            timerController.onTimerComplete.AddListener(OnResearchComplete);
            timerController.StartTimer(currentResearch.secondsToComplete, inProcessMessage);//start researching thing
            readyIcon.SetActive(false);
            //deduct money
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
            Debug.Log("Research Complete! Increment up!", this);
            currentResearch.OnResearchComplete();
            timerController.onTimerComplete.RemoveListener(OnResearchComplete);
            StartCoroutine(ShowCompleteWindow());//show message to player
            //TimerCycle();//or do so immediately
        }
    }
}
