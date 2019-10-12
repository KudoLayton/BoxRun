﻿using UnityEngine;
using System.Collections;

public class TutorialSelectMenu : MonoBehaviour {

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
		if(GUI.Button(new Rect(maincamera.pixelWidth*0.5f-80f, maincamera.pixelHeight*0.5f-21f, 160f, 42f), "Tutorial 1", buttonstyle)){
			Destroy (GameObject.Find("StartBGM"));
			Application.LoadLevel(6);
		}
		if(GUI.Button(new Rect(maincamera.pixelWidth*0.5f-83f, maincamera.pixelHeight*0.6f-21f, 166f, 42f), "Tutorial 2", buttonstyle)){
			Destroy (GameObject.Find("StartBGM"));
			Application.LoadLevel(7);
		}
		if(GUI.Button(new Rect(maincamera.pixelWidth*0.5f-83f, maincamera.pixelHeight*0.7f-21f, 166f, 42f), "Tutorial 3", buttonstyle)){
			Destroy (GameObject.Find("StartBGM"));
			Application.LoadLevel(8);
		}
		if(GUI.Button(new Rect(maincamera.pixelWidth*0.5f-110f, maincamera.pixelHeight*0.8f-21f, 220f, 42f), "LevelSelect", buttonstyle)){
			DontDestroyOnLoad(GameObject.Find("StartBGM"));
			GameObject.Find("StartBGM").tag = "UsingStartSound";
			Application.LoadLevel(2);
		}
	}
}
