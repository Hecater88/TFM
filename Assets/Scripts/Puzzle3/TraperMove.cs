using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class TraperMove : MonoBehaviour {
	public float speed;
	public int points;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward * Time.deltaTime*speed);
		Destroy (this.gameObject, 10f);
	}

	void OnMouseDown()
	{
		GameManagerPuzzle3.GMP3.TakeScore (points);
		Destroy (this.gameObject);
	}
}
