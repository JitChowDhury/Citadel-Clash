using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceNearbyOverlay : MonoBehaviour
{
    private ResourceGeneratorData resourceGeneratorData;
    void Awake()
    {
        Hide();
    }
    void Update()
    {
        int nearByResourceAmount = ResourceGenerator.GetNearByResourceAmount(resourceGeneratorData, transform.position);
        float percent = Mathf.RoundToInt((float)nearByResourceAmount / resourceGeneratorData.maxResourceAmount * 100f);
        transform.Find("text").GetComponent<TextMeshPro>().SetText(percent + "%");
    }
    public void Show(ResourceGeneratorData resourceGeneratorData)
    {
        gameObject.SetActive(true);
        this.resourceGeneratorData = resourceGeneratorData;
        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;

    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
