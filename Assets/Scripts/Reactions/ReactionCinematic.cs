using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionCinematic : Reaction {

	public bool ifCinematic;

	protected override IEnumerator React(){
		yield return new WaitForSeconds (delay);
		if(ifCinematic){
			Debug.Log ("Entra");
			TextManager.TM.StartCinematic ();
		}else{
			TextManager.TM.FinishCinematic ();

		}
	}
}
