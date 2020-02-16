using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BotonesPreguntas : MonoBehaviour {

	public GameObject controller;
	private UI UI;
	Animation rabbit;
	private Button botonOpciones;
	void start(){
		controller = GameObject.Find("ControladorJuego");
		UI = controller.gameObject.GetComponent<UI>();
		
	}
	public void volver(){

		Application.LoadLevel("menu");				
	}
	public void cine(){
		UI.ui.Categoria = "cine";
		Application.LoadLevel("preguntas");				
	}
	
	public void deportes(){
		UI.ui.Categoria = "deportes";
		Application.LoadLevel("preguntas");		
	}
	public void historia(){
		UI.ui.Categoria = "historia";
		Application.LoadLevel("preguntas");		
	}
	public void literatura(){
		UI.ui.Categoria = "literatura";
		Application.LoadLevel("preguntas");		
	}
	public void informatica(){
		UI.ui.Categoria = "informatica";
		Application.LoadLevel("preguntas");		
	}
	public void musica(){
		UI.ui.Categoria = "musica";
		Application.LoadLevel("preguntas");		
	}

	public void abrirOpciones(){
		GameObject cameraObj;
		cameraObj = GameObject.Find("PanelOpciones").gameObject;
		cameraObj.GetComponent<Animation>().Play("opcionfromleft");
	}

	public void cerrarOpciones(){
		GameObject cameraObj;
		cameraObj = GameObject.Find("PanelOpciones").gameObject;
		cameraObj.GetComponent<Animation>().Play("opciontoleft");
	}
	// Update is called once per frame
	void Update () {
	
	}
}
