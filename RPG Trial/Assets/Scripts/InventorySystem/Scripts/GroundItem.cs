using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GroundItem : MonoBehaviour, ISerializationCallbackReceiver
{
    public ItemObject item;

    public void OnAfterDeserialize()
    {
    }

    public void OnBeforeSerialize()
    {
#if UNITY_EDITOR
        GetComponentInChildren<MeshFilter>().mesh = item.groundDisplay;
        EditorUtility.SetDirty(GetComponentInChildren<MeshFilter>());
#endif
    }
}