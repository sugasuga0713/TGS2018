using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour {

	private GameObject explosion;
	//private Rigidbody2D rb;

	private void Awake()
	{
	//	rb = transform.parent.GetComponent<Rigidbody2D>();
		explosion = transform.parent.Find("explosion").gameObject;
		explosion.SetActive(false);
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Fire")
		{
			Explosion();
		}
	}

	public void Explosion()
	{
		explosion.transform.position = transform.position;
		explosion.SetActive(true);
		explosion.GetComponent<SpriteAnimation>().Play();
		gameObject.SetActive(false);
	}
}
