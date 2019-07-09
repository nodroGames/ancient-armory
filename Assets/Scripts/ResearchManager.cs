using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchManager : MonoBehaviour
{
    [SerializeField]
    private ResearchFacilityState researchFacilityState;

    [SerializeField]
    private Research[] tier1Research;

    private float nextResearchPromptTime;

    [SerializeField]
    private GameObject researchPrompt;

    [SerializeField]
    private GameObject researchIcon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleResearch();
    }

    private void HandleResearch()
    {
        switch (researchFacilityState)
        {
            case ResearchFacilityState.WaitingForNextResearch:
                if(Time.time > nextResearchPromptTime)
                {
                    researchFacilityState = ResearchFacilityState.ShowResearchIcon;//move on to next state
                }
                else
                {
                    UpdateVisuals();
                }
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
                UpdateVisuals();
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

    private void UpdateVisuals()
    {

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
