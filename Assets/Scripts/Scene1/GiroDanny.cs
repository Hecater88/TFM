﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiroDanny : MonoBehaviour {

	public float speed;
	void Start(){
		
		//TextManager.TM.FinishDialogue ();	
	}
	// Update is called once per frame
	void Update () {
		transform.Rotate (0f, 0f, speed);
	}
}
