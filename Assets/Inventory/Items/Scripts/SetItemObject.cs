using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Set Item", menuName = "Inventory System/Items/set item")]
public class SetItem : ScriptableObject
{
    public ItemObject[] Items;
    public int Id;
    public SetItemBuff[] SetBuffs;
    //public ItemBuff[] Buffs;

    public SetItem()
    {
        Id = -1;
    }

    public SetItem(SetItem setItem)
    {
        Items = setItem.Items;
        Id = setItem.Id;
        SetBuffs = new SetItemBuff[setItem.SetBuffs.Length];
        for (int i = 0; i < setItem.SetBuffs.Length; i++)
        {
            // 세트 아이템 효과 복사
        }

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
