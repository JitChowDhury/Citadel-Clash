using System;
using Mono.Cecil;
using UnityEngine;

public class BuildingConstruction : MonoBehaviour
{
    public static BuildingConstruction Create(Vector3 position, BuildingTypeSO buildingType)
    {
        Transform pfBuildingConstruction = Resources.Load<Transform>("pfBuildingConstruction");
        Transform buildingConstructionTransform = Instantiate(pfBuildingConstruction, position, Quaternion.identity);

        BuildingConstruction buildingConstruction = buildingConstructionTransform.GetComponent<BuildingConstruction>();
        buildingConstruction.SetBuildingType(buildingType);

        return buildingConstruction;
    }

    private BuildingTypeSO buildingType;

    private float constructionTimer;
    private float constructionTimerMax;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private BuildingTypeHolder buildingTypeHolder;
    private Material constructionMaterial;
    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = transform.Find("sprite").GetComponent<SpriteRenderer>();
        buildingTypeHolder = GetComponent<BuildingTypeHolder>();
        constructionMaterial = spriteRenderer.material;
    }
    private void Update()
    {
        constructionTimer -= Time.deltaTime;
        constructionMaterial.SetFloat("_Progress", GetConstructionTimerNormalized());
        if (constructionTimer <= 0f)
        {
            Instantiate(buildingType.buildingPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void SetBuildingType(BuildingTypeSO buildingType)
    {
        constructionTimerMax = buildingType.constructionTimerMax;
        this.buildingType = buildingType;
        constructionTimer = constructionTimerMax;

        spriteRenderer.sprite = buildingType.sprite;
        this.boxCollider.offset = buildingType.buildingPrefab.GetComponent<BoxCollider2D>().offset;
        this.boxCollider.size = buildingType.buildingPrefab.GetComponent<BoxCollider2D>().size;

        buildingTypeHolder.buildingType = buildingType;
    }

    public float GetConstructionTimerNormalized()
    {
        return 1 - constructionTimer / constructionTimerMax;
    }
}
