using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food Object", menuName = "Inventory System/Items/Food")]
public class FoodItem : ItemObject
{
    public int restoreHealthValue;
    private void Awake()
    {
        type = Type.Food;
    }
}
