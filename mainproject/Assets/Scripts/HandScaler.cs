﻿using UnityEngine;
using System;
using System.Collections;

public class HandScaler : MonoBehaviour {
	[NonSerialized] public Vector3 robotScale = new Vector3(1f, 1f, 1f);
	// Use this for initialization
	void Start () {
		gameObject.transform.localScale = robotScale;
	}
	
}
