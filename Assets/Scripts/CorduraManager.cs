using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CorduraManager : MonoBehaviour {
	//referencia al canvas del slider
	public Slider corduraSlider;
	//referencia a la estática
	public static CorduraManager CM;
	private float cord;

	void Awake(){
		
		//Recuperamos la referencia estática.
		if (CM == null) {
			CM = GetComponent<CorduraManager> ();

		}
	}
	void Update(){
		//Si la cordura es menor o igual a 0
		if (DataManager.DManager.data.cordura <= 0) {
			//Game over para el jugador
			Death ();
		}
	}
	// Use this for initialization
	void Start () {
		cord = DataManager.DManager.data.cordura;
		UpdateCordura ();
	}

	/*void Update(){
		if (DataManager.DManager.data.cordura < 0) {
			Death ();
		}
	}*/
	/// <summary>
	/// Actualiza la barra de cordura
	/// </summary>
	public void UpdateCordura(){
		DataManager.DManager.data.cordura = cord;
		corduraSlider.value = cord;
	}

	/// <summary>
	/// Metodo que quita Cordura
	/// </summary>
	/// <param name="dmg">Dmg.</param>
	public void TakeDamage(float dmg){
		cord = cord - dmg;
		UpdateCordura ();

	}

	/// <summary>
	/// Metodo que da Cordura
	/// +
	/// </summary>
	/// <param name="cdr">Cdr.</param>
	public void TakeCordura(float cdr){
		DataManager.DManager.data.cordura += cdr;
		UpdateCordura ();
	}

	/// <summary>
	/// Metodo que lleva a la escena GameOver
	/// </summary>
	public void Death(){
		//SceneController.SC.FadeAndLoadScene ("GameOver");
		SceneManager.LoadScene ("GameOver");
	}

}
