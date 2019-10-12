using UnityEngine;
using System.Collections;

public class howtoplay : MonoBehaviour {
	public Camera maincamera;
	public GUIStyle buttonstyle;
	void OnGUI(){
		if(GUI.Button(new Rect(maincamera.pixelWidth*0.5f-125f, maincamera.pixelHeight*0.9f-35f, 250f, 70f), "Main Menu", buttonstyle))
			Application.LoadLevel(0);
	}
}
