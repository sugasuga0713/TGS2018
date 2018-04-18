using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferBullet : PlayerBullet {

	protected override void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "Stage")
		{
			Delete();
			return;
		}
		else if(coll.tag == "Player")
		{
			return;
		}
		else
		{
			Hit(coll);
		}
		Delete();
	}

	private void Hit(Collider2D coll)
	{
		shotManager.Transfer(coll.transform, coll, coll.bounds.size,true);
	}
}
