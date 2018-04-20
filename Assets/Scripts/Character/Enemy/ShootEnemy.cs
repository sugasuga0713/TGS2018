using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : BaseEnemyController {

	[SerializeField] private GameObject bulletPregab;
	[SerializeField] private Transform shotPosition;

	[SerializeField] private float shotInterval = 1.0f;
	private float timeCount;

	protected override void FixedUpdateCharacter()
	{
		timeCount += Time.deltaTime;

		if(timeCount >= shotInterval)
		{
			Shot();
		}
	}

	private void Shot()
	{
		Instantiate(bulletPregab,shotPosition.position,myTransform.rotation);
	}
}
