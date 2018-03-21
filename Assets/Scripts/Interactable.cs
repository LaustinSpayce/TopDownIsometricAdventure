﻿using UnityEngine;

public class Interactable : MonoBehaviour 
{

	public float radius = 2f;
	public Transform interactionTransform;

	bool isFocus = false;
	Transform player;
	bool hasInteracted = false;

	public virtual void Interact()
	{
		Debug.Log("Interacting with " + transform.name);
	}

	private void Update() 
	{
		if (isFocus && !hasInteracted)
		{
			float distance = Vector3.Distance(interactionTransform.position, player.position);
			if (distance <= radius)
			{
				Interact();
				hasInteracted = true;
			}
		}
	}

	public void OnFocused(Transform playerTransform)
	{
		isFocus = true;
		player = playerTransform;
		hasInteracted = false;
	}

	public void OnDeFocused()
	{
		isFocus = false;
		player = null; 
		hasInteracted = false;
	}

	void OnDrawGizmosSelected()
	{
		if (interactionTransform = null)
			interactionTransform = this.transform;
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(interactionTransform.position, radius);
	}

}
