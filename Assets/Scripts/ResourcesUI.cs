using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesUI : MonoBehaviour
{


    private ResourceTypeListSO resourceTypeList;
    private Dictionary<ResourceTypeSO, Transform> resourceTypeTransformDictionary;
    private void Awake()
    {
        resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);//load from resource folder
        resourceTypeTransformDictionary = new Dictionary<ResourceTypeSO, Transform>();
        Transform resourceTemplate = transform.Find("ResourceTemplate");

        resourceTemplate.gameObject.SetActive(false);
        int index = 0;
        foreach (ResourceTypeSO resourceType in resourceTypeList.List)
        {
            Transform resourceTransform = Instantiate(resourceTemplate, transform);
            resourceTransform.gameObject.SetActive(true);

            float offsetAmount = -160f;
            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

            resourceTransform.Find("image").GetComponent<Image>().sprite = resourceType.sprite;


            resourceTypeTransformDictionary[resourceType] = resourceTransform;

            index++;


        }
    }

    void Start()
    {
        UpdateResourceAmount();
        ResourceManager.Instance.OnResourceAmountChange += ResourceManager_OnResourceAmountChange;
    }

    private void ResourceManager_OnResourceAmountChange(object sender, EventArgs e)
    {
        UpdateResourceAmount();
    }

    private void UpdateResourceAmount()
    {
        foreach (ResourceTypeSO resourceType in resourceTypeList.List)
        {
            int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceType);

            Transform resourceTransform = resourceTypeTransformDictionary[resourceType];
            resourceTransform.Find("text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());

        }
    }

}
