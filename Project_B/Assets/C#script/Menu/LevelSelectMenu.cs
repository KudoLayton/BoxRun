using UnityEngine;
using System.Collections;

public class LevelSelectMenu : MonoBehaviour {

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
		if(GUI.Button(new Rect(maincamera.pixelWidth*0.4f-65f, maincamera.pixelHeight*0.4f-21f, 130f, 42f), "Tutorial", buttonstyle)){
			DontDestroyOnLoad(GameObject.Find("StartBGM"));
			GameObject.Find("StartBGM").tag = "UsingStartSound";
			Application.LoadLevel(5);
		}
		if(GUI.Button(new Rect(maincamera.pixelWidth*0.4f-62f, maincamera.pixelHeight*0.5f-21f, 124f, 42f), "Level 1", buttonstyle)){
			DontDestroyOnLoad(GameObject.Find("StartBGM"));
			GameObject.Find("StartBGM").tag = "UsingStartSound";
			Application.LoadLevel(9);
		}
		if(GUI.Button(new Rect(maincamera.pixelWidth*0.4f-66f, maincamera.pixelHeight*0.6f-21f, 132f, 42f), "Level 2", buttonstyle)){
			DontDestroyOnLoad(GameObject.Find("StartBGM"));
			GameObject.Find("StartBGM").tag = "UsingStartSound";
			Application.LoadLevel(15);
		}
		if(GUI.Button(new Rect(maincamera.pixelWidth*0.4f-66f, maincamera.pixelHeight*0.7f-21f, 132f, 42f), "Level 3", buttonstyle)){
			DontDestroyOnLoad(GameObject.Find("StartBGM"));
			GameObject.Find("StartBGM").tag = "UsingStartSound";
			Application.LoadLevel(21);
		}
		if(GUI.Button(new Rect(maincamera.pixelWidth*0.6f-66f, maincamera.pixelHeight*0.4f-21f, 132f, 42f), "Level 4", buttonstyle)){
			DontDestroyOnLoad(GameObject.Find("StartBGM"));
			GameObject.Find("StartBGM").tag = "UsingStartSound";
			Application.LoadLevel(27);
		}
		if(GUI.Button(new Rect(maincamera.pixelWidth*0.6f-66f, maincamera.pixelHeight*0.5f-21f, 132f, 42f), "Level 5", buttonstyle)){
			DontDestroyOnLoad(GameObject.Find("StartBGM"));
			GameObject.Find("StartBGM").tag = "UsingStartSound";
			Application.LoadLevel(33);
		}
		if(GUI.Button(new Rect(maincamera.pixelWidth*0.6f-66f, maincamera.pixelHeight*0.6f-21f, 132f, 42f), "Level 6", buttonstyle)){
			DontDestroyOnLoad(GameObject.Find("StartBGM"));
			GameObject.Find("StartBGM").tag = "UsingStartSound";
			Application.LoadLevel(39);
		}
		if(GUI.Button(new Rect(maincamera.pixelWidth*0.6f-66f, maincamera.pixelHeight*0.7f-21f, 132f, 42f), "Level 7", buttonstyle)){
			DontDestroyOnLoad(GameObject.Find("StartBGM"));
			GameObject.Find("StartBGM").tag = "UsingStartSound";
			Application.LoadLevel(45);
		}
		if(GUI.Button(new Rect(maincamera.pixelWidth*0.5f-105f, maincamera.pixelHeight*0.8f-21f, 210f, 42f), "User Making", buttonstyle)){
			Destroy(GameObject.Find("StartBGM"));
			Application.LoadLevel(3);
		}
		if(GUI.Button(new Rect(maincamera.pixelWidth*0.5f-90f, maincamera.pixelHeight*0.9f-21f, 180f, 42f), "Main Menu", buttonstyle)){
			DontDestroyOnLoad(GameObject.Find("StartBGM"));
			GameObject.Find("StartBGM").tag = "UsingStartSound";
			Application.LoadLevel(0);
		}
	}
}
