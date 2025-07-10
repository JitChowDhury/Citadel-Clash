using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{

    public static BuildingManager Instance { get; private set; }
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
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (activeBuildingType != null)
                Instantiate(activeBuildingType.buildingPrefab, GetMousePosition(), Quaternion.identity);
        }

    }


    private Vector3 GetMousePosition()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        return mousePosition;
    }

    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        activeBuildingType = buildingType;
    }

    public BuildingTypeSO GetActiveBuildingType()
    {
        return activeBuildingType;
    }
}
