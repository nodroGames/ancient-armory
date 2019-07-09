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
                UpdateVisuals();
                break;
            case ResearchFacilityState.StartPrompt:
                researchPrompt.SetActive(true);
                break;
            case ResearchFacilityState.WaitForPlayer:
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



}
