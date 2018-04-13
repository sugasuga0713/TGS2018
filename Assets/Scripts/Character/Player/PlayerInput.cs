using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : ManagedUpdateBehaviour {

	private PlayerController playerController;
	private int type = 0;

	private float InputInterval = 0;

	public override void UpdateMe()
	{
		playerController.InputX(Input.GetAxisRaw("Horizontal"));

		/*distance = playerTransform.position.y - aimingMarkTransform.transform.position.y;
		distance = Mathf.Clamp(distance, -5, 5);
		playerController.InputY(distance/-5);*/
		//           or  プレイヤーの縦の向き
		//playerController.InputY((int)(Input.GetAxisRaw("Vertical")));

		if (Input.GetButtonDown("Jump"))
		{
			if (InputInterval > 0.2f)
			{
				playerController.Jump();
				InputInterval = 0.0f;
			}
		}

		if (Input.GetButtonDown("Shot"))
		{
			//type = (playerController.GetBulletStats()) ? 1 : 0;
			playerController.Shot(type);
		}

		/*if (Input.GetButtonDown("Shot1"))
		{
			Debug.Log("Shot1");
			playerController.Shot(0);
		}
		if (Input.GetButtonDown("Shot2"))
		{
			playerController.Shot(1);
			Debug.Log("Shot2");
		}*/

		if (Input.GetMouseButtonDown(0))
		{
			playerController.Shot(0);
		}
		if (Input.GetMouseButtonDown(1))
		{
			playerController.Shot(1);
		}

		if (Input.GetKeyDown(KeyCode.Joystick6Button14))
		{
			playerController.Shot(0);
		}

		if (Input.GetKeyDown(KeyCode.Joystick7Button14))
		{
			playerController.Shot(1);
		}

		if (Input.GetButtonDown("ShotCancel"))
		{
			type = 0;
		}
		if (Input.GetButtonDown("ShotChange"))
		{
			type = (type == 1) ? 0 : 1;
		}
		InputInterval += Time.deltaTime;
	}

	protected override void Initialize()
	{
		playerController = GetComponent<PlayerController>();
	}
}
