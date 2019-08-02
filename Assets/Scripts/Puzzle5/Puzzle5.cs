using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Libreria para utilizar "Tolist"
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Puzzle5 : MonoBehaviour {
		
		public static GameManagerPuzzle GMP1;
		public GameObject canvas;
		//Array de preguntas
		public Answer[] questions;
		//Lista para las preguntas
		private static List<Answer> facts;
		public float time;
		public Text timeText;
		private bool ready = false;
		

		[SerializeField]
		//variable para el numero de preguntas
		public static int questionNumber;
		//variable para guardar la pregunta actual
		private Answer currentQuestion;

		[SerializeField]
		//referencia al texto del canvas de la pregunta
		public Text questionText;
		public Text requestText;
		public Text answerText1, answerText2, answerText3;


		/*[SerializeField]
		//referencia al animator
		Animator animator;*/

		[SerializeField]
		//variable que guarda le tiempo entre preguntas
		private float timeBetweenQuestions = 1f;

		public GameObject questionCanvas, requestCanvas, answerCanvas1, answerCanvas2, answerCanvas3;

		void Awake(){
			if(GMP1 == null){
				GMP1 = GetComponent<GameManagerPuzzle>();
			}
		}

		void Start(){

			Debug.Log (questionNumber);
			//recuperamos la referencia de las preguntas
			if (facts == null || facts.Count == 0) {
				facts = questions.ToList<Answer> ();
			}
			//Ponemos las preguntas en juego
			StartCoroutine (SetCurrentQuestion ());
			//Debug.Log (currentQuestion.question + " is "+ currentQuestion.isTrue);
		}
		
		void Update(){
			if (Input.GetKey(KeyCode.DownArrow)) {
				DataManager.DManager.SetCondition ("PuzzleInsulto", true);
				SceneController.SC.LoadSceneWithoutFade("NewScene");
			}

			if(ready){
				CountDown ();
			}
				
				
		}


		/// <summary>
		/// Introduce las preguntas en juego
		/// </summary>
		IEnumerator SetCurrentQuestion(){
			//Cogemos la pregunta de la lista y la igualamos a la variable de "currentQuestion"
		currentQuestion = facts [questionNumber];
			yield return new WaitForSeconds (1f);
			questionCanvas.SetActive (true);
			questionText.text = currentQuestion.question;
			//cogemos la variable string de question y la igualamos al Text del canvas
			yield return new WaitForSeconds (2f);
			requestCanvas.SetActive (true);
			requestText.text = currentQuestion.request;
			yield return new WaitForSeconds (2f);
			ready = true;
			timeText.gameObject.SetActive (true);
			answerCanvas1.SetActive (true);
			answerText1.text = currentQuestion.answer1;
			answerCanvas2.SetActive (true);
			answerText2.text = currentQuestion.answer2;
			answerCanvas3.SetActive (true);
			answerText3.text = currentQuestion.answer3;
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
				SceneController.SC.LoadSceneWithoutFade("Puzzle5");

			}

			//si questionNumber supera el tamaño del array, significa que ya ha recorrido toda la lista de preguntas
			if (questionNumber >= questions.Length) {
				//canvas.SetActive (false);

				//DataManager.DManager.SetCondition ("NPC3", true);
				
				yield return new WaitForSeconds (timeBetweenQuestions);
				DataManager.DManager.SetCondition ("PuzzleInsulto", true);
				//cargamos la siguente escena del juego
				SceneController.SC.LoadSceneWithoutFade("NewScene");
			}







			//SceneController.SC.LoadSceneWithoutFade("Puzzle1");
		}


		/// <summary>
		///Funcion para el boton de True 
		/// </summary>
		public void PlayerSelectOne(){
			//Activamos la animacion del boton rtue
			//animator.SetTrigger ("True");

			//si  la pregunta es verdadera
			if (currentQuestion.one) {
				Debug.Log ("Has acertado!");
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
		public void PlayerSelectTwo(){
			//activamos ela animacion del boton false
			//animator.SetTrigger ("False");

			//si la pregunta es falsa
			if (currentQuestion.two) {
				Debug.Log ("Has acertado!");
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
	public void PlayerSelectThree(){
		//activamos ela animacion del boton false
		//animator.SetTrigger ("False");

		//si la pregunta es falsa
		if (currentQuestion.three) {
			Debug.Log ("Has acertado!");
		}else{
			CorduraManager.CM.TakeDamage (25f);
			Debug.Log ("Has fallado!");
		}
		//Activamos la corrutina TransitionToNextQuestion, para que introduzca la siguiente pregunta
		StartCoroutine (TransitionToNextQuestion ());
	}
		
	public void CountDown(){
		time -= Time.deltaTime;
		timeText.text = ((int)time).ToString ();
	}

	public void ExitGame(){
		if (questionNumber < questions.Length) {
				CorduraManager.CM.TakeDamage (50f);
		}
		SceneController.SC.FadeAndLoadScene ("NewScene");
	}

		public void ReinicioNivel(){
			questionNumber = 0;
			
		}
	}


