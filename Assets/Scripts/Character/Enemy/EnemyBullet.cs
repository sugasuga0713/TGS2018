using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : BulletBase {

	[SerializeField] private float power = 1.0f;

	protected override void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.tag == "Player")
		{
			coll.transform.GetComponent<PlayerController>().Damage(power);
			Delete();
		}
		Delete();
	}

	public override void FixedUpdateMe()
	{
		if (!this.gameObject.activeSelf)
			return;

		myTransform.Translate(vector * speed * Time.deltaTime);

		if (!CameraRange.CameraRangeCheck(myTransform.position))
		{
			Delete();
		}
	}
}
