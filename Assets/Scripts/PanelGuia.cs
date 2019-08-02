using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelGuia : MonoBehaviour {
	//referencia al panel del canvas
	public GameObject panel;
	//booleana para controlar la activacion del panel
	bool active;
	/// <summary>
	/// Habilita o deshabilita el panel
	/// </summary>
	public void EnableDisableGuia(){
		active = !active;
		panel.SetActive(active);
	}
}
