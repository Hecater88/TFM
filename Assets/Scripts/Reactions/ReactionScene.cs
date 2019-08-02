using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionScene : Reaction {

	//Nombre de la escena a la que vamos a cambiar.
	public string nextScene;

	protected override IEnumerator React(){
		yield return new WaitForSeconds (delay);

		//Llamámos al método de SceneController que lleva a cabo la tarea del cambio de escena.
		SceneController.SC.FadeAndLoadScene (nextScene);
	}
}
