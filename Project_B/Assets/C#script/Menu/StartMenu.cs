using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {

	public Camera maincamera;
	public GUIStyle buttonstyle;

	// Use this for initialization
	void Start () {
		if(GameObject.FindWithTag("UsingStartSound"))
			Destroy(GameObject.FindWithTag("UnusedStartSound"));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI(){
		if(GUI.Button(new Rect(maincamera.pixelWidth*0.5f-60f, maincamera.pixelHeight*0.45f-35f, 120f, 70f), "Play", buttonstyle)){
			DontDestroyOnLoad(GameObject.Find("StartBGM"));
			GameObject.Find("StartBGM").tag = "UsingStartSound";
			Application.LoadLevel(2);
		}
		if(GUI.Button(new Rect(maincamera.pixelWidth*0.5f-166f, maincamera.pixelHeight*0.6f-35f, 332f, 70f), "Key Explain", buttonstyle)){
			DontDestroyOnLoad(GameObject.Find("StartBGM"));
			GameObject.Find("StartBGM").tag = "UsingStartSound";
			Application.LoadLevel(51);
		}
		if(GUI.Button(new Rect(maincamera.pixelWidth*0.5f-177f, maincamera.pixelHeight*0.75f-35f, 354f, 70f), "Game Maker", buttonstyle)){
			DontDestroyOnLoad(GameObject.Find("StartBGM"));
			GameObject.Find("StartBGM").tag = "UsingStartSound";
			Application.LoadLevel (1);
		}
		if(GUI.Button(new Rect(maincamera.pixelWidth*0.5f-60f, maincamera.pixelHeight*0.9f-35f, 120f, 70f), "Exit", buttonstyle))
			Application.Quit();
	}
}
