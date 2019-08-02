using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle4 : MonoBehaviour {
	
	public Text screen;
	//, number1, number2, number3, number4, number5;
	public bool isCorrect = false;
	private int[] password = new int[4];
	public int[] passForUnlock = new int[4];
	private int j = 0;
		
	// Use this for initialization
	void Start () {
		screen.text = "";
		password = new int[]{4,5,6,2};
		for (int i = 0; i <password.Length; i++) {
			//password [i] = Random.Range (0,9);
			Debug.Log (password[i]);
		}

		//StartCoroutine (DisplayNumbers());

	}
	
	// Update is called once per frame
	void Update () {
		if(j>4){
			j = 4;
		}
		if (Input.GetKey(KeyCode.DownArrow)) {
			DataManager.DManager.SetCondition ("OpenDoor1", true);
			SceneController.SC.FadeAndLoadScene ("DannysRoom2");
		}
	}

	public void WritePassword(int number){
		screen.text += ((int)number).ToString();
		passForUnlock [j] = number;
		j++;
	}

	public void ClearPassword(){
		screen.text = "";

		for (int i = 0; i <passForUnlock.Length; i++) {
			passForUnlock [i] = 0;
		}


		j = 0;
	}

	public void LookPassword(){
		for (int i = 0; i < password.Length; i++) {
			if (passForUnlock [i] == password [i]) {
				isCorrect = true;
			} else {
				isCorrect = false;
			}
		}

		ClearPassword ();

		if (isCorrect) {
			
			screen.text = "Correct";
			StartCoroutine (CompletePuzzle());
		} else {
			screen.text = "Incorrect";
		}

	}

	/*IEnumerator DisplayNumbers(){
		while (!isCorrect) {
			number1.text = ((int)password[3]).ToString();
			number2.text = ((int)password[4]).ToString();
			number3.text = ((int)password[0]).ToString();
			number4.text = ((int)password[1]).ToString();
			number5.text = ((int)password[2]).ToString();
			yield return new WaitForSeconds (5f);
			number1.text = ((int)password[1]).ToString();
			number2.text = ((int)password[3]).ToString();
			number3.text = ((int)password[2]).ToString();
			number4.text = ((int)password[0]).ToString();
			number5.text = ((int)password[4]).ToString();
			yield return new WaitForSeconds (5f);

		}
	}*/

	IEnumerator CompletePuzzle(){
		yield return new WaitForSeconds (1f);
		DataManager.DManager.SetCondition ("OpenDoor1", true);
		//Salimos de la escena del puzzle, y cargamos nueva escena
		SceneController.SC.FadeAndLoadScene ("DannysRoom2");
	}

	public void ExitGame(){
		if(!isCorrect){
			CorduraManager.CM.TakeDamage (25f);
		}
		SceneController.SC.FadeAndLoadScene ("DannysRoom2");
	}
}
