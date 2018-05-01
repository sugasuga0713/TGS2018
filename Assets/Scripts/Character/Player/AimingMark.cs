using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingMark : ManagedUpdateBehaviour {

	//[SerializeField] private float speed = 5.0f;

	private Transform myTransform;

	[SerializeField] private GameObject[] spriteObjs = null;
	[SerializeField] private float magnification = 1.3f; //拡大する際の倍率
	private bool near = false;
	private bool previousFlag = false; //直前のフラグ

	private Vector2 startScale;
	private Vector2 nearScale;

	public override void FixedUpdateMe()
	{
		Move();
	}

	public override void UpdateMe()
	{
		if (near == previousFlag)
			return;

		if (near)
		{
			myTransform.localScale = nearScale;
		}
		else
		{
			myTransform.localScale = startScale;
		}
		previousFlag = near;
	}

	private void Move()
	{
		//myTransform.Translate(movement * speed * Time.deltaTime);

		Vector3 cameraPosition = Input.mousePosition;
		cameraPosition.z = 10.0f;
		Vector2 pos = Camera.main.ScreenToWorldPoint(cameraPosition);

		myTransform.position = pos;
	}

	protected override void Initialize()
	{
		myTransform = GetComponent<Transform>();
		startScale = myTransform.localScale;
		nearScale = new Vector2(startScale.x * magnification,startScale.y * magnification);
	}

	public void DistanceCheck(Vector2 pos)
	{
		float distance = Vector2.Distance(myTransform.position,pos);
		if(distance <= 1.5f)
		{
			near = true;
		}
		else
		{
			near = false;
		}
	}

	private void ObjectChange(int trueNum,int falseNum)
	{
		spriteObjs[trueNum].SetActive(true);
		spriteObjs[falseNum].SetActive(false);
	}

	public bool GetNearFlag()
	{
		return near;
	}

	public Vector2 GetPosition()
	{
		return myTransform.position;
	}
}
