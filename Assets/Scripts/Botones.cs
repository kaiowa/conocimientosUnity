using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class Botones : MonoBehaviour {


	public GameObject controller;
	private UI UI;
	private Object rabbit;
	private Button botonOpciones;

	void start(){
		controller = GameObject.Find("ControladorJuego");
		UI = controller.gameObject.GetComponent<UI>();

	
	}
	public void jugar(){

		Application.LoadLevel("categorias");
	}
	public void opciones(){
		//Application.LoadLevel("opciones");
	}
	public  void puntuaciones(){
		//Application.LoadLevel("puntuaciones");

	}

	public void cine(){

		PlayerPrefs.SetString ("categoria", "cine");
		UI.ui.Categoria = "cine";
		Application.LoadLevel("preguntas");

		
	}

	public void deportes(){
		Application.LoadLevel("preguntas");
			
	}


	public void abrirMenuOpciones()
	{
		GameObject cameraObj;
		cameraObj = GameObject.Find("submenuopciones").gameObject;
		cameraObj.GetComponent<Animation>().Play("AnimacionOpciones");
		
	}


}
