using UnityEngine;
using System;
using System.Collections;

public class RobotScaler : MonoBehaviour {
	[NonSerialized]public Vector3 robotScale =Vector3.one * .2f;// new Vector3(.2f, .2f, .2f);
	// Use this for initialization
	void Start ()
	{
		gameObject.transform.localScale = robotScale;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
