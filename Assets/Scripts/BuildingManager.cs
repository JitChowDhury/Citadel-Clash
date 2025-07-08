using UnityEngine;

public class BuildingManager : MonoBehaviour
{
 
    private Camera mainCamera;
    private BuildingTypeListSO buildingTypeList;
    private BuildingTypeSO buildingType;


    private void Start()
    {
        mainCamera = Camera.main;
        buildingTypeList=Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        buildingType = buildingTypeList.buildings[0];
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(buildingType.buildingPrefab,GetMousePosition(),Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            buildingType = buildingTypeList.buildings[0];
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            buildingType = buildingTypeList.buildings[1];
        }
    }


    private Vector3 GetMousePosition()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        return mousePosition;
    }
}
