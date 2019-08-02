using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionObjectActivation : Reaction {

	// gameobject objetivo de la activación y desactivación
	public GameObject targetObject;
	// indicamos si vamos a actuivar o desactivar el gameobject
	public bool active;

	/// <summary>
	/// Corrutina que será ejecutada como reacción.
	/// </summary>
	protected override IEnumerator React (){
		yield return new WaitForSeconds (delay);

		// activa o desactiva el gameobject indicado como variable pública.
		targetObject.SetActive (active);

	}
}
