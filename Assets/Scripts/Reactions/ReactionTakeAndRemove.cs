using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionTakeAndRemove : Reaction {


	// el objeto recogido pasa al inventario
	public bool moveToInventory = true;
	//Referencia al objeto que será desactivado.
	public InteractableItem itemReference;
	//Nombre del objeto a recoger.
	private string itemName;

	void Start(){
		//Recuperamos la referencia al componente interactableItem, para obtener el itemName.
		itemName = itemReference.itemName;
	}

	/// <summary>
	/// Realiza la recogida del objeto.
	/// </summary>
	private void PickupItem(){
		//Recorremos los objetos disponibles del juego.
		foreach (Item item in DataManager.DManager.data.allItems) {
			//Si encuentra el objeto,
			if (item.name == itemName) {
				//cambia su estado a recogido.
				item.picked = true;
				//Lo eliminamos de la escena, ya que pasa a estar recogido.
				itemReference.gameObject.SetActive (false);

				//Desactivamos el objeto de la escena, ya que pasa a estar en nuestro inventario
				if (moveToInventory) {
					//Añadimos el objeto al inventario.
					InventoryManager.IM.AddInventory (itemName);
				}
				return;
			}
		}

		//Mostramos una advertencia si el objeto no ha sido encontrado.
		Debug.LogWarning ("El nombre del objeto buscado no ha sido encotnrado: "+itemName);
	}

	/// <summary>
	/// Corrutina que será ejecutada como reacción.
	/// </summary>
	protected override IEnumerator React (){
		yield return new WaitForSeconds (delay);

		//Método que realizará las acciones de recogida.
		PickupItem ();
	}
}
