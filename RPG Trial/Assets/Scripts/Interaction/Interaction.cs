using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Interaction : MonoBehaviour
{
	[SerializeField] private RaycastData data = null;
	public Transform viewCamera = null;
	[SerializeField] private float interactionDistance = 5f;
	[SerializeField] private LayerMask layersToRaycast = 0;
	public Inventory inti;
	void Start()
	{
		viewCamera = Camera.main.transform;
		data.Reset();
	}
	void Update()
	{
		//if (Time.frameCount % 4 == 0)
		//{
		if(data.interactible == true)
		{
			if(Input.GetKeyDown(KeyCode.E))
			{
					Debug.Log(data.HitTransform.name);
				inti.ItemInteraction(data.HitTransform);
				//Interact / activate a script on an object
			}
		}
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
				data.interactible = false;
				}
			}
		}
	//}
	private RaycastHit? DoRayCasting()
	{
		Ray ray = new Ray(viewCamera.position, viewCamera.forward);
		RaycastHit hit;
		bool hashit = Physics.SphereCast(ray, 2.3f, out hit, interactionDistance, layersToRaycast);
		if (hashit)
		{
			return hit;
		}
		return null;
	}
}
