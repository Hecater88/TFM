using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Condition {

	//Nombre de la condición e identificador de la misma. Debe ser único, ya que se localizará por éste.
	public string name;

	//Descripción de apoyo. Solo será usada en el inspector.
	public string description;

	//Para indicar si ha sido cumplida la condición.
	public bool done;
}
