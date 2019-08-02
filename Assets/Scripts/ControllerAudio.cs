using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAudio : MonoBehaviour {

	// Use this for initialization
	void Start () {
				// Ejecutamos la música de puzzle
		FindObjectOfType<AudioManager>().Play("Puzzle");
		// Y paramos el maintheme
		FindObjectOfType<AudioManager>().Stop("Caketown");
	
	}
	

}
