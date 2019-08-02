using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveCamera : MonoBehaviour {
	//Referencia a todos los flechas
	public GameObject buttonMoveLeftMain, buttonMoveRightMain,buttonMoveLeft ,buttonMoveRight ;
	//Referencia al canvas de las flechas
	public GameObject arrowCanvas;
	//Referencia al objeto que contiene la camar
	public GameObject cameraMove;
	//velocidad de rotacion
	public float speedRotation;
	// Use this for initialization
	void Start () {
		TextManager.TM.FinishDialogue ();

		//Activamos el canvas de las flechas
		arrowCanvas.SetActive (true);
		//Activamos las flechas principales
		buttonMoveLeftMain.SetActive (true);
		buttonMoveRightMain.SetActive (true);
		//Desactivamos las flechas secundarias
		buttonMoveLeft.SetActive (false);
		buttonMoveRight.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Mueve la camara hacia la izquierda
	/// </summary>
	/// <returns>The left main.</returns>
	IEnumerator MoveLeftMain(){
		//desactivamos las flechas principales del plano principal
		buttonMoveLeftMain.SetActive (false);
		buttonMoveRightMain.SetActive (false);
		//activamos la flecha que nos devuelve al plano principal
		buttonMoveRight.SetActive (true);
		//mientras el angulo de cameraMove es mayor que 60
		while(cameraMove.transform.rotation.eulerAngles.y > 60f){
			//guardamos en una variable temporal, el angulo actual
			Vector3 anguloActual = cameraMove.transform.rotation.eulerAngles;
			//giramos hacia la izquierda el objeto que contiene la camara
			cameraMove.transform.rotation = Quaternion.Euler ( anguloActual.x, 
				anguloActual.y - speedRotation *Time.deltaTime, 
				anguloActual.z);
			yield return null;
		}
	}

	/// <summary>
	/// Mueve la camara hacia la derecha
	/// </summary>
	/// <returns>The right main.</returns>
	IEnumerator MoveRightMain(){
		//desactivamos las flechas principales del plano principal
		buttonMoveLeftMain.SetActive (false);
		buttonMoveRightMain.SetActive (false);
		//activamos la flecha que nos devuelve al plano principal
		buttonMoveLeft.SetActive (true);
		//mientras el angulo de cameraMove es menor que 263
		while(cameraMove.transform.rotation.eulerAngles.y < 263f){
			//guardamos en una variable temporal, el angulo actual
			Vector3 anguloActual = cameraMove.transform.rotation.eulerAngles;
			//giramos hacia la derecha el objeto que contiene la camara
			cameraMove.transform.rotation = Quaternion.Euler ( anguloActual.x, 
				anguloActual.y + speedRotation *Time.deltaTime, 
				anguloActual.z);
			yield return null;
		}
	}

	/// <summary>
	/// Mueve la camara hacia el plano principal, girandolo hacia la derecha
	/// </summary>
	/// <returns>The right.</returns>
	IEnumerator MoveRight(){
		//desactivamos la flecha que nos devuelve al plano principal
		buttonMoveRight.SetActive (false);
		//activamos las flechas principales
		buttonMoveLeftMain.SetActive (true);
		buttonMoveRightMain.SetActive (true);
		//mientras el angulo de cameraMove es menor que 169
		while(cameraMove.transform.rotation.eulerAngles.y <169.7791f){
			//guardamos en una variable temporal, el angulo actual
			Vector3 anguloActual = cameraMove.transform.rotation.eulerAngles;
			//giramos hacia la derecha el objeto que contiene la camara
			cameraMove.transform.rotation = Quaternion.Euler ( anguloActual.x, 
				anguloActual.y + speedRotation *Time.deltaTime, 
				anguloActual.z);
			yield return null;
		}
	}

	/// <summary>
	/// Mueve la camara hacia el plano principal, girandolo hacia la izquierda
	/// </summary>
	/// <returns>The left.</returns>
	IEnumerator MoveLeft(){
		//desactivamos la flecha que nos devuelve al plano principal
		buttonMoveLeft.SetActive (false);
		//activamos las flechas principales
		buttonMoveLeftMain.SetActive (true);
		buttonMoveRightMain.SetActive (true);
		//mientras el angulo de cameraMove es mayor que 169
		while(cameraMove.transform.rotation.eulerAngles.y > 169.7791f){
			//guardamos en una variable temporal, el angulo actual
			Vector3 anguloActual = cameraMove.transform.rotation.eulerAngles;
			//giramos hacia la izquierda el objeto que contiene la camara
			cameraMove.transform.rotation = Quaternion.Euler ( anguloActual.x, 
				anguloActual.y - speedRotation *Time.deltaTime, 
				anguloActual.z);
			yield return null;
		}
	}


	/// <summary>
	///  Metodos para los botones, que inician la currutina
	/// </summary>
	public void ButtonMoveLeftMain(){
		StartCoroutine ( MoveLeftMain());
	}

	/// <summary>
	///  Metodos para los botones, que inician la currutina
	/// </summary>
	public void ButtonMoveRightMain(){
		StartCoroutine ( MoveRightMain());
	}

	/// <summary>
	///  Metodos para los botones, que inician la currutina
	/// </summary>
	public void ButtonMoveRight(){
		StartCoroutine ( MoveRight());
	}

	/// <summary>
	/// Metodos para los botones, que inician la currutina
	/// </summary>
	public void ButtonMoveLeft(){
		StartCoroutine ( MoveLeft());
	}


}
