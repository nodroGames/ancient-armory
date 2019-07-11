using UnityEngine;
using System.Collections;

namespace AncientArmory
{
    public class ControllerBase : MonoBehaviour
    {
        //shared Class references
        protected static Transform mainCameraTransform;
        protected static BattlefieldController battlefieldControllerInstance;
        protected static TavernController tavernControllerInstance;
        protected static ResearchController researchControllerInstance;
        protected static ArmoryController armoryControllerInstance;

        [Header("---ControllerBase---")]
        [SerializeField]
        protected TimerController timerController;

        [SerializeField]
        protected InfoPromptController infoPromptController;

        [Header("---UI---")]
        [SerializeField]
        protected Transform UIRoot;

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
        
        protected virtual void Start()
        {
            PointUITowardsCamera();
        }

        /// <summary>
        /// Cache a handle on each of the major Controllers in the Scene, if it does not exist.
        /// </summary>
        protected static void GatherStaticReferences()
        {
            GameObject searchObject;//resue a single allocation of memory

            //controllers
            if (!armoryControllerInstance)//if it does not exist already
            {
                searchObject = GameObject.FindGameObjectWithTag("ArmoryController");//look for GO
                if(searchObject) armoryControllerInstance = searchObject.GetComponent<ArmoryController>();//assign if it exists
            }

            if (!battlefieldControllerInstance)//if it does not exist already
            {
                searchObject = GameObject.FindGameObjectWithTag("BattlefieldController");//look for GO
                if (searchObject) battlefieldControllerInstance = searchObject.GetComponent<BattlefieldController>();//assign if it exists
            }

            if (!researchControllerInstance)//if it does not exist already
            {
                searchObject = GameObject.FindGameObjectWithTag("ResearchController");//look for GO
                if (searchObject) researchControllerInstance = searchObject.GetComponent<ResearchController>();//assign if it exists
            }

            if (!tavernControllerInstance)//if it does not exist already
            {
                searchObject = GameObject.FindGameObjectWithTag("TavernController");//look for GO
                if (searchObject) tavernControllerInstance = searchObject.GetComponent<TavernController>();//assign if it exists
            }

            //main camera
            if (!mainCameraTransform)//if it does not exist already
            {
                searchObject = GameObject.FindGameObjectWithTag("MainCamera");//look for GO
                if (searchObject) mainCameraTransform = searchObject.transform;//assign if it exists
            }
        }

        protected virtual void Update()
        {
            PointUITowardsCamera();//if object is static, remove this.
        }

        protected virtual void OnEnable()
        {
            InitListeners();

            StartTimerCycle();
        }

        protected virtual void OnDisable()
        {
            //unsubscribe from events
            timerController.onTimerComplete.RemoveListener(OnCooldownComplete);
            infoPromptController.acceptPrompt.RemoveListener(OnLeftButton);
            infoPromptController.rejectPrompt.RemoveListener(OnRightButton);
        }

        protected virtual void PointUITowardsCamera()
        {
            UIRoot.LookAt(mainCameraTransform.position);
        }

        protected virtual void InitListeners()
        {
            //add listeners
            infoPromptController.acceptPrompt.AddListener(OnLeftButton);
            infoPromptController.rejectPrompt.AddListener(OnRightButton);
        }

        protected virtual void OnCooldownComplete()
        {
            Debug.Log("Click on the Leader!  Your thing is ready.", this);
            timerController.onTimerComplete.RemoveListener(OnCooldownComplete);
            ShowReadyIcon();
        }

        protected virtual void ShowReadyIcon()
        {
            readyIcon.SetActive(true);
        }

        protected virtual void ShowInfoPrompt()
        {
            infoPromptController.ToggleVisuals(true);
        }

        protected virtual void StartTimerCycle()
        {
            //hide UI elements
            readyIcon.SetActive(false);
            infoPromptController.ToggleVisuals(false);
            completeWindow.SetActive(false);

            timerController.onTimerComplete.AddListener(OnCooldownComplete);
            timerController.StartTimer(cooldownDelay, cooldownMessage);//Gathering new research...
        }

        /// <summary>
        /// Called by Button.
        /// </summary>
        protected virtual void OnLeftButton()
        {
            Debug.Log("Left Button Pressed.", this);
            //do left thing
        }

        /// <summary>
        /// Called by Button.
        /// </summary>
        protected virtual void OnRightButton()
        {
            Debug.Log("Right Button Pressed.", this);
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
            readyIcon.SetActive(false);
            ShowInfoPrompt();
        }
    }
}
