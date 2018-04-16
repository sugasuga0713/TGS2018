using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : BaseObject {

	[SerializeField] private float power = 15.0f;
	[SerializeField] private Vector3 leftButtom = Vector2.zero;
	[SerializeField] private Vector2 rightTop = Vector2.zero;
	private Collider2D coll;
	private Collider2D myColl;

	private void Bounce(Rigidbody2D rb)
	{
		rb.velocity = Vector2.zero;
		rb.AddForce(Vector2.up * power * rb.mass, ForceMode2D.Impulse);
	}

	public override void FixedUpdateMe()
	{
		coll = Physics2D.OverlapBox(myTransform.position + leftButtom, rightTop, 0.0f);

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
			Bounce(coll.GetComponent<Rigidbody2D>());
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
		myColl = GetComponent<Collider2D>();
	}
}
