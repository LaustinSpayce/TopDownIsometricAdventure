using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 
using UnityEngine.EventSystems;

public class PlayerNavMover : MonoBehaviour {

	public float smallSwipeLength = 100f;
	public float longSwipeLength = 500f;

	public float timeForSpecialSkill = 0.3f;

	public GameObject swordSwing;
	public GameObject playerBody;
	public GameObject shield;

	// Private variables
	NavMeshAgent agent;
	Transform target;
	int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.
	Vector2 touchDownPosition;
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
			if (EventSystem.current.IsPointerOverGameObject(firstTouch.fingerId))
            {
                // Debug.Log("Touched the UI");
				return;
            }
		}

        if (firstTouch.phase == TouchPhase.Began) 
		{
			touchDownPosition = firstTouch.position; // Determine the start position of the mouse on screen.
			// Debug.Log(touchDownPosition);
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
			RotateBody(firstTouch.position);
			if (firstTouch.phase == TouchPhase.Ended)
			{
				shieldUp = false;
				shield.SetActive(false);
				clicked = false;
				playerBody.transform.localRotation = Quaternion.identity;
			}		
			if (Input.touchCount > 1)
			{
				secondTouch = Input.GetTouch(1);
				if (secondTouch.phase == TouchPhase.Began)
					{
					if (EventSystem.current.IsPointerOverGameObject(secondTouch.fingerId))
						{
							// Debug.Log("Touched the UI");
							return;
						}
					else
					SetDestinationByTouch(secondTouch.position);
					}
			}
			return;
		}
		
		if (firstTouch.phase == TouchPhase.Ended && clicked)
		{
			// Debug.Log(Vector3.Magnitude(touchDownPosition - firstTouch.position)); // How long is the swipe
			// A tap or very short swipe will move the character to the location hit.
			clicked = false;

			if (Vector3.Magnitude(touchDownPosition - firstTouch.position) < smallSwipeLength ) // Small Swipe sets target
            {
                SetDestinationByTouch(firstTouch.position);
            }
            else if (Vector3.Magnitude(touchDownPosition - firstTouch.position) < longSwipeLength) // Long Swipe 
			{
				// Debug.Log("Attack!");
				var averagePosition = (touchDownPosition + firstTouch.position)/2;
				swordSwing.SetActive(true);
				RotateBody(averagePosition); // Should attack in the direction of the middle of the swipe, so player swipes the enemies.
			}
		}

	}

    private bool SetDestinationByTouch(Vector2 touchPosition)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        if (Physics.Raycast(ray, out hit))
		{
            agent.SetDestination(hit.point);
			return true;
		}
		else
			return false;

    }

    void RotateBody(Vector2 touchPosition) // Rotate the body to face the Touch Position
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(touchPosition);
		if (Physics.Raycast(ray, out hit))
			{
				Vector3 playerToMouse = hit.point - transform.position;

				playerToMouse.y = 0f;

				Quaternion newRotation = Quaternion.LookRotation (playerToMouse);

            	playerBody.transform.rotation = newRotation;
			}
	}

}
