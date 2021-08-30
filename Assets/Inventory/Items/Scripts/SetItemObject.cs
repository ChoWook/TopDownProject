using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Set Item", menuName = "Inventory System/Items/set item")]
[System.Serializable]
public class SetItemObject : ScriptableObject
{
    public string SetBuffDescription;
    [SerializeField]
    public SetItem data = new SetItem();

    public SetItem CreateSetItem()
    {
        SetItem newSetItem = new SetItem(this);
        return newSetItem;
    }

}

[System.Serializable]
public class SetItem
{
    public ItemObject[] Items;
    public int Id;
    public SetItemBuff[] SetBuffs;
    public string Name;
    public string SetBuffDescription;

    //public ItemBuff[] Buffs;

    public SetItem()
    {
        Id = -1;
        Name = "Base";
        SetBuffDescription = "세트 효과 설명";
    }

    public SetItem(SetItemObject setItem)
    {
        Items = setItem.data.Items;
        Id = setItem.data.Id;
        Name = setItem.data.Name;
        SetBuffDescription = setItem.SetBuffDescription;
        
        SetBuffs = new SetItemBuff[setItem.data.SetBuffs.Length];
        /*
        for (int i = 0; i < setItem.data.SetBuffs.Length; i++)
        {
            // 세트 아이템 효과 복사
        }
        */

        //Buffs = new ItemBuff[setItem.Buffs.Length];
        //for (int i = 0; i < Buffs.Length; i++)
        //{
        //    Buffs[i] = new ItemBuff(setItem.Buffs[i].min, setItem.Buffs[i].max)
        //    {
        //        attribute = setItem.Buffs[i].attribute
        //    };
        //}
    }

}


public class SetItemBuff
{
    //세트아이템 별 효과를 기술할 클래스 예정
}
