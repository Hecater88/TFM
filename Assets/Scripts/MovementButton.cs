using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementButton : MonoBehaviour {


	// Referencia al canvas de desplazamiento
	private GameObject arrowCanvas;
	private bool desActive;
	// Use this for initialization
	void Start () {
		//arrowCanvas = GameObject.Find ("ArrowsCanvas");
	}
	

	public void OnClick() {
		arrowCanvas = GameObject.Find ("ArrowsCanvas");
		desActive = !desActive;
		// Buscamos el canvas en la escena con el tag de Move
		// Y lo activamos.
		arrowCanvas.SetActive (desActive);
	}
}
