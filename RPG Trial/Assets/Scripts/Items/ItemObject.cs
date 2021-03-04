using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    Food,
    Equipment,
    Miscellaneous
}

public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab;
}
