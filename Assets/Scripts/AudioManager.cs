using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
// Para usar la librería de unity sobre Audios
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;

    // Creamos una variable estática de nuestro AudioManager que hará de instancia, que nos servirá para que no se repita el audio manager de una escena con otra.
    public static AudioManager AM;

    // Use this for initialization
    void Awake () {
        // Si la referencia no existe...
        if (AM == null) {
            // ...la creamos
            AM = GetComponent<AudioManager>();
        } else { // Y si ya existe...
            // ...destruimos la otra
            Destroy(gameObject);
            return;
        }

        // Hacemos esto para que no destruya el objeto del AudioManager y permita continuar la música del juego
        //DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            // Estamos haciendo que todas las partes de este audio manager se relacionen con los clips de audio que se le van a meter, para que cuando cambiemos el pitch y el volumen, estos cambios se hagan en el propio clip del audio
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;


        }
	}

   void Start() {
       // Si las escenas son las indicadas por esos nombres; se ejecuta la música deseada y se para la del else, pero si las escenas no son ninguna de esas, se ejecuta Caketown y se para las de puzzle
        if (SceneManager.GetActiveScene().name == "Puzzle2" || SceneManager.GetActiveScene().name == "Puzzle3" ||SceneManager.GetActiveScene().name == "Puzzle4" || SceneManager.GetActiveScene().name == "Puzzle5" ||SceneManager.GetActiveScene().name == "Puzzle6" ||SceneManager.GetActiveScene().name == "Puzzle7" ||SceneManager.GetActiveScene().name == "Puzzle8" ){
		FindObjectOfType<AudioManager>().Play("Puzzle");
		FindObjectOfType<AudioManager>().Stop("Caketown");
        } else if  (SceneManager.GetActiveScene().name == "Testeo" || SceneManager.GetActiveScene().name == "Testeo1" ||SceneManager.GetActiveScene().name == "Testeo2") {        
		FindObjectOfType<AudioManager>().Stop("Caketown");
		FindObjectOfType<AudioManager>().Play("Comercial");
        } else if  (SceneManager.GetActiveScene().name == "ThaiGaryen") {        
		FindObjectOfType<AudioManager>().Stop("Caketown");
        FindObjectOfType<AudioManager>().Play("ThaiGaryen");
        } else if  (SceneManager.GetActiveScene().name == "Tester") {        
		FindObjectOfType<AudioManager>().Stop("Caketown");
        FindObjectOfType<AudioManager>().Play("Dungeon");
        } else if  (SceneManager.GetActiveScene().name == "Snobtown" ||SceneManager.GetActiveScene().name == "Fin") {        
		FindObjectOfType<AudioManager>().Stop("Caketown");
        FindObjectOfType<AudioManager>().Play("Mystical");
        } else if  (SceneManager.GetActiveScene().name == "GameOver") {        
		FindObjectOfType<AudioManager>().Stop("Caketown");
        FindObjectOfType<AudioManager>().Play("GameOver");
        } else {
		FindObjectOfType<AudioManager>().Stop("Puzzle");
		FindObjectOfType<AudioManager>().Play("Caketown");
        }
    }

    // Método que hace que el sonido con el nombre indicado se ejecute
    public void Play (string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
// Método que hace que el sonido con el nombre indicado se pare
public void Stop (string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }


}
