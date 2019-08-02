using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InteractableClick : MonoBehaviour {

	private bool handleInput = true;
	private Interactable currentInteractable;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Controla el click sobre un objeto interactivo 
	/// </summary>
	/// <param name="interactable">Interactable.</param>
	public void OnInteractableClick(Interactable interactable){

		if (!handleInput) {
			return;
		}

		currentInteractable = interactable;

	}
}
