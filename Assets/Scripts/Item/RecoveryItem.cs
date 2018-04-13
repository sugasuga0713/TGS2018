using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryItem : BaseItem {

	[SerializeField] float recoveryPower = 1.0f;

	protected override void Effect(PlayerController playerController)
	{
		playerController.HPUpdate(recoveryPower);
	}
}