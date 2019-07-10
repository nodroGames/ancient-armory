using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class NewResearchPromptController : MonoBehaviour
{
    [Header("---UI Elements---")]
    [SerializeField]
    private TextMeshProUGUI researchInfoText;

    [SerializeField]
    private TextMeshProUGUI researchCostText;

    [SerializeField]
    private TextMeshProUGUI windowTitleText;

    [SerializeField]
    private Image backgroundImage;

    //events
    [HideInInspector]
    public UnityEvent acceptResearch;

    [HideInInspector]
    public UnityEvent rejectResearch;

    private void Start()
    {
        InitEvents();
    }

    private void InitEvents()
    {
        if(acceptResearch == null)
        {
            acceptResearch = new UnityEvent();
        }

        if (rejectResearch == null)
        {
            rejectResearch = new UnityEvent();
        }
    }

    /// <summary>
    /// Load UI with data from SO.
    /// </summary>
    /// <param name="newResearch"></param>
    public void ReadResearchSO(Research researchSO)
    {
        researchInfoText.text = researchSO.description;
        researchCostText.text = researchSO.goldCost.ToString();
        windowTitleText.text = researchSO.researchName.ToString();
    }

    /// <summary>
    /// Called by Button.
    /// </summary>
    public void AcceptResearch()
    {
        acceptResearch.Invoke();
    }

    /// <summary>
    /// Called by Button.
    /// </summary>
    public void RejectResearch()
    {
        rejectResearch.Invoke();
    }

    public void ToggleVisuals(bool active)
    {
        this.gameObject.SetActive(active);
    }
}
