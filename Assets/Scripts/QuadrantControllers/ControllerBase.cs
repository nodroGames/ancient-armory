using UnityEngine;
using System.Collections;

namespace AncientArmory
{
    public class ControllerBase : MonoBehaviour
    {
        protected static BattlefieldController battlefieldControllerInstance;
        protected static TavernController tavernControllerInstance;
        protected static ResearchController researchControllerInstance;
        protected static ArmoryController armoryControllerInstance;
        protected static InfoPromptController infoPromptControllerInstance;
        protected static Transform mainCameraTransform;

        [Header("---ControllerBase---")]
        [SerializeField]
        protected TimerController timerController;

        [Header("---UI---")]

        [SerializeField]
        protected GameObject readyIcon;

        [SerializeField]
        protected GameObject completeWindow;

        [Header("---Timer---")]
        [SerializeField]
        protected int cooldownDelay = 5;

        [SerializeField]
        protected string cooldownMessage;

        [SerializeField]
        protected string inProcessMessage;

        protected virtual void Awake()
        {
            GatherStaticReferences();
        }

        /// <summary>
        /// Cache a handle on each of the major Controllers in the Scene, if it does not exist.
        /// </summary>
        protected static void GatherStaticReferences()
        {
            // Long individual lines here to reduce total length of function
            if (!armoryControllerInstance)//if it does not exist already
                armoryControllerInstance = GameObject.FindGameObjectWithTag("ArmoryController").GetComponent<ArmoryController>();

            if (!battlefieldControllerInstance)//if it does not exist already
                battlefieldControllerInstance = GameObject.FindGameObjectWithTag("BattlefieldController").GetComponent<BattlefieldController>();

            if (!researchControllerInstance)//if it does not exist already
                researchControllerInstance = GameObject.FindGameObjectWithTag("ResearchController").GetComponent<ResearchController>();

            if (!tavernControllerInstance)//if it does not exist already
                tavernControllerInstance = GameObject.FindGameObjectWithTag("TavernController").GetComponent<TavernController>();

            if (!infoPromptControllerInstance)//if it does not exist already
                infoPromptControllerInstance = GameObject.FindGameObjectWithTag("UIPromptController").GetComponent<InfoPromptController>();

            if (!mainCameraTransform)//if it does not exist already
                mainCameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;//assign if it exists
        }

        protected virtual void Start()
        {
            
        }

        protected virtual void OnEnable()
        {
            StartTimerCycle();
        }

        protected virtual void OnDisable()
        {
            //unsubscribe from events
            timerController.onTimerComplete.RemoveListener(OnCooldownComplete);
        }

        protected virtual void OnCooldownComplete()
        {
            Debug.Log("Click on the Leader!  " + this.name  + " thing is ready.", this);
            timerController.onTimerComplete.RemoveListener(OnCooldownComplete);
            ShowReadyIcon();
        }

        protected virtual void ShowReadyIcon()
        {
            readyIcon.SetActive(true);
        }

        protected virtual void ShowInfoPrompt()
        {
            infoPromptControllerInstance.ShowInfoPrompt(this);
        }

        protected virtual void StartTimerCycle()
        {
            //hide UI elements
            readyIcon.SetActive(false);
            completeWindow.SetActive(false);

            timerController.onTimerComplete.AddListener(OnCooldownComplete);
            timerController.StartTimer(cooldownDelay, cooldownMessage);//Gathering new research...
        }

        /// <summary>
        /// Called by Button. Starts new Timer Cycle.
        /// </summary>
        public virtual void OnLeftButton()
        {
            Debug.Log("Left Button Pressed.", this);
            StartTimerCycle();//get a different one
            readyIcon.SetActive(false);
            //do the left thing
        }

        /// <summary>
        /// Called by Button. Starts new Timer Cycle.
        /// </summary>
        public virtual void OnRightButton()
        {
            Debug.Log("Right Button Pressed.", this);
            StartTimerCycle();//get a different one
            readyIcon.SetActive(false);
            //do the right thing
        }
        
        /// <summary>
        /// Coroutine to show happy success image to player before restarting.
        /// </summary>
        /// <param name="moment"></param>
        /// <returns></returns>
        protected virtual IEnumerator ShowCompleteWindow(float moment = 2)
        {
            completeWindow.SetActive(true);

            yield return new WaitForSeconds(moment);

            StartTimerCycle();
        }

        /// <summary>
        /// Called by Button.
        /// </summary>
        public virtual void OnReadyIconPressed()
        {
            // TODO: Move call to InfoPromptController.ShowInfoPrompt here?

            //Debug.Log("Ready icon pressed!", this);
            ShowInfoPrompt();
        }
    }
}
