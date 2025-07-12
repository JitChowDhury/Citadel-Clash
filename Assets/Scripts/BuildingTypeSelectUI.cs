using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    [SerializeField] private Sprite arrowSprite; // Sprite for the deselect button (arrow)
    [SerializeField] private List<BuildingTypeSO> ignoreBuildingTypeList; // Types to ignore in the UI

    private Transform arrowButton; // Reference to the arrow button
    private Dictionary<BuildingTypeSO, Transform> buttonTransformDictionary; // Links building types to their UI buttons

    void Awake()
    {
        Transform buttonTemplate = transform.Find("btnTemplate"); // Get the button template
        buttonTemplate.gameObject.SetActive(false); // Hide the template

        buttonTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();

        // Load list of building types from Resources folder
        BuildingTypeListSO buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        int index = 0;

        // Create the arrow (deselect) button
        arrowButton = Instantiate(buttonTemplate, transform);
        arrowButton.gameObject.SetActive(true);
        float offsetAmount = 160f;
        arrowButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);
        arrowButton.Find("image").GetComponent<Image>().sprite = arrowSprite;
        arrowButton.Find("image").GetComponent<RectTransform>().sizeDelta = new Vector2(0, -30);

        // On click, deselect any active building type
        arrowButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            BuildingManager.Instance.SetActiveBuildingType(null);
        });

        index++;

        // Create a button for each building type
        foreach (BuildingTypeSO buildingType in buildingTypeList.List)
        {
            if (ignoreBuildingTypeList.Contains(buildingType)) continue;

            Transform buttonTransform = Instantiate(buttonTemplate, transform);
            buttonTransform.gameObject.SetActive(true);
            buttonTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);
            buttonTransform.Find("image").GetComponent<Image>().sprite = buildingType.sprite;

            buttonTransformDictionary[buildingType] = buttonTransform;

            // On click, set this building type as active
            buttonTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuildingManager.Instance.SetActiveBuildingType(buildingType);
            });

            index++;
        }
    }

    void Start()
    {
        // Subscribe to building type change event
        BuildingManager.Instance.OnActiveBuildingTypeChange += BuildingManager_OnActiveBuildingTypeChange;
        UpdateActiveBuildingType(); // Highlight the active one initially
    }

    private void BuildingManager_OnActiveBuildingTypeChange(object sender, BuildingManager.OnActiveBuildingTypeChangeEventArgs e)
    {
        UpdateActiveBuildingType(); // Update UI when active building changes
    }

    private void UpdateActiveBuildingType()
    {
        // Hide all "selected" highlights
        arrowButton.Find("selected").gameObject.SetActive(false);
        foreach (BuildingTypeSO buildingType in buttonTransformDictionary.Keys)
        {
            Transform buttonTransform = buttonTransformDictionary[buildingType];
            buttonTransform.Find("selected").gameObject.SetActive(false);
        }

        // Show highlight on the currently active building type
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
