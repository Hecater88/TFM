using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionSound : Reaction {

	//[Header("")]
	//[Tooltip("")]

	//Clip de audio que va a ser reproducido.
	public AudioClip audioClip;

	//Referencia al componente audiosource.
	private AudioSource audioSource;

	void Start () {
		//Recuperamos la referencia al audiosource.
		audioSource = GetComponent<AudioSource> ();
	}

	/// <summary>
	/// Método que ejecuta la reacción, con override para que pise la corrutina heredada.
	/// </summary>
	protected override IEnumerator React ()	{
		//Realizamos la espera.
		yield return new WaitForSeconds (delay);

		//Asignamos el clip a reproducir.
		audioSource.clip = audioClip;

		//Reproducimos el clip.
		audioSource.Play();
	}
}