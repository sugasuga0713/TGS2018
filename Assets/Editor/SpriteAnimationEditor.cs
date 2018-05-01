using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpriteAnimation), true)]
public class SpriteAnimationEditor : Editor
{

	public override void OnInspectorGUI()
	{
		if (GUILayout.Button("Play"))
		{
			(target as SpriteAnimation).Play();
		}
		if (GUILayout.Button("Loop"))
		{
			(target as SpriteAnimation).Loop();
		}
		if (GUILayout.Button("Stop"))
		{
			(target as SpriteAnimation).Stop();
		}
		base.OnInspectorGUI();
	}

}