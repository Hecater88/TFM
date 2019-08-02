using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolverButton : MonoBehaviour {

	// referencia al canvas para desactivarlo
	public GameObject canvasDisable;
	// referencia al canvas para desactivarlo
	public GameObject itemDisable;






	/// <summary>
	/// Desactiva el canvas del menu de inventario
	/// </summary>
	public void DissableInventory (){
		// Buscamos el canvas de las flechas
		canvasDisable.SetActive (false);
		itemDisable.SetActive (false);
	
	}
}
