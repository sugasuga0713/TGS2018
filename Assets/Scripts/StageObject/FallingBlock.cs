using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : BaseObject {

	[SerializeField] private float fallingSpeed = 1.0f; //ブロックが落ちる速度
	[SerializeField] private float risingSpeed = 10f;//ブロックが上昇する速度

	[SerializeField] private Vector3 boxPoint = Vector2.zero;
	[SerializeField] private Vector2 boxSize = Vector2.zero;
	private Collider2D coll;
	private BoxCollider2D myColl;
	private BoxCollider2D boxColl;
	private Vector2 collPos;
	private float velocityY = 0.0f;

	[SerializeField] private int state = 0; //0が通常、1が落下、2が上昇、3が停止

	[SerializeField] private LayerMask layerMask = 1;
	[SerializeField] private LayerMask risingLayer = 1;

	private void FixedUpdate()
	{
		if (state == 3)
			return;
		else if (state == 1)
		{
			Falling();
		}
		else if (state == 2)
		{
			Rising();
		}
		else
		{
			coll = Physics2D.OverlapBox(myTransform.position + boxPoint, boxSize, myTransform.eulerAngles.z,layerMask);

			if (coll == null)
				return;

				state = 1;
				velocityY = -3.0f;
		}
	}
	private void Falling()
	{
		myTransform.Translate(new Vector2(0.0f,velocityY * Time.deltaTime)); 
		velocityY -= fallingSpeed;
		coll = Physics2D.OverlapBox(myTransform.position, myColl.bounds.size, myTransform.eulerAngles.z);

		if (coll == null)
			return;
		if (coll == myColl)
			return;

		collPos = coll.transform.position;
		boxColl = coll.GetComponent<BoxCollider2D>();
		collPos.x = myTransform.position.x;
		collPos.y = collPos.y + boxColl.size.y/2 + myColl.bounds.size.y/2;
		myTransform.position = collPos;
		velocityY = 0.0f;
		StartCoroutine(StateChange(2));
	}

	private void Rising()
	{
		myTransform.Translate(new Vector2(0.0f, risingSpeed * Time.deltaTime));
		coll = Physics2D.OverlapBox(myTransform.position, myColl.bounds.size, myTransform.eulerAngles.z,risingLayer);

		if (coll == null)
			return;
		if (coll == myColl)
			return;

		collPos = coll.transform.position;
		boxColl = coll.GetComponent<BoxCollider2D>();
		collPos.x = myTransform.position.x;
		collPos.y = collPos.y - boxColl.size.y / 2 - myColl.bounds.size.y / 2;
		myTransform.position = collPos;
		state = 0;
	}

	protected override void Initialize()
	{
		state = 0;
		base.Initialize();
		myColl = GetComponent<BoxCollider2D>();
	}

	private IEnumerator StateChange(int stateNum)
	{
		state = 3;
		yield return new WaitForSeconds(1.0f);
		state = stateNum;
	}

}
