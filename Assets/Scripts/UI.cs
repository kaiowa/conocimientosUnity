using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UI : MonoBehaviour {


	public static UI ui;

	private float _score;
	private int _lives;
	private int round;
	private PlayerPrefs jugador;

	public  int _totalPreguntas;

	public int _aciertos;
	public int _fallos;
	public string Categoria;

	public AudioClip clicksonido;
	public AudioClip fallosonido;
	public AudioClip aciertosonido;

	public Text vidastext;
	public TextMesh puntostext;
	public TextMesh totalpreguntasText;
	private GameObject botonsonido,botonayuda;

	public float Score {
		get {
			return _score;
		}
		set {
			_score = value;
		}
	}
	
	public int Lives {
		get {
			return _lives;
		}
		set {
			_lives = value;
		}
	}
	
	void Awake ()
	{

		if (ui == null) {	
			ui = this;
			DontDestroyOnLoad (transform.gameObject);
		}else if (ui!=null)
		{
			Destroy(gameObject);	
		}

	}
	public void SetCategoria(string Categoria){
		this.Categoria = Categoria;
	}


	public string GetCategoria(){
		return this.Categoria;

	}
	// Use this for initialization
	void Start () {
		Lives = 3;

		botonayuda = GameObject.Find ("ButtonHelp");
		botonsonido = GameObject.Find ("ButtonSound");

		this.Categoria = "cine";
		if (botonayuda) {
			botonayuda.SetActive (false);
		}
		if (botonsonido) {
			botonsonido.SetActive (false);
		}
		//vidastext.text = "x"+Lives.ToString ();
		//puntostext.text = "0";
		//playerSpawn = GameObject.Find ("PlayerSpawner").GetComponent<PlayerSpawner> ();

	}
	public void updatePreguntas(){
		this._totalPreguntas++;
		Debug.Log ("total preguntas:"+_totalPreguntas);
	}

	public void updateLives(){
		this._lives--;
		if (this._lives <= 0) {
			Debug.Log("juego terminadooo");
			Application.LoadLevel("final");
		}
		mostrar_lives ();

	}
	public void mostrar_lives()
	{
		String mtexto="";

		vidastext = GameObject.Find ("TextoVidas").GetComponent<Text>();
		if (_lives == 3) {
			mtexto="x3";
		}
		if (_lives == 2) {
			mtexto="x2";
		}
		if (_lives == 1) {
			mtexto="x1";
		}

		vidastext.text = mtexto;

	}
	public void clickAudio(){

		GetComponent<AudioSource>().PlayOneShot (clicksonido, 0.3f);
	}
	/*
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI ()
	{

		puntostext.text = Score.ToString();
		vidastext.text = "x"+Lives.ToString ();



	}
	public void StartDeath (GameObject player, GameObject other)
	{
		StartCoroutine (PlayerDeath (player, other));
	}
	
	IEnumerator PlayerDeath (GameObject player, GameObject other)
	{
		if (Lives > 0) {
			//Debug.Log("PlayerDeath Co-routine");
			//Destroy (player);

			Destroy (other); // other es el player
			GetComponent<AudioSource>().PlayOneShot (playerDeathSound);
			yield return new WaitForSeconds (2);
			Lives -= 1;
			yield return new WaitForSeconds (1);
			playerSpawn.Spawn();
		

		} else {
			Debug.Log ("menuuuu");
			yield return new WaitForSeconds (2);
			Application.LoadLevel (0);
		}
		
	}
	
	public void BombCheck ()
	{
		GameObject[] bombs = GameObject.FindGameObjectsWithTag ("coin") as GameObject[];
		int numberOfBombsLeft = bombs.Length - 1;

		GetComponent<AudioSource>().PlayOneShot (bombPickupSound, 0.3f);


		if (numberOfBombsLeft == 0) {
			
			if (Application.loadedLevel < 3) {
				GetComponent<AudioSource>().PlayOneShot (completedSound);
				Application.LoadLevel (Application.loadedLevel + 1);
			} else {
				//GetComponent<AudioSource>().PlayOneShot(endedSound);
				Application.LoadLevel ("Menu");
			}
		}
	}
	public void dibujarVidas()
	{
		

	}
*/

}
