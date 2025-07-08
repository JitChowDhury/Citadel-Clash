using UnityEngine;

public class BuildingManager : MonoBehaviour
{
 
    private Camera mainCamera;
    private BuildingTypeListSO buildingTypeList;//list aof all building type SO
    private BuildingTypeSO buildingType;


    private void Start()
    {
        mainCamera = Camera.main;
        buildingTypeList=Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);//loads from resource folder
        buildingType = buildingTypeList.List[0];
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(buildingType.buildingPrefab,GetMousePosition(),Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            buildingType = buildingTypeList.List[0];
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            buildingType = buildingTypeList.List[1];
        }
    }


    private Vector3 GetMousePosition()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        return mousePosition;
    }
}
