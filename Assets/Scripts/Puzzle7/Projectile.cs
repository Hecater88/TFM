using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    // Velocidad a la que irá el proyectil
    public float speed;
	
	// Update is called once per frame
	void Update () {
        // Hacemos que el proyectil se mueva a la velocidad  del speed hacia la derecha por el tiempo del juego
        transform.Translate(Vector2.right * speed * Time.deltaTime);
	}
}
