using UnityEngine;
//attached to the resource generate buildings
public class ResourceGenerator : MonoBehaviour
{
    private BuildingTypeSO buildingType;
    private ResourceGeneratorData resourceGeneratorData;
    private float timer;
    private float timerMax;

    private void Awake()
    {
        resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingType.resourceGeneratorData;
        timerMax = resourceGeneratorData.timerMax;
    }
    void Start()
    {
        Collider2D[] collider2dArray = Physics2D.OverlapCircleAll(transform.position, resourceGeneratorData.resourceDetectionRadius);
        int nearByResourceAmount = 0;
        foreach (Collider2D collider in collider2dArray)
        {
            ResourceNode resourceNode = collider.GetComponent<ResourceNode>();
            if (resourceNode != null)
            {
                //its a resource node then
                if (resourceNode.resourceType == resourceGeneratorData.resourceType)
                {
                    nearByResourceAmount++;
                }
            }


        }

        nearByResourceAmount = Mathf.Clamp(nearByResourceAmount, 0, resourceGeneratorData.maxResourceAmount);
        if (nearByResourceAmount == 0)
        {
            //no resource nearby
            //disable resource generator
            enabled = false;
        }
        else
        {
            timerMax = (resourceGeneratorData.timerMax / 2f) + resourceGeneratorData.timerMax *
            (1 - (float)nearByResourceAmount / resourceGeneratorData.maxResourceAmount);
        }
        Debug.Log("nearByResourceAmount: " + nearByResourceAmount + ";" + "timermax: " + timerMax);
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer += timerMax;

            ResourceManager.Instance.AddResource(resourceGeneratorData.resourceType, 1);
        }

    }
}
