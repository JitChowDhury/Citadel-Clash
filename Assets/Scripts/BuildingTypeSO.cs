using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]

public class BuildingTypeSO : ScriptableObject
{
    public string nameString;
    public Transform buildingPrefab;
    public ResourceGeneratorData resourceGeneratorData;
    public Sprite sprite;
    public float minConstructionRadius;

    public ResourceAmount[] constructionResourceCostArray;
}
