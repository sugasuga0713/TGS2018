using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : ManagedUpdateBehaviour{

	[SerializeField] private Transform redBar = null;
	private float hpBarScaleX;

	protected override void Initialize()
	{
		hpBarScaleX = redBar.localScale.x;
	}

	public void HPUpdate(float hp, float hpMax)
	{
		Vector2 hpBarScale = redBar.localScale;
		hpBarScale.x = hp / hpMax * hpBarScaleX;
		redBar.localScale = hpBarScale;
	}
}
