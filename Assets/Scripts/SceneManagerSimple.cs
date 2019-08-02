using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System.IO;

public class SceneManagerSimple : MonoBehaviour {

/// <summary>
	/// Cambia a la escena solicitada
	/// </summary>
	/// <param name="name">Name.</param>
	public void SceneLoad(string name){
		SceneManager.LoadScene (name);
	}
	/// <summary>
	/// Cambia a la escena solicitada con un Fade
	/// </summary>
	/// <param name="name">Name.</param>
	public void FadeandSwitchScene(string name){
		SceneController.SC.FadeAndLoadScene (name);
	}

	/// <summary>
	/// Metodo que sale del juego
	/// </summary>
	public void QuitGame(){
		Application.Quit ();
	}
}
