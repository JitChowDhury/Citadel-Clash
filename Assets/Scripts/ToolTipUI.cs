using System.ComponentModel;
using TMPro;
using UnityEngine;

public class ToolTipUI : MonoBehaviour
{

    public static ToolTipUI Instance { get; private set; }

    [SerializeField] private RectTransform canvasRectTranform;
    private TextMeshProUGUI textMeshPro;
    private RectTransform backgroundRectTransform;
    private RectTransform rectTransform;

    void Awake()
    {
        Instance = this;
        rectTransform = GetComponent<RectTransform>();
        textMeshPro = transform.Find("text").GetComponent<TextMeshProUGUI>();
        backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();

        Hide();
    }

    private void Update()
    {
        Vector2 anchoredPosition = Input.mousePosition / canvasRectTranform.localScale.x;


        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTranform.rect.width)
        {
            anchoredPosition.x = canvasRectTranform.rect.width - backgroundRectTransform.rect.width;
        }


        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTranform.rect.height)
        {
            anchoredPosition.y = canvasRectTranform.rect.height - backgroundRectTransform.rect.height;
        }

        rectTransform.anchoredPosition = anchoredPosition;
    }

    private void SetText(string toolTipText)
    {
        textMeshPro.SetText(toolTipText);
        textMeshPro.ForceMeshUpdate();
        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(8, 8);
        backgroundRectTransform.sizeDelta = textSize + padding;
    }

    public void Show(string toolTipText)
    {
        gameObject.SetActive(true);
        SetText(toolTipText);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
