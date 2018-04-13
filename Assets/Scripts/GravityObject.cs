using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityObject : ManagedUpdateBehaviour {

	[SerializeField] private float gravityScale = -5.0f;
	private Transform myTransform;

	public override void FixedUpdateMe()
	{
		myTransform.Translate(new Vector2(0,gravityScale * Time.deltaTime));
	}

	protected override void Initialize()
	{
		myTransform = GetComponent<Transform>();
	}
}
