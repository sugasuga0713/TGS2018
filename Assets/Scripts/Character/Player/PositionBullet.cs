using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionBullet : PlayerBullet {

	private Vector2 collSize; //ヒットしたコライダーのサイズ
	private Vector2 hitCollPosition; //ヒットしたコライダーを持つオブジェクトの位置
	private Vector2 hitCollPositionTop;
	private Vector2 hitCollPositionButtom;

	private float halfSizeX,halfSizeY; //コライダーの半分のサイズ
	private float bulletX, bulletY; //弾の位置
	private float collX, collY; //コライダーオブジェクトの位置

	[SerializeField] private Vector2 boxPoint = Vector2.zero;
	[SerializeField] private float boxSize = 0.1f;
	private int vec;
	private float sidePos;
	private bool topPosition; //弾を撃った位置が弾に当たったものより上かどうか

	[SerializeField] private LayerMask layerMask = 0;

	protected override void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "Player")
			return;

		Hit(coll);

		Delete();
	}

	private void Hit(Collider2D coll)
	{
		collSize = coll.bounds.size; //コライダーのサイズ
		hitCollPosition = coll.transform.position; //ヒットしたオブジェクトの位置

		hitCollPositionTop = hitCollPosition + boxPoint;
		hitCollPositionTop.y += halfSizeY;

		hitCollPositionButtom = hitCollPosition - boxPoint;
		hitCollPositionButtom.y -= halfSizeY;

		halfSizeX = collSize.x * 0.5f; //コライダーのXサイズの半分
		halfSizeY = collSize.y * 0.5f; //コライダーのXサイズの半分
		bulletX = myTransform.position.x; //弾のX位置
		bulletY = myTransform.position.y; //弾のY位置
		collX = coll.transform.position.x; //コライダーオブジェクトのX位置
		collY = coll.transform.position.y; //コライダーオブジェクトのY位置

		topPosition = (startY > coll.transform.position.y) ? true : false;

		if(bulletY >= collY  + halfSizeY - 0.3f && topPosition && !(Physics2D.OverlapBox(hitCollPositionTop, collSize * boxSize, coll.transform.eulerAngles.z,layerMask)))
		{
			//Debug.Log("上にヒット");
			vec = 0;
			sidePos = collY + halfSizeY;
			hitCollPosition.y += halfSizeY;
		}else if(bulletY <= collY - halfSizeY + 0.3f && !topPosition && !(Physics2D.OverlapBox(hitCollPositionButtom, collSize * boxSize, coll.transform.eulerAngles.z,layerMask)))
		{

			//Debug.Log("下にヒット");
			vec = 1;
			sidePos = collY - halfSizeY;
			hitCollPosition.y -= halfSizeY;
		}
		else if(bulletX >= collX + halfSizeX - 0.5f)
		{
			Debug.Log(bulletY + " > " + (collY + halfSizeY - 0.3f));
			Debug.Log("TopPosition = " + topPosition);
			//Debug.Log("Physics2D.OverlapBox = " + Physics2D.OverlapBox(hitCollPositionButtom, collSize * boxSize, coll.transform.eulerAngles.z, layerMask));
			Debug.Log("右にヒット");
			vec = 2;
			sidePos = collX + halfSizeX;
			hitCollPosition.x += halfSizeX;
		}
		else
		{
			Debug.Log("左にヒット");
			vec = 3;
			sidePos = collX - halfSizeX;
			hitCollPosition.x -= halfSizeX;
		}

		shotManager.TransferSet(myTransform, coll.transform, coll, hitCollPosition,vec,sidePos);
	}

	private void SideCheck()
	{

	}
}

/*ワープホールの位置補正メモ
 * 
 * ワープホールに関して、単純にぶつかった位置に生成するのではワープさせた物体が壁にめりこむため補正をかける
 * 具体的なやり方として、PositionBulletがヒットした物体のどこに当たったかを調べ、そこから物体のコライダーサイズ分ずらした位置にワープホールを生成する
 * 例えば、物体の上辺に当たったとして
 * PositionBulletが当たった物体の位置とColliderのサイズを調べる
 **/

