using UnityEngine;

public class ResearchController : MonoBehaviour
{
    [SerializeField]
    private ResearchFacilityState researchFacilityState;

    [SerializeField]
    private Research[] tier1Research;

    [Header("---Controllers---")]
    [SerializeField]
    private TimerController timerController;

    [SerializeField]
    private NewResearchPromptController newResearchPromptController;

    [Header("---Windows---")]
    [SerializeField]
    private GameObject researchPrompt;

    [SerializeField]
    private GameObject researchIcon;
    
    private Research currentResearch;
    
    private void OnEnable()
    {
        Init();
    }

    private void OnDisable()
    {
        //unsubscribe from events
        timerController.onTimerComplete.RemoveListener(OnNextResearchTime);
    }

    private void Init()
    {
        researchFacilityState = ResearchFacilityState.WaitingForNextResearch;

        researchIcon.SetActive(false);
        researchPrompt.SetActive(false);

        timerController.onTimerComplete.AddListener(OnNextResearchTime);
        timerController.StartTimer(15, "Gathering new research...");
    }

    private void OnNextResearchTime()
    {
        Debug.Log("Click on the Researcher!  Research is ready.");
        ShowResearchIcon();
    }

    private void ShowResearchIcon()
    {
        researchIcon.SetActive(true);
        researchFacilityState = ResearchFacilityState.WaitForPlayer;
    }

    private void ShowResearchPrompt()
    {
        researchPrompt.SetActive(true);
        researchFacilityState = ResearchFacilityState.WaitForPlayer;
    }

    private static int SumRandomWeights(Research[] researchArray)
    {
        var weightSum = 0;

        foreach(var research in researchArray)
        {
            weightSum += research.randomWeight;
        }

        return weightSum;
    }

    private static Research GetRandomResearch(Research[] researchArray)
    {
        var randomWeight = Random.Range(0, SumRandomWeights(researchArray));
        Research selectedResearch = null;

        foreach(var research in researchArray)
        {
            if(randomWeight < research.randomWeight)
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

    public void StartNewResearch()
    {
        researchFacilityState = ResearchFacilityState.Researching;
        //timerController.StartTimer();
    }

    public void OnResearchIconPressed()
    {//called by Button
        researchIcon.SetActive(false);
        currentResearch = GetRandomResearch(tier1Research);
        ShowResearchPrompt();
    }

}
