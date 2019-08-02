using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Referencia a la librería de Audio de Unity
using UnityEngine.Audio;

[System.Serializable]
public class Sound  {

    // Variable que se encargará de ejecutar el sonido
    [HideInInspector]
    public AudioSource source;


    // Nombre del audio que irá dentro del array
    public string name;


    // Variable que almacena el clip de audio del juego
    public AudioClip clip;
    
    // Variable que almacena el valor del volumen del sonido
    [RangeAttribute(0f, 1f)]
    public float volume;

    // Variable que almacena el valor del volumen del sonido
    [RangeAttribute(0.1f, 3f)]
    public float pitch;

    // Booleana que indicará si el sonido queremos que esté en loop
    public bool loop;

	
}
