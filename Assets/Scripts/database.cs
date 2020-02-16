using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using SQLite4Unity3d;
using UnityEngine;
using System.Data; 
using Mono.Data.Sqlite;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;


public class database : MonoBehaviour {

	private SQLiteConnection _connection;

	private int RespuCorrecta;

	private Image ImagenNormal;

	private GameObject ImagenCorrecto,ImagenFallo;
	private GUIContent content;
	public GameObject controller;
	public Image barra;
	// Use this for initialization
	private UI UI;
	private ProgressBarScript progressbar;
	private string ppregunta;
	private string prespuesta1,prespuesta2,prespuesta3;

	private GameObject mPregunta,panelCorrecto;
	private Button btn1,btn2,btn3;

	private float startingtime;
	private GameObject barraprogreso, panelCanvas;

	private int pararTimer;
	private Image barram;
	private GameObject TextoPreguntas;
	private int nPregunta;

	private string DatabaseName;
	public  IDbConnection dbconn;
	private string categoriaActiva;

	void Start () {
		this.startingtime = 10;
		this.DatabaseName = "itrivial.sqlite";

		controller = GameObject.Find("ControladorJuego");
		UI = controller.gameObject.GetComponent<UI>();
			
		ImagenCorrecto=GameObject.Find("ImgCorrecto");
		ImagenCorrecto.SetActive(false);

		ImagenFallo=GameObject.Find("ImgFallo");
		ImagenFallo.SetActive(false);

		barraprogreso = GameObject.Find ("barraprogreso");
		barram = GameObject.Find ("barra").GetComponent<Image>();

		panelCanvas = GameObject.Find ("Panel");
		panelCanvas.SetActive (false);

		this.nPregunta = 0;
		TextoPreguntas = GameObject.Find("TextoNpreguntas");
		TextoPreguntas.GetComponent<Text>().text= this.nPregunta.ToString();

		string conn = "URI=file:" + Application.dataPath + "/StreamingAssets/"+DatabaseName; //Path to database.


		ImagenNormal = Resources.Load("boton") as Image;
	

		this.mPregunta = GameObject.Find("TextoPregunta");

		this.btn1=GameObject.Find("Button1").GetComponent<Button>();
		this.btn2=GameObject.Find("Button2").GetComponent<Button>();
		this.btn3=GameObject.Find("Button3").GetComponent<Button>();


		openDatabase ();
		GameObject textoPreguntas=GameObject.Find("TxtAciertos");
		textoPreguntas.GetComponent<Text>().text="0";

		SiguientePregunta ();


	}

	public void openDatabase()
	{
		#if UNITY_EDITOR
		var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
		#else
		// check if file exists in Application.persistentDataPath
		var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);
		
		if (!File.Exists(filepath))
		{
			Debug.Log("Database not in Persistent path");
			// if it doesn't ->
			// open StreamingAssets directory and load the db ->
			
			#if UNITY_ANDROID 
			var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
			while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
			// then save to Application.persistentDataPath
			File.WriteAllBytes(filepath, loadDb.bytes);
			#elif UNITY_IOS
			var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
			// then save to Application.persistentDataPath
			File.Copy(loadDb, filepath);
			#elif UNITY_WP8
			var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
			// then save to Application.persistentDataPath
			File.Copy(loadDb, filepath);
			
			#elif UNITY_WINRT
			var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
			// then save to Application.persistentDataPath
			File.Copy(loadDb, filepath);
			#else
			var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
			// then save to Application.persistentDataPath
			File.Copy(loadDb, filepath);
			
			#endif
			
			Debug.Log("Database written");
		}
		
		var dbPath = filepath;
		#endif
		string conn2 = "URI=file:" + dbPath;
		
