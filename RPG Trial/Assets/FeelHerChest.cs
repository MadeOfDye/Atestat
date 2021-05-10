using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FeelHerChest : MonoBehaviour
{
   /* private ItemObjectDatabase database;
    public InventoryObject chestInventory;
    private void Awake()
    {
        FeelChests();
    }
    public void FeelChests()
    {
        database = (ItemObjectDatabase)AssetDatabase.LoadAssetAtPath("Assets/Resources/ItemDatabase.asset", typeof(ItemObjectDatabase));
// chestInventory = new InventoryObject();
        int randNoI = Random.Range(2, 10);
        for (int i = 0; i <= randNoI; i++)
        {
            for (int j = 0; j < database.items.Length; j++)
            {
                int randO = Random.Range(1, 10);
                if (randO >= 1 && randO <= 3)
                {
                    int amount = Random.Range(1, 4);
                    Debug.Log(database.items[j]);
                    chestInventory.AddItem(database.items[j], amount);
                }
            }
        }
    }*/
}
