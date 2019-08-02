using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactionDialogue: Reaction{

	public bool ifDialogue;

	protected override IEnumerator React(){
		yield return new WaitForSeconds (delay);
		if(ifDialogue){
			Debug.Log ("Entra");
			TextManager.TM.StartDialogue ();
		}else{
			TextManager.TM.FinishDialogue ();

		}
	}
}
