using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{

    public static BuildingManager Instance { get; private set; }

    public event EventHandler<OnActiveBuildingTypeChangeEventArgs> OnActiveBuildingTypeChange;

    public class OnActiveBuildingTypeChangeEventArgs : EventArgs
    {
        public BuildingTypeSO activeBuildingType;//passing the building type via events
    }
    private Camera mainCamera;
    private BuildingTypeListSO buildingTypeList;//list aof all building type SO
    private BuildingTypeSO activeBuildingType;

    void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        mainCamera = Camera.main;
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);//loads from resource folder

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())//checks if the pointer is on a UI
        {
            if (activeBuildingType != null && CanSpawnBuilding(activeBuildingType, UtilsClass.GetMousePosition()))
            {
                if (ResourceManager.Instance.CanAfford(activeBuildingType.constructionResourceCostArray))
                {
                    ResourceManager.Instance.SpendResources(activeBuildingType.constructionResourceCostArray);
                    Instantiate(activeBuildingType.buildingPrefab, UtilsClass.GetMousePosition(), Quaternion.identity);
                }
            }


        }

    }




    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        activeBuildingType = buildingType;
        OnActiveBuildingTypeChange?.Invoke(this, new OnActiveBuildingTypeChangeEventArgs { activeBuildingType = activeBuildingType });
    }

    public BuildingTypeSO GetActiveBuildingType()
    {
        return activeBuildingType;
    }

    private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position)
    {
        BoxCollider2D boxCollider2D = buildingType.buildingPrefab.GetComponent<BoxCollider2D>();
        Collider2D[] collider2DArray = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);//checks
        //if thers 

        bool isAreaClear = collider2DArray.Length == 0;//first we check if actual area is clear or not
        if (!isAreaClear) return false;

        //checks all colliders in the minconstructionradius
        collider2DArray = Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);//getting colliders inside the min construction radius

        foreach (Collider2D collider in collider2DArray)
        {
            //colliders inside the construction radiius
            BuildingTypeHolder buildingTypeHolder = collider.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null)
            {
                //has a building of same type withing the minconstruction radius
                if (buildingTypeHolder.buildingType == buildingType) return false;
            }

        }
        //not placed too far from anyother build
        float maxConstructionRadius = 25f;
        collider2DArray = Physics2D.OverlapCircleAll(position, maxConstructionRadius);//getting colliders inside the maxConstructionRadius radius

        foreach (Collider2D collider in collider2DArray)
        {
            //colliders inside the construction radiius
            BuildingTypeHolder buildingTypeHolder = collider.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null)
            {
                //its a building 
                return true;

            }

        }

        return false;
    }
}
