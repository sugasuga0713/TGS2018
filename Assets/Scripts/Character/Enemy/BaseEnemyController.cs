using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyController : BaseCharacterController {

	public int power = 1;

	protected override void OnTriggerEnter2D(Collider2D coll)
	{
		base.OnTriggerEnter2D(coll);
		if(coll.tag == "Player")
		{
			coll.transform.parent.GetComponent<PlayerController>().Damage(power);
		}
	}

	protected override void FixedUpdateCharacter()
	{
		if (myTransform.position.y <= -6.0f)
		{
			rb.velocity = Vector2.zero;
			rb.isKinematic = true;
			Vector2 pos = myTransform.position;
			pos.y += 1.0f;
			EffectManager.Instance.PlayEffect("fall",pos,0.5f);
			Destroy(gameObject);
		}
	}

}
