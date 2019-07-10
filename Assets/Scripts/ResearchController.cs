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

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        Init();
    }

    private void OnDisable()
    {
        //unsubscribe from events
        timerController.onTimerComplete -= OnNextResearchTime;
    }

    // Update is called once per frame
    void Update()
    {
        HandleResearch();
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
        researchFacilityState = ResearchFacilityState.ShowResearchIcon;
        Debug.Log("Click on the Researcher!  Research is ready.");
    }

    private void HandleResearch()
    {
        switch (researchFacilityState)
        {
            case ResearchFacilityState.WaitingForNextResearch:
                //wait for timer to expire
                break;

            case ResearchFacilityState.ShowResearchIcon:
                researchIcon.SetActive(true);
                researchFacilityState = ResearchFacilityState.WaitForPlayer;
                break;

            case ResearchFacilityState.StartPrompt:
                //draw random reserach
                //give data to researchPrompt
                researchPrompt.SetActive(true);
                researchFacilityState = ResearchFacilityState.WaitForPlayer;
                break;

            case ResearchFacilityState.WaitForPlayer:
                //do nothing while waiting for the Player to make up their mind.
                break;

            case ResearchFacilityState.Researching:
                //wait for timer to expire
                break;
        }
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
        
    }

    public void OnResearchIconPressed()
    {
        researchIcon.SetActive(false);
        researchFacilityState = ResearchFacilityState.StartPrompt;
        //space for coroutines
    }

}
