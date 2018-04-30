using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotRay : ManagedUpdateBehaviour {

	[SerializeField] LayerMask layerMask = 0;
	[SerializeField] LineRenderer lRenderer = null;

	protected override void Initialize()
	{

	}

	public void Ray(Vector2 shotPos,Vector2 markerPos,Vector2 dir)
	{
		Vector2 endPoint = Physics2DExtentsion.RaycastAndDraw(shotPos, dir,100, layerMask);
		lRenderer.SetPosition(0,shotPos);
	/*	if (endPoint == Vector2.zero)
		{
			lRenderer.SetPosition(1, markerPos);
		}*/
			lRenderer.SetPosition(1, endPoint);
		
	}
}
