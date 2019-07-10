using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeArmoryResearch_", menuName = "ScriptableObjects/UpgradeArmoryResearch")]
public class UpgradeArmoryResearch : Research
{
    [Header("---UpgradeArmoryResearch---")]
    public float modifierValue = 0.1f;

    public override void OnResearchComplete()
    {
        base.OnResearchComplete();
        //increase armory values of some kind.
    }
}
