using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ProgressBarScript : MonoBehaviour {

	public GameObject fondo;
	public Image barra;
	private Image foregroundImage;
	private float startingtime;


	// Use this for initialization
	void Start () {
		this.startingtime = 10;


	}
	

	
	public void actualizarBarra(){
		barra.fillAmount = barra.fillAmount-0.01f;
	
	}
	// Update is called once per frame
	void Update () {


		
	}
	

	

	
}
