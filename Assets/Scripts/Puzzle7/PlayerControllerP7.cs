using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerP7 : MonoBehaviour {


    // Creamos los proyectiles que serán lanzados por el player para destruir los noodles
    public GameObject[] projectiles;
	// Update is called once per frame
	void Update () {
        // Si pulsamos la tecla A
        if (Input.GetKeyDown(KeyCode.A)) {
            // Crearemos una instancia del proyectil 0 con la posición y la rotación del objeto
            Instantiate(projectiles[0], transform.position, Quaternion.identity);
            // y si pulsamos la W
        } else if (Input.GetKeyDown(KeyCode.W)) {
            // Instanciamos al proyectil 1
            Instantiate(projectiles[1], transform.position, Quaternion.identity);

        } else if (Input.GetKeyDown(KeyCode.D)) {
            // Instanciamos al proyectil 2
            Instantiate(projectiles[2], transform.position, Quaternion.identity);
        }
		
	}

}
