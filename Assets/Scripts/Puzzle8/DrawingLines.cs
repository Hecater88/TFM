using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class DrawingLines : MonoBehaviour {
private new Camera camera;

    Animator anim;
    GameObject yosoywapa;

public Material lineMaterial;
public float lineWidth;
public float depth =  5;

private Vector3? lineStartPoint = null;

public static DrawingLines DLines;
public bool pulsado = false;
public bool[] ifClicked = new bool[4];
    public bool cosido;
    private void Awake() {
        if (DLines == null) {
            DLines = GetComponent<DrawingLines>();
        }
    }

    
    void Start () {
		camera = GetComponent<Camera>();
        yosoywapa = GameObject.Find("YosoyWapas");
        anim = yosoywapa.gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        CheckCondition();
        DonePuzzle();

        if (Input.GetMouseButtonDown(0) && pulsado == false) {

            lineStartPoint = GetMousePoint();
            pulsado = true;
        } else if (Input.GetMouseButtonDown(0) == true){

			if(!lineStartPoint.HasValue){
				return;
			}
				
			var lineEndPoint = GetMousePoint();
			var gameObject = new GameObject();
			var lineRenderer = gameObject.AddComponent<LineRenderer>();
			lineRenderer.material = lineMaterial;
			//lineRenderer.positionCOunt = 2;
			lineRenderer.SetPositions(new Vector3[] { lineStartPoint.Value, lineEndPoint});
			lineRenderer.startWidth = lineWidth;
			lineRenderer.endWidth = lineWidth;

            pulsado = false;
			lineStartPoint = null;
		}
	}

	private Vector3 GetMousePoint(){
		var ray = camera.ScreenPointToRay(Input.mousePosition);
		return ray.origin + ray.direction * depth;
	}

    public void SetConditions( int number) {
        ifClicked[number] = true;
    }

    private void CheckCondition() {
        for (int i = 0; i< ifClicked.Length; i++) {
            if (ifClicked[i] == false) {
                return;
            }

            if (i == (ifClicked.Length-1) && ifClicked[i] == true) {
                cosido = true;
            }
        }





        
        
    }

    private void DonePuzzle() {
        if (cosido == true) {
            anim.SetTrigger("Juntar");
        }
    }

}
