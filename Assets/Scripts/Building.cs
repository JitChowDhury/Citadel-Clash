using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private HealthSystem healthSystem;
    private BuildingTypeSO buildingType;
    private Transform buildingDemolishBtn;
    private Transform buildingRepairBtn;

    void Awake()
    {
        buildingDemolishBtn = transform.Find("pfBuildingDemolishBtn");
        buildingRepairBtn = transform.Find("pfBuildingRepairBtn");
        if (buildingDemolishBtn != null)
            buildingDemolishBtn.gameObject.SetActive(false);

        HideBuildingRepairButton();
    }
    void Start()
    {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.setHealthAmountMax(buildingType.healthAmountMax, true);
        healthSystem.OnDied += healthSystem_OnDied;
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnHealed += HealthSystem_OnHealed;

    }

    private void HealthSystem_OnHealed(object sender, EventArgs e)
    {
        if (healthSystem.isFullHealth())
        {
            HideBuildingRepairButton();
        }
    }

    private void HealthSystem_OnDamaged(object sender, EventArgs e)
    {
        ShowBuildingRepairButton();
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDamaged);
        CinemachineShake.Instance.ShakeCamera(7f, .15f);
        ChromaticAberrationEffect.Instance.SetWeight(1f);
    }


    private void healthSystem_OnDied(object sender, EventArgs e)
    {
        Instantiate(Resources.Load<Transform>("pfBuildingDestroyedParticles"), transform.position, Quaternion.identity);
        CinemachineShake.Instance.ShakeCamera(10f, .2f);
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDestroyed);
        ChromaticAberrationEffect.Instance.SetWeight(1f);
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


    private void ShowBuildingRepairButton()
    {
        if (buildingRepairBtn != null)
        {
            buildingRepairBtn.gameObject.SetActive(true);
        }
    }
    private void HideBuildingRepairButton()
    {
        if (buildingRepairBtn != null)
        {
            buildingRepairBtn.gameObject.SetActive(false);
        }
    }

}
