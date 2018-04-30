using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : BaseEnemyController {

	[SerializeField] private Transform[] frontwardTransform = new Transform[2];

	[SerializeField] private LayerMask layerMask = 0;

	protected override void FixedUpdateCharacter()
	{
		if (!grounded)
			return;
		FrontwardCheck();
		InputX(dirX);
	}

	private void FrontwardCheck()
	{
		if (Physics2D.OverlapPoint(frontwardTransform[0].position,layerMask) || !Physics2D.OverlapPoint(frontwardTransform[1].position,layerMask)) //前方に物がある、あるいは前方下に何もないときに反転
		{
			dirX *= -1;
		}
	}

}
