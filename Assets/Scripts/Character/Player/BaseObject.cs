using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : ManagedUpdateBehaviour {

	[System.NonSerialized] public Transform myTransform; //Transformのキャッシュ
	[System.NonSerialized] public Rigidbody2D rb; //RigidBody2Dのキャッシュ

	private bool active = false;//行動可能か
	public bool Active
	{
		get
		{
			return active;
		}
		set
		{
			active = value;
		}
	}

	protected override void Initialize()
	{
		myTransform = GetComponent<Transform>();
		rb = GetComponent<Rigidbody2D>();

		Active = true; //操作可能
	}

	public virtual void Pause()
	{
		Active = false;
		rb.velocity = Vector2.zero;
		rb.isKinematic = true;
	}

	public virtual void CancelPause()
	{
		Active = true;
		rb.isKinematic = false;
	}
}
