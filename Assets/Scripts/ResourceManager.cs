using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private Dictionary<ResourceTypeSO, int> resourceAmountDictionary;
    ResourceTypeListSO resourceTypeList;

    private void Awake()
    {
        resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();
        resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

        foreach (ResourceTypeSO resourceType in resourceTypeList.List)
        {
            resourceAmountDictionary[resourceType] = 0;
        }

        TestLogResourceAmountDictionary();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            AddResource(resourceTypeList.List[0], 2);
            TestLogResourceAmountDictionary();

        }
    }

    private void TestLogResourceAmountDictionary()
    {
        foreach (ResourceTypeSO resourceType in resourceAmountDictionary.Keys)
        {
            Debug.Log(resourceType.nameString + ": " + resourceAmountDictionary[resourceType]);
        }
    }

    public void AddResource(ResourceTypeSO resourceType, int amount)
    {
        resourceAmountDictionary[resourceType] += amount;
    }

}
