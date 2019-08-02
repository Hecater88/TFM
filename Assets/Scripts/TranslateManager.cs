using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NameSpace necesario para poder hacer la lectura del XML.
using System.Xml;

public class TranslateManager : MonoBehaviour {

	//[Header("")]
	//[Tooltip("")]

	//Idioma por defecto.
	public string defaultLanguage = "English";

	//Listado de frases usando un Hashtable. Es un tipo de array pero que permite índices alfanuméricos. Un índice puede ser un carácter.
	public Hashtable strings;

	//Referencia estática.
	public static TranslateManager TM;

	void Awake () {
		//Recuperamos la referencia estática.
		if (TM == null) {
			TM = GetComponent<TranslateManager> ();
		}
	}

	void Start(){
		//Así obtenemos el idioma del sistema operativo. Lo guardamos dentro de un string.
		string language = Application.systemLanguage.ToString ();

		//Recuperamos el xml como texto.
		TextAsset textAsset = (TextAsset) Resources.Load ("lang", typeof(TextAsset));

		//Creamos una variable de tipo xmldocument para gestionar el XML.
		XmlDocument xml = new XmlDocument();

		//Cargamos el xml desde el texto.
		xml.LoadXml (textAsset.text);

		//Verificamos si existe el idioma.
		if (xml.DocumentElement [language] == null) {
			//Si no existe el idioma del sistema en las traducciones, usaremos el idioma por defecto.
			language = defaultLanguage;
		}

		//Llamamos al método que cargará los literales de texto en el hashtable.
		SetLanguage (xml, language);
	}

	public void SetLanguage(XmlDocument xml, string language){
		//Inicializamos el hashtable.
		strings = new Hashtable ();

		//Recuperamos el bloque con el idioma seleccionado.
		XmlElement element = xml.DocumentElement [language];

		//Si se ha podido recuperar el bloque,
		if (element != null) {
			//mediante este método, recuperamos un tipo enumerador que nos permite recorrer el XML como si fuese un foreach.
			IEnumerator elemEnum = element.GetEnumerator();

			//Mientras queden elementos por recorrer, se ejecutará el while.
			while (elemEnum.MoveNext()) {
				//Recuperamos el valor actual.
				XmlElement xmlItem = (XmlElement)elemEnum.Current;
				//Añadimos el literal actual en el hashtable, usando como índice el valor del atributo name.
				strings.Add (xmlItem.GetAttribute ("name"), xmlItem.InnerText);
			} 
		}else {
			Debug.LogWarning("El lenguaje especificado no existe.");
		}
	}
		
	/// <summary>
	/// Recupera el literal de texto de la hashtable mediante el índice.
	/// </summary>
	/// <returns>The string.</returns>
	/// <param name="name">Name.</param>
	public string GetString(string name){
		//Primero verificamos si no existe el índice.
		if (!strings.ContainsKey (name)) {
			Debug.LogWarning ("La cadena no existe " + name);
			return "";
		} else {
			//Si existe, devolvemos el valor del índice.
			return (string)strings [name];
		}
	}
}
