using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotRay : ManagedUpdateBehaviour {

	private new LineRenderer renderer;
	[SerializeField] private Transform aimingMarkTransform;
	[SerializeField] private Transform bulletPositionTransform;

	protected override void Initialize()
	{
		renderer = GetComponent<LineRenderer>();
		// 線の幅
		renderer.SetWidth(0.01f, 0.01f);
		// 頂点の数
		renderer.SetVertexCount(2);
		// 頂点を設定
		renderer.SetPosition(0, Vector3.zero);
		renderer.SetPosition(1, new Vector3(1f, 1f, 0f));
	}

	public override void UpdateMe()
	{
		renderer.SetPosition(0,bulletPositionTransform.position);
		renderer.SetPosition(1,aimingMarkTransform.position);
	}
}
