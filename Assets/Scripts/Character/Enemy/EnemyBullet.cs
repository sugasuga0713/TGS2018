using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : BulletBase {

	[SerializeField] private float power = 1.0f;

	public enum BulletType
	{
		NORMAL,FIRE,CUTTER,
	}

	public BulletType bulletType = BulletType.NORMAL;

	protected override void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.tag == "Player")
		{
			coll.transform.parent.GetComponent<PlayerController>().Damage(power);
			Delete();
		}
		else if(coll.tag == "TransferBullet" || coll.tag == "StageObject_static")
		{
			return;
		}
		Delete();
	}

	public override void FixedUpdateMe()
	{
		if (!this.gameObject.activeSelf)
			return;

		if (!Active)
			return;

		myTransform.Translate(vector * speed * Time.deltaTime);

		if (!CameraRange.CameraRangeCheck(myTransform.position))
		{
			Delete();
		}
	}

}
