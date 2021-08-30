using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    private static Tooltip instance;
    public InventoryObject equipment;
    public SetItemDatabaseObject setItemDatabase;
    
    public Camera uiCamera;

    public Image ItemImage;
    public Text NameText;
    public Text SetNameText;
    public Text DescriptionText;
    public Text SetItemCheckLabel;
    public Text SetItemBuffText;
    public Text[] SetItemCheckText;

    public Color green;
    public Color gray;

    private RectTransform backgroundRectTransform;
    
    public float textPaddingSize = 4f;

    private void Awake()
    {
        //TooltipText = transform.Find("Text").GetComponent<Text>();
        //backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
        //green = new Color(17, 89, 0, 255);
        //gray = new Color(58, 58, 58, 222);
        ShowTooltip("툴팁이다");
    }

    private void Update()
    {
        //Vector2 localPoint;
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
        //transform.localPosition = localPoint;

        //Vector2 anchoredPosition = transform.GetComponent<RectTransform>().anchoredPosition;
        //if(anchoredPosition.x + backgroundRectTransform.rect.width > canvasRecttange)
    }

    private void ShowTooltip(string tooltipString)
    {
        gameObject.SetActive(true);

        //TooltipText.text = tooltipString;
        //Vector2 backgroundSize = new Vector2(TooltipText.preferredWidth + textPaddingSize * 2f, TooltipText.preferredHeight + textPaddingSize * 2f);
        //backgroundRectTransform.sizeDelta = backgroundSize;
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

    public void SetTooltipText(InventorySlot obj)
    {
        var itemObject = obj.ItemObject;
        var setItem = setItemDatabase.SetItems[itemObject.data.setItem];
        ItemImage.sprite = itemObject.uiDisplay;
        NameText.text = itemObject.data.Name;
        SetNameText.text = setItem.Name;

        DescriptionText.text = "";
        for (int i = 0; i < obj.item.buffs.Length; i++)
        {
            switch (obj.item.buffs[i].attribute)
            {
                case Attributes.Attack:
                    DescriptionText.text += "공격력 ";
                    break;
                case Attributes.Health:
                    DescriptionText.text += "체력 ";
                    break;
                case Attributes.Speed:
                    DescriptionText.text += "이동속도 ";
                    break;
                case Attributes.AttackSpeed:
                    DescriptionText.text += "공격속도 ";
                    break;
            }
            DescriptionText.text += "+" + obj.item.buffs[i].value.ToString() + "\n";
        }

        SetItemCheckLabel.text = setItem.Name;

        for(int i = 0; i < setItem.Items.Length; i++)
        {
            SetItemCheckText[i].text = setItem.Items[i].data.Name;
            SetItemCheckText[i].color = gray;

            // 장비가 장착되어 있다면 색을 녹색으로 바꿈
            if (equipment.GetSlots[(int)setItem.Items[i].type - 1].item.Id == setItem.Items[i].data.Id)
            {
                SetItemCheckText[i].color = green;
            }
        }

        SetItemBuffText.text = setItem.SetBuffDescription;
    }
}
