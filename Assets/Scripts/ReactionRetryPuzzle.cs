using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionRetryPuzzle : Reaction {

	/// <summary>
	/// Corrutina que será ejecutada como reacción.
	/// </summary>
	protected override IEnumerator React(){
		yield return new WaitForSeconds (delay);

		// eliminamos el objeto del inventario
		GameManagerPuzzle.GMP1.ReinicioNivel();

	}
}
