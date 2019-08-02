using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueCollider : MonoBehaviour {

public bool trueColider = false;
 void OnTriggerEnter(Collider other){
	 if(Input.GetMouseButtonDown(0)){
		 trueColider = true;
	 }
 }
}
