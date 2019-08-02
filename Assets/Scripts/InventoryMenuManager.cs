using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenuManager : MonoBehaviour {

	// Referencia al canvas del inventario
	public GameObject canvasInventory;
	// Referencia la canvas del inventario de los objetos
	public GameObject itemInventory;
	// Referencia al canvas de las flechas
	//private GameObject arrowCanvas;


	/// <summary>
	/// Metodo que abre el inventario 
	/// </summary>
	public void OpenInventory(){
		// Buscamos el canvas de las flechas con su tag
		//arrowCanvas = GameObject.FindGameObjectWithTag("Move");

		canvasInventory.SetActive (true);
		itemInventory.SetActive (true);

		// Desactivamos el canvas de las flechas
		//arrowCanvas.SetActive (false);
	}
}
