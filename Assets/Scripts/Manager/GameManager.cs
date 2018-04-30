using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : ManagedUpdateBehaviour {

	[SerializeField] private PlayerController playerController = null;

	public override void UpdateMe()
	{
		if(playerController.myTransform.position.x>= 42)
		{
			SceneManager.LoadScene("Main");
		}
	}
}
