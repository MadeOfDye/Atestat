using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    Food,
    Equipment,
    Default
}

public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public Type type;

    [TextArea(15, 20)]
    public string description;
}
