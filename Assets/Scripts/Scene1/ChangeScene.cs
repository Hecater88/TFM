using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour {
	public string name;
	public void FadeAndChangeEscene(){
		SceneController.SC.FadeAndLoadScene (name);
	}
}
