using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item {

	//Nombre del objeto.
	public string name;

	//Descripción del objeto. Interna para el desarrollador.
	public string description;

	//Nombre de la imagen que vamos a cargar. Estará dentro de la carpeta Resources.
	public string imageName;

	//Booleana para indicar si se ha conseguido o no el objeto. Esto será usado en el connjunto de AllItems, no en el propio inventario.
	public bool picked = false;
}
