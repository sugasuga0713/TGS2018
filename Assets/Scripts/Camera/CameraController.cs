using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : ManagedUpdateBehaviour {

	private Transform myTransform;
	[SerializeField] private Transform targetTransform = null;
	[SerializeField] private Vector3 offset = Vector3.zero;
	private Vector3 pos;

	[System.Serializable]
	class MovingRange
	{
		public Transform movingRangeMin = null;
		public Transform movingRangeMax = null;
	}
	[SerializeField]
	private MovingRange[] movingRange = new MovingRange[0];

	[SerializeField] private float baseSmoothSpeed = 5.0f;
	private float smoothSpeed = 5.0f;

	private int rangeNum = 0; //現在のカメラ範囲

	//private int i; //ループ用

	[SerializeField] private float cameraRangeX = 11;

	protected override void Initialize()
	{
		myTransform = GetComponent<Transform>();
		//offset = myTransform.position - targetTransform.position;
		smoothSpeed = baseSmoothSpeed;
	}

	public override void LateUpdateMe()
	{
		//myTransform.position = targetTransform.position + offset;

		pos = Vector2.Lerp(myTransform.position, targetTransform.position + offset, smoothSpeed * Time.deltaTime); //カメラの位置をLerpで補間

		//移動制限処理
		pos.x = Mathf.Clamp(pos.x,movingRange[rangeNum].movingRangeMin.position.x + cameraRangeX, movingRange[rangeNum].movingRangeMax.position.x - cameraRangeX); 
		pos.y = Mathf.Clamp(pos.y,movingRange[rangeNum].movingRangeMin.position.y,movingRange[rangeNum].movingRangeMax.position.y);

		myTransform.position = pos;

		if(targetTransform.position.x >= movingRange[rangeNum].movingRangeMax.position.x - 1.0f)
		{
			rangeNum++;
		}
	}

	public float ReturnMinX()
	{
		return movingRange[rangeNum].movingRangeMin.position.x + 0.8f;
	}

	public float ReturnMaxX()
	{
		return movingRange[rangeNum].movingRangeMax.position.x - 0.8f;
	}

	public void Smooth()
	{
		smoothSpeed = 5.0f;
		StartCoroutine(CancelSmooth());
	}

	private IEnumerator CancelSmooth()
	{
		yield return new WaitForSeconds(1.0f);
		smoothSpeed = baseSmoothSpeed;
	}

}
