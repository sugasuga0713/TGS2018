using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

	[SerializeField] private Animator animator = null;

	public enum AnimationType
	{
		NORMAL, RUN, ATTACK, DAMAGE, DESTROY, JUMP, UP, DOWN, UP_DIAGONAL, DOWN_DIAGONAL,FALL,LANDING,BACK,
	}

	private AnimationType animationType;

	public void AnimationChange(AnimationType type)
	{
		if (animationType != type)
		{
			animator.Play(type.ToString());
			animationType = type;
		}
	}
}

