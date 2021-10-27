using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItem : MonoBehaviour, ISerializationCallbackReceiver
{
    public ItemObject item;

    private void Awake()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = item.uiDisplay;
        //EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
    }
    public void OnAfterDeserialize()
    {
    }

    public void OnBeforeSerialize()
    {

    }
}
