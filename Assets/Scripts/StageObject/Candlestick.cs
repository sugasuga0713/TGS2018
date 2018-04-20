using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candlestick : BaseTrigger {

	private SpriteRenderer fireSprite;
	private SpriteAnimation spriteAnim;
	[SerializeField] private bool fire = true;

	protected override void Initialize()
	{
		base.Initialize();
		fireSprite = transform.Find("fire").GetComponent<SpriteRenderer>();
		spriteAnim = fireSprite.GetComponent<SpriteAnimation>();
		if (fire)
		{
			Light();
		}
		else
		{
			PutOut();
		}
	}

	protected override void TriggerOn()
	{
		base.TriggerOn();
		Light();
	}

	protected override void TriggerOff()
	{
		base.TriggerOff();
		PutOut();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Fire" && !fire)
		{
			TriggerOn();
		}

	}

	private void Light() //火をつける処理
	{
		fire = true;
		fireSprite.enabled = true;
		spriteAnim.Play();
	}

	private void PutOut() //火を消す処理
	{
		fire = false;
		fireSprite.enabled = false;
		spriteAnim.Stop();
	}

}
