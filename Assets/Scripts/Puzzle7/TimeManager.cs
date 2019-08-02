using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

    public float time = 30f;
    public Text timeText;
	// Comentario


	void Start (){
		
	}
	// Update is called once per frame
	void Update () {
        time -= Time.deltaTime;
		timeText.text = ((int)time).ToString ();

		if (Input.GetKey (KeyCode.DownArrow)) {
			DataManager.DManager.SetCondition ("PuzzleThai", true);
			SceneController.SC.LoadSceneWithoutFade ("ThaiGaryen");
		}

        if (time <= 0) {
            DataManager.DManager.SetCondition("PuzzleThai", true);
            SceneController.SC.FadeAndLoadScene("ThaiGaryen");
        }
	}
}
