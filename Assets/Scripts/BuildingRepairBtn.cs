using UnityEngine;
using UnityEngine.UI;

public class BuildingRepairBtn : MonoBehaviour
{
    [SerializeField] private HealthSystem buildingHealthSystem;
    [SerializeField] private ResourceTypeSO goldResourceType;

    private void Awake()
    {
        transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
           {
               int missinghealth = buildingHealthSystem.GetHealthAmountMax() - buildingHealthSystem.GetHealthAmount();
               int repairCost = missinghealth / 2;

               ResourceAmount[] resourceAmountCost = new ResourceAmount[] { new ResourceAmount { resourceType = goldResourceType, amount = repairCost } };
               if (ResourceManager.Instance.CanAfford(resourceAmountCost))
               {
                   //can afford the repairs
                   ResourceManager.Instance.SpendResources(resourceAmountCost);
                   buildingHealthSystem.HealFull();
               }
               else
               {
                   //cannot afford;
                   ToolTipUI.Instance.Show("Cannot afford repair cost!", new ToolTipUI.ToolTipTimer { timer = 2f });
               }



           });
    }





}
