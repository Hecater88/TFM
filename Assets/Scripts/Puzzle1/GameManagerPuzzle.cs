using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Libreria para utilizar "Tolist"
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerPuzzle : MonoBehaviour {
	public static GameManagerPuzzle GMP1;
	public GameObject canvas;
	//Array de preguntas
	public Question[] questions;
	//Lista para las preguntas
	private static List<Question> facts;

	[SerializeField]
	//variable para el numero de preguntas
	public static int questionNumber;
	//variable para guardar la pregunta actual
	private Question currentQuestion;

	[SerializeField]
	//referencia al texto del canvas de la pregunta
	private Text questionText;

	[SerializeField]
	//referencia al texto del canvas detras del boton true
	private Text trueText;

	[SerializeField]
	//referencia al texto del canvas detras del boton false
	private Text falseText;

	[SerializeField]
	//referencia al animator
	Animator animator;

	[SerializeField]
	//variable que guarda le tiempo entre preguntas
	private float timeBetweenQuestions = 1f;

	void Awake(){
		if(GMP1 == null){
			GMP1 = GetComponent<GameManagerPuzzle>();
		}
	}

	void Start(){
		
		Debug.Log (questionNumber);
		//recuperamos la referencia de las preguntas
		if (facts == null || facts.Count == 0) {
			facts = questions.ToList<Question> ();
		}
		//Ponemos las preguntas en juego
		SetCurrentQuestion ();
		Debug.Log (currentQuestion.question + " is "+ currentQuestion.isTrue);
	}
	void Update(){
		if (Input.GetKey(KeyCode.DownArrow)) {
			SceneController.SC.LoadSceneWithoutFade("PlazaCentral3D");
		}
	}

	/// <summary>
	/// Introduce las preguntas en juego
	/// </summary>
	void SetCurrentQuestion(){
		//Cogemos la pregunta de la lista y la igualamos a la variable de "currentQuestion"
		currentQuestion = facts [questionNumber];
		//cogemos la variable string de question y la igualamos al Text del canvas
		questionText.text = currentQuestion.question;

		//Si la pregunta es verdadero
		if (currentQuestion.isTrue) {
			trueText.text = "¡CORRECTO!";
			falseText.text = "¡FALLASTE!";
		//Si la pregunta es falsa
		} else {
			falseText.text = "¡CORRECTO!";
			trueText.text = "¡FALLASTE!";
		
		}
	}
	/// <summary>
	///Funcion para controlar la transicion entre preguntas 
	/// </summary>
	/// <returns>The to next question.</returns>
	IEnumerator TransitionToNextQuestion(){
		questionNumber++;
		//yield return new WaitForSeconds (timeBetweenQuestions);
		//si questionNumber es menor que el tamaño del array de questions
		if (questionNumber < questions.Length) {
			//aumentamos questionNumber

			yield return new WaitForSeconds (timeBetweenQuestions);
			SceneController.SC.LoadSceneWithoutFade("Puzzle1");

		}

		//si questionNumber supera el tamaño del array, significa que ya ha recorrido toda la lista de preguntas
		if (questionNumber >= questions.Length) {
			//canvas.SetActive (false);

			//DataManager.DManager.SetCondition ("NPC3", true);
			yield return new WaitForSeconds (timeBetweenQuestions);
			//cargamos la siguente escena del juego
			SceneController.SC.FadeAndLoadScene ("PlazaCentral3D");
		}




	


		//SceneController.SC.LoadSceneWithoutFade("Puzzle1");
	}


	/// <summary>
	///Funcion para el boton de True 
	/// </summary>
	public void PlayerSelectTrue(){
		//Activamos la animacion del boton rtue
		animator.SetTrigger ("True");
		//habilitamos el texto de detras del boton true
		trueText.enabled = true;
		//habilitamos el texto de detras del boton false
		falseText.enabled = true;

		//si  la pregunta es verdadera
		if (currentQuestion.isTrue) {
			Debug.Log ("Has aceretado!");
		}else{
			CorduraManager.CM.TakeDamage (25f);
			Debug.Log ("Has fallado!");
		}
		//Activamos la corrutina TransitionToNextQuestion, para que introduzca la siguiente pregunta
		StartCoroutine (TransitionToNextQuestion ());
	}

	/// <summary>
	/// Funcion para el boton de false
	/// </summary>
	public void PlayerSelectFalse(){
		//activamos ela animacion del boton false
		animator.SetTrigger ("False");
		//habilitamos el texto de detras del boton true
		trueText.enabled = true;
		//habilitamos el texto de detras del boton false
		falseText.enabled = true;

		//si la pregunta es falsa
		if (!currentQuestion.isTrue) {
			Debug.Log ("Has aceretado!");
		}else{
			CorduraManager.CM.TakeDamage (25f);
			Debug.Log ("Has fallado!");
		}
		//Activamos la corrutina TransitionToNextQuestion, para que introduzca la siguiente pregunta
		StartCoroutine (TransitionToNextQuestion ());
	}

	public void ExitGame(){
		if (questionNumber < questions.Length) {
			CorduraManager.CM.TakeDamage (50f);
		}
		SceneController.SC.FadeAndLoadScene ("LeftTraperScene");
	}

	public void ReinicioNivel(){
		questionNumber = 0;
	}
}
