using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

using System.IO;

public class SceneMng : MonoBehaviour {
 	public GameObject checkCanvas;
		
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

	/// <summary>
	/// Metodo que activa el panel de seguridad antes de borrar una partida
	/// </summary>
	public void CheckToErase(){
		checkCanvas.SetActive(true);
	}
	/// <summary>
	/// Metodo que activa el panel de seguridad antes de borrar una partida
	/// </summary>
	public void CheckToEraseClose(){
		checkCanvas.SetActive(false);
	}

	/// <summary>
	/// Metodo que borra el archivo de guardado e inicia una nueva partida.
	/// </summary>
	public  void RetryGame(){
		//borramos el archivo data
		File.Delete (Application.persistentDataPath + "/" + "data.dat");
		////hacemos que el tiempo sea normal
		Time.timeScale = 1f;
		//y cargamos la escena MainMenu
		SceneManager.LoadScene ("Intro");
	}

	/// <summary>
	/// Metodo que continua la partida
	/// </summary>
	public void Continue(){
		//cargamos escena
		SceneManager.LoadScene ("MainMenu");
	}
}