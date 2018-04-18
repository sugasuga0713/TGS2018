using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candlestick : BaseTrigger {

	private SpriteRenderer fireSprite;
	private SpriteAnimation spriteAnim;

	protected override void Initialize()
	{
		base.Initialize();
		fireSprite = transform.Find("fire").GetComponent<SpriteRenderer>();
		fireSprite.enabled = false;
		spriteAnim = fireSprite.GetComponent<SpriteAnimation>();
	}

	protected override void TriggerOn()
	{
		base.TriggerOn();
		fireSprite.enabled = true;
		spriteAnim.Loop();
	}

	protected override void TriggerOff()
	{
		base.TriggerOff();
		fireSprite.enabled = false;
		spriteAnim.Stop();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Fire")
		{
			TriggerOn();
		}

	}
}
