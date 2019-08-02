using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactionText : Reaction {

	//[Header("")]
	//[Tooltip("")]

	//Mensaje que se mostrará.
	public string text;
	public string textName;

	public bool cinematic;
	public bool Danny, CottonLeft, CottonRight,Paolo, Tracer, Heineken, Fuertesco, Juan, StanLi, Turenmoshi;
	//Color del texto.
	public Color textColor = Color.black;

	protected override IEnumerator React(){
		yield return new WaitForSeconds (delay);
		TextManager.TM.ifCinematic = cinematic;
		TextManager.TM.Danny = Danny;
		TextManager.TM.CottonLeft = CottonLeft;
		TextManager.TM.CottonRight = CottonRight;
		TextManager.TM.Paolo = Paolo;
		TextManager.TM.Tracer = Tracer;
		TextManager.TM.Heineken = Heineken;
        TextManager.TM.Fuertesco = Fuertesco;
        TextManager.TM.Juan = Juan;
        TextManager.TM.StanLi= StanLi;
        TextManager.TM.Turenmoshi = Turenmoshi;
        //Llamamos al textmanager para que se haga cargo del dibujado del texto con el color indicado.
        TextManager.TM.DisplayMessage (TranslateManager.TM.GetString(text), textColor, textName);
	}
}
