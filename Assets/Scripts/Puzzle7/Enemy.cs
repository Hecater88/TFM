using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Enemy : MonoBehaviour {

    // Velocidad de los noodles enemigos
    public float speed;

    // Hacemos que el objeto se vaya moviendo a la izquierda con la velocidad dada por la variable y el time delta
    private void Update() {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag("Player")) {
            CorduraManager.CM.TakeDamage(5f);

        }

        // Si el otro objeto con el que hace trigger tiene el mismo tag que el propio objeto
        if (other.tag == gameObject.tag) {
            // Destruye el objeto propio y el otro objeto que será el proyectil
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else {
            CorduraManager.CM.TakeDamage(10f);

        }
    }
}
