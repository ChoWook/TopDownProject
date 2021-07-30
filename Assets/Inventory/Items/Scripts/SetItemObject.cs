using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Set Item", menuName = "Inventory System/Items/set item")]
public class SetItem : ScriptableObject
{
    public ItemObject[] Items;
    public int Id;
    public SetItemBuff[] Buffs;
}

public class SetItemBuff
{
    //세트아이템 별 효과를 기술할 클래스 예정
}
