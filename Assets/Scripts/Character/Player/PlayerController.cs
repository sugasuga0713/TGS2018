using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseCharacterController {

	private float shotAngle;

	private bool hasJumped = false; //ジャンプ直後　//ジャンプ直後に接地判定をとらないために使用する
	private bool hasLanded = false; //着地直後
	private bool hasDamaged = false; //ダメージを受けたとき

	//[System.NonSerialized] public Rigidbody2D rb; //RigidBody2Dのキャッシュ

	[SerializeField] private PlayerShotManager shotManager = null;
	[SerializeField] private HPManager hpManager = null;
	[SerializeField] private CameraController cameraController = null;

	[SerializeField] private Transform aimingMarkTransform = null;
	[SerializeField] private Transform bulletPositionBase = null;

	[SerializeField] private Collider2D myCollider = null;

	[SerializeField] private SpriteRenderer sprite = null;

	private AimingMark aimingMark;

	private Vector3 dustPosition;
	[SerializeField] private float dustPosY = 0.5f;

	[SerializeField] private float bottomLine = -8.0f;
	[SerializeField] private float cameraRangeAdjustmentX = 0.5f;
	private Vector3 respawnPosition;
	private Vector3 pos;

	private int i,j;

	public override void Jump()
	{
		if (!Active)
			return;

		if (grounded)
		{
			if (hasJumped)
				return;

			hasJumped = true;
			//rb.velocity = new Vector2(rb.velocity.x,0);
			rb.velocity = Vector2.zero;
			rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
			StartCoroutine(JumpFlagOFF(0.2f));

		}
	}

	protected override void Move()
	{
		if(!Active)
			return;

		//移動処理
		if(dirX != moveX)
		{
			rb.velocity = new Vector2(speed * moveX * Time.deltaTime * 0.8f, rb.velocity.y);
		}
		else
		{
			rb.velocity = new Vector2(speed * moveX * Time.deltaTime, rb.velocity.y);
		}

		//------キャラクターが画面外に出ないようにする処理--------------
		pos = myTransform.position;
		pos.x = Mathf.Clamp(pos.x, cameraController.ReturnMinX() - cameraRangeAdjustmentX, cameraController.ReturnMaxX() + cameraRangeAdjustmentX);
		myTransform.position = pos;
		//--------------------------------------------------------------

		if (rb.velocity.y <= fallSpeedLimit)
		{
			rb.velocity = new Vector2(rb.velocity.x, fallSpeedLimit);
		}

		cameraController.LateUpdateMe();
	}

	protected override void FixedUpdateCharacter()
	{
		dirX = (myTransform.position.x - aimingMarkTransform.position.x < 0) ? 1 : -1;
		myTransform.localScale = new Vector3(dirX * startScale.x,
			startScale.y, 1f);
		aimingMark.DistanceCheck(myTransform.position);
		shotAngle = ShotAngle();
		//AngleYCheck();
		AnimationChange();

		//------ステージ外の落下確認処理--------------------------------
		if(myTransform.position.y <= bottomLine)
		{
			Damage(2.0f);
			myTransform.position = respawnPosition;
		}

	}

	public override void InputX(float x)
	{
		moveX = x;
	}

	public void Shot(int num)
	{
		if (!Active)
			return;

		if (num == 1 && aimingMark.GetNearFlag())
		{
			shotManager.Transfer(myTransform, myCollider, myCollider.bounds.size,false);
			cameraController.Smooth();
			return;
		}

		shotManager.Shot(num,shotAngle);
	}

	private float ShotAngle()
	{
		var vec = (aimingMarkTransform.position - shotManager.GetShotPosition()).normalized;
		var angle = (Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg) - 90.0f;
		return angle;
	}

	public IEnumerator JumpFlagOFF(float delayTime)
	{
		yield return new WaitForSeconds(delayTime);
		hasJumped = false;
	}	

	public IEnumerator LandingFlagOFF(float delayTime)
	{
		yield return new WaitForSeconds(delayTime);
		hasLanded = false;
	}

	public IEnumerator DamageFlagOFF(float delayTime)
	{
		for (j = 0; j < 20; j++)
		{
			if(j%2 == 0)
			{
				sprite.enabled = false;
			}
			else
			{
				sprite.enabled = true;
			}
			yield return new WaitForSeconds(0.025f);
		}

		hasDamaged = false;
	}

	protected override void Initialize()
	{
		base.Initialize();
		aimingMark = aimingMarkTransform.GetComponent<AimingMark>();
		respawnPosition = myTransform.position;
	}

	protected override void Landing()
	{
		if (rb.velocity.y > 0)
		{
			return;
		}
		grounded = true;
		hasLanded = true;
		animationController.AnimationChange(AnimationController.AnimationType.LANDING);
		StartCoroutine(LandingFlagOFF(0.1f));
		dustPosition = groundCheck[1].position;
		dustPosition.y += dustPosY;
		EffectManager.Instance.PlayEffect("smoke", dustPosition, 0.5f);
	}

	private void AngleYCheck()
	{
		var vec = (aimingMarkTransform.position - bulletPositionBase.position).normalized;
		var angle = (Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg) - 90.0f;
		if (dirX == 1)
		{
			if (angle >= -20) //up
			{
				animationController.AnimationChange(AnimationController.AnimationType.UP);
			}
			else if(angle >= -60)
			{
				animationController.AnimationChange(AnimationController.AnimationType.UP_DIAGONAL);
			}
			else if(angle >= -120)
			{
				animationController.AnimationChange(AnimationController.AnimationType.NORMAL);
			}
			else if (angle >= -160) //right
			{
				animationController.AnimationChange(AnimationController.AnimationType.DOWN_DIAGONAL);
			}
			else //down
			{
				animationController.AnimationChange(AnimationController.AnimationType.DOWN);
			}
		}
		else
		{
			if (angle <= 20 && angle >= 0) //up
			{
				animationController.AnimationChange(AnimationController.AnimationType.UP);
			}
			else if (angle <= 60 && angle >= 0)
			{
				animationController.AnimationChange(AnimationController.AnimationType.UP_DIAGONAL);
			}
			else if ((angle <= 90 && angle >= 0) || (angle >= -270 && angle <= -240))
			{
				animationController.AnimationChange(AnimationController.AnimationType.NORMAL);
			}
			else if (angle <= -200) //right
			{
				animationController.AnimationChange(AnimationController.AnimationType.DOWN_DIAGONAL);
			}
			else //down
			{
				animationController.AnimationChange(AnimationController.AnimationType.DOWN);
			}
		}
	}

	public override void HPUpdate(float value)
	{
		hp += value;

		if (hp >= hpMax)
		{
			hp = hpMax;
		}

		if (hp <= 0f)
		{
			hp = 0f;
			DestroyAction();
		}
		hpManager.HPUpdate(hp, hpMax);

	}

	public override void Damage(float d)
	{
		if (hasDamaged)
			return;

		HPUpdate(-d);
		//ダメージアニメーション等の処理
		hasDamaged = true;
		StartCoroutine(DamageFlagOFF(1.0f));
	}

	public void ShotDamage(float value)
	{
		HPUpdate(-value);
		//ダメージアニメーション等の処理
	}

	private void AnimationChange()
	{
		if (hasLanded)
		{
			return;
		}

		if (grounded && !hasJumped)
		{
			if (moveX != 0)
			{
				if (dirX != moveX)
				{
					animationController.AnimationChange(AnimationController.AnimationType.BACK);
				}
				else
				{
					animationController.AnimationChange(AnimationController.AnimationType.RUN);
				}
			}
			else
			{
				animationController.AnimationChange(AnimationController.AnimationType.NORMAL);
			}
		}
		else
		{
		//	if(rb.velocity.y >= 0) //上昇アニメーション
		//	{
		//		animationController.AnimationChange(AnimationController.AnimationType.JUMP);
		//	}
		//	else //下降アニメーション
		//	{
				animationController.AnimationChange(AnimationController.AnimationType.FALL);
		//	}
		}
	}

}
