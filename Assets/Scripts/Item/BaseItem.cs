using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : BaseObject {

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player")
		{
			Effect(collision.transform.parent.GetComponent<PlayerController>());
			gameObject.SetActive(false);
		}
	}

	protected virtual void Effect(PlayerController playerController)
	{

	}
}
