using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	[SerializeField] private float power = 5.0f;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Dynamite")
		{
			collision.GetComponent<Dynamite>().Explosion();
		}
		else if(collision.tag == "Stage_Break")
		{
			collision.gameObject.SetActive(false);
		}
		else if(collision.tag == "Player")
		{
			collision.transform.parent.GetComponent<PlayerController>().Damage(power);
		}
	}

}
