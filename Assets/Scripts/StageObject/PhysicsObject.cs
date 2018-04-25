using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : BaseObject {

	[SerializeField] private float velocityY = 0;
	[SerializeField] private float gravity = -9.81f;

	private Vector2[] bottomCheckPos = new Vector2[3];

	private Collider2D myColl;
	private Collider2D otherColl;
	private Vector2 myCollSize;
	private Vector2 adjustmentPos; //落下して他のオブジェクトと重なった際に、調整するための変数

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
		float bottomY = myTransform.position.y - myCollSize.y * 0.5f - 0.1f;
		bottomCheckPos[0].y = bottomCheckPos[1].y = bottomCheckPos[2].y = bottomY;
		bottomCheckPos[0].x -= myCollSize.x - 0.1f;
		bottomCheckPos[2].x += myCollSize.x - 0.1f;

		for(i = 0; i < 3; i++)
		{
			otherColl = Physics2D.OverlapPoint(bottomCheckPos[i]);
			if (otherColl != null)
			{
				grounded = true;
				velocityY = 0.0f;
				adjustmentPos = otherColl.transform.position;
				adjustmentPos.x = myTransform.position.x;
				adjustmentPos.y += otherColl.bounds.size.y * 0.5f + myCollSize.y * 0.5f;
				myTransform.position = adjustmentPos;
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
		velocityY += gravity * 0.1f;
	}

	protected override void Initialize()
	{
		base.Initialize();
		myColl = transform.Find("coll").GetComponent<Collider2D>();
		myCollSize = myColl.bounds.size;
	}
}
