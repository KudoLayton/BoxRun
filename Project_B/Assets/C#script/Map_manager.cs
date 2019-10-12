using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class Map_manager : MonoBehaviour {
	public GameObject black;
	public GameObject white;
	public GameObject anti;//해당 박스 프리팹 설정
	public GameObject plus;
	public GameObject minus;
	public GameObject left;
	public GameObject right;
	public GameObject front;
	public GUIText cleartext;
	public int wide;//맵 너비 결정
	public int length;//맵 길이 결정
	public float linetime;//맵 한줄이 떨어지는데 걸리는 시간
	public string map_name;
	public bool islinedelete;
	public bool isgodown;
	public bool win;
	public bool nextable;
	public Camera cam;
	public bool isnexttutorial;
	public GUIStyle smallbutton;
	enum playerstate {alive, clear, fail};
	// 전체 맵을 관장하는 ArrayList
	public ArrayList map;
	// Use this for initialization
	void Start () {
		if(GameObject.FindWithTag("UsingTutorialSound"))
			Destroy(GameObject.FindWithTag("UnusedTutorialSound"));
		if(GameObject.FindWithTag("UsingGameSound"))
			Destroy(GameObject.FindWithTag("UnusedGameSound"));
		if(GameObject.FindWithTag("UnusedGameSound"))
			if(GameObject.FindWithTag("UsingTutorialSound"))
				Destroy (GameObject.FindWithTag("UsingTutorialSound"));
		cleartext.enabled=false;
		win = false;
		map = new ArrayList();
		Mapmaking();
		if(islinedelete){
			StartCoroutine(Linedown());
		}
		GameObject.Find("Player").GetComponent<BoxGun>().drawvirbox();
	}
	//맵 생성
	void Mapmaking(){
		StreamReader mapdata = new StreamReader( new FileStream("..\\Map\\map_"+map_name+".txt", FileMode.Open));
		wide = int.Parse(mapdata.ReadLine());
		length = int.Parse(mapdata.ReadLine());
		GameObject.Find("Player").GetComponent<BoxGun>().bulletnum = int.Parse(mapdata.ReadLine());
		GameObject.Find("Player").GetComponent<BoxGun>().shootnum = int.Parse(mapdata.ReadLine());
		int width;//맵 크기 결정
		string s;
		int k;
		left.transform.position = new Vector3(-((float)wide)-0.5f, left.transform.position.y, left.transform.position.z);
		right.transform.position = new Vector3(((float)wide)+0.5f, right.transform.position.y, right.transform.position.z);
		front.transform.position = new Vector3(front.transform.position.x, front.transform.position.y, ((float)length)-0.5f);
		width = wide * 2 + 1;
		for (int i= 0; i < length; i++) {
			ArrayList col = new ArrayList();
			for (int j = 0; j < width; j++) {
				ArrayList height = new ArrayList();
				s = mapdata.ReadLine();
				string[] a = s.Split(' ');
				int q;
				for(q = 0; a[q] == ""; q++);
				k = int.Parse(a[q]);
					//물질 상자
				if(k>0){
					for(int l = 0; l < Mathf.Abs(k); l++){
						Vector3 vec = new Vector3 ((float)j - wide, (float)l, (float)i);
						GameObject box;
						if ((i * width + j + width + wide) % 2 == 0) {
							box = (GameObject)Instantiate(black, vec, Quaternion.identity);
						} else {
							box = (GameObject)Instantiate(white, vec, Quaternion.identity);
						}
						box.GetComponent<MatterBox>().isgodown=false;
						height.Add (box);
						if(height.Count == 1){
							box.rigidbody.isKinematic = true;
						}
						//빈 공간
					}
				}else if(k==0){
					height.Add (null);
					//반물질 상자
				}else if(k < 0){
					for(int l=0; l < Mathf.Abs(k); l++){
						Vector3 vec_ = new Vector3 ((float)j - wide, (float)l, (float)i);
						GameObject box_;
						box_ = (GameObject)Instantiate(anti, vec_, Quaternion.identity);
						box_.rigidbody.isKinematic = true;
						height.Add(box_);
						if(height.Count == 1){
							box_.rigidbody.isKinematic = true;
						}
					}
				}
				foreach(string itemsign in a){
					if(itemsign == "+"){
						GameObject item;
						item = (GameObject)Instantiate(plus, new Vector3((float)j - wide, (float)Mathf.Abs(k), (float)i), Quaternion.identity);
					}else if(itemsign == "-"){
						GameObject item;
						item = (GameObject)Instantiate(minus, new Vector3((float)j - wide, (float)Mathf.Abs(k), (float)i), Quaternion.identity);
					}
				}
				col.Add(height);
			}
			map.Add(col);
			Sync();
		}//초기맵 완성
	}
	//모든 맵의 좌표 정보를 다시 맞춤
	void Sync (){
		for(int i=0; i < map.Count; i++){
			for(int j=0; j < ((ArrayList)map[i]).Count; j++){
				for(int k=0; k < ((ArrayList)((ArrayList)map[i])[j]).Count; k++){
					if(((ArrayList)((ArrayList)map[i])[j])[k] != null){
						((GameObject)((ArrayList)((ArrayList)map[i])[j])[k]).GetComponent<Box>().row = i;
						((GameObject)((ArrayList)((ArrayList)map[i])[j])[k]).GetComponent<Box>().col = j;
						((GameObject)((ArrayList)((ArrayList)map[i])[j])[k]).GetComponent<Box>().hight = k;
					}
				}
			}
		}
	}
	//맵의 요소중 하나를 삭제함
	public void DeleteMap(int row, int col, int height){
		((ArrayList)((ArrayList)map[row])[col])[height]=null;
		if(((ArrayList)((ArrayList)map[row])[col]).Count > 1){
			((ArrayList)((ArrayList)map[row])[col]).RemoveAt(height);
		}
		Sync ();
	}
	//맵의 요소중 하나를 추가함
	public void AddMap(int row, int col, GameObject box){
		if(((ArrayList)((ArrayList)map[row])[col])[0] != null){
			((ArrayList)((ArrayList)map[row])[col]).Add (box);
		}else{
			((ArrayList)((ArrayList)map[row])[col])[0] = box;
		}
		Sync ();
	}
	//맵의 한 줄을 삭제함
	public void DeleteLine(int row){
		map.RemoveAt(row);
		Sync();
	}
	public void DeleteBottom(int row, int col){
		while(((ArrayList)((ArrayList)map[row])[col]).Count != 1){
			int i = ((ArrayList)((ArrayList)map[row])[col]).Count;
			((ArrayList)((ArrayList)map[row])[col]).RemoveAt(i-1);
		}
		((ArrayList)((ArrayList)map[row])[col])[0] = null;
	}
	IEnumerator Linedown(){
		while(map.Count > 0){
			for(int i =0; i<((ArrayList)map[0]).Count; i++){
				if(((ArrayList)((ArrayList)map[0])[i])[0]!=null){
					if(((GameObject)(((ArrayList)((ArrayList)map[0])[i])[0])).tag == "black"){
						((GameObject)((ArrayList)((ArrayList)map[0])[i])[0]).GetComponent<BlackBox>().cracked.GetComponent<Crackedbox>().detlefttime((int)linetime);
						if(!(((GameObject)((ArrayList)((ArrayList)map[0])[i])[0]).GetComponent<BlackBox>().cracked.GetComponent<Crackedbox>().isvibe)){
							((GameObject)((ArrayList)((ArrayList)map[0])[i])[0]).GetComponent<MeshRenderer>().enabled = false;
							((GameObject)((ArrayList)((ArrayList)map[0])[i])[0]).GetComponent<BlackBox>().cracked.GetComponent<Crackedbox>().isvibe = true;
						}
					}else if(((GameObject)(((ArrayList)((ArrayList)map[0])[i])[0])).tag == "white"){
						((GameObject)((ArrayList)((ArrayList)map[0])[i])[0]).GetComponent<WhiteBox>().cracked.GetComponent<Crackedbox>().detlefttime((int)linetime);
						if(!(((GameObject)((ArrayList)((ArrayList)map[0])[i])[0]).GetComponent<WhiteBox>().cracked.GetComponent<Crackedbox>().isvibe)){
							((GameObject)((ArrayList)((ArrayList)map[0])[i])[0]).GetComponent<MeshRenderer>().enabled = false;
							((GameObject)((ArrayList)((ArrayList)map[0])[i])[0]).GetComponent<WhiteBox>().cracked.GetComponent<Crackedbox>().isvibe = true;
					
						}
					}
				}
			}
			yield return new WaitForSeconds(linetime);
			for(int i=0; i<((ArrayList)map[0]).Count; i++){
				if(((ArrayList)((ArrayList)map[0])[i])[0]!=null){
					((GameObject)((ArrayList)((ArrayList)map[0])[i])[0]).rigidbody.isKinematic = false;
					((GameObject)((ArrayList)((ArrayList)map[0])[i])[0]).transform.Translate(new Vector3(0, -0.01f, 0));
				}
			}
			DeleteLine (0);
		}
	}

	playerstate playercheck(){
		if(GameObject.Find("Player")){
			if(GameObject.Find ("Player").GetComponent<BoxGun>().bulletnum <= 0 ){
				GameObject.Find ("GameLog").GetComponent<GameLog>().dead = GameLog.deadcause.nobulletlength;
				return playerstate.fail;
			}
			if(GameObject.Find("Player").GetComponent<Player_move>().selfcheckdie()){
				if(Mathf.RoundToInt(GameObject.Find("Player").transform.position.z) == length - 1){
					GameObject.Find("Player").GetComponent<Player_move>().movable = false;
					GameObject.Find("Player").GetComponent<BoxGun>().shootable = false;
					return playerstate.clear;
				}else{
					return playerstate.alive;
				}
			}else{
				return playerstate.alive;
			}
		}else{
			GameObject.Find ("GameLog").GetComponent<GameLog>().dead = GameLog.deadcause.breakplayer;
			return playerstate.fail;
		}
	}
	void gamecheck(playerstate ps){
		switch(ps){
		case playerstate.fail:
			if(GameObject.FindWithTag("UnusedTutorialSound"))
				Destroy(GameObject.FindWithTag("UnusedTutorialSound"));
			if(GameObject.FindWithTag("UsingTutorialSound"))
				Destroy(GameObject.FindWithTag("UsingTutorialSound"));
			if(GameObject.FindWithTag("UnusedGameSound"))
				Destroy(GameObject.FindWithTag("UnusedGameSound"));
			if(GameObject.FindWithTag("UsingGameSound"))
				Destroy(GameObject.FindWithTag("UsingGameSound"));
			DontDestroyOnLoad(GameObject.Find ("GameLog"));
			Application.LoadLevel("GameOver_page");
			break;
		case playerstate.clear:
			islinedelete = false;
			isgodown = false;
			GameObject.Find("Player").GetComponent<Player_move>().movable = false;
			GameObject.Find("Player").GetComponent<BoxGun>().shootable = false;
			win = true;
			cleartext.enabled=true;
			break;
		case playerstate.alive:
			break;
		}
	}

	void OnGUI(){
		if(cam){
			if(GUI.Button(new Rect(cam.pixelWidth*0.86f-90f, cam.pixelHeight*0.55f-25f, 180f, 50f), "Level Select\n(l)", smallbutton) || Input.GetKeyDown ("l")){
				if(isnexttutorial){
					if(GameObject.FindWithTag("UnusedTutorialSound"))
						GameObject.FindWithTag("UnusedTutorialSound").tag = "UsingTutorialSound";
					Destroy(GameObject.FindWithTag("UsingTutorialSound"));
				}else{
					if(GameObject.FindWithTag("UnusedGameSound"))
						GameObject.FindWithTag("UnusedGameSound").tag = "UsingGameSound";
					Destroy(GameObject.FindWithTag("UsingGameSound"));
				}
				Application.LoadLevel(2);
			}
			if(GUI.Button(new Rect(cam.pixelWidth*0.961f-50f, cam.pixelHeight*0.55f-25f, 100f, 50f), "Replay\n(r)", smallbutton)|| Input.GetKeyDown("r")){
				if(GameObject.FindWithTag("UnusedTutorialSound"))
					GameObject.FindWithTag("UnusedTutorialSound").tag = "UsingTutorialSound";
				if(GameObject.FindWithTag("UsingTutorialSound"))
					DontDestroyOnLoad(GameObject.FindWithTag("UsingTutorialSound"));
				if(GameObject.FindWithTag("UnusedGameSound"))
					GameObject.FindWithTag("UnusedGameSound").tag = "UsingGameSound";
				if(GameObject.FindWithTag("UsingGameSound"))
					DontDestroyOnLoad(GameObject.FindWithTag("UsingGameSound"));
				Application.LoadLevel(Application.loadedLevel);
			}
		}
		if(win){
			if(GUI.Button(new Rect(cam.pixelWidth*0.5f-50f, cam.pixelHeight*0.4f-25f, 100f, 50f), "Replay\n(r)", smallbutton)){
				if(GameObject.FindWithTag("UnusedTutorialSound"))
					GameObject.FindWithTag("UnusedTutorialSound").tag = "UsingTutorialSound";
				if(GameObject.FindWithTag("UsingTutorialSound"))
					DontDestroyOnLoad(GameObject.FindWithTag("UsingTutorialSound"));
				if(GameObject.FindWithTag("UnusedGameSound"))
					GameObject.FindWithTag("UnusedGameSound").tag = "UsingGameSound";
				if(GameObject.FindWithTag("UsingGameSound"))
					DontDestroyOnLoad(GameObject.FindWithTag("UsingGameSound"));
				Application.LoadLevel(Application.loadedLevel);
			}
			if(nextable){
				if(GUI.Button(new Rect(cam.pixelWidth*0.5f-80f, cam.pixelHeight*0.5f-25f, 160f, 50f), "Next Stage\n(n)", smallbutton) || Input.GetKeyDown ("n")){
					if(isnexttutorial){
						if(GameObject.FindWithTag("UnusedTutorialSound"))
							GameObject.FindWithTag("UnusedTutorialSound").tag = "UsingTutorialSound";
						DontDestroyOnLoad(GameObject.FindWithTag("UsingTutorialSound"));
					}else{
						if(GameObject.FindWithTag("UnusedGameSound"))
							GameObject.FindWithTag("UnusedGameSound").tag = "UsingGameSound";
						DontDestroyOnLoad(GameObject.FindWithTag("UsingGameSound"));
					}
					Application.LoadLevel(GameObject.Find("GameLog").GetComponent<GameLog>().nextstage);
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {
		gamecheck(playercheck());
	}
}
