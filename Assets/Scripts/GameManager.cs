using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//NameSpace para usar OnGroundClick
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {

	//Tiempo de espera entre pulsaciones.
	public float inputHoldDelay = 0.5f;
	//Clase que permite realizar una espera de un tiempo determinado.
	private WaitForSeconds inputHoldWait;
	//Contenedor de último objeto interactivo pulsado.
	private Interactable currentInteractable;
	//Para controlar cuándo se gestionará el input.
	public bool handleInput = true;
	//Referencia al Slider que contendrá la vida del player
	//public Slider healthSlider;
	//Vida del player
	public static float healthPlayer = 100f;
	//Vida actual del player
	//public float currentHealth;
	//Referencia al daño que va a recibir el player
	//public float damage = 25f;

	//Referencia al menú de muerte
	//public GameObject deadMenu;
	//Referencia a todo el canvas de la persistente que será desactivado
	//public GameObject hUD;

	// Referencia estática al componente PlayerMovement
	public static GameManager GM;


	void Start () {
		// Si Pm no existe
		if (GM == null) {
			// Recogemos sus componentes.
			GM = GetComponent<GameManager> ();
		}
		//Inicializamos la vida actual para que sea igual al a vida del player
		//currentHealth = healthPlayer;

		//Definimos la variable la espera de tiempo entre pulsaciones.
		inputHoldWait = new WaitForSeconds (inputHoldDelay);

	

	

	}
		
	/// <summary>
	/// Controla el click sobre un objeto interactivo.
	/// </summary>
	/// <param name="">.</param>
	public void OnInteractableClick(Interactable interactable){
		//Si no hay control del input, no hacemos nada.
		if (!handleInput) {
			return;
		}

		//Indicamos con qué objeto interactivo vamos a interactuar.
		currentInteractable = interactable;

		//Activamos la interacción programada en el objeto interactivo.
		currentInteractable.Interact();

		//Una vez hemos interactuado con el objeto, lo ponemos a null para una futura pulsación
		currentInteractable = null;

		//Iniciamos la corrutina para realizar las esperas de tiempo.
		StartCoroutine (WaitForInteraction ());
	}

	/// <summary>
	/// Función que realizará una espera hasta que la interacción termine.
	/// </summary>
	/// <returns>The for interaction.</returns>
	private IEnumerator WaitForInteraction(){
		//Establecemos handleInput a false para evitar las interaccciones.
		handleInput = false;

		//Realizamos una espera previa controlada.
		yield return inputHoldWait;

		//Una vez terminado el bucle, devolvemos el control al jugador.
		handleInput = true;
	}
	/*/// <summary>
	/// Método que quita vida al Player.
	/// </summary>
	public void TakeDamage(){
		// Le quitamos vida al player
		healthPlayer -= damage;
		// Igualamos el slider al a vida actual
		healthSlider.value = currentHealth;
		// Si la vida es menor o igual que cero, llamamos al método de fin de juego.
		if (currentHealth <= 0) {
			EndGame ();
		}


	}
	/// <summary>
	/// Método que llama al fin del juego.
	/// </summary>
	public void EndGame(){
		//Desactivamos el menú de la persistente
		hUD.SetActive (false);
		// Activamos el menú de muerte
		deadMenu.SetActive (true);

	}*/
}
