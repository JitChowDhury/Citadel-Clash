using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    [SerializeField] private Sprite arrowSprite;
    [SerializeField] private List<BuildingTypeSO> ignoreBuildingTypeList;
    private Transform arrowButton;
    private Dictionary<BuildingTypeSO, Transform> buttonTransformDictionary;
    void Awake()
    {
        Transform buttonTemplate = transform.Find("btnTemplate");
        buttonTemplate.gameObject.SetActive(false);

        buttonTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();

        BuildingTypeListSO buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        int index = 0;

        arrowButton = Instantiate(buttonTemplate, transform);
        arrowButton.gameObject.SetActive(true);
        float offsetAmount = 160f;
        arrowButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);
        arrowButton.Find("image").GetComponent<Image>().sprite = arrowSprite;
        arrowButton.Find("image").GetComponent<RectTransform>().sizeDelta = new Vector2(0, -30);

        arrowButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            BuildingManager.Instance.SetActiveBuildingType(null);
        });

        index++;

        foreach (BuildingTypeSO buildingType in buildingTypeList.List)
        {
            if (ignoreBuildingTypeList.Contains(buildingType)) continue;
            Transform buttonTransform = Instantiate(buttonTemplate, transform);
            buttonTransform.gameObject.SetActive(true);
            offsetAmount = 160f;
            buttonTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);
            buttonTransform.Find("image").GetComponent<Image>().sprite = buildingType.sprite;

            buttonTransformDictionary[buildingType] = buttonTransform;

            buttonTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuildingManager.Instance.SetActiveBuildingType(buildingType);
            });
            index++;
        }


    }
    void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChange += BuildingManager_OnActiveBuildingTypeChange;
        UpdateActiveBuildingType();
    }

    private void BuildingManager_OnActiveBuildingTypeChange(object sender, BuildingManager.OnActiveBuildingTypeChangeEventArgs e)
    {
        UpdateActiveBuildingType();
    }

    private void UpdateActiveBuildingType()
    {
        arrowButton.Find("selected").gameObject.SetActive(false);
        foreach (BuildingTypeSO buildingType in buttonTransformDictionary.Keys)
        {
            Transform buttonTransform = buttonTransformDictionary[buildingType];
            buttonTransform.Find("selected").gameObject.SetActive(false);
        }

        BuildingTypeSO activeBuilding = BuildingManager.Instance.GetActiveBuildingType();
        if (activeBuilding == null)
        {
            arrowButton.Find("selected").gameObject.SetActive(true);
        }
        else
        {
            buttonTransformDictionary[activeBuilding].Find("selected").gameObject.SetActive(true);
        }
    }

}
