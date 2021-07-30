using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Set Item Database", menuName = "Inventory System/Items/set Database")]
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
