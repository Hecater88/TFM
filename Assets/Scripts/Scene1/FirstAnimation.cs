using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAnimation : MonoBehaviour {
	public Camera firstCamera;
	// Use this for initialization
	void Start () {
		//Quitamos la barra de cordura
		//CorduraManager.CM.corduraSlider.gameObject.SetActive (false);
		//InventoryManager.IM.inventoryCanvas.gameObject.SetActive (false);
		firstCamera.gameObject.SetActive (true);
		ActiveAllRections ();

	}

	/// <summary>
	/// Desactives the object.
	/// </summary>
	/// <param name="obj">Object.</param>
	public void DesactiveObject(GameObject obj){
		obj.SetActive (false);
	}

	/// <summary>
	/// Actives the object.
	/// </summary>
	/// <param name="obj">Object.</param>
	public void ActiveObject(GameObject obj){
		obj.SetActive (true);
	}

	public void ActiveAllRections(){
		//almacenamos en una variable el hijo PositiveReactions, que almacena las reacciones positivas.
		Transform positiveReactions = transform;
		//Luego, recorremos todas las reactions que se encuentren como hijas de PositiveReactions.
		for (int i = 0; i < positiveReactions.childCount; i++) {
			//Ejecutamos las reactions.
			positiveReactions.GetChild (i).GetComponent<Reaction> ().ExecuteReaction ();
		}
	}

	/*
	/// <summary>
	/// Actives the fade animation.
	/// </summary>
	public void ActiveFadeAnimation(){
		StartCoroutine(SceneController.SC.Fade(1f));
	}
	/// <summary>
	/// Desactives the fade animation.
	/// </summary>
	public void DesactiveFadeAnimation(){
		StartCoroutine(SceneController.SC.Fade(0f));
	}
		*/
}
