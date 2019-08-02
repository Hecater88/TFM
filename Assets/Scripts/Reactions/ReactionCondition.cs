using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionCondition : Reaction {
	// nombre de la condición a modificar
	public string conditionName;
	// estado en que dejaremos la condición
	public bool conditionStatus;

	protected override IEnumerator React (){
		yield return new WaitForSeconds (delay);

		// modificamos el estado de la condición
		DataManager.DManager.SetCondition (conditionName, conditionStatus);
	
	}
}

