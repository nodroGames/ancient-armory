using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;


namespace AncientArmory
{
    public class InfoPromptController : MonoBehaviour
    {
        [Header("---UI Elements---")]
        [SerializeField]
        private TextMeshProUGUI infoText;

        [SerializeField]
        private TextMeshProUGUI costText;

        [SerializeField]
        private TextMeshProUGUI windowTitleText;

        [SerializeField]
        private Image backgroundImage;

        //events
        [HideInInspector]
        public UnityEvent acceptPrompt;

        [HideInInspector]
        public UnityEvent rejectPrompt;

        private void Start()
        {
            InitEvents();
        }

        private void InitEvents()
        {
            if (acceptPrompt == null)
            {
                acceptPrompt = new UnityEvent();
            }

            if (rejectPrompt == null)
            {
                rejectPrompt = new UnityEvent();
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
            windowTitleText.text = researchSO.researchName.ToString();
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
            windowTitleText.text = "MISSINGNO.";
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
            windowTitleText.text = "MISSINGNO.";
        }

        /// <summary>
        /// Called by Button.
        /// </summary>
        public void LeftButton()
        {
            acceptPrompt.Invoke();
        }

        /// <summary>
        /// Called by Button.
        /// </summary>
        public void RightButton()
        {
            rejectPrompt.Invoke();
        }

        public void ToggleVisuals(bool active)
        {
            this.gameObject.SetActive(active);
        }
    }
}