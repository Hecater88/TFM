using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
	//metodos encapsulados que no devuelven valor
	public event System.Action<Block> OnBlockPressed;
	public event System.Action OnFinishedMoving;

	//variables que representan las corrdenadas de una dimension 2D usando integers
	public Vector2Int coord;
	Vector2Int startingCoord;

	/// <summary>
	/// Metodo que especifica las coordenadas de un bloque y la colocacion de la imagen
	/// </summary>
	/// <param name="startingCoord">Starting coordinate.</param>
	/// <param name="image">Image.</param>
	public void Init (Vector2Int startingCoord, Texture2D image){
		this.startingCoord = startingCoord;
		coord = startingCoord;

		GetComponent<MeshRenderer> ().material.shader = Shader.Find("Unlit/Texture");
		GetComponent<MeshRenderer> ().material.mainTexture = image;
	}
	/// <summary>
	/// Metodo que mueve el bloque
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="duration">Duration.</param>
	public void MoveToPosition (Vector2 target, float duration){
		StartCoroutine (AnimateMove (target, duration));
	}

	void OnMouseDown(){
		if (OnBlockPressed != null) {
			OnBlockPressed (this);
		}
	}

	/// <summary>
	/// Corrutina que activa el movimiento del bloque al pulsarlo
	/// </summary>
	/// <returns>The move.</returns>
	/// <param name="target">Target.</param>
	/// <param name="duration">Duration.</param>
	IEnumerator AnimateMove(Vector2 target, float duration){
		//guardamos la posicion inicial del bloque
		Vector2 initialPos = transform.position;
		//varibable para mover el bloque con la funcion Lerp
		float percent = 0;

		//mientras percent se menor que 0
		while (percent < 1) {
			//aumentamos la variable percent
			percent += Time.deltaTime / duration;
			//con la funcion Lerp hacemos que el bloque se mueva desde "initialPos"(posicion inicial) hasta el "target" (destino)
			transform.position = Vector2.Lerp (initialPos, target, percent);
			yield return null;
		}
		//si el while no devuelve null, activamos el metodo encapsulado
		if (OnFinishedMoving != null) {
			OnFinishedMoving ();
		}
	}
	/// <summary>
	/// Metodo para iniciar las coordenadas
	/// </summary>
	/// <returns><c>true</c> if this instance is at starting coordinate; otherwise, <c>false</c>.</returns>
	public bool IsAtStartingCoord(){
		return coord == startingCoord;
	}
}
