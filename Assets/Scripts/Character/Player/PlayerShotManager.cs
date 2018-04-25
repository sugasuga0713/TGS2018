using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerShotManager : ManagedUpdateBehaviour
{
	//---------------------------------------------------------------------------

	[SerializeField] private PlayerBullet[] bulletScripts = null; //弾のスクリプト
	[SerializeField] private PlayerController playerController = null; //Playerスクリプト
	private GameObject[] bullets = null; //弾のオブジェクト
	[SerializeField] private Transform bulletPosition = null; //弾の生成位置

	[SerializeField] private float shotIntervalMax = 0.5f; //弾を撃つ間隔
	private float shotInterval = 0; //弾を撃ってからの秒数

	//---------------------------------------------------------------------------

	[SerializeField] private Transform[] warpHoleTransform = null; //ワープホールのTransform
	[System.NonSerialized] public GameObject[] warpHole = new GameObject[2]; //ワープホールのオブジェクト
	[SerializeField] private SpriteAnimation[] warpHoleAnim = null;
	[SerializeField] private SpriteAnimation[] muzzleFlashAnim = null;
	private BaseObject objectScript;
	private Collider2D warpCollider;
	private Vector3 warpPosition; //ワープするポジション
	public int setVector; //ワープホールの向きを設定するためのベクトル Vector2型のvectorに対応する 0で左、1で右、2で上、3で下
	public float sidePosition; //ワープホールが当たったコライダーの端の位置　ワープ時の位置補正に使用

	//---------------------------------------------------------------------------
	//float damage; //移動時のダメージ計算用
	[SerializeField] private float damage = 0.25f;
	int i;

	public void Shot(int num,float angle)
	{
		if (shotInterval < shotIntervalMax)
			return;

		shotInterval = 0;
		for (i = num * 3; i < (num * 3) + 3; i++)
		{
			if (!bullets[i].activeSelf)
			{
				Quaternion ro = Quaternion.Euler(0.0f, 0.0f, angle);
				bulletScripts[i].Set(bulletPosition.position,ro);
				muzzleFlashAnim[num].Play();
				return;
			}
		}
	}

	public override void UpdateMe()
	{
		shotInterval += Time.deltaTime;
	}

	//-------------------ワープホールセット-----------------------------------------------------------------------------------------------------------------

	public void TransferSet(Transform bulletTransform, Transform collTransform, Collider2D collider , Vector2 warpHolePos,int vector, float sidePos)
	{
		setVector = vector; //弾の向きを取得
		sidePosition = sidePos;
		warpCollider = collider;

		warpHoleTransform[0].position = warpHolePos; //ワープホールの位置を弾の衝突位置に変更

		warpHole[0].SetActive(true);
		warpHoleAnim[0].Play();
		warpHoleTransform[0].parent = collTransform;
	}

	//-------------------ワープ処理------------------------------------------------------------------------------------------------------------------------

	public void Transfer(Transform collTransform,Collider2D collider,Vector2 collSize,bool type) //typeがtrueのときに他の物、falseのときはプレイヤーを移動
	{
		if (!warpHole[0].activeSelf)
			return;
		if (collider == warpCollider)
			return;

		warpHoleTransform[1].position = collTransform.localPosition;
		warpHoleAnim[1].Play();

		if (type) //移動するものがプレイヤー以外のとき
		{
			//移動等の無効化処理
		}
		else //プレイヤーを移動するとき
		{
		}

		objectScript = collTransform.GetComponent<BaseObject>();
		if (objectScript == null)
		{
			objectScript = collTransform.parent.GetComponent<BaseObject>();
		}
		objectScript.Pause();

		StartCoroutine(TransferCoroutine(collTransform, collSize, type));
	}

	private IEnumerator TransferCoroutine(Transform collTransform, Vector2 collSize, bool type)
	{
		yield return new WaitForSeconds(0.5f);
		collTransform.gameObject.SetActive(false);

		yield return new WaitForSeconds(0.5f);
		collTransform.gameObject.SetActive(true);

		warpPosition = warpHoleTransform[0].position;
		Vector2 pos = warpPosition;
		switch (setVector)
		{
			case 0:
				pos.y = sidePosition + collSize.y * 0.5f;
				break;
			case 1:
				pos.y = sidePosition - collSize.y * 0.5f;
				break;
			case 2:
				pos.x = sidePosition + collSize.x * 0.5f;
				break;
			case 3:
				pos.x = sidePosition - collSize.x * 0.5f;
				break;
		}

		//playerController.ShotDamage(ReturnDamege(pos, collTransform.localPosition)); //移動時のダメージ計算
		if (type)
		{
			playerController.ShotDamage(damage);
		}
		else
		{
			playerController.ShotDamage(damage * 4.0f);
		}

		objectScript.CancelPause();
		collTransform.localPosition = pos; //移動
	}

	protected override void Initialize() //初期化
	{
		Array.Resize(ref bullets, bulletScripts.Length);
		for (i = 0; i < bulletScripts.Length; i++)
		{
			bullets[i] = bulletScripts[i].gameObject;
		}
		warpHole[0] = warpHoleTransform[0].gameObject;
		warpHole[1] = warpHoleTransform[1].gameObject;

	}

	public Vector3 GetShotPosition() 
	{
		return bulletPosition.position;
	}

	private float ReturnDamege(Vector2 warpPosition,Vector2 targetPosition) 
	{
		damage = Vector2.Distance(warpPosition,targetPosition);
		damage *= 0.05f;
		//Debug.Log(damage);
		return damage;
	}

	public override void FixedUpdateMe()
	{
		if (!CameraRange.CameraRangeCheck(warpHoleTransform[0].position))
		{
			warpHole[0].SetActive(false);
		}
	}
}
