using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data {

	//Nombre de la escena actual.
	public string actualScene;

	//Nombre del punto de entrada a la escena.
	public string startPosition;

	//Array con el estado de todas las condiciones.
	public Condition[] allCondition;

	//Array con el estado de todos los objetos del juego.
	public Item[] allItems;

	//Array con el inventario actual del ususario, definimos la dimensión inicial en función del número de slots.
	public Item[] inventory = new Item[4];

	//Cordura para el jugador
	public  float cordura = 100f;
}
