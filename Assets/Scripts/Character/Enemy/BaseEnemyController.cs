using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyController : BaseCharacterController {

	public int power = 1;

	public virtual void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.tag == "Player")
		{
			coll.transform.parent.GetComponent<PlayerController>().Damage(power);
		}
	}

}
