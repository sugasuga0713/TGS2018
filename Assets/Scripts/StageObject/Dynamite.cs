using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : BaseObject {

	private GameObject explosion;
	//private Rigidbody2D rb;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Fire")
		{
			Explosion();
		}
	}

	public void Explosion()
	{
		explosion.transform.position = myTransform.position;
		explosion.SetActive(true);
		explosion.GetComponent<SpriteAnimation>().Play();
		gameObject.SetActive(false);
	}

	protected override void Initialize()
	{
		base.Initialize();
		explosion = myTransform.parent.Find("explosion").gameObject;
		explosion.SetActive(false);
	}
}
