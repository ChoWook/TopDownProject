using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Set Item Database", menuName = "Inventory System/Items/SetDatabase")]
public class SetItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public SetItem[] SetItems;

    [ContextMenu("Update ID's")]
    public void UpdateID()
    {
        for (int i = 0; i < SetItems.Length; i++)
        {
            if (SetItems[i].Id != i)
                SetItems[i].Id = i;
        }
    }
    public void OnAfterDeserialize()
    {
        UpdateID();
    }

    public void OnBeforeSerialize()
    {
    }
}

public class SetItem
{
    public ItemObject[] Items;
    public int Id;
    public SetItemBuff[] Buffs;
}

public class SetItemBuff
{
    //세트아이템 별 효과를 기술할 클래스 예정
}