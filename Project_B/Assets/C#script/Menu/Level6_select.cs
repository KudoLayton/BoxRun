using UnityEngine;
using System.Collections;

public class Level6_select : MonoBehaviour {

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
		if(GUI.Button(new Rect(maincamera.pixelWidth*0.5f-82f, maincamera.pixelHeight*0.4f-21f, 164f, 42f), "Level 6-1", buttonstyle)){
			Destroy (GameObject.Find("StartBGM"));
			Application.LoadLevel(40);
		}
		if(GUI.Button(new Rect(maincamera.pixelWidth*0.5f-87f, maincamera.pixelHeight*0.5f-21f, 174f, 42f), "Level 6-2", buttonstyle)){
			Destroy (GameObject.Find("StartBGM"));
			Application.LoadLevel(41);
		}
		if(GUI.Button(new Rect(maincamera.pixelWidth*0.5f-87f, maincamera.pixelHeight*0.6f-21f, 174f, 42f), "Level 6-3", buttonstyle)){
			Destroy (GameObject.Find("StartBGM"));
			Application.LoadLevel(42);
		}
		if(GUI.Button(new Rect(maincamera.pixelWidth*0.5f-87f, maincamera.pixelHeight*0.7f-21f, 174f, 42f), "Level 6-4", buttonstyle)){
			Destroy (GameObject.Find("StartBGM"));
			Application.LoadLevel(43);
		}
		if(GUI.Button(new Rect(maincamera.pixelWidth*0.5f-87f, maincamera.pixelHeight*0.8f-21f, 174f, 42f), "Level 6-5", buttonstyle)){
			Destroy (GameObject.Find("StartBGM"));
			Application.LoadLevel(44);
		}
		if(GUI.Button(new Rect(maincamera.pixelWidth*0.5f-110f, maincamera.pixelHeight*0.9f-21f, 220f, 42f), "LevelSelect", buttonstyle)){
			DontDestroyOnLoad(GameObject.Find("StartBGM"));
			GameObject.Find("StartBGM").tag = "UsingStartSound";
			Application.LoadLevel(2);
		}
	}
}
