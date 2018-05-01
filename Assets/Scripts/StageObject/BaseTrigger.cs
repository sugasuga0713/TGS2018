using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTrigger : BaseObject {

	[SerializeField] BaseGimmick gimmick = null;

	protected virtual void TriggerOn()
	{
		gimmick.Event();
	}

	protected virtual void TriggerOff()
	{

	}
}
