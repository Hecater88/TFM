using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {
	
	public Texture2D baseTexture;
	public Texture2D andarTexture;
	public Texture2D dialogoTexture;
	
	public Texture2D interaccionTexture;

	//Determina si el cursor es renderizado por software o por hardware
	//si seleccionamos el modo "auto" hacemos que el cursor se renderice mediante hardware que soporte otras plataformas
	public CursorMode autoMode = CursorMode.Auto;

	public Vector2 tillingCursor = Vector2.zero;
	// Use this for initialization
	void Start(){
	Cursor.SetCursor(baseTexture, tillingCursor, autoMode);	
	}
	void OnMouseEnter(){
		if(gameObject.tag == "Interaccion"){
			Cursor.SetCursor(interaccionTexture, tillingCursor, autoMode);
		}else if(gameObject.tag == "Dialogo"){
			Cursor.SetCursor(dialogoTexture, tillingCursor, autoMode);
		}else if(gameObject.tag == "Andar"){
			Cursor.SetCursor(andarTexture, tillingCursor, autoMode);
		}
	}

	void OnMouseExit(){
		Cursor.SetCursor(baseTexture, tillingCursor, autoMode);
	}
}
