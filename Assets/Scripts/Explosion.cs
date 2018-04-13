using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Dynamite")
		{
			collision.GetComponent<Dynamite>().Explosion();
		}
		else if(collision.tag != "Player")
		{
			collision.gameObject.SetActive(false);
		}
	}

}
