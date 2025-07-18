using UnityEngine;
using UnityEngine.UI;

public class ConstructionTimerUI : MonoBehaviour
{
    [SerializeField] BuildingConstruction buildingConstruction;
    private Image constructionProgressImage;
    void Awake()
    {
        constructionProgressImage = transform.Find("mask").Find("image").GetComponent<Image>();

    }
    private void Update()
    {
        constructionProgressImage.fillAmount = buildingConstruction.GetConstructionTimerNormalized();
    }
}