		dbconn = (IDbConnection) new SqliteConnection(conn2);
		dbconn.Open(); //Open connection to the database.

	}
	/**********************************
	 * CONSULTA PREGUNTA A LA BBDD
	 **********************************/
	void SiguientePregunta(){

		this.nPregunta++;
		TextoPreguntas.GetComponent<Text>().text= this.nPregunta.ToString();
	
		this.categoriaActiva = UI.ui.Categoria;

		this.startingtime = 11;
		Debug.Log (this.categoriaActiva);
		if (String.IsNullOrEmpty(this.categoriaActiva)) {

			this.categoriaActiva = "cine";
			
		} 
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = "SELECT cPregunta,cRespuesta1,cRespuesta2,cRespuesta3 " + "FROM "+this.categoriaActiva+" order by RANDOM()";
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		while (reader.Read())
		{
			this.ppregunta = reader.GetString(0);
			this.prespuesta1= reader.GetString(1);
			this.prespuesta2 = reader.GetString(2);
			this.prespuesta3 = reader.GetString(3);

		}

		UI.updatePreguntas();
		displayPregunta ();

	}



	/**********************
	 * muestra la pregunta
	 ********************/
	void displayPregunta()
	{
		this.pararTimer = 0;
		barram.fillAmount = 1;
		int _variable=UnityEngine.Random.Range(1,3);	
		panelCanvas.SetActive (false);
		this.mPregunta.GetComponentInChildren<Text>().text=this.ppregunta;


		actualizar_marcador_preguntas();
		this.btn1.onClick.RemoveAllListeners();
		this.btn2.onClick.RemoveAllListeners();
		this.btn3.onClick.RemoveAllListeners();

		if (_variable == 1) {

			RespuCorrecta=1;
			this.btn1.GetComponentInChildren<Text> ().text = this.prespuesta1;
			this.btn2.GetComponentInChildren<Text> ().text = this.prespuesta2;
			this.btn3.GetComponentInChildren<Text> ().text = this.prespuesta3;
			this.btn1.onClick.AddListener (() => { 
				responder ("correcto");				
			});

			this.btn2.onClick.AddListener (() => { 
				responder ("incorrecto");				
			});

			this.btn3.onClick.AddListener (() => { 
				responder ("incorrecto");				
			});


		}
		if (_variable == 2) {
			RespuCorrecta=2;
			this.btn1.GetComponentInChildren<Text> ().text = this.prespuesta2;
			this.btn2.GetComponentInChildren<Text> ().text = this.prespuesta1;
			this.btn3.GetComponentInChildren<Text> ().text = this.prespuesta3;

			this.btn1.onClick.AddListener (() => { 
				responder ("incorrecto");				
			});


			this.btn2.onClick.AddListener (() => { 
				responder ("correcto");				
			});
			
			this.btn3.onClick.AddListener (() => { 
				responder ("incorrecto");				
			});
		}

		if (_variable == 3) {
			RespuCorrecta=3;
			this.btn1.GetComponentInChildren<Text> ().text = this.prespuesta3;
			this.btn2.GetComponentInChildren<Text> ().text = this.prespuesta2;
			this.btn3.GetComponentInChildren<Text> ().text = this.prespuesta1;
	
			this.btn1.onClick.AddListener (() => { 
				responder ("incorrecto");				
			});
			
			this.btn2.onClick.AddListener (() => { 
				responder ("incorrecto");				
			});
			
			this.btn3.onClick.AddListener (() => { 
				responder ("correcto");				
			});

		}
	
		//btn1.onClick.AddListener(() => this.responder("1"));
		//btn1.onClick.AddListener( responder("1") );

	}

	public void actualizar_marcador_preguntas(){
		GameObject textoPreguntas=GameObject.Find("TxtTotalPreguntas");
		textoPreguntas.GetComponent<Text>().text=UI.ui._totalPreguntas.ToString();

	}
	/***************************
	 * ENVIA LA RESPUESTA 
	 ***************************/
	public void responder(string respuesta){

		Debug.Log(respuesta);

		UI.ui.updateLives ();
		panelCanvas.SetActive (true);
		if (respuesta == "correcto") {
			UI.ui._aciertos++;
			//panelCorrecto.SetActive (true);
			StartCoroutine (MostrarPanelCorrecto(1));
		} else {
			UI.ui._fallos++;
			//panelCorrecto.SetActive (false);
			StartCoroutine (MostrarPanelCorrecto(0));

		}

		//SiguientePregunta ();
	}


	
	IEnumerator MostrarPanelCorrecto (int acierto)
	{
		if (acierto == 1) {
		
			GameObject textoPreguntas=GameObject.Find("TxtAciertos");
			textoPreguntas.GetComponent<Text>().text=UI.ui._aciertos.ToString();
			ImagenCorrecto.SetActive (true);
			GameObject cameraObj;
			cameraObj = GameObject.Find("ImgCorrecto").gameObject;
			cameraObj.GetComponent<Animation>().Play("showfallo");


		} else {

			ImagenFallo.SetActive(true);
			GameObject cameraObj;
			cameraObj = GameObject.Find("ImgFallo").gameObject;
			cameraObj.GetComponent<Animation>().Play("showfallo");
				
		}
		//panelCorrecto.SetActive (true);
			this.pararTimer = 1;
			yield return new WaitForSeconds (1.3f);
		
			ImagenFallo.SetActive(false);
			ImagenCorrecto.SetActive (false);
		if (UI.ui._fallos < 3) {
			SiguientePregunta ();
		}

		
	}

	void CloseConnection(){

	}
	// Update is called once per frame
	void Update () {

		if (this.pararTimer == 0) {
			if(barram){

				barram.fillAmount = barram.fillAmount-0.1f;
				startingtime -= Time.deltaTime;

				barram.fillAmount = (float) startingtime/10f;
				//this.barra.fillAmount = barra.fillAmount-0.1f;

				if (Math.Round (startingtime) == 0) {
					responder ("incorrecto");

				}
			}
		}
	}
}
