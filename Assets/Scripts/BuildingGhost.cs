using System;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    private GameObject spriteGameObject;
    private ResourceNearbyOverlay resourceNearbyOverlay;

    private void Awake()
    {

        spriteGameObject = transform.Find("sprite").gameObject;//finds the sprite in transform
        resourceNearbyOverlay = transform.Find("pfResourceNearbyOverlay").GetComponent<ResourceNearbyOverlay>();
        Hide(); //hides by default
        resourceNearbyOverlay.Hide();//hides resource
    }

    void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChange += BuildingManager_OnActiveBuildingTypeChange;
    }

    private void BuildingManager_OnActiveBuildingTypeChange(object sender, BuildingManager.OnActiveBuildingTypeChangeEventArgs e)
    {
        if (e.activeBuildingType == null)
        {
            Hide();
            resourceNearbyOverlay.Hide();

        }
        else
        {
            Show(e.activeBuildingType.sprite);//from passed argument
            if (e.activeBuildingType.hasResourceGeneratorData)
                resourceNearbyOverlay.Show(e.activeBuildingType.resourceGeneratorData);//pass the data
            else
                resourceNearbyOverlay.Hide();

        }
    }

    private void Update()
    {
        transform.position = UtilsClass.GetMousePosition();//gets active mouse position

    }

    private void Show(Sprite ghostSprite)
    {
        spriteGameObject.SetActive(true);
        spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
    }

    private void Hide()
    {
        spriteGameObject.SetActive(false);

    }
}
