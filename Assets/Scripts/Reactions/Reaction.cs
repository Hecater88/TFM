using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Reaction : MonoBehaviour {

	//[Header("")]
	//[Tooltip("")]

	//Descripción de la reacción que será visible desde el inspector.
	public string description;
	//Tiempo de espera antes de ejecutar la reacción.
	public float delay;

	/// <summary>
	/// Método genérico que ejecutará la reacción, será extendido a todas las clases que lo hereden.
	/// </summary>
	public virtual void ExecuteReaction(){
		//Iniciamos la corrutina de reacción.
		StartCoroutine (React ());
	}

	/// <summary>
	/// Corrutina que será ejecutada como reacción.
	/// </summary>
	protected virtual IEnumerator React(){
		//Realizamos la espera por el tiempo indicado.
		yield return new WaitForSeconds (delay);
	}
}
