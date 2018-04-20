using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : BaseObject {

	[SerializeField] private float speed = 1.0f;
	[SerializeField] private Vector2 dir = Vector2.zero;
	[SerializeField] private float moveTime = 3.0f;
	private float timeCount = 0.0f;

	public override void FixedUpdateMe()
	{
		myTransform.Translate(dir * speed * Time.deltaTime);
		timeCount += Time.deltaTime;
		if(timeCount > moveTime)
		{
			timeCount = 0.0f;
			dir *= -1;
		}

	}
}
