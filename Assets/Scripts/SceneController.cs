using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Para el control de escenas.
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	//Group con el que haremos el oscurecido de pantalla.
	public CanvasGroup faderCanvasGroup;

	//Control del tiempo de fundido.
	public float fadeDuration = 1f;

	//Valor por defecto que usaremos si no está definido el DataManager.
	public string startingSceneName = "BlockingScene";

	//Para controlar si ya se está realizando un fundido de pantalla.
	private bool isFading;

	//Referencia estática.
	public static SceneController SC;

	void Awake(){
		//Recuperamos la referencia estática.
		if (SC == null) {
			SC = GetComponent<SceneController> ();
		}
	}

	private IEnumerator Start(){
		//Establecemos el alfa a 1.
		faderCanvasGroup.alpha = 1f;

		//Si hay definido un actualScene en el DataManager, usamos ese valor. En caso contrario, dejamos el hardcodeado.
		startingSceneName = DataManager.DManager.data.actualScene != "" ? DataManager.DManager.data.actualScene : startingSceneName;

		//Solicitamos la carga de la escena y la ponemos activa.
		yield return StartCoroutine (LoadSceneAndSetActive (startingSceneName));

		//Fade in.
		StartCoroutine (Fade (0f));
	}

	/// <summary>
	/// Llamada pública para el cambio de escenas.
	/// </summary>
	/// <param name="sceneName">Scene name.</param>
	/// <param name="startPosition">Start position.</param>
	public void FadeAndLoadScene(string sceneName){
		//Actualizamos el valor de la escena actual.
		DataManager.DManager.data.actualScene = sceneName;

		//Guardamos al realizar un cambio de escena.
		DataManager.DManager.Save ();

		if (!isFading) {
			StartCoroutine (FadeAndSwitchScenes(sceneName));
		}
	}
	/// <summary>
	/// Fades the and load scene.
	/// </summary>
	/// <param name="sceneName">Scene name.</param>
	public void LoadSceneWithoutFade(string sceneName){
		//Actualizamos el valor de la escena actual.
		DataManager.DManager.data.actualScene = sceneName;

		//Guardamos al realizar un cambio de escena.
		DataManager.DManager.Save ();

		StartCoroutine (SwitchScenesWithoutFade(sceneName));
		
	}

	/// <summary>
	/// Switchs the scenes without fade.
	/// </summary>
	/// <returns>The scenes without fade.</returns>
	/// <param name="sceneName">Scene name.</param>
	private  IEnumerator SwitchScenesWithoutFade(string sceneName){

		//Descargamos la escena actual, esperando a que termine.
		yield return SceneManager.UnloadSceneAsync (SceneManager.GetActiveScene ().buildIndex);

		//Cargamos la escena actual, esperando a que termine la carga.
		yield return StartCoroutine (LoadSceneAndSetActive (sceneName));

	}

	/// <summary>
	/// Corrutina que cambia de escena con un fade previo.
	/// </summary>
	/// <returns>The and switch scenes.</returns>
	/// <param name="">.</param>
	private  IEnumerator FadeAndSwitchScenes(string sceneName){
		//Realizamos un fade out y esperamos a que termine de ejecutarse.
		yield return StartCoroutine (Fade (1f));

		//Descargamos la escena actual, esperando a que termine.
		yield return SceneManager.UnloadSceneAsync (SceneManager.GetActiveScene ().buildIndex);

		//Cargamos la escena actual, esperando a que termine la carga.
		yield return StartCoroutine (LoadSceneAndSetActive (sceneName));

		//Hacemos un fade in.
		yield return StartCoroutine (Fade (0f));
	}

	/// <summary>
	/// Carga de una nueva escena.
	/// </summary>
	/// <returns>The scene and set active.</returns>
	/// <param name="sceneName">Scene name.</param>
	private IEnumerator LoadSceneAndSetActive(string sceneName){
		//Cargamos la escena de forma aditiva, sin destruir la escena principal.
		yield return SceneManager.LoadSceneAsync (sceneName, LoadSceneMode.Additive);

		//Para recuperar la última escena añadida, tomamos el número actual de escenas, menos 1. Esto nos dará el índice de la última escena añadida.
		Scene newlyLoadedScene = SceneManager.GetSceneAt (SceneManager.sceneCount - 1);

		//Establecemos esta última escena como la escena activa.
		SceneManager.SetActiveScene (newlyLoadedScene);
	}

	/// <summary>
	/// Realiza el fundido a negro y a visible.
	/// </summary>
	/// <param name="finalAplha">Final aplha.</param>
	private IEnumerator Fade(float finalAplha){
		//Indicamos que estamos haciendo un fundido.
		isFading = true;

		//Activamos el bloqueo de Raycasts para  que el jugador no pueda interactuar mientras se produce el fade.
		faderCanvasGroup.blocksRaycasts = true;

		//Espacio / tiempo = velocidad.
		float fadeSpeed = 1f / fadeDuration;

		//Mientras que el valor actual del aplha no sea un valor aproximado del alfa final, se ejecuta el siguiente bucle.
		while (!Mathf.Approximately (faderCanvasGroup.alpha, finalAplha)) {
			//Modificamos su valor de alpha desde su valor actual al valor final.
			faderCanvasGroup.alpha = Mathf.MoveTowards (faderCanvasGroup.alpha, finalAplha, fadeSpeed * Time.deltaTime);

			//Para hacer que la corrutina se ejecute nuevamente sin esperas en el siguiente frame.
			yield return null;
		}

		//Indicamos que hemos terminado el fundido.
		isFading = false;

		//Hacemos que el fader deje de bloquear los raycast para que el jugador pueda interactuar de nuevo.
		faderCanvasGroup.blocksRaycasts = false;
	}
}
