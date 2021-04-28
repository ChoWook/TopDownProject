using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour // 플레이어 인벤토리 클래스
                                    // 플레이어 캐릭터가 하나씩 가져야함
{
    public MouseItem mouseItem = new MouseItem();

    public InventoryObject inventory;
    // Start is called before the first frame update

    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            Item _item = new Item(item.item);
            Debug.Log(_item.Id);
            inventory.AddItem(_item, 1);
            Destroy(other.gameObject);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.Save();
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            inventory.Load();
        }
    }
    private void OnApplicationQuit()
    {
        inventory.Container.Items = new InventorySlot[35];
    }
}
