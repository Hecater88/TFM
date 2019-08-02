using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class TextManager : MonoBehaviour {

	//[Header("")]
	//[Tooltip("")]

	//Referencia al text del canvas.
	public Text text;
	// Referencia al background de imagen del texto
	public Image textImage, danny, cottonLeft, cottonright,paolo, tracer, heineken, fuertesco, juan, stanli, turenmoshi, fade;
	// Referencia al nombre del pj que esta hablando en ese momento
	public Text textName;

	public GameObject inventory, cordura;


	//public Text textName;
	//Referencia a la imagen de texto
	//public Image image;
	//Tiempo que durará la imagen de texto
	private float timeImage;
	//Tiempo mínimo de mostrado por carácter.
	public float displayTimePerCharacter=0.1f;
	//Tiempo adicional que permanecerá el texto en pantalla como mínimo.
	public float additionalDisplayTime=0.5f;
	//Temporizador para el borrado.
	private float clearTime;

	//Referencia estática.
	public static TextManager TM;

	private bool gameIsPaused;

	public bool ifCinematic;

	public bool Danny, CottonLeft, CottonRight,Paolo, Tracer, Heineken, Fuertesco, Juan, StanLi, Turenmoshi;

	void Awake(){
		//Recuperamos la referencia estática.
		if (TM == null) {
			TM = GetComponent<TextManager> ();
		}
	}

	void Start () {
		
	}

	void Update () {
		//Si se ha superado el tiempo de mostrado de texto, lo eliminamos.
		if (Time.time > clearTime && ifCinematic) {
			text.text = "";
			textName.text = "";
			textImage.gameObject.SetActive(false);
			DesactiveMessage ();
		}
		// Si el juego no está pausado y no es una cinematica, mostramos el texto
		//if (Time.time > clearTime) {
		if(!gameIsPaused && !ifCinematic){
			text.text = "";
			textName.text = "";
			//textImage.gameObject.SetActive(false);
			DesactiveMessage();
		}
		// Sui el jueego está pausado y hacemos click y no es una cinematica, el juego se reanuda y el tiempo vuelve a ejecutarse en él
		if(gameIsPaused && Input.GetButtonDown("Fire1") && !ifCinematic){
			gameIsPaused = false;
			Time.timeScale = 1f;
		}



		//Si se ha superado el tiempo de mostrado de texto, lo eliminamos.
		/*if (Time.time > timeImage && ifCinematic) {
			//image.gameObject.SetActive(false);
		}*/


	}

	/// <summary>
	/// Muestra el texto por pantalla.
	/// </summary>
	/// <param name="message">Message.</param>
	/// <param name="textColor">Text color.</param>
	/// //Danny, CottonLeft, Cottonright,Paolo, Tracer, Heineken;
	public void DisplayMessage(string message, Color textColor, string name){
		danny.gameObject.SetActive (Danny);
		cottonLeft.gameObject.SetActive (CottonLeft);
		cottonright.gameObject.SetActive (CottonRight);
		paolo.gameObject.SetActive (Paolo);
		tracer.gameObject.SetActive (Tracer);
		heineken.gameObject.SetActive (Heineken);
        fuertesco.gameObject.SetActive(Fuertesco);
        juan.gameObject.SetActive (Juan);
        stanli.gameObject.SetActive (StanLi);
        turenmoshi.gameObject.SetActive (Turenmoshi);
        //Calculamos el tiempo de duración de la frase.
        float displayDuration = message.Length * displayTimePerCharacter + additionalDisplayTime;

		//Calculamos en qué momento deberá desaparecer el texto.
		clearTime = Time.time + displayDuration;
		//El tiempo del texto de imagen será igual al del texto
		timeImage = clearTime;
		//Asignamos el texto del mensaje.
		text.text = message;
		//Asignamos el color del texto.
		text.color = textColor;
		textName.text = name;

		if(!ifCinematic){
			gameIsPaused = true;
			Time.timeScale = 0f;
		}
		if(ifCinematic){
			textImage.gameObject.SetActive(true);
		}
	}

	public void DesactiveMessage(){
		danny.gameObject.SetActive (false);
		cottonLeft.gameObject.SetActive (false);
		cottonright.gameObject.SetActive (false);
		paolo.gameObject.SetActive (false);
		tracer.gameObject.SetActive (false);
        fuertesco.gameObject.SetActive(false);
        juan.gameObject.SetActive (false);
        stanli.gameObject.SetActive(false);
        turenmoshi.gameObject.SetActive(false);
    }

	// Método que se encarga de comenzar el diálogo
	public void StartDialogue(){
		// Hacemos un fade en negro para los diálogos, añadimos las imágenes de los pj que hablan y desactivamos los objetos de interfaz de jugador
		fade.gameObject.SetActive (true);
		textImage.gameObject.SetActive (true);

		inventory.gameObject.SetActive (false);
		//movement.gameObject.SetActive (false);	
		cordura.gameObject.SetActive (false);

	}
	// Método que se encarga de finalizar el diálogo
	public void FinishDialogue(){
		// Desactivamos los objetos que componen el diáologo y activamos los objetos de vida, y de inventario
		fade.gameObject.SetActive (false);
		inventory.gameObject.SetActive (true);

		//movement.gameObject.SetActive (true);	
		cordura.gameObject.SetActive (true);
		textImage.gameObject.SetActive (false);
		DesactiveMessage ();
	}

	// Método que activa una cinemática
	public void StartCinematic(){
		// Desactiva el inventario y la barra de cordura
		inventory.gameObject.SetActive (false);
		//movement.gameObject.SetActive (false);	
		cordura.gameObject.SetActive (false);
	}

	// Método que hace que acabe la cinemática
	public void FinishCinematic(){
		// Activa la barra de cordura y el inventario
		//movement.gameObject.SetActive (true);	
		cordura.gameObject.SetActive (true);
		inventory.gameObject.SetActive (true);

	}
}