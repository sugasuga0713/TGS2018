using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterController : BaseObject {

	//------------------キャラクター情報---------------------

	public float hpMax = 10.0f;//最大HP
	public float hp = 10.0f;//現在のHP
	public float speed_Default = 5.0f;//基本スピード
	public float speed = 5.0f;//現在のスピード
	public float jumpPower = 5.0f;//ジャンプ力
	public float dirX = 1;//プレイヤーの向き
	public float moveX = 0.0f;//移動量
	public float fallSpeedLimit = -5.0f;

	[System.NonSerialized] public bool grounded = false;//接地しているか

	//-------------------------------------------------------

	[System.NonSerialized] public Vector3 startScale; //初期サイズ 反転に使用

	public AnimationController animationController; //
		
	public override void FixedUpdateMe()
	{
		//キャラクターの個別処理
		FixedUpdateCharacter();

		Move();
	}

	protected virtual void FixedUpdateCharacter(){

	}

	protected virtual void Move()
	{
		//移動処理
		//myTransform.Translate(speed * moveX * Time.deltaTime, 0, 0);
		rb.velocity = new Vector2(speed * moveX * Time.deltaTime, rb.velocity.y);
		if (rb.velocity.y <= fallSpeedLimit)
		{
			rb.velocity = new Vector2(rb.velocity.x, fallSpeedLimit);
		}
	}

	public virtual void InputX(float x){// int xには移動量が入る
		if (x < 0)
			dirX = -1;
		else if (x > 0)
			dirX = 1;
		myTransform.localScale = new Vector3(dirX * startScale.x,
			startScale.y, 1f);
		moveX = x;
	}

	public virtual void Jump()
	{
		rb.velocity = new Vector2(rb.velocity.x, 0);
		rb.AddForce(Vector2.up * Time.deltaTime * jumpPower,ForceMode2D.Impulse);
	}

	public virtual void Damage(float d)
	{
		HPUpdate(-d);
		//ダメージアニメーション等の処理
	}

	public virtual void HPUpdate(float value)
	{
		hp += value;
		if(hp>= hpMax)
		{
			hp = hpMax;
		}
		if (hp <= 0f)
		{
			hp = 0f;
			DestroyAction();
		}
	}

	protected virtual void DestroyAction(){
		//死亡時の処理
	}

	protected override void Initialize()
	{
		base.Initialize();
		startScale = transform.localScale; //初期サイズを取得
		rb = GetComponent<Rigidbody2D>(); //Rigidbodyを取得
		dirX = (transform.localScale.x > 0.0f) ? 1 : -1; //キャラが右向きのときは画像サイズを1、左向きのときは-1(反転)にする
		/*transform.localScale = new Vector3(dirX * startScale.x,
			startScale.y, 1f);*/
	}

	protected virtual void OnTriggerEnter2D(Collider2D collision)
	{
		Rigidbody2D collRb = collision.GetComponent<Rigidbody2D>();
		if (collRb)
		{
			if(collRb.velocity.y <= -3.0f)
			{
				Damage(3.0f);
			}
		}
	}

}
