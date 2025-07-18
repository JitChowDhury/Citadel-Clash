using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]

public class BuildingTypeSO : ScriptableObject
{
    public string nameString;
    public Transform buildingPrefab;
    public bool hasResourceGeneratorData;
    public ResourceGeneratorData resourceGeneratorData;
    public Sprite sprite;
    public float minConstructionRadius;
    public int healthAmountMax;
    public float constructionTimerMax;

    public ResourceAmount[] constructionResourceCostArray;

    public string GetConstructionResourceCostString()
    {
        string str = "";
        foreach (ResourceAmount resourceAmount in constructionResourceCostArray)
        {
            str += "<color=#" + resourceAmount.resourceType.colorHex + ">" + resourceAmount.resourceType.nameShort +
            resourceAmount.amount + "</color> ";
        }
        return str;
    }
}
