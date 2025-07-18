using System;
using UnityEngine;
//attached to the resource generate buildings
public class ResourceGenerator : MonoBehaviour
{
    //returns the nearbyresourceamount count
    public static int GetNearByResourceAmount(ResourceGeneratorData resourceGeneratorData, Vector3 position)
    {
        Collider2D[] collider2dArray = Physics2D.OverlapCircleAll(position, resourceGeneratorData.resourceDetectionRadius);
        int nearByResourceAmount = 0;
        foreach (Collider2D collider in collider2dArray)
        {
            //get all the resource node
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
        //clamps to nearest value
        nearByResourceAmount = Mathf.Clamp(nearByResourceAmount, 0, resourceGeneratorData.maxResourceAmount);//clamps it according to the stats 
        return nearByResourceAmount;

    }
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
        int nearByResourceAmount = GetNearByResourceAmount(resourceGeneratorData, transform.position);
        if (nearByResourceAmount == 0)
        {
            //no resource nearby
            //disable resource generator
            enabled = false;//disable the script
        }
        else
        {
            timerMax = (resourceGeneratorData.timerMax / 2f) + resourceGeneratorData.timerMax *
            (1 - (float)nearByResourceAmount / resourceGeneratorData.maxResourceAmount);

            // Calculate how long it should take to generate 1 resource (timerMax) based on nearby resource nodes
            // Formula:
            //   timerMax = (baseTime / 2) + baseTime * (1 - (nearbyAmount / maxAmount));
            //
            // Explanation:
            // 1. baseTime = resourceGeneratorData.timerMax
            // 2. (nearbyAmount / maxAmount) gives a value between 0 (no resources) and 1 (max resources)
            // 3. (1 - nearby/max) means: more nearby resources = smaller value
            // 4. So if you have maximum resources nearby:
            //        (1 - 1) = 0 → timerMax = (baseTime / 2) + 0 → Faster production
            //    If you have no resources (won't happen because it's disabled, but theoretically):
            //        (1 - 0) = 1 → timerMax = (baseTime / 2) + baseTime = 1.5 * baseTime → Slower production
            // 5. This balances the production speed:
            //      - Faster when more resources are nearby
            //      - Slower when fewer resources are nearby

        }

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


    public ResourceGeneratorData GetResourceGeneratorData()
    {
        return resourceGeneratorData;
    }

    public float GetTimerNormalized()
    {
        return timer / timerMax;
    }

    public float GetAmountGeneratedPerSecond()
    {
        return 1 / timerMax;
    }
}
