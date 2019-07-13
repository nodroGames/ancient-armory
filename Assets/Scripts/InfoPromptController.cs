using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;


namespace AncientArmory
{
    public class InfoPromptController : MonoBehaviour
    {
        [SerializeField]
        private GameObject UIRoot;

        [Header("---Leader UI Elements---")]
        [SerializeField]
        private GameObject recruiterUIElement;

        [SerializeField]
        private GameObject commanderUIElement;

        [SerializeField]
        private GameObject quartermasterUIElement;

        [SerializeField]
        private GameObject researchUIElement;

        [Header("---Money UI Elements---")]
        [SerializeField]
        private TextMeshProUGUI infoText;

        [SerializeField]
        private TextMeshProUGUI costText;

        [SerializeField]
        private TextMeshProUGUI availableFundsText;

        private readonly ControllerBase[] controllerBaseList = new ControllerBase[4];//4 controllers

        private void Start()
        {
            ToggleVisuals(false);//start with everything hidden
        }

        /// <summary>
        /// Register a controller with this Class for callbacks.
        /// </summary>
        /// <param name="newControllerBase"></param>
        public void RegisterController(ControllerBase newControllerBase)
        {
            for(var i = 0; i < controllerBaseList.Length; ++i)
            {
                if(controllerBaseList[i] == null)
                {
                    controllerBaseList[i] = newControllerBase;
                    return;
                }
            }
            Debug.LogError("ERROR! More Controllers being registered than expected. more than 4??", this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestingController"></param>
        /// <param name="infoString"></param>
        /// <param name="costString"></param>
        public void ShowInfoPrompt(ControllerBase requestingController, string infoString = "", string costString = "")
        {
            if(requestingController is ArmoryController)
            {
                SetActiveWindow(quartermasterUIElement);
                costText.gameObject.SetActive(true);
                availableFundsText.gameObject.SetActive(true);
            }

            else if (requestingController is BattlefieldController)
            {
                SetActiveWindow(commanderUIElement);
                costText.gameObject.SetActive(false);
                availableFundsText.gameObject.SetActive(false);
            }

            else if (requestingController is ResearchController)
            {
                SetActiveWindow(researchUIElement);
                costText.gameObject.SetActive(true);
                availableFundsText.gameObject.SetActive(true);
            }

            else if (requestingController is TavernController)
            {
                SetActiveWindow(recruiterUIElement);
                costText.gameObject.SetActive(true);
                availableFundsText.gameObject.SetActive(true);
            }

            else
            {
                //default
                SetActiveWindow(recruiterUIElement);//place holder for default
                costText.gameObject.SetActive(true);//place holder for default
                availableFundsText.gameObject.SetActive(true);//place holder for default
                Debug.Log("Generic info prompt. Do something generic.  YOLO!", requestingController);
            }

            //show description
            infoText.text = infoString;
            costText.text = costString;
        }
        
        /// <summary>
        /// Load UI with data from SO.
        /// </summary>
        /// <param name="newResearch"></param>
        public void LoadInfo(Research researchSO)
        {
            infoText.text = researchSO.description;
            costText.text = researchSO.goldCost.ToString();
        }

        /// <summary>
        /// Load UI with data from given object.
        /// </summary>
        /// <param name="newResearch"></param>
        public void LoadInfo(MercController merController)
        {
            Debug.Log("LOAD INFO NOT YET IMPLEMENTED!");
            infoText.text = "this is a merc.";
            costText.text = "$99998";
        }

        /// <summary>
        /// Load UI with data from given object.
        /// </summary>
        /// <param name="newResearch"></param>
        public void LoadInfo(RpgDB.IRpgDBEntry merController)
        {
            Debug.Log("LOAD INFO NOT YET IMPLEMENTED!");
            infoText.text = "this is an item.";
            costText.text = "$99999";
        }
                
        /// <summary>
        /// Disable all windows except for this one.
        /// </summary>
        /// <param name="desiredWindow"></param>
        public void SetActiveWindow(GameObject desiredWindow)
        {
            UIRoot.SetActive(true);//show background

            DisableAllPrompts();

            desiredWindow.SetActive(true);
        }

        public void ToggleVisuals(bool active)
        {
            UIRoot.SetActive(active);//toggle all at once
        }

        /// <summary>
        /// Hide all visuals for this object
        /// </summary>
        public void CloseDialogue()
        {
            ToggleVisuals(false);
        }

        void DisableAllPrompts()
        {
            recruiterUIElement.SetActive(false);
            commanderUIElement.SetActive(false);
            quartermasterUIElement.SetActive(false);
            researchUIElement.SetActive(false);
        }
    }
}