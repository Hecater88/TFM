using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInventory : MonoBehaviour {

	public GameObject inventario;

	public void OnAwake (){
		inventario = GetComponent<GameObject> ();
	}
	/// <summary>
	/// Metodo que activa el menu de inventario cuando pulsamos en el objeto.
	/// </summary>
	public void OnClick (){
		inventario.SetActive (true);

	}

}
