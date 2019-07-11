using UnityEngine;
using System.Collections;

namespace AncientArmory
{
    public class ResearchController : MonoBehaviour
    {
        [SerializeField]
        private Research[] tier1Research;

        [Header("---Controllers---")]
        [SerializeField]
        private TimerController timerController;

        [SerializeField]
        private NewResearchPromptController newResearchPromptController;

        [Header("---UI---")]
        [SerializeField]
        private Transform UIRoot;

        [SerializeField]
        private GameObject researchIcon;

        [SerializeField]
        private GameObject researchCompleteWindow;

        [Header("---Timer---")]
        [SerializeField]
        private int timeToGetNewResearch = 5;

        /// <summary>
        /// SO currently loaded.
        /// </summary>
        private Research currentResearch;

        private Transform mainCameraTransform;

        private void Start()
        {
            if (!mainCameraTransform)
            {
                mainCameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform as Transform;
            }

            PointUITowardsCamera();
        }

        private void Update()
        {
            PointUITowardsCamera();
        }

        private void OnEnable()
        {
            InitListeners();
            
            StartGettingNewResearch();
        }

        private void OnDisable()
        {
            //unsubscribe from events
            timerController.onTimerComplete.RemoveListener(OnNextResearchTime);
            timerController.onTimerComplete.RemoveListener(OnResearchComplete);
            newResearchPromptController.acceptResearch.RemoveListener(OnResearchAccepted);
            newResearchPromptController.rejectResearch.RemoveListener(OnResearchRejected);
        }

        private void PointUITowardsCamera()
        {
            UIRoot.LookAt(mainCameraTransform.position);
        }

        private void InitListeners()
        {
            //add listeners
            newResearchPromptController.acceptResearch.AddListener(OnResearchAccepted);
            newResearchPromptController.rejectResearch.AddListener(OnResearchRejected);
        }

        private void OnNextResearchTime()
        {
            Debug.Log("Click on the Researcher!  Research is ready.");
            ShowResearchIcon();
        }

        private void ShowResearchIcon()
        {
            researchIcon.SetActive(true);
        }

        private void ShowResearchPrompt()
        {
            newResearchPromptController.ToggleVisuals(true);
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

        private void StartGettingNewResearch()
        {
            //hide UI elements
            researchIcon.SetActive(false);
            newResearchPromptController.ToggleVisuals(false);
            researchCompleteWindow.SetActive(false);

            timerController.onTimerComplete.AddListener(OnNextResearchTime);
            timerController.StartTimer(timeToGetNewResearch, "Gathering new Research...");//Gathering new research...
        }

        /// <summary>
        /// Called by Button.
        /// </summary>
        private void OnResearchAccepted()
        {
            Debug.Log("Research Accepted.");
            newResearchPromptController.ToggleVisuals(false);
            timerController.onTimerComplete.AddListener(OnResearchComplete);
            timerController.StartTimer(currentResearch.secondsToComplete, "Researching...");
            //deduct money
        }

        /// <summary>
        /// Called by Button.
        /// </summary>
        private void OnResearchRejected()
        {
            Debug.Log("Rejected offer. No Deal, Howie");
            StartGettingNewResearch();//get a different one
        }

        /// <summary>
        /// Called by Timer Event.
        /// </summary>
        private void OnResearchComplete()
        {
            Debug.Log("Research Complete! Increment up!");
            currentResearch.OnResearchComplete();
            StartCoroutine(WaitAMoment());
            //StartGettingNewResearch(); //recursion problem!!!!! 
        }

        /// <summary>
        /// Show happy success image to player before restarting.
        /// </summary>
        /// <param name="moment"></param>
        /// <returns></returns>
        private IEnumerator WaitAMoment(float moment = 2)
        {
            researchCompleteWindow.SetActive(true);

            yield return new WaitForSeconds(moment);
            
            StartGettingNewResearch();
        }

        /// <summary>
        /// Called by Button.
        /// </summary>
        public void OnResearchIconPressed()
        {
            researchIcon.SetActive(false);
            currentResearch = GetRandomResearch(tier1Research);
            newResearchPromptController.ReadResearchSO(currentResearch);
            ShowResearchPrompt();
        }

    }
}
