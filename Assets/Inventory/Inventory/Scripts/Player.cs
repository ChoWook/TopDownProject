﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public InventoryObject inventory;
    public InventoryObject equipment;

    public Attribute[] attributes;

    private Transform helmet;
    private Transform weapon;
    private Transform glove;
    private Transform boots;
    private Transform chest;

    public Transform weaponTransform;
    public Transform offhandWristTransform;
    public Transform offhandHandTransform;


    //private BoneCombiner boneCombiner;

    private void Start()
    {
        //boneCombiner = new BoneCombiner(gameObject);

        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].SetParent(this);
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
                print(string.Concat("Removed ", _slot.ItemObject, " on ", _slot.parent.inventory.type,
                    ", Allowed Items: ", string.Join(", ", _slot.AllowedItems)));

                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                            attributes[j].value.RemoveModifier(_slot.item.buffs[i]);
                    }
                }

                if (_slot.ItemObject.characterDisplay != null)
                {
                    switch (_slot.AllowedItems[0])
                    {
                        case ItemType.Helmet:
                            Destroy(helmet.gameObject);
                            break;
                        case ItemType.Weapon:
                            Destroy(weapon.gameObject);
                            break;
                        case ItemType.Glove:
                            Destroy(glove.gameObject);
                            break;
                        case ItemType.Boots:
                            Destroy(boots.gameObject);
                            break;
                        case ItemType.Chest:
                            Destroy(chest.gameObject);
                            break;
                    }
                }

                break;
            case InterfaceType.Chest:
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
                print(
                    $"Placed {_slot.ItemObject}  on {_slot.parent.inventory.type}, Allowed Items: {string.Join(", ", _slot.AllowedItems)}");

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


                break;
            case InterfaceType.Chest:
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.Save();
            equipment.Save();
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            inventory.Load();
            equipment.Load();
        }
    }

    public void AttributeModified(Attribute attribute)
    {
        Debug.Log(string.Concat(attribute.type, " was updated! Value is now ", attribute.value.ModifiedValue));
    }


    private void OnApplicationQuit()
    {
        inventory.Clear();
        equipment.Clear();
    }

    public int getAttack()
    {
        return attributes[0].value.ModifiedValue;
    }

    public int getHealth()
    {
        return attributes[1].value.ModifiedValue;
    }

    public float getSpeed()
    {
        return ((float)(attributes[2].value.ModifiedValue) / 5.0f);
    }

    public int getAttackSpeed()
    {
        return attributes[3].value.ModifiedValue;
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
                value.BaseValue = 6;
                break;
            case Attributes.Speed:
                value.BaseValue = 5;
                break;
            case Attributes.AttackSpeed:
                value.BaseValue = 5;
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