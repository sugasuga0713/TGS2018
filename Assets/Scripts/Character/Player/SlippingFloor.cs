using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlippingFloor : ManagedUpdateBehaviour {

	private Transform playerTransform;
	private Vector2 playerPos;

	private Collider2D myCollider;

	[SerializeField] private float sizeX = 0.5f;
	[SerializeField] private float topPoint = 0.0f;
	[SerializeField] private float sizeY = 3.0f;

	private float rangeMinX, rangeMaxX;
	private float rangeMinY, rangeMaxY;

	protected override void Initialize()
	{
		myCollider = GetComponent<Collider2D>();
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		rangeMinX = transform.position.x - sizeX;
		rangeMaxX = transform.position.x + sizeX;
		rangeMinY = transform.position.y - sizeY;
		rangeMaxY = transform.position.y + topPoint;
	}

	public override void FixedUpdateMe()
	{
		playerPos = playerTransform.position;
		if(playerPos.x > rangeMinX && playerPos.x < rangeMaxX)
		{
			if(playerPos.y > rangeMinY && playerPos.y < rangeMaxY)
			{
				myCollider.enabled = false;
				return;
			}
		}

		myCollider.enabled = true;
	}
}