using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : BaseEnemyController {

	protected override void FixedUpdateCharacter()
	{
		InputX(dirX);
	}
}
