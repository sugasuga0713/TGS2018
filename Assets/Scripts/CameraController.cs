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

	private int rangeNum = 0; //現在のカメラ範囲

	private float posX;
	private int i; //ループ用

	[SerializeField] private float cameraRangeX = 11;

	protected override void Initialize()
	{
		myTransform = GetComponent<Transform>();
		//offset = myTransform.position - targetTransform.position;

	/*	Vector3 topLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
		Vector3 bottomRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
		Debug.Log(bottomRight.x - topLeft.x);
		Debug.Log("左端 = " + topLeft.x.ToString() + ", 右端 = " + bottomRight.x);*/
	}

	private void LateUpdate()
	{
		posX = myTransform.position.x;

		myTransform.position = targetTransform.position + offset;
	
		//移動制限処理
		pos = myTransform.position;
		pos.x = Mathf.Clamp(pos.x,movingRange[rangeNum].movingRangeMin.position.x + cameraRangeX, movingRange[rangeNum].movingRangeMax.position.x - cameraRangeX);
		pos.y = Mathf.Clamp(pos.y,movingRange[rangeNum].movingRangeMin.position.y,movingRange[rangeNum].movingRangeMax.position.y);
		myTransform.position = pos;
	}

	public float ReturnMinX()
	{
		return movingRange[rangeNum].movingRangeMin.position.x + 0.8f;
	}

	public float ReturnMaxX()
	{
		return movingRange[rangeNum].movingRangeMax.position.x - 0.8f;
	}

}
