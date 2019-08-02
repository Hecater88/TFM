using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : Interactable {

	//[Header("")]
	//[Tooltip("")]

	//Nombre del item, este nombre tiene que ser el mismo que aparezca en allItems.
	public string itemName;

	void Start () {
		//Verificamos si el objeto ya ha sido recogido.
		if (IsPicked ()) {
			//Si lo ha sido, lo desactivamos.
			gameObject.SetActive (false);
		}
	}

	/// <summary>
	///
	/// Verifica si el objeto ha sido recogido.
	/// </summary>
	/// <returns><c>true</c> if this instance is picked; otherwise, <c>false</c>.</returns>
	public bool IsPicked(){
		//Recorremos el listado de objetos existentes en nuestro juego.
		foreach (Item item in DataManager.DManager.data.allItems) {
			//Si el nombre coincide con el del interactuable,
			if (item.name == itemName) {
				//devolvemos la información de si ya ha sido recogido.
				return item.picked;
			}
		}

		//Mostramos un mensaje en caso de que el objeto no haya sido encontrado.
		Debug.LogWarning ("El nombre del item no existe. "+itemName);
		return false;
	}
}
