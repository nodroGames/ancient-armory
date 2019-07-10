using UnityEngine;

//[CreateAssetMenu(fileName = "Research_", menuName = "ScriptableObjects/Research")]
public class Research : ScriptableObject
{
    [Header("---Research Base---")]
    public string researchName = "Research";
    public int secondsToComplete = 30;
    public int goldCost = 100;
    public int randomWeight = 100;
    public int tier = 0;
    public int playerExperienceGained = 10;
    public string description = "Increase by 10%";

    public virtual void OnResearchComplete()
    {
        Debug.Log(this.name + " Research Complete!");
    }
}
