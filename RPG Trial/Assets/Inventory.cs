using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventoryObject inventory;
    public void ItemInteraction(Transform other)
    {
        Debug.Log("Here");
        var item = other.GetComponent<Item>();
        if(item)
        {
            inventory.AddItem(item.item, 1);
            Destroy(other.gameObject);
        }
    }
    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }
}
