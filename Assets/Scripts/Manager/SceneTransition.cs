using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {

	public enum SceneName
	{
		Title,Main
	}

	[SerializeField] private SceneName sceneName = SceneName.Title;

	public void Transition()
	{
		SceneManager.LoadScene(sceneName.ToString());
	}
}
