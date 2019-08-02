using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NameSpace para serializar en binario.
using System.Runtime.Serialization.Formatters.Binary;

//NameSpace para guardar y leer ficheros (Input Output).
using System.IO;

public class DataManager : MonoBehaviour {

	//Objeto que contendrá toda la información de la partida, incluidas las condiciones.
	public Data data;

	public bool doNotLoadForDebug;
	//Nombre que tomará el fichero guardado.
	public string fileName = "data.dat";

	//Referencia estática.
	public static DataManager DManager;

	void Awake(){
		//Recuperamos la referencia estática.
		if (DManager == null) {
			DManager = GetComponent<DataManager> ();
			//Recuperamos toda la información guardada en el disco al inicio del DataManager.
			if(!doNotLoadForDebug){
				Load ();
			}
		}

	}

	/// <summary>
	/// Guardado de la información al disco.
	/// </summary>
	public void Save(){
		//Objeto usado para serializar / deserializar.
		BinaryFormatter bf = new BinaryFormatter ();

		//Creamos el fichero, o lo sobreescribimos si ya existe.
		FileStream file = File.Create (Application.persistentDataPath + "/" + fileName);

		//File.Delete (Application.persistentDataPath + "/" + fileName);

		//Serializamos el archivo con la informacion que contiene data.
		bf.Serialize (file, data);

		//Cerramos el archivo.
		file.Close ();
	}

	/// <summary>
	/// Recupera la información del disco.
	/// </summary>
	public void Load(){
		//Ruta de guardado del fichero de datos, para poder acceder fácilmente a ella.
		Debug.Log (Application.persistentDataPath + "/" + fileName);

		//Si el fichero de guardado existe, 
		if (File.Exists (Application.persistentDataPath + "/" + fileName)) {
			//Objeto usado para serializar / deserializar.
			BinaryFormatter bf = new BinaryFormatter ();

			//Abrimos el fichero para lectura.
			FileStream file = File.Open (Application.persistentDataPath + "/" + fileName, FileMode.Open);

			//Deserializamos el contenido del fichero de datos y lo volcamos a data.
			data = (Data)bf.Deserialize (file);

			//Cerramos el fichero.
			file.Close();
		}
	}

	/// <summary>
	/// Devuelve el estado en el que se encuentra la condición recibida como parámetro.
	/// </summary>
	/// <returns><c>true</c>, if condition was checked, <c>false</c> otherwise.</returns>
	/// <param name="conditionName">Condition name.</param>
	public bool CheckCondition(string conditionName){
		//Recorremos todas las condiciones del array en busca de la condición buscada.
		foreach (Condition cond in data.allCondition) {
			//Si el nombre de la condición actual coincide,
			if (cond.name == conditionName) {
				//devolvemos el estado de dicha condición.
				return cond.done;
			}
		}

		//Aviso de que la condición no existe.
		Debug.LogWarning (conditionName + " no existe");
		return false;
	}

	/// <summary>
	/// Cambia el estado de una condición.
	/// </summary>
	/// <param name="conditionName">Condition name.</param>
	/// <param name="done">If set to <c>true</c> done.</param>
	public void SetCondition(string conditionName, bool done){
		//Recorremos todas las condiciones del array en busca de la condición buscada.
		foreach (Condition cond in data.allCondition) {
			//Si encontramos la condición,
			if (cond.name == conditionName) {
				//cambiamos su estado.
				cond.done = done;
				return;
			}
		}

		//Aviso de que la condición no existe.
		Debug.LogWarning (conditionName + " no existe");
	}
}
