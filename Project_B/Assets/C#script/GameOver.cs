using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
	public Camera cam;
	public GUIText deadcausetext;
	public GUIStyle buttonstyle;

	// Use this for initialization
	void Start () {
		switch(GameObject.Find("GameLog").GetComponent<GameLog>().dead){
		case GameLog.deadcause.breakplayer:
			deadcausetext.text = "The Prompt Box was broken.";
			break;
		case GameLog.deadcause.nobulletlength:
			deadcausetext.text = "There was no Bullet Length.";
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI(){
		if(GUI.Button(new Rect(cam.pixelWidth*0.5f-50f, cam.pixelHeight*0.4f-25f, 100f, 50f), "Replay?\n(r)", buttonstyle)|| Input.GetKeyDown("r")){
			int k;
			k = GameObject.Find("GameLog").GetComponent<GameLog>().thisstage;
			Destroy(GameObject.Find ("GameLog"));
			Application.LoadLevel(k);
		}
		if(GUI.Button(new Rect(cam.pixelWidth*0.5f-50f, cam.pixelHeight*0.6f-25f, 100f, 50f), "Level Select\n(l)", buttonstyle)|| Input.GetKeyDown("l")){
			DontDestroyOnLoad(GameObject.Find("StartBGM"));
			Destroy(GameObject.Find ("GameLog"));
			GameObject.Find("StartBGM").tag = "UsingStartSound";
			Application.LoadLevel(2);
		}
		if(GUI.Button(new Rect(cam.pixelWidth*0.5f-50f, cam.pixelHeight*0.8f-25f, 100f, 50f), "Exit\n(q)", buttonstyle)|| Input.GetKeyDown("q"))
			Application.Quit();
	}
}
