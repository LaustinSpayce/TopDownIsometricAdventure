using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwingArea : MonoBehaviour {

	public float appearTime = 0.25f;

	private float timeElapsed = 0f;

	// Use this for initialization
	void Start () {
		timeElapsed = 0f;
	}

 	void OnEnable() {
		timeElapsed = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		timeElapsed += Time.deltaTime;
		if (timeElapsed >= appearTime)
		{
			gameObject.SetActive(false);
		}
	}

	private void OnDisable()
	{
		transform.parent.localRotation = Quaternion.identity;
	}
}
