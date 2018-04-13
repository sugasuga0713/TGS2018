using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : SingletonMonoBehaviour<UpdateManager> {

	public ManagedUpdateBehaviour[] scripts;

	private int i;
	public int arraySize;

	private void Awake()
	{
		arraySize = scripts.Length;
	}

	private void Update()
	{
		for (i = 0; i < arraySize; i++)
		{
			if (scripts[i] == null) continue;
			scripts[i].UpdateMe();
		}
	}

	private void FixedUpdate()
	{
		for (i = 0; i < arraySize; i++)
		{
			if (scripts[i] == null) continue;
			scripts[i].FixedUpdateMe();
		}
	}

	public void Add(ManagedUpdateBehaviour behaviour)
	{
		if (behaviour == null) return;

		arraySize++;
		Array.Resize(ref scripts, arraySize);
		scripts[arraySize-1] = behaviour;
	}

}
