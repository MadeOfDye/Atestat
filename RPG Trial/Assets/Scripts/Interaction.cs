using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Interaction : MonoBehaviour
{
	[SerializeField] private RaycastData data = null;
	[SerializeField] private Transform viewCamera = null;
	[SerializeField] private float interactionDistance = 5f;
	[SerializeField] private LayerMask layersToRaycast = 0;

	void Start()
	{
		data.Reset();
	}
	void Update()
	{
		if (Time.frameCount % 4 == 0)
		{

			RaycastHit? hit = DoRayCasting();
			if (hit.HasValue)
			{
				if (data.IsThisMewObject(hit.Value.transform))
				{
					data.UpdateData(hit);
				}
			}
			else
			{
				if (data.HitTransform)
				{
					data.Reset();
				}
			}
		}
	}
	private RaycastHit? DoRayCasting()
	{
		Ray ray = new Ray(viewCamera.position, viewCamera.forward);

		RaycastHit hit;

		bool hashit = Physics.SphereCast(ray, 0.5f, out hit, interactionDistance, layersToRaycast);

		if (hashit)
		{
			return hit;
		}
		return null;
	}
}
