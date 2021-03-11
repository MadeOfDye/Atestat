using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RaycastData" , menuName = "RPGTrial/new RaycastData")]
public class RaycastData : ScriptableObject
{
	public bool interactible = false;
	public bool IsThisMewObject(Transform transform)
	{
		if (HitTransform == null)
		{
			return true;
		}
		if (HitTransform == transform) return false;
		return true;
	}
	public Transform HitTransform { private set; get; }
	public RaycastHit? Hit { private set; get; }
	public void UpdateData(RaycastHit? _hit)
	{
		HitTransform = _hit.Value.transform;
		Hit = _hit;
		interactible = true;
		}
		
	public void Reset()
	{
		HitTransform = null;
		Hit = null;
	}
}
