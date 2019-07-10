using UnityEngine;

public class ResearchController : MonoBehaviour
{
    [SerializeField]
    private ResearchFacilityState researchFacilityState;

    [SerializeField]
    private Research[] tier1Research;
    
    [Header("---Windows---")]
    [SerializeField]
    private GameObject researchPrompt;

    [SerializeField]
    private GameObject researchIcon;

    [SerializeField]
    private TimerController timerController;
    
    private void OnEnable()
    {
        Init();
    }

    private void OnDisable()
    {
        //unsubscribe from events
        timerController.onTimerComplete -= OnNextResearchTime;
    }

    private void Init()
    {
        researchFacilityState = ResearchFacilityState.WaitingForNextResearch;

        researchIcon.SetActive(false);
        researchPrompt.SetActive(false);

        timerController.onTimerComplete += OnNextResearchTime;
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
        //draw random reserach
        //give data to researchPrompt
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
    }

    public void OnResearchIconPressed()
    {//called by Button
        researchIcon.SetActive(false);
        ShowResearchPrompt();
    }

}
