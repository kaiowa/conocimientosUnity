using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections;

public class preguntas : MonoBehaviour {
	public Text TextoPregunta;
	public Button btnRespu1;
	public Button btnRespu2;
	public Button btnRespu3;

	public GameObject controller;
	private UI UI;

	private GUIContent content;
	// Use this for initialization
	void Start () {

		controller = GameObject.Find("ControladorJuego");
		UI = controller.gameObject.GetComponent<UI>();

		string categoria = PlayerPrefs.GetString ("categoria");
		Debug.Log ("Categoria:"+UI.ui.Categoria);
		
		/*string conn = "URI=file:" + Application.dataPath + "/itrivial.sqlite"; //Path to database.

		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.
		ImagenNormal = Resources.Load("boton") as Image;
		SiguientePregunta ();*/

	}


	// Update is called once per frame

}
