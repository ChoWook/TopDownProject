using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicInterface : UserInterface
{
    public GameObject inventoryPrefab;
    public int X_START = -93;
    public int Y_START = 140;
    public int X_SPACE_BETWEEN_ITEM = 47;
    public int NUMBER_OF_COLUMN = 5;
    public int Y_SPACE_BETWEEN_ITEMS = 47;

    public override void CreateSlots()
    {
        slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            AddEvent(obj, EventTriggerType.PointerClick, delegate { OnPointerClick(obj); });

            inventory.GetSlots[i].slotDisplay = obj;

            slotsOnInterface.Add(obj, inventory.GetSlots[i]);
        }
    }
    private Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMN)), 0f);
    }

    public void OnPointerClick(GameObject obj)
    {
        if (slotsOnInterface[obj].item.Id == -1)
        {
            return;
        }
        tooltip.gameObject.SetActive(false);

        var inventory = GetComponentInParent<Player>().inventory;
        var equipment = GetComponentInParent<Player>().equipment;
        var type = inventory.database.ItemObjects[slotsOnInterface[obj].item.Id].type;
        switch (type)
        {
            case ItemType.Food:
                break;
            case ItemType.Helmet:
                inventory.SwapItems(slotsOnInterface[obj], equipment.GetSlots[0]);
                break;
            case ItemType.Weapon:
                inventory.SwapItems(slotsOnInterface[obj], equipment.GetSlots[1]);
                break;
            case ItemType.Glove:
                inventory.SwapItems(slotsOnInterface[obj], equipment.GetSlots[2]);
                break;
            case ItemType.Boots:
                inventory.SwapItems(slotsOnInterface[obj], equipment.GetSlots[3]);
                break;
            case ItemType.Chest:
                inventory.SwapItems(slotsOnInterface[obj], equipment.GetSlots[4]);
                break;
            default: break;
        }
    }
}
