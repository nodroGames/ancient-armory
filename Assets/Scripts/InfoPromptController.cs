using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;


namespace AncientArmory
{
    public class InfoPromptController : MonoBehaviour
    {

        [Header("---Leader UI Elements---")]
        [SerializeField]
        private GameObject UIRoot;

        [SerializeField]
        private TextMeshProUGUI windowTitleTMP;

        [Header("---Buttons---")]
        [SerializeField]
        private Button leftButton;

        [SerializeField]
        private Button rightButton;

        [SerializeField]
        private TextMeshProUGUI leftButtonTMP;
            
        [SerializeField]
        private TextMeshProUGUI rightButtonTMP;

        [Header("---Money UI Elements---")]
        [SerializeField]
        private TextMeshProUGUI infoText;

        [SerializeField]
        private TextMeshProUGUI costText;

        [SerializeField]
        private TextMeshProUGUI availableFundsText;

        private void Start()
        {
            ToggleVisuals(false);//start with everything hidden
        }

        /// <summary>
        /// Subscribe Left and Right funcs to LR Buttons.
        /// </summary>
        /// <param name="leftButton"></param>
        /// <param name="rightButton"></param>
        /// <param name="quadrantController"></param>
        private void SubscribeToLRButtonEvents(ControllerBase quadrantController)
        {
            leftButton.onClick.AddListener(quadrantController.OnLeftButton);
            rightButton.onClick.AddListener(quadrantController.OnRightButton);
        }

        /// <summary>
        /// RemoveAllListeners from both Left and Right Buttons.
        /// </summary>
        private void UnsubcribeLRButtons()
        {
            leftButton.onClick.RemoveAllListeners();
            rightButton.onClick.RemoveAllListeners();

            //always have CloseDiaglogue subscribed;
            leftButton.onClick.AddListener(CloseDialogue);
            rightButton.onClick.AddListener(CloseDialogue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestingController"></param>
        /// <param name="infoString"></param>
        /// <param name="costString"></param>
        public void ShowInfoPrompt(ControllerBase requestingController, string leftButtonString, string rightButtonString, string infoString, string costString = "")
        {
            bool showMoney = true;//should any money be shown?

            //figure out which window needs to be shown, and then show appropriate elements needed for each
            if(requestingController is ArmoryController)
            {
                showMoney = true;
            }

            else if (requestingController is BattlefieldController)
            {
                showMoney = false;
            }

            else if (requestingController is ResearchController)
            {
                showMoney = true;
            }

            else if (requestingController is TavernController)
            {
                showMoney = true;
            }

            else//default
            {
                showMoney = true;
                Debug.Log("Generic info prompt. Do something generic.  YOLO!", requestingController);
            }

            ToggleVisuals(true);

            SubscribeToLRButtonEvents(requestingController);//subscribe to Button events

            costText.gameObject.SetActive(showMoney);//show/hide money elements
            availableFundsText.gameObject.SetActive(showMoney);

            windowTitleTMP.text = requestingController.GetLeaderName();//set common texts
            infoText.text = infoString;//set info paragraph

            leftButtonTMP.text = leftButtonString;//set button info
            rightButtonTMP.text = rightButtonString;

            if (showMoney)//fill UI text
            {
                costText.text = costString;
                //availableFundsText = //Get available funds!
            }

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
            UnsubcribeLRButtons();
        }
    }
}