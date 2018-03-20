using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCoolDown : MonoBehaviour {

	[SerializeField]
	float cooldownDuration = 0.5f;

	Button thisButton;

	void Awake() 
	{
		thisButton = GetComponent<Button>();

		if (thisButton != null)
		{
			thisButton.onClick.AddListener(OnButtonClick);
		}	
	}
	
	void OnButtonClick()
	{
		StartCoroutine(Cooldown());
	}

	IEnumerator Cooldown()
	{
		thisButton.interactable = false;

		yield return new WaitForSeconds(cooldownDuration);

		thisButton.interactable = true;
	}
}
