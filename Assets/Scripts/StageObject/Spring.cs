using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : PhysicsObject {

	[SerializeField] private float power = 15.0f;
	[SerializeField] private Vector3 leftButtom = Vector2.zero;
	[SerializeField] private Vector2 rightTop = Vector2.zero;
	private Collider2D coll;
	[SerializeField] private LayerMask layerMask = 0;

	private void Bounce(Rigidbody2D rb)
	{
		rb.velocity = Vector2.zero;
		rb.AddForce(Vector2.up * power * rb.mass, ForceMode2D.Impulse);
	}

	private void Bounce(PhysicsObject physicsObject)
	{
		physicsObject.VelocityY = power * 0.7f;
	}

	public override void FixedUpdateMe()
	{
		base.FixedUpdateMe();
		coll = Physics2D.OverlapBox(myTransform.position + leftButtom, rightTop, 0.0f,layerMask);

		if (coll == null)
			return;

		if (coll.tag == "Stage")
			return;

		if (coll.tag == "Player")
		{
			Bounce(coll.transform.parent.GetComponent<Rigidbody2D>());
		}
		else
		{
			//Bounce(coll.transform.parent.GetComponent<Rigidbody2D>());
			Bounce(coll.transform.parent.GetComponent<PhysicsObject>());
		}
	}

	/*private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Stage")
			return;

		if (collision.tag == "Player")
		{
			Bounce(collision.transform.parent.GetComponent<Rigidbody2D>());
		}
		else
		{
			Bounce(collision.GetComponent<Rigidbody2D>());
		}
	}*/

	protected override void Initialize()
	{
		base.Initialize();
	}
}
