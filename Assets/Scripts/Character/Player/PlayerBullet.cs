using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : BulletBase {

	[System.NonSerialized] public PlayerShotManager shotManager;
	[System.NonSerialized] public float startY; //コライダーとの接触位置と弾の初期位置を比べるために使う

	public void Set(Vector3 pos,Quaternion ro) //dirはキャラの向きを表す 
	{
		this.gameObject.SetActive(true); //このゲームオブジェクトを表示
		lifeTime = defaultLifeTime;
		myTransform.position = pos; //位置を設定
									/*	RotationSet(dirX, dirY);*/
		myTransform.localRotation = ro;
		startY = myTransform.position.y;
	}

	protected override void OnTriggerEnter2D(Collider2D coll)
	{

	}

	protected override void Initialize()
	{
		base.Initialize();
		shotManager = transform.parent.parent.GetComponent<PlayerShotManager>();
	}


}
