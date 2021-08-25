using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    private static Tooltip instance;

    [SerializeField]
    private Camera uiCamera;


    private Text TooltipText;
    private RectTransform backgroundRectTransform;
    
    public float textPaddingSize = 4f;

    private void Awake()
    {
        TooltipText = transform.Find("Text").GetComponent<Text>();
        backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();

        ShowTooltip("툴팁이다");
    }

    private void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
        transform.localPosition = localPoint;

        Vector2 anchoredPosition = transform.GetComponent<RectTransform>().anchoredPosition;
        //if(anchoredPosition.x + backgroundRectTransform.rect.width > canvasRecttange)
    }

    private void ShowTooltip(string tooltipString)
    {
        gameObject.SetActive(true);

        TooltipText.text = tooltipString;
        Vector2 backgroundSize = new Vector2(TooltipText.preferredWidth + textPaddingSize * 2f, TooltipText.preferredHeight + textPaddingSize * 2f);
        backgroundRectTransform.sizeDelta = backgroundSize;
    }

    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public static void ShowTooltip_Static(string tooltipString)
    {
        instance.ShowTooltip(tooltipString);
    }

    public static void HideTooltip_Static()
    {
        instance.HideTooltip();
    }
}
