using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycastfollower : MonoBehaviour {

	public Transform player;

	// Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = player.transform.position;
	}
}
