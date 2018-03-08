using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class PlayerNavMover : MonoBehaviour {

	NavMeshAgent agent;

	private Transform target;
	public float smallSwipeLength = 100f;
	public float longSwipeLength = 500f;

	public float timeForSpecialSkill = 0.3f;

	public GameObject swordSwing;
	public GameObject playerBody;
	public GameObject shield;

	int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.
	Vector2 mouseDownPosition;
	float timeSinceButtonDown;
	bool shieldUp = false;
	bool clicked = false;
	Touch firstTouch;
	Touch secondTouch;

	// Use this for initialization
	void Start () 
	{
		agent = GetComponent<NavMeshAgent>();
		swordSwing.SetActive(false);
		shield.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{	
		if (Input.touchCount > 0)
		{
			firstTouch = Input.GetTouch(0);
		}

        if (firstTouch.phase == TouchPhase.Began) 
		{
			mouseDownPosition = firstTouch.position; // Determine the start position of the mouse on screen.
			// Debug.Log(mouseDownPosition);
			timeSinceButtonDown = 0f;
			clicked = true;
			return;
		}

		timeSinceButtonDown += Time.deltaTime;

		if (timeSinceButtonDown > timeForSpecialSkill && clicked)
		{
			if (!shieldUp)
			{
				shieldUp = true;
				shield.SetActive(true);
			}
			RotateBody();
			if (firstTouch.phase == TouchPhase.Ended)
			{
				shieldUp = false;
				shield.SetActive(false);
				clicked = false;
				playerBody.transform.localRotation = Quaternion.identity;
			}		
			return;
		}
		
		if (firstTouch.phase == TouchPhase.Ended && clicked)
		{
			Debug.Log(Vector3.Magnitude(mouseDownPosition - firstTouch.position)); // How long is the swipe

			// A tap or very short swipe will move the character to the location hit.
			clicked = false;

			if (Vector3.Magnitude(mouseDownPosition - firstTouch.position) < smallSwipeLength )
			{
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(firstTouch.position);
				if (Physics.Raycast(ray, out hit))
					agent.SetDestination(hit.point);
			}
			else if (Vector3.Magnitude(mouseDownPosition - firstTouch.position) < longSwipeLength)
			{
				// Debug.Log("Attack!");
				swordSwing.SetActive(true);
				RotateBody();
			}
		}

	}

	void RotateBody()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(firstTouch.position);
		if (Physics.Raycast(ray, out hit))
			{
				Vector3 playerToMouse = hit.point - transform.position;

				playerToMouse.y = 0f;

				Quaternion newRotation = Quaternion.LookRotation (playerToMouse);

            	playerBody.transform.rotation = newRotation;
			}
	}

}
