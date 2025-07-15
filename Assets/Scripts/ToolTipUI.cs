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
    private ToolTipTimer toolTipTimer;

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
        HandleFollowMouse();



        if (toolTipTimer != null)
        {
            toolTipTimer.timer -= Time.deltaTime;
            if (toolTipTimer.timer <= 0)
            {
                Hide();
            }

        }
    }

    private void SetText(string toolTipText)
    {
        textMeshPro.SetText(toolTipText);
        textMeshPro.ForceMeshUpdate();
        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(8, 8);
        backgroundRectTransform.sizeDelta = textSize + padding;
    }

    public void Show(string toolTipText, ToolTipTimer toolTipTimer = null)
    {
        this.toolTipTimer = toolTipTimer;
        gameObject.SetActive(true);
        SetText(toolTipText);
        HandleFollowMouse();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void HandleFollowMouse()
    {
        Vector2 anchoredPosition = Input.mousePosition / canvasRectTranform.localScale.x;

        float elementWidth = backgroundRectTransform.rect.width;
        float elementHeight = backgroundRectTransform.rect.height;
        float canvasWidth = canvasRectTranform.rect.width;
        float canvasHeight = canvasRectTranform.rect.height;

        // Clamp right
        if (anchoredPosition.x + elementWidth > canvasWidth)
        {
            anchoredPosition.x = canvasWidth - elementWidth;
        }

        // Clamp left
        if (anchoredPosition.x < 0)
        {
            anchoredPosition.x = 0;
        }

        // Clamp top
        if (anchoredPosition.y + elementHeight > canvasHeight)
        {
            anchoredPosition.y = canvasHeight - elementHeight;
        }

        // Clamp bottom
        if (anchoredPosition.y < 0)
        {
            anchoredPosition.y = 0;
        }

        rectTransform.anchoredPosition = anchoredPosition;
    }


    public class ToolTipTimer
    {
        public float timer;
    }
}
