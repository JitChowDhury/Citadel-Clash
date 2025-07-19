using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private HealthSystem healthSystem;
    private BuildingTypeSO buildingType;
    private Transform buildingDemolishBtn;

    void Awake()
    {
        buildingDemolishBtn = transform.Find("pfBuildingDemolishBtn");
        if (buildingDemolishBtn != null)
            buildingDemolishBtn.gameObject.SetActive(false);
    }
    void Start()
    {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.setHealthAmountMax(buildingType.healthAmountMax, true);
        healthSystem.OnDied += healthSystem_OnDied;

    }

    void Update()
    {

    }

    private void healthSystem_OnDied(object sender, EventArgs e)
    {
        Destroy(gameObject);
    }

    private void OnMouseEnter()
    {
        if (buildingDemolishBtn != null)
            buildingDemolishBtn.gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        if (buildingDemolishBtn != null)
            buildingDemolishBtn.gameObject.SetActive(false);

    }


}
