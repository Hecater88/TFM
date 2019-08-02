using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

	//[Header("")]
	//[Tooltip("")]

	//Referencia al canvas del inventario.
	public GameObject inventoryCanvas;

	//Referencia estática.
	public static InventoryManager IM;

	void Awake(){
		//Recuperamos la referencia estática.
		if (IM == null) {
			IM = GetComponent<InventoryManager> ();
		}
	}

	void Start () {
		//Actualizamos el inventario al iniciar. Así, mostramos si yab había algo en él previamente.
		UpdateInventory ();
	}

	/// <summary>
	/// Actualiza los objetos mostrados en el inventario.
	/// </summary>
	public void UpdateInventory(){
		//Recorremos cada uno de los slots de nuestro inventario.
		for (int i = 0; i < DataManager.DManager.data.inventory.Length; i++) {
			//Recupero la referencia a la imagen del slot actual.
			Image tempImage = inventoryCanvas.transform.GetChild (i).Find ("Item").GetComponent<Image>();

			//Si hay un item en el slot actual,
			if (DataManager.DManager.data.inventory [i].name != "") {
				//Cargamos ese item dentro de la imagen del slot.
				tempImage.sprite = Resources.Load<Sprite> ("Items/" + DataManager.DManager.data.inventory [i].name);

				//Mostramos un mensaje de advertencia en el caso de que la imagen no haya sido encontrada.
				if (tempImage.sprite == null) {
					Debug.LogWarning ("La imagen no ha sido encontrada en Resources: "+"Items/" + DataManager.DManager.data.inventory [i].name);
				}

				//Activamos la imagen para que sea visible.
				tempImage.gameObject.SetActive (true);

			} else {
				//Desactivamos la imagen de fondo.
				tempImage.gameObject.SetActive (false);
			}
		}
	}

	/// <summary>
	/// Localiza los datos del item y los añade al inventario.
	/// </summary>
	/// <param name="itemName">Item name.</param>
	public void AddInventory(string itemName){
		//Variable donde almacenaremos el nuevo item a añadir.
		Item newItem = new Item ();

		//Recorremos el listado de items disponibles.
		foreach (Item item in DataManager.DManager.data.allItems) {
			if (item.name == itemName) {
				//Recuperamos y almacenamos en la variable temporal, los distintos elementos del item.
				newItem.name = item.name;
				newItem.description = item.description;
				newItem.imageName = item.imageName;
				newItem.picked = item.picked;

				//Salimos del bucle.
				break;
			}
		}

		//Recorremos los huecos de nuestro inventario.
		for (int i = 0; i < DataManager.DManager.data.inventory.Length; i++) {
			//Presuponemos que si el nombre está en blanco, el hueco estará vacío.
			if (DataManager.DManager.data.inventory [i].name == "") {
				//Volcamos el valor de newItem al índice.
				DataManager.DManager.data.inventory [i] = newItem;

				//Tras volcar la información, actualizamos el inventario para que se muestre la imagen.
				UpdateInventory();

				//Para evitar que nos agregue el nuevo objeto en cada hueco, salimos del bucle.
				break;
			}
		}
	}

	/// <summary>
	/// ELimina el item del inventario.
	/// </summary>
	/// <param name="itemName">Item name.</param>
	public void RemoveItemInventory(string itemName){
		//Recorremos los distintos huecos del inventario.
		foreach (Item item in DataManager.DManager.data.inventory) {
			if (item.name == itemName) {
				item.name = "";
				item.description = "";
				item.imageName = "";
				item.picked = false;

				//Actualizamos el inventario, para que se elimine la imagen del objeto borrado.
				UpdateInventory ();

				//Salimos del método.
				return;
			}
		}

		//Mostramos un mensaje de emergencia. Si hemos llegado hasta aquí es que no se ha encontrado el objeto.
		Debug.LogWarning ("No se ha encontrado el objeto en el inventario: " + itemName);
	}
}
