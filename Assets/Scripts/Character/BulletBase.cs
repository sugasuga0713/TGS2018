using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : BaseObject {

	//-----------------------------------------------
	public float speed = 5.0f;//現在のスピード
	public float defaultLifeTime = 1.5f;
	[System.NonSerialized] public float lifeTime;

	public Vector2 vector = new Vector2(1, 0);
	//-----------------------------------------------

	public virtual void Set(float dirX, float dirY, Vector3 pos) //dirはキャラの向きを表す 
	{
		this.gameObject.SetActive(true); //このゲームオブジェクトを表示
		lifeTime = defaultLifeTime;
		myTransform.position = pos; //位置を設定
		RotationSet(dirX,dirY);
	}

	public override void UpdateMe()
	{
		if (!this.gameObject.activeSelf)
			return;

		lifeTime -= Time.deltaTime;
		if (lifeTime <= 0)
		{
			Delete();
		}

	}

	public override void FixedUpdateMe()
	{
		if (!this.gameObject.activeSelf)
			return;
		myTransform.Translate(Vector2.up * speed * Time.deltaTime);

		if (!CameraRange.CameraRangeCheck(myTransform.position))
		{
			Delete();
		}
	}

	protected virtual void RotationSet(float dirX,float dirY)
	{
		myTransform.eulerAngles = new Vector3(0, (dirX == 1) ? 0 : 180, dirY * 90);

	}

	protected virtual void OnTriggerEnter2D(Collider2D coll)
	{
		Delete();
	}

	protected virtual void Delete()
	{
		this.gameObject.SetActive(false);
	}

}
