using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public InventoryObject inventory;
    public InventoryObject equipment;
    public SetItemDatabaseObject setItemDatabase;

    public Attribute[] attributes;

    private Transform helmet;
    private Transform weapon;
    private Transform glove;
    private Transform boots;
    private Transform chest;

    public Transform weaponTransform;
    public Transform offhandWristTransform;
    public Transform offhandHandTransform;

    public Image[] hearts;  //하트는 최대 10칸임 (이미지를 10개밖에 안만들어놓음)
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;
    public SetItemCheck setItemCheck;
    public static int HP;
    static bool isGameStart = true;


    //private BoneCombiner boneCombiner;

    private void Start()
    {
        //boneCombiner = new BoneCombiner(gameObject);
        setItemCheck = new SetItemCheck(setItemDatabase);

        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].SetParent(this);
        }

        if (isGameStart)
        {
            HP = getHealth();
            isGameStart = false;
        }


        for (int i = 0; i < equipment.GetSlots.Length; i++)
        {
            equipment.GetSlots[i].OnBeforeUpdate += OnRemoveItem;
            equipment.GetSlots[i].OnAfterUpdate += OnAddItem;
        }
    }


    public void OnRemoveItem(InventorySlot _slot)
    {
        if (_slot.ItemObject == null)
            return;
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                //print(string.Concat("Removed ", _slot.ItemObject, " on ", _slot.parent.inventory.type,
                //    ", Allowed Items: ", string.Join(", ", _slot.AllowedItems)));

                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                            attributes[j].value.RemoveModifier(_slot.item.buffs[i]);
                    }
                }

                //if (_slot.ItemObject.characterDisplay != null)
                //{
                //    switch (_slot.AllowedItems[0])
                //    {
                //        case ItemType.Helmet:
                //            Destroy(helmet.gameObject);
                //            break;
                //        case ItemType.Weapon:
                //            Destroy(weapon.gameObject);
                //            break;
                //        case ItemType.Glove:
                //            Destroy(glove.gameObject);
                //            break;
                //        case ItemType.Boots:
                //            Destroy(boots.gameObject);
                //            break;
                //        case ItemType.Chest:
                //            Destroy(chest.gameObject);
                //            break;
                //    }
                //}

                OnSetItemCheck(_slot, true);

                break;
            default:
                break;
        }
    }

    public void OnAddItem(InventorySlot _slot)
    {
        if (_slot.ItemObject == null)
            return;
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                //print(
                //    $"Placed {_slot.ItemObject}  on {_slot.parent.inventory.type}, Allowed Items: {string.Join(", ", _slot.AllowedItems)}");

                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                            attributes[j].value.AddModifier(_slot.item.buffs[i]);
                    }
                }

                if (_slot.ItemObject.characterDisplay != null)
                {
                    // 여기는 착용된 장비를 스프라이트에 표시할 때 사용

                    //switch (_slot.AllowedItems[0])
                    //{
                    //    case ItemType.Helmet:
                    //        helmet = boneCombiner.AddLimb(_slot.ItemObject.characterDisplay,
                    //            _slot.ItemObject.boneNames);
                    //        break;
                    //    case ItemType.Weapon:
                    //        weapon = Instantiate(_slot.ItemObject.characterDisplay, weaponTransform).transform;
                    //        break;
                    //    case ItemType.Glove:
                    //        switch (_slot.ItemObject.type)
                    //        {
                    //            case ItemType.Weapon:
                    //                glove = Instantiate(_slot.ItemObject.characterDisplay, offhandHandTransform)
                    //                    .transform;
                    //                break;
                    //            case ItemType.Glove:
                    //                glove = Instantiate(_slot.ItemObject.characterDisplay, offhandWristTransform)
                    //                    .transform;
                    //                break;
                    //        }

                    //        break;
                    //    case ItemType.Boots:
                    //        boots = boneCombiner.AddLimb(_slot.ItemObject.characterDisplay, _slot.ItemObject.boneNames);
                    //        break;
                    //    case ItemType.Chest:
                    //        chest = boneCombiner.AddLimb(_slot.ItemObject.characterDisplay, _slot.ItemObject.boneNames);
                    //        break;
                    //}
                }

                OnSetItemCheck(_slot, false);

                break;
            default:
                break;
        }
    }

    /*
    public void OnTriggerEnter(Collider other)
    {
        var groundItem = other.GetComponent<GroundItem>();
        if (groundItem)
        {
            Item _item = new Item(groundItem.item);
            if (inventory.AddItem(_item, 1))
            {
                Destroy(other.gameObject);
            }
        }
    }
    */

    private void Update()
    {

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    inventory.Save();
        //    equipment.Save();
        //}

        //if (Input.GetKeyDown(KeyCode.KeypadEnter))
        //{
        //    inventory.Load();
        //    equipment.Load();
        //}

        if (HP > getHealth())
        {
            HP = getHealth();
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            // 현재 체력에 맞게 스프라이트 바꾸기
            if (i < HP / 2)
            {
                hearts[i].sprite = fullHeart;
            }
            else if (i == HP / 2 && HP % 2 == 1)
            {
                hearts[i].sprite = halfHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            // 전체 체력에 맞게 스프라이트 온오프

            if (i < getHealth() / 2)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void AttributeModified(Attribute attribute)
    {
        //Debug.Log(string.Concat(attribute.type, " was updated! Value is now ", attribute.value.ModifiedValue));
    }


    private void OnApplicationQuit()
    {
        inventory.Clear();
        equipment.Clear();
    }

    public int getAttack()
    {
        int ret;
        ret = attributes[0].value.ModifiedValue;

        if (SetItemCheck.getChecked(2)) // 기사 세트
        {
            ret += 1;
        }

        if(SetItemCheck.getChecked(5))  // 광전사
        {
            ret = (int)((float)ret * 1.5f);
        }
        return ret;
    }

    public int getHealth()
    {
        int ret;
        ret = attributes[1].value.ModifiedValue;

        if (SetItemCheck.getChecked(2))     // 기사 세트
        {
            ret += 1;
        }

        return ret * 2;
    }

    public float getSpeed()
    {
        float ret;
        ret = (float)(attributes[2].value.ModifiedValue);

        if (SetItemCheck.getChecked(2))     // 기사 세트
        {
            ret += 1;
        }

        if (SetItemCheck.getChecked(4))
        {
            ret += 3;
        }

        return ret / 5.0f;
    }

    public float getAttackSpeed()
    {
        float ret;
        ret = (float)(attributes[3].value.ModifiedValue);

        if (SetItemCheck.getChecked(2))     // 기사 세트
        {
            ret += 1;
        }

        if (SetItemCheck.getChecked(6))     // 광기 세트
        {
            ret += 3;
        }

        return ret / 10.0f;
    }

    public void OnSetItemCheck(InventorySlot slot, bool isRemove)
    {
        //세트아이템 체크
        Debug.Log("SetItemCheck");
        if(slot.item.Id < 0)
        {
            return;
        }
        var setItem = setItemDatabase.SetItems[slot.item.SetItemId];

        if (!isRemove)
        {
            if (slot.item.Id >= 0)
            {
                if (setItem == null)
                {
                    Debug.Log("setItem is null");
                    return;
                }

                bool isSet = true;
                for (int j = 0; j < setItem.Items.Length; j++)
                {
                    if (equipment.GetSlots[(int)setItem.Items[j].type - 1].item.Id != setItem.Items[j].data.Id)
                    {
                        Debug.Log(setItemDatabase.SetItems[setItem.Id].Name + false);
                        isSet = false;
                        break;
                    }
                }

                if (isSet)
                {
                    SetItemCheck.setChecked(setItem.Id, true);
                }
            }
        }
        else
        {
            SetItemCheck.setChecked(setItem.Id, false);
        }

    }
}

[System.Serializable]
public class Attribute
{
    [System.NonSerialized] public Player parent;
    public Attributes type;
    public ModifiableInt value;

    public void SetParent(Player _parent)
    {
        parent = _parent;
        value = new ModifiableInt(AttributeModified);
        switch (type)       // 기본 능력치 초기화
        {
            case Attributes.Attack:
                value.BaseValue = 5;
                break;
            case Attributes.Health:
                value.BaseValue = 4;
                break;
            case Attributes.Speed:
                value.BaseValue = 5;
                break;
            case Attributes.AttackSpeed:
                value.BaseValue = 10;
                break;
            default:
                break;
        }
    }

    public void AttributeModified()
    {
        parent.AttributeModified(this);
    }


}

public class SetItemCheck
{
    static SetItemDatabaseObject setItemDatabase;
    static bool[] Checked;

    public SetItemCheck(SetItemDatabaseObject setItemDatabase) {
        if (Checked == null)
        {
            Checked = new bool[setItemDatabase.SetItems.Length];
        }
        SetItemCheck.setItemDatabase = setItemDatabase;
    }

    public static void setChecked(int id, bool isChecked)
    {
        if(Checked[id] != isChecked)
        {
            Checked[id] = isChecked;
            Debug.Log(setItemDatabase.SetItems[id].Name + isChecked);
            if (isChecked)
            {
                //세트효과 발동
                switch (id)
                {
                    default:break;
                }
            }
            else
            {
                //세트효과 제거
                switch (id)
                {
                    default: break;
                }
            }
        }
    }

    public static bool getChecked(int id)
    {
        return Checked[id];
    }

    public static bool getChecked(string name)
    {
        for(int i = 0; i < setItemDatabase.SetItems.Length; i++)
        {
            if(setItemDatabase.SetItems[i].Name == name)
            {
                return Checked[setItemDatabase.SetItems[i].Id];
            }
        }
        return false;
    }
}