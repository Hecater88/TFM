using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionAnimation : Reaction {

	//[Header("")]
	//[Tooltip("")]

	//Objeto que será animado.
	public GameObject target;
	//Nombre del trigger del animator a disparar.
	public string triggerName;

	/// <summary>
	/// Método que ejecuta la reacción, con override para que pise la corrutina heredada.
	/// </summary>
	protected override IEnumerator React(){
		//Este delay lo está cogiendo de la clase padre "Reaction".
		//Tiempo de espera anteds de iniciar la animación.
		yield return new WaitForSeconds (delay);

		//Disparamos el trigger de la animación.
		target.GetComponent<Animator> ().SetTrigger (triggerName);
	}
}
