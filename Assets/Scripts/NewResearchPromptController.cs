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
}
