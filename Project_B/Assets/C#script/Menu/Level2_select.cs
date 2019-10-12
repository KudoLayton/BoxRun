using UnityEngine;
using System.Collections;

public class Level2_select : MonoBehaviour {

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
		if(GUI.Button(new Rect(maincamera.pixelWidth*0.5f-82f, maincamera.pixelHeight*0.4f-21f, 164f, 42f), "Level 2-1", buttonstyle)){
			Destroy (GameObject.Find("StartBGM"));
			Application.LoadLevel(16);
		}
		if(GUI.Button(new Rect(maincamera.pixelWidth*0.5f-85f, maincamera.pixelHeight*0.5f-21f, 170f, 42f), "Level 2-2", buttonstyle)){
			Destroy (GameObject.Find("StartBGM"));
			Application.LoadLevel(17);
		}
		if(GUI.Button(new Rect(maincamera.pixelWidth*0.5f-85f, maincamera.pixelHeight*0.6f-21f, 170f, 42f), "Level 2-3", buttonstyle)){
			Destroy (GameObject.Find("StartBGM"));
			Application.LoadLevel(18);
		}
		if(GUI.Button(new Rect(maincamera.pixelWidth*0.5f-85f, maincamera.pixelHeight*0.7f-21f, 170f, 42f), "Level 2-4", buttonstyle)){
			Destroy (GameObject.Find("StartBGM"));
			Application.LoadLevel(19);
		}
		if(GUI.Button(new Rect(maincamera.pixelWidth*0.5f-85f, maincamera.pixelHeight*0.8f-21f, 170f, 42f), "Level 2-5", buttonstyle)){
			Destroy (GameObject.Find("StartBGM"));
			Application.LoadLevel(20);
		}
		if(GUI.Button(new Rect(maincamera.pixelWidth*0.5f-110f, maincamera.pixelHeight*0.9f-21f, 220f, 42f), "LevelSelect", buttonstyle)){
			DontDestroyOnLoad(GameObject.Find("StartBGM"));
			GameObject.Find("StartBGM").tag = "UsingStartSound";
			Application.LoadLevel(2);
		}
	}
}
