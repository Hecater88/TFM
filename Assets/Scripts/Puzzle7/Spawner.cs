using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public float startTimeBetweenSpawn;

    private float timeBetweenSpawn;

    public GameObject[] enemies;

    private void Update() {
        if (timeBetweenSpawn <= 0) {
            int rand = Random.Range(0, enemies.Length);
            Instantiate(enemies[rand], transform.position, Quaternion.identity);
            timeBetweenSpawn = startTimeBetweenSpawn;

        } else {

            timeBetweenSpawn -= Time.deltaTime;
        }

    }

}
