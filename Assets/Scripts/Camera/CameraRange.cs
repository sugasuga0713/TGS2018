using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRange : MonoBehaviour {

	static public float top, bottom, left, right; //カメラの端

	static public bool CameraRangeCheck(Vector2 pos)
	{
		bottom = Camera.main.ScreenToWorldPoint(Vector2.zero).y;
		left = Camera.main.ScreenToWorldPoint(Vector2.zero).x;

		top = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).y;
		right = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x;

		if (pos.x > left && pos.x < right && pos.y > bottom && pos.y < top)
			return true;
		else
			return false;
	}
}
