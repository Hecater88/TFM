using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SlidingBlocks : MonoBehaviour {
	//Referencia al boton del canvas
	public Button finish;
	//Referencia a la imagen
	public Texture2D image;
	//Numero de bloques por linea
	public int blocksPerLine = 4;
	//numero de movientos para mezclar el puzzle
	public int shuffleLength = 20;
	//Duracion del movimiento del bloque
	public float defaultMoveDuration = 0.2f;
	//Duracion del movimiento del bloque cuando se esta mezclando
	public float shuffleMoveDuration = 0.1f;
	//Referenci al texto
	public Text timeText;
	//Variable para el tiempo
	public float time;
	//Diferentes estados del puzzle
	enum PuzzleState{Initiation,Solved, Shuffling, InPlay, GameOver};
	PuzzleState state;
	//Referencia al script Block
	Block emptyBlock;
	//Array de Blocks
	Block[,] blocks;
	//Lista de Bloques
	Queue <Block> inputs;
	//Booleana para controlar si el bloque se esta moviendo
	bool blockIsMoving;
	//variable que indica los movientos de mezclan que quedan por producirse
	int shuffleMovesRemaining;
	//variable que indica las coordenadas del bloque vacio anterior
	Vector2Int prevShuffleOfffset;

	void Start () {
		CreatePuzzle ();
		// Ejecutamos la música de puzzle
		FindObjectOfType<AudioManager>().Play("Puzzle");
		// Y paramos el maintheme
		FindObjectOfType<AudioManager>().Stop("Caketown");
	}

	void Update(){
		if (Input.GetKey(KeyCode.DownArrow)) {
			DataManager.DManager.SetCondition ("PipeCondition", true);
			SceneController.SC.FadeAndLoadScene ("PlazaCentral3D");
		}
		//si el puzzle está en modo inicial
		if (state == PuzzleState.Initiation ) {
			//empieza a mezclarse
			StartShuffle ();
		}
		//si el puzzle esta en modo play
		if (state == PuzzleState.InPlay) {
			//cuenta atras
			time -= Time.deltaTime;
			timeText.text = ((int)time).ToString ();
		}
		//si el tiempo llega a 0
		if (time <= 0) {
			//fin del juego
			EndGamePuzzle2 ();
		}
	}

	/// <summary>
	/// Creates the puzzle.
	/// </summary>
	void CreatePuzzle(){
		//Biarray de bloques
		blocks = new Block[blocksPerLine, blocksPerLine];
		//Variable temporal de una imagen para cortarla
		Texture2D[,] imageSlices = ImageSlicer.GetSlices (image, blocksPerLine);
		//Recorremos el biarray
		for (int i = 0;	i < blocksPerLine; i++) {
			for (int j = 0; j < blocksPerLine; j++) {

				//Declaramos una variable temporal para crear quads.
				GameObject blockObject = GameObject.CreatePrimitive (PrimitiveType.Quad);
				//posicionamos los quads en orden, empezando en la esquina inferior izquierda de la imagen
				blockObject.transform.position = -Vector2.one * (blocksPerLine - 1) * 0.5f + new Vector2(j,i);
				//hacemos que el padre tenga el mismo transform para segurarnos de que se colocan bien
				blockObject.transform.parent = transform;
				//Declaramos otra variable temporal y le asignamos a "blockObject" el sript Block
				Block block = blockObject.AddComponent <Block> ();
				//Cuando pulsamos el bloque,activamos el metodo PlayerMoveBlockInput
				block.OnBlockPressed += PlayerMoveBlockInput;
				//Cuando acabe el movimiento del bloque, activamos el metodo OnBlockFinishedMoving
				block.OnFinishedMoving += OnBlockFinishedMoving;
				//Introdce las coordenadas e imagen a cada bloque
				block.Init (new Vector2Int (j, i), imageSlices[j,i]);
				blocks [j, i] = block;

				//El bloque que esta en la esquina inferior derecha desaparecerá, para permitir los movientos de los demas bloques.
				if (i == 0 && j == blocksPerLine - 1) {
					emptyBlock = block;
				}
			}
		}

		//Posicionamos la camara en base al numero de bloques que hay
		Camera.main.orthographicSize = blocksPerLine * 0.55f;
		//Pone en cola los click de raton
		inputs = new Queue <Block> ();
	}

	/// <summary>
	/// Players the move block input.
	/// </summary>
	/// <param name="blockToMove">Block to move.</param>
	void PlayerMoveBlockInput (Block blockToMove){
		//Si el puzzle esta en estado InPLay
		if (state == PuzzleState.InPlay) {
			//metemos en cola los scripts Block
			inputs.Enqueue (blockToMove);
			//y activamos el metodo
			MakeNextPlayerMove ();
		}
	}

	/// <summary>
	/// Activa el siguiente moviento del player
	/// </summary>
	void MakeNextPlayerMove(){
		//mientras los inputs sean mayor que 0 y el bloque no se este moviendo
		while (inputs.Count > 0 && !blockIsMoving) {
			//activamos el metodo MoveBlock
			MoveBlock (inputs.Dequeue (), defaultMoveDuration);
		}
	}

	/// <summary>
	/// Mueve el bloque
	/// </summary>
	/// <param name="blockToMove">Block to move.</param>
	/// <param name="duration">Duration.</param>
	void MoveBlock (Block blockToMove, float duration){
		//Si la longitud entre el bloque que queremos mover y el bloque vacio es igual a 1
		if ((blockToMove.coord - emptyBlock.coord).sqrMagnitude == 1) {
			//El bloque seleccionado se convierte en un bloque vacio
			blocks[blockToMove.coord.x, blockToMove.coord.y] = emptyBlock;
			//EL bloque seleccionado se mueve hacia el bloque vacio
			blocks [emptyBlock.coord.x, emptyBlock.coord.y] = blockToMove;

			//variable temporal que guarda las coordenadas del bloque vacio
			Vector2Int targetCoord = emptyBlock.coord;
			//igualamos las coordenadas del bloque vacio al bloque que quermos moverlo
			emptyBlock.coord = blockToMove.coord;
			//coordenadas en la que queremos mover el bloque
			blockToMove.coord = targetCoord;
			//variable temporal que guarda las posicion del bloque vacio
			Vector2 targetPosition = emptyBlock.transform.position;
			//convertimos la posicion que estaba el bloque que queriamos mover, en un bloque vacio
			emptyBlock.transform.position = blockToMove.transform.position;
			//movemos el bloque
			blockToMove.MoveToPosition (targetPosition, duration);
			blockIsMoving = true;
		}
	}

	/// <summary>
	/// Metodo que controla el estado del puzzle
	/// </summary>
	void OnBlockFinishedMoving(){
		blockIsMoving = false;
		CheckIfSolved ();

		//Si el puzzle esta en modo Inplay
		if (state == PuzzleState.InPlay) {
			MakeNextPlayerMove ();
		}
		//Si el puzzle esta en modo Shuffling
		else if (state == PuzzleState.Shuffling){
			//mezclamos el puzzle
			if (shuffleMovesRemaining > 0) {
				MakeNextShuffleMove ();
			} else {
				//ponemos el puzzle en modo Inplay
				state = PuzzleState.InPlay;

			}
		}
	}

	/// <summary>
	/// Metodo para mezclar el puzzle
	/// </summary>
	void StartShuffle() {
		//ponemos el puzzle en modo Shuffling
		state = PuzzleState.Shuffling;
		//Introducimos le numero de movientos para mezclar el puzzle
		shuffleMovesRemaining = shuffleLength;
		//Desactivamos el bloque para que se convierta en un puzzle vacio
		emptyBlock.gameObject.SetActive (false);
		//activamos el siguiente moviento del puzzle
		MakeNextShuffleMove();
	}

	/// <summary>
	///Metodo que activa el siguiente moviento del puzzle
	/// </summary>
	void MakeNextShuffleMove(){
		//posicion de los 4 Bloques que hay alrededor del bloque vacio
		Vector2Int[] offsets = { new Vector2Int (1, 0), new Vector2Int (-1, 0), new Vector2Int (0, 1), new Vector2Int (0, -1) };
		//variable random, que elige uno de los 4 bloques
		int randomIndex = Random.Range (0, offsets.Length);

		//recorremos los 4 bloques
		for (int i = 0; i < offsets.Length; i++) {
			//Selecionamos uno de los 4 bloques al azar
			Vector2Int offset = offsets [(randomIndex + i) % offsets.Length];
			//si el bloque seleccionado no fue mezclado
			if (offset != prevShuffleOfffset * -1) {
				//instanciamos las coordenadas para mover el bloque seleccionado
				Vector2Int moveBlockCoord = emptyBlock.coord + offset;
				//si las coordenadas de los bloques no salen del rango
				if (moveBlockCoord.x >= 0 && moveBlockCoord.x < blocksPerLine && moveBlockCoord.y >= 0 && moveBlockCoord.y < blocksPerLine) {
					//movemos los bloques
					MoveBlock (blocks [moveBlockCoord.x, moveBlockCoord.y], shuffleMoveDuration);
					//restamos el numero de movimientos para mezclar
					shuffleMovesRemaining--;
					//instanciamos las coordenadas del bloque vacio anterior
					prevShuffleOfffset = offset;
					break;
				}
			}
		}
			
	}

	/// <summary>
	/// Metodo que controla si el puzzle está resuelto
	/// </summary>
	void CheckIfSolved(){
		foreach (Block block in blocks) {
			//si algun bloque esta fuera de su posicion
			if (!block.IsAtStartingCoord ()) {
				//salimos del metodo
				return;
			}
		}
		//ponemos el puzzle en estado resuelto
		state = PuzzleState.Solved;
		//activamos el trozo que falta de la imagen
		emptyBlock.gameObject.SetActive (true);
		//deshabilitamos el boton de terminar
		finish.enabled = false;
		//Ponemos las condiciones del puzzle y del NPC  a true
		//DataManager.DManager.SetCondition ("Puzzle2", false);
		DataManager.DManager.SetCondition ("PipeCondition", true);
		//Salimos de la escena del puzzle, y cargamos nueva escena
		SceneController.SC.FadeAndLoadScene ("PlazaCentral3D");

	}
	/// <summary>
	/// Metodo para salir del puzzle
	/// </summary>
	public void EndGamePuzzle2(){
		//si el puzzle esta en estado play
		if(state == PuzzleState.InPlay){
			//Quitamos vida al jugador
			CorduraManager.CM.TakeDamage (25f);
		}
		//Salimos de la escena del puzzle, y cargamos nueva escena
		SceneController.SC.FadeAndLoadScene ("PlazaCentral3D");
		//ponemos el estado del puzzle en GameOver
		state = PuzzleState.GameOver;
	}
}
