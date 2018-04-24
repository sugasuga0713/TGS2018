using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : BaseTrigger {

	[SerializeField] private SpriteRenderer buttonSprite = null;
	[SerializeField] private SpriteAnimation spriteAnim = null;

	[SerializeField] private Vector3 leftButtom = Vector2.zero;
	[SerializeField] private Vector2 rightTop = Vector2.zero;
	private Collider2D coll;
	private bool triggerFlag = false;

	public override void FixedUpdateMe()
	{
		coll = Physics2D.OverlapBox(myTransform.position + leftButtom, rightTop, 0.0f);

		if(triggerFlag == coll)
		{
			return;
		}
		triggerFlag = coll;

		if(coll != null)
		{
			TriggerOn();
		}
		else
		{
			TriggerOff();
		}
	}

	protected override void TriggerOn()
	{
		buttonSprite.enabled = false;
		spriteAnim.Play();
	}

	protected override void TriggerOff()
	{
		buttonSprite.enabled = true;
	}
}
