using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerPuzzle3 : MonoBehaviour {
	private float time;
	public float maxTime;
	public float delaySpawn;
	private float containerTime;
	private int score;

	public Text timeText;

	public Text scoreText;

	public GameObject[] spawns;
	public GameObject traperPrefab;

	public static GameManagerPuzzle3 GMP3;

	void Awake(){
		if(GMP3 == null){
			GMP3 = GetComponent<GameManagerPuzzle3> ();
		}	
	}
	// Use this for initialization
	void Start () {
		time = maxTime;
		containerTime = 0;
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		time -= Time.deltaTime;
		timeText.text = ((int)time).ToString ();
		scoreText.text = score.ToString ();

		if(Time.time > containerTime){
			containerTime = Time.time + delaySpawn;
			SpawnTraper ();
		}

		if(time <= 0){
			EndGamePuzzle3 ("GamePolisEdition");
		}
	}

	public void SpawnTraper(){
		int flipCoin = Random.Range (0, spawns.Length);
		Instantiate (traperPrefab, spawns [flipCoin].transform.position,spawns [flipCoin].transform.rotation);
	}

	public void TakeScore(int point){
		score += point;
	}
	public void EndGamePuzzle3(string name){
		SceneController.SC.FadeAndLoadScene (name);
	}
}
