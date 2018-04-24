using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : BaseEnemyController {

	[SerializeField] private GameObject bulletPregab = null;
	[SerializeField] private Transform shotPosition = null;

	[SerializeField] private float shotInterval = 1.0f;
	private float timeCount;

	protected override void FixedUpdateCharacter()
	{
		timeCount += Time.deltaTime;

		if(timeCount >= shotInterval)
		{
			Shot();
			timeCount = 0.0f;
		}
	}

	private void Shot()
	{
		GameObject obj = Instantiate(bulletPregab,shotPosition.position,myTransform.rotation) as GameObject;
		obj.GetComponent<EnemyBullet>().Set(myTransform.localScale.x,0.0f,shotPosition.position);
	}
}
