using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : BaseObject {

	[SerializeField] private float speed = 1.0f;
	[SerializeField] private Vector2 dir = new Vector2(1.0f,0.0f);

	public override void FixedUpdateMe()
	{
		myTransform.Translate(Vector2.right * speed * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "fire")
			return;

		speed = 0.0f;
	}
}
