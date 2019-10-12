using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class Tutorial_1 : MonoBehaviour {
	public GUIText text;
	public GUIText text_2;
	public GUIText shootnum;
	public GUIStyle buttonstyle;
	public Camera cam;
	public int stage;
	bool nextable;
	bool button;
	StreamReader scriptdata;
	// Use this for initialization
	void Start () {
		GameObject.Find("Player").GetComponent<Player_move>().movecontrol=false;
		GameObject.Find("Player").GetComponent<BoxGun>().controlshoot=false;
		shootnum.enabled = false;
		nextable = true;
		text_2.text = "";
		scriptdata = new StreamReader( new FileStream("..\\Script\\Script_tutorial_1.txt", FileMode.Open));
		text.text = scriptdata.ReadLine();
		stage = 0;
		button = false;
	}
	
	// Update is called once per frame
	void Update () {
		Stageselect();
		if(Input.GetKeyDown("space") && nextable)
			button = true;
	}

	void OnGUI(){
		if(nextable){
			if(GUI.Button(new Rect(cam.pixelWidth*0.75f-50f,cam.pixelHeight*0.37f-25f,100,50), "다음으로\n(Space Bar)", buttonstyle) || button){
				button = false;
				text_2.text = "";
				string s;
				string[] a;
				if(!scriptdata.EndOfStream){
					s = scriptdata.ReadLine();
					if(s.StartsWith("*")){
						nextable = false;
						s = (s.Split('*'))[1];
						GameObject.Find("Player").GetComponent<Player_move>().movecontrol = true;
						a = s.Split('/');
						text.text = a[0];
						if(a.Length > 1){
							text_2.text = a[1];
						}
						stage++;
					}else{
						a = s.Split('/');
						text.text = a[0];
						GameObject.Find("Player").GetComponent<Player_move>().movecontrol = false;
						GameObject.Find("Player").GetComponent<BoxGun>().controlshoot = false;
						if(a.Length > 1){
							text_2.text = a[1];
						}
					}
				}else{
					Debug.Log("법규");
				}
			}
		}
	}
	void Stageselect(){
		if(GameObject.Find ("Player")){
			switch(stage){
			case 1:
				if(new Vector3(2f, 1f, 4f) == new Vector3(Mathf.Round(GameObject.Find ("Player").transform.position.x), Mathf.Round(GameObject.Find ("Player").transform.position.y), Mathf.Round(GameObject.Find ("Player").transform.position.z))){
					stage++;
					nextable = true;
					GameObject.Find("Player").GetComponent<Player_move>().movecontrol = false;
					GameObject.Find("Player").GetComponent<BoxGun>().controlshoot = false;
					string s;
					string[] a;
					s = scriptdata.ReadLine();
					a = s.Split('/');
					text.text = a[0];
					text_2.text = "";
					if(a.Length > 1){
						text_2.text = a[1];
					}
				}
				break;
			case 3:
				GameObject.Find ("Player").GetComponent<BoxGun>().controlq = true;
				GameObject.Find ("Player").GetComponent<BoxGun>().controle = true;
				if(GameObject.Find("Player").GetComponent<BoxGun>().gundir == BoxGun.direction.L)
					GameObject.Find("Player").GetComponent<BoxGun>().controlshoot = true;
				if((GameObject.Find("Player").GetComponent<BoxGun>().controlshoot == true) && Input.GetMouseButtonDown(0)){
					stage++;
					GameObject.Find("Player").GetComponent<Player_move>().movecontrol = true;
					GameObject.Find("Player").GetComponent<BoxGun>().controlshoot = false;
					string s;
					string[] a;
					s = scriptdata.ReadLine();
					s = (s.Split('*'))[1];
					a = s.Split('/');
					text.text = a[0];
					text_2.text = "";
					if(a.Length > 1){
						text_2.text = a[1];
					}
				}
				break;
			case 4:
				if(new Vector3(-2f, 1f, 6f) == new Vector3(Mathf.Round(GameObject.Find ("Player").transform.position.x), Mathf.Round(GameObject.Find ("Player").transform.position.y), Mathf.Round(GameObject.Find ("Player").transform.position.z))){
					stage++;
					nextable = true;
					GameObject.Find("Player").GetComponent<Player_move>().movecontrol = false;
					GameObject.Find("Player").GetComponent<BoxGun>().controlshoot = false;
					string s;
					string[] a;
					s = scriptdata.ReadLine();
					a = s.Split('/');
					text.text = a[0];
					text_2.text = "";
					if(a.Length > 1){
						text_2.text = a[1];
					}
				}
				break;
			case 6:
				stage++;
				nextable = true;
				shootnum.enabled = true;
				GameObject.Find("Player").GetComponent<BoxGun>().showgunstate = true;
				GameObject.Find("Player").GetComponent<Player_move>().movecontrol = false;
				GameObject.Find("Player").GetComponent<BoxGun>().controlshoot = false;
				break;
			case 8:
				if(new Vector3(-2f, 1f, 7f) == new Vector3(Mathf.Round(GameObject.Find ("Player").transform.position.x), Mathf.Round(GameObject.Find ("Player").transform.position.y), Mathf.Round(GameObject.Find ("Player").transform.position.z))){
					stage++;
					nextable = true;
					GameObject.Find("Player").GetComponent<Player_move>().movecontrol = false;
					GameObject.Find("Player").GetComponent<BoxGun>().controlshoot = false;
					string s;
					string[] a;
					s = scriptdata.ReadLine();
					a = s.Split('/');
					text.text = a[0];
					text_2.text = "";
					if(a.Length > 1){
						text_2.text = a[1];
					}
				}
				break;
			case 10:
				if(new Vector3(-2f, 1f, 6f) == new Vector3(Mathf.Round(GameObject.Find ("Player").transform.position.x), Mathf.Round(GameObject.Find ("Player").transform.position.y), Mathf.Round(GameObject.Find ("Player").transform.position.z)) && GameObject.Find("Player").GetComponent<BoxGun>().gundir == BoxGun.direction.R)
					GameObject.Find("Player").GetComponent<BoxGun>().controlshoot = true;
				if((GameObject.Find("Player").GetComponent<BoxGun>().controlshoot == true) && Input.GetMouseButtonDown(0)){
					stage++;
					GameObject.Find("Player").GetComponent<Player_move>().movecontrol = true;
					GameObject.Find("Player").GetComponent<BoxGun>().controlshoot = false;
					string s;
					string[] a;
					s = scriptdata.ReadLine();
					s = (s.Split('*'))[1];
					a = s.Split('/');
					text.text = a[0];
					text_2.text = "";
					if(a.Length > 1){
						text_2.text = a[1];
					}
				}
				break;
			case 11:
				if(new Vector3(2f, 1f, 8f) == new Vector3(Mathf.Round(GameObject.Find ("Player").transform.position.x), Mathf.Round(GameObject.Find ("Player").transform.position.y), Mathf.Round(GameObject.Find ("Player").transform.position.z))){
					stage++;
					GameObject.Find("Player").GetComponent<Player_move>().movecontrol = false;
					GameObject.Find("Player").GetComponent<BoxGun>().controlshoot = false;
					string s;
					string[] a;
					s = scriptdata.ReadLine();
					s = (s.Split('*'))[1];
					a = s.Split('/');
					text.text = a[0];
					text_2.text = "";
					if(a.Length > 1){
						text_2.text = a[1];
					}
				}
				break;
			case 12:
				GameObject.Find ("Player").GetComponent<BoxGun>().controlq = true;
				GameObject.Find ("Player").GetComponent<BoxGun>().controle = true;
				if(GameObject.Find("Player").GetComponent<BoxGun>().gundir == BoxGun.direction.NL)
					GameObject.Find("Player").GetComponent<BoxGun>().controlshoot = true;
				if((GameObject.Find("Player").GetComponent<BoxGun>().controlshoot == true) && Input.GetMouseButtonDown(0)){
					stage++;
					GameObject.Find("Player").GetComponent<Player_move>().movecontrol = true;
					GameObject.Find("Player").GetComponent<BoxGun>().controlshoot = false;
					string s;
					string[] a;
					s = scriptdata.ReadLine();
					s = (s.Split('*'))[1];
					a = s.Split('/');
					text.text = a[0];
					text_2.text = "";
					if(a.Length > 1){
						text_2.text = a[1];
					}
				}
				break;
			case 13:
				if(new Vector3(-2f, 1f, 13f) == new Vector3(Mathf.Round(GameObject.Find ("Player").transform.position.x), Mathf.Round(GameObject.Find ("Player").transform.position.y), Mathf.Round(GameObject.Find ("Player").transform.position.z))){
					text.text = "";
					text_2.text = "";
				}
				scriptdata.Close();
				break;
			}
		}
	}
}
