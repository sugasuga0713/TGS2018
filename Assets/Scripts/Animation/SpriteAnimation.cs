using System;
using System.Collections;
using UnityEngine;
using UniRx;

public class SpriteAnimation : MonoBehaviour
{
	public float fps = 24.0f;   // FPSを設定
	public Sprite[] frames;     // スプライトのリストを設定
	public bool playAwake = false;
	public bool enabledEnd;
	public bool changeColl = false;
	new SpriteRenderer renderer;

	int frameIndex;

	public new BoxCollider2D collider;

	[System.Serializable]
	class AnimationData
	{
		public Sprite sprite = null;
		public Vector2 size = Vector2.zero;
	}
	[SerializeField]
	private AnimationData[] animFrames = new AnimationData[0];

	IDisposable disposable; // 途中停止用

	public enum DefaultAction
	{ // OnEnable時の動作指定
		None,
		Play,
		Loop
	}

	[SerializeField]
	DefaultAction defaultAction = DefaultAction.None;


	private void Awake()
	{
		if (playAwake)
		{
			Play();
		}
	}

	void OnStart()
	{
		switch (defaultAction)
		{
			case DefaultAction.Play:
				Play();
				break;
			case DefaultAction.Loop:
				Loop();
				break;
		}
	}

	/**
     * アニメーション初期化
     */
	void Init()
	{
		Stop();

		renderer = GetComponentInChildren<SpriteRenderer>();
		renderer.sprite = null;
		frameIndex = 0;

	}

	/**
     * 一回だけアニメーション実行
     */
	/*public void Play()
	{
		Init();

		disposable = Observable.Interval(System.TimeSpan.FromMilliseconds(1000 / fps))
			.Take(frames.Length + 1)
			.Subscribe(_ => {
				if (frames.Length <= frameIndex)
				{
					if (enabledEnd)
					{
						renderer.sprite = null;
					}
					frameIndex = 0;
					OnAnimationFinish();
				}
				else
				{
					renderer.sprite = frames[frameIndex];
					frameIndex++;
				}
			});
	}*/
	public void Play()
	{
		if (changeColl)
		{
			Play2();
		}
		else
		{
			Play1();
		}
	}

	private void Play1()
	{
		Init();

		disposable = Observable.Interval(System.TimeSpan.FromMilliseconds(1000 / fps))
			.Take(frames.Length + 1)
			.Subscribe(_ => {
				if (frames.Length <= frameIndex)
				{
					if (enabledEnd)
					{
						renderer.sprite = null;
					}
					frameIndex = 0;
					OnAnimationFinish();
				}
				else
				{
					renderer.sprite = frames[frameIndex];
					frameIndex++;
				}
			});
	}

	private void Play2()
	{
		Init();

		disposable = Observable.Interval(System.TimeSpan.FromMilliseconds(1000 / fps))
			.Take(animFrames.Length + 1)
			.Subscribe(_ =>
			{
				if (animFrames.Length <= frameIndex)
				{
					if (enabledEnd)
					{
						renderer.sprite = null;
					}
					frameIndex = 0;
					OnAnimationFinish();
				}
				else
				{
					renderer.sprite = animFrames[frameIndex].sprite;
					collider.size = animFrames[frameIndex].size;
					frameIndex++;
				}
			});
	}

	// アニメーション終了時に呼び出す関数
	protected virtual void OnAnimationFinish()
	{
	}

	/**
     * ループ実行
     */
	public void Loop()
	{
		Init();

		disposable = Observable.Interval(System.TimeSpan.FromMilliseconds(1000 / fps))
			.Take(frames.Length)
			.RepeatUntilDisable(this)
			.Subscribe(_ => {
				if (frames.Length <= frameIndex)
				{
					renderer.sprite = null;
					frameIndex = 0;
				}
				renderer.sprite = frames[frameIndex];
				frameIndex++;
			});
	}

	/**
     * 停止
     */
	public void Stop()
	{
		if (disposable != null)
		{
			disposable.Dispose();
		}
		disposable = null;
	}
}