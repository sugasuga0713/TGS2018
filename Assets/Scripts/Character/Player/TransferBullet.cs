﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferBullet : PlayerBullet {

	protected override void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "Stage" || coll.tag == "Stage_Break" || coll.tag == "StageObject_static")
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
		shotManager.Transfer(coll.transform.parent, coll, coll.bounds.size,true);
	}
}
