using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObject : ManagedUpdateBehaviour {

	[SerializeField] private float rotateSpeed = 1.0f;
	private Transform myTransform;
	private Vector3 rotateDir;

	public override void FixedUpdateMe()
	{
		myTransform.Rotate(rotateDir);
	}

	protected override void Initialize()
	{
		myTransform = GetComponent<Transform>();
		rotateDir = new Vector3(0.0f,0.0f,rotateSpeed);
	}
}
