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
		public float rangeMin = 0.0f;
		public float rangeMax = 0.0f;
		public float pointMin = 0.0f;
		public float pointMax = 0.0f;
	}
	[SerializeField]
	private MovingRange[] movingRangeX = new MovingRange[0];
	[SerializeField]
	private MovingRange[] movingRangeY = new MovingRange[0];

	[SerializeField] private float baseSmoothSpeed = 5.0f;
	private float smoothSpeed = 5.0f;

	[SerializeField] private int rangeNumX = 0; //現在のカメラのX範囲
	[SerializeField] private int rangeNumY = 0; //現在のカメラのY範囲

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

		if(targetTransform.position.x >= movingRangeX[rangeNumX].pointMax)
		{
			rangeNumX++;
		}
		if(targetTransform.position.x <= movingRangeX[rangeNumX].pointMin)
		{
			rangeNumX--;
			if (rangeNumX < 0)
			{
				rangeNumX = 0;
			}
		}
		if (targetTransform.position.y >= movingRangeY[rangeNumY].pointMax)
		{
			rangeNumY++;
		}
		if (targetTransform.position.y <= movingRangeY[rangeNumY].pointMin)
		{
			rangeNumY--;
			if (rangeNumY < 0)
			{
				rangeNumY = 0;
			}
		}

		pos = Vector2.Lerp(myTransform.position, targetTransform.position + offset, smoothSpeed * Time.deltaTime); //カメラの位置をLerpで補間

		//移動制限処理
		pos.x = Mathf.Clamp(pos.x, movingRangeX[rangeNumX].rangeMin + cameraRangeX, movingRangeX[rangeNumX].rangeMax - cameraRangeX);
		pos.y = Mathf.Clamp(pos.y, movingRangeY[rangeNumY].rangeMin, movingRangeY[rangeNumY].rangeMax);
		 
		myTransform.position = pos;

	}

	public float ReturnMinX()
	{
		return movingRangeX[rangeNumX].rangeMin + 0.8f;
	}

	public float ReturnMaxX()
	{
		return movingRangeX[rangeNumX].rangeMax - 0.8f;
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
