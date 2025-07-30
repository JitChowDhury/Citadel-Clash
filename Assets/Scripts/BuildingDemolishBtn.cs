
using UnityEngine;
using UnityEngine.UI;

public class BuildingDemolishBtn : MonoBehaviour
{
    [SerializeField] private Building building;

    private void Awake()
    {
        transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
           {
               BuildingTypeSO buildingType = building.GetComponent<BuildingTypeHolder>().buildingType;
               foreach (ResourceAmount resourceAmount in buildingType.constructionResourceCostArray)
               {
                   ResourceManager.Instance.AddResource(resourceAmount.resourceType, Mathf.FloorToInt(resourceAmount.amount * Random.Range(.4f, .7f)));
               }
               Instantiate(Resources.Load<Transform>("pfBuildingDestroyedParticles"), transform.position, Quaternion.identity);
               Destroy(building.gameObject);

           });
    }



}
