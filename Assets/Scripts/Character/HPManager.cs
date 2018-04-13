using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : ManagedUpdateBehaviour{

	[SerializeField] private Transform myTransform = null;
	[SerializeField] private Transform redBar = null;
	[SerializeField] private Transform targetTransform = null;
	private Vector3 offset;
	private float hpBarScaleX;

	protected override void Initialize()
	{
		offset = myTransform.position - targetTransform.position;
		hpBarScaleX = redBar.localScale.x;
		Debug.Log(hpBarScaleX);
	}

	/*	private void LateUpdate()
		{
			Vector3 pos = myTransform.position;
			pos.x = targetTransform.position.x + offset.x;
			myTransform.position = pos;
		}
		*/

	public void HPUpdate(float hp, float hpMax)
	{
		Vector2 hpBarScale = redBar.localScale;
		hpBarScale.x = hp / hpMax * hpBarScaleX;
		redBar.localScale = hpBarScale;
	}
}
