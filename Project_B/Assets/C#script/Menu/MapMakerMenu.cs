using UnityEngine;
using System.Collections;

public class MapMakerMenu : MonoBehaviour {

	public Camera maincamera;
	public GUIStyle buttonstyle;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI(){
		if(GUI.Button(new Rect(maincamera.pixelWidth*0.5f-125f, maincamera.pixelHeight*0.9f-35f, 250f, 70f), "Main Menu", buttonstyle))
		Application.LoadLevel(0);
	}
}
