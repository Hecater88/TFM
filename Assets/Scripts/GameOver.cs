using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
public class GameOver : MonoBehaviour {

	
	
	public  void LoadMenu(){
		//borramos el archivo data
		File.Delete (Application.persistentDataPath + "/" + "data.dat");
		//y cargamos la escena MainMenu
		SceneManager.LoadScene ("MainMenu");
	}

	
	public void Retry(){
		// Volvemos a cargar la escena persistente
		SceneManager.LoadScene ("Persistent 1");
	}
}
