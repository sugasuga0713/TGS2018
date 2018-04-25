using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : BaseObject {

	[SerializeField] private float velocityY = 0;
	[SerializeField] private float gravity = -9.81f;

	private Vector2[] bottomCheckPos = new Vector2[3];

	private Collider2D myColl;
	private Vector2 myCollSize;

	private bool grounded;

	private int i;

	public override void FixedUpdateMe()
	{
		Gravity();
		BottomCheck();
	}

	protected virtual void BottomCheck()
	{
		bottomCheckPos[0] = bottomCheckPos[1] = bottomCheckPos[2] = myTransform.position;
		float bottomY = myTransform.position.y - myCollSize.y * 0.5f + 0.1f;
		bottomCheckPos[0].y = bottomCheckPos[1].y = bottomCheckPos[2].y = bottomY;
		bottomCheckPos[0].x -= 0.1f;
		bottomCheckPos[2].x += 0.1f;

		for(i = 0; i < 3; i++)
		{
			if (Physics2D.OverlapPoint(bottomCheckPos[i]) != null)
			{
				grounded = true;
				velocityY = 0.0f;
				break;
			}
			else
			{
				grounded = false;
			}
		}
	}

	protected virtual void Gravity()
	{
		myTransform.Translate(new Vector2(0.0f, velocityY * Time.deltaTime));
		velocityY -= gravity * 0.1f;
	}

	protected override void Initialize()
	{
		base.Initialize();
		myColl = transform.Find("coll").GetComponent<Collider2D>();
		myCollSize = myColl.bounds.size;
	}
}
