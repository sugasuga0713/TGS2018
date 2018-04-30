using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotRay : ManagedUpdateBehaviour {

	[SerializeField] LayerMask layerMask = 0;
	[SerializeField] LineRenderer lRenderer = null;

	protected override void Initialize()
	{

	}

	public void Ray(Vector2 shotPos,Vector2 dir)
	{
		Vector2 endPoint = Physics2DExtentsion.RaycastAndDraw(shotPos, dir,10.0f, layerMask);
		lRenderer.SetPosition(0,shotPos);
		lRenderer.SetPosition(1,endPoint);
	}
}
