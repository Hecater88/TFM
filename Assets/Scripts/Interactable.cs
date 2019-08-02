/*
################################################################################
Name		: Interactable
Description	: "Descripción de las acciones que ejecuta el script"
Author		: Adrián
Changes		: 05/03/2018 - Creación del documento
################################################################################
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

	//Array de condiciones a cumplir para que se ejecute la reacción positiva.
	[Header("Condiciones")]
	public string[] conditions;

	/// <summary>
	/// Gestionará las condiciones y las reacciones.
	/// </summary>
	public void Interact(){

		//Variable para controlar si se cumplen las condiciones.
		bool success = true;

		//Recorremos la lista de condiciones.
		foreach (string cond in conditions) {
			//Si la condición no se cumple,
			if (!DataManager.DManager.CheckCondition (cond)) {
				//indicamos que no se ha cumplido la lista de condiciones,
				success = false;
				//y rompemos el bucle, para ahorrar ciclos.
				break;
			}
		}

		//Si las condiciones se cumplen,
		if (success && conditions.Length > 0) {
			//almacenamos en una variable el hijo PositiveReactions, que almacena las reacciones positivas.
			Transform positiveReactions = transform.Find ("PositiveReactions");
			//Luego, recorremos todas las reactions que se encuentren como hijas de PositiveReactions.
			for (int i = 0; i < positiveReactions.childCount; i++) {
				//Ejecutamos las reactions.
				positiveReactions.GetChild (i).GetComponent<Reaction> ().ExecuteReaction ();
			}

		} else {
			//Si no, almacenamos en una variable el hijo DefaultReactions, que almacena las reacciones por defecto.
			Transform defaultReactions = transform.Find ("DefaultReactions");
			//Luego, recorremos todas las reactions que se encuentren como hijas de DefaultReactions.
			for (int i = 0; i < defaultReactions.childCount; i++) {
				//Ejecutamos las reactions.
				defaultReactions.GetChild (i).GetComponent<Reaction> ().ExecuteReaction ();
			}
		}
	}
}
