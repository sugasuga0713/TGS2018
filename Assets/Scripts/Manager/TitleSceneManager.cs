using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneManager : MonoBehaviour {

	[SerializeField] private SceneTransition sceneMangager = null;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			sceneMangager.Transition();
		}
	}
}
