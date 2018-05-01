using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : BaseObject {

	[SerializeField] private EnemyBullet enemyBullet;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "EnemyAttack")
		{
			enemyBullet = collision.GetComponent<EnemyBullet>();
			if(enemyBullet.bulletType == EnemyBullet.BulletType.CUTTER)
			{
				//Destroy(gameObject);
				gameObject.SetActive(false);
			}
		}
	}
}
