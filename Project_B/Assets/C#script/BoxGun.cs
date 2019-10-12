using UnityEngine;
using System.Collections;


public class BoxGun : Box {
	public bool gunselect;//물질 총인지 반물질 총인지 여부에 대한 것, true면 물질총 false면 반물질 총
	public bool showgunstate;
	public GameObject black;
	public GameObject white;
	public GameObject anti;
	public GameObject shotselect;
	public GameObject whiteplane;
	public GameObject blackplane;
	public GameObject antiplane;
	public GUIText ShootingChance;
	private ArrayList boxlist;
	public Map_manager map_manager;
	public bool shootable;
	public enum direction {R, NR, N, NL, L};
	private ArrayList virtuallist;
	public direction gundir;
	enum wantinfo {row, col};
	public int bulletnum;
	public int shootnum;
	public bool controlshoot;
	public bool controlq;
	public bool controle;
	public bool virtualbox;
	public GameObject virblack;
	public GameObject virwhite;
	public GameObject viranti;
	// Use this for initialization
	void Start () {
		boxlist = new ArrayList();
		gundir = direction.N;
		virtualbox = false;
		gunselect = true;
		virtuallist = new ArrayList();
		if(showgunstate)
			drawstate(bulletnum, gunselect, shootnum);
	}
	
	// Update is called once per frame
	void Update () {
		gunselect = checkbox ();// 총 종류 확인
		if(gunselect){
			shotselect.GetComponent<TextMesh>().color = Color.white;
		}else{
			shotselect.GetComponent<TextMesh>().color = Color.black;
		}
		if(shootnum <= 0){
			shotselect.GetComponent<TextMesh>().color = Color.red;
			shootable = false;
		}
		changedir();
		changeshotselect();
		if(GameObject.Find("ShootNumText"))
			GameObject.Find("ShootNumText").GetComponent<GUIText>().text = "X " + shootnum;
		if(controlshoot && shootable && (shootnum > 0)){
			if(Input.GetMouseButtonDown(0))
				Shoot(gunselect, bulletnum, gundir);
		}
		if(Input.GetKeyDown("v")){
			virtualbox = !virtualbox;
		}
		if(showgunstate)
			drawstate(bulletnum, gunselect, shootnum);
	}

	void drawstate(int bulletlength, bool gunselect, int shootingchance){
		int i;
		for(i = 0; i < boxlist.Count; i++){
			GameObject box = (GameObject)boxlist[i];
			Destroy(box);
		}
		boxlist.Clear();
		if(gunselect){
			for(i = 0; i < bulletlength; i++){
				if(i%2 == 0){
					GameObject box;
					box = (GameObject)Instantiate(whiteplane, new Vector3(0.01f, 0.81f), Quaternion.identity);
					boxlist.Add(box);
					box.GetComponent<GUITexture>().pixelInset = new Rect(i*80f,0f ,80f, 80f);
				}else{
					GameObject box;
					box = (GameObject)Instantiate(blackplane, new Vector3(0.01f, 0.81f), Quaternion.identity);
					boxlist.Add(box);
					box.GetComponent<GUITexture>().pixelInset = new Rect(i*80f,0f ,80f, 80f);
				}
			}
		}else{
			for(i = 0; i < bulletlength; i++){
				GameObject box = (GameObject)Instantiate(antiplane, new Vector3(0.01f, 0.81f), Quaternion.identity);
				boxlist.Add(box);
				box.GetComponent<GUITexture>().pixelInset = new Rect(i*80f,0f ,80f, 80f);
			}
		}
		ShootingChance.pixelOffset = new Vector2(i*80f+10f, 0f);
		if(shootingchance < 1)
			ShootingChance.color = Color.red;
		else if(shootingchance < 2)
			ShootingChance.color = new Color(1f, 0.392f, 0);
		else if(shootingchance < 3){
			ShootingChance.color = Color.yellow;
		}
		else
			ShootingChance.color = Color.white;
	}

	bool checkbox(){
		RaycastHit h;//raycast 선언
		if (Physics.Raycast (myBox.transform.position, -transform.up , out h, 1F)) {
			if(h.collider.gameObject.tag == "white")
				return true;//밑에 있는 박스가 white 면 true
		}
		return false;//밑에 있는 박스가 white가 아니면 false
	}

	void changedir(){
		if(controlq && Input.GetKeyDown("q")&&!Input.GetKey("e")){
			gundir++;
			if(gundir <= direction.L)
				drawvirtualbox(gunselect, bulletnum, gundir);
		}
		if(controle && Input.GetKeyDown("e")&&!Input.GetKey("q")){
			gundir--;
			if(gundir >= 0)
				drawvirtualbox(gunselect, bulletnum, gundir);
		}
		if(gundir < 0){
			gundir = direction.L;
			drawvirtualbox(gunselect, bulletnum, gundir);
		}
		if(gundir > direction.L){
			gundir = direction.R;
			drawvirtualbox(gunselect, bulletnum, gundir);
		}
	}

	void changeshotselect(){
		switch(gundir){
		case direction.R:
			shotselect.transform.rotation = Quaternion.Euler(new Vector3(80f, 90f, 0));
			break;
		case direction.NR:
			shotselect.transform.rotation= Quaternion.Euler(new Vector3(80f, 45f, 0));
			break;
		case direction.N:
			shotselect.transform.rotation= Quaternion.Euler(new Vector3(80f, 0f, 0));
			break;
		case direction.NL:
			shotselect.transform.rotation= Quaternion.Euler(new Vector3(80f, -45f, 0));
			break;
		case direction.L:
			shotselect.transform.rotation= Quaternion.Euler(new Vector3(80f, -90f, 0));
			break;
		}
	}

	Vector3 Def_dir(direction dir){
		switch(dir){
		case direction.L:
			return new Vector3(-1f, 0, 0);
		case direction.N:
			return new Vector3(0, 0, 1f);
		case direction.NL:
			return new Vector3(-1f, 0, 1f);
		case direction.NR:
			return new Vector3(1f, 0, 1f);
		case direction.R:
			return new Vector3(1f, 0, 0);
		default:
			return Vector3.zero;
		}
	}

	public void deletevirtualboxlist(){
		GameObject a;
		for(int i = 0; i < virtuallist.Count; i++){
			a = (GameObject)virtuallist[i];
			Destroy(a);
		}
		virtuallist.Clear();
	}

	public void drawvirbox(){
		drawvirtualbox(gunselect, bulletnum, gundir);
	}

	void drawvirtualbox(bool bullet, int bulletnum, direction dir){
		if(shootnum<=0)
			return;
		Vector3 bulletdir;
		Vector3 bulletpos;
		int bullet_row;
		int bullet_col;
		RaycastHit h;
		deletevirtualboxlist();
		Physics.Raycast (myBox.transform.position, transform.up * (-1f), out h, 1F);
		bullet_row = h.collider.gameObject.GetComponent<Box>().row;
		bullet_col = h.collider.gameObject.GetComponent<Box>().col;
		bulletdir = Def_dir (dir);
		bulletpos = myBox.transform.position;
		for(int i =0; i < bulletnum; i++){
			bulletpos = bulletpos + bulletdir;
			bullet_row += (int) bulletdir.z;
			bullet_col += (int) bulletdir.x;
			if((bullet_row < 0) || (bullet_row > map_manager.map.Count-1))
				break;
			if((bullet_col < 0) || (bullet_col > ((ArrayList)map_manager.map[bullet_row]).Count-1))
				break;
			if( ((ArrayList)((ArrayList)map_manager.map[bullet_row])[bullet_col])[0]!=null){
				float realheight;
				int myheigh;
				myheigh = ((ArrayList)((ArrayList)map_manager.map[bullet_row])[bullet_col]).Count;
				realheight = ((GameObject)((ArrayList)((ArrayList)map_manager.map[bullet_row])[bullet_col])[myheigh-1]).transform.position.y;
				bulletpos.y = realheight + 1f;
			}else{
				bulletpos.y = 0f;
			}
			if(bullet){
				int detbulletcol;
				detbulletcol = (i+1)*(((int)Mathf.Abs(bulletdir.x))+((int)Mathf.Abs(bulletdir.z)));
				GameObject box;
				if(detbulletcol%2 == 0){
					box = (GameObject)Instantiate(virwhite, bulletpos, Quaternion.identity);
					virtuallist.Add(box);
				}else{
					box = (GameObject)Instantiate(virblack, bulletpos, Quaternion.identity);
					virtuallist.Add(box);
				}
			}else{
				GameObject box;
				box = (GameObject)Instantiate(viranti, bulletpos, Quaternion.identity);
				virtuallist.Add(box);
			}
		}
	}

	void Shoot(bool bullet, int bulletnum, direction dir){
		Vector3 bulletdir;
		Vector3 bulletpos;
		int bullet_row;
		int bullet_col;
		bool kinetic;
		RaycastHit h;
		myBox.GetComponent<Player_move>().movable = false;
		deletevirtualboxlist();
		shootnum--;
		myBox.GetComponent<AudioSource>().Play();
		Physics.Raycast (myBox.transform.position, transform.up * (-1f), out h, 1F);
		bullet_row = h.collider.gameObject.GetComponent<Box>().row;
		bullet_col = h.collider.gameObject.GetComponent<Box>().col;
		bulletdir = Def_dir (dir);
		bulletpos = myBox.transform.position;
		for(int i =0; i < bulletnum; i++){
			kinetic = false;
			bulletpos = bulletpos + bulletdir;
			bullet_row += (int) bulletdir.z;
			bullet_col += (int) bulletdir.x;
			if((bullet_row < 0) || (bullet_row > map_manager.map.Count-1))
				break;
			if((bullet_col < 0) || (bullet_col > ((ArrayList)map_manager.map[bullet_row]).Count-1))
				break;
			if( ((ArrayList)((ArrayList)map_manager.map[bullet_row])[bullet_col])[0]!=null){
				float realheight;
				int myheigh;
				myheigh = ((ArrayList)((ArrayList)map_manager.map[bullet_row])[bullet_col]).Count;
				realheight = ((GameObject)((ArrayList)((ArrayList)map_manager.map[bullet_row])[bullet_col])[myheigh-1]).transform.position.y;
				bulletpos.y = realheight + 1f;
			}else{
				bulletpos.y = 0f;
				kinetic = true;
			}
			if(bullet){
				int detbulletcol;
				detbulletcol = (i+1)*(((int)Mathf.Abs(bulletdir.x))+((int)Mathf.Abs(bulletdir.z)));
				GameObject box;
				if(detbulletcol%2 == 0){
					box = (GameObject)Instantiate(white, bulletpos, Quaternion.identity);
				}else{
					box = (GameObject)Instantiate(black, bulletpos, Quaternion.identity);
				}
				if(kinetic)
					box.rigidbody.isKinematic=true;
				map_manager.AddMap(bullet_row, bullet_col, box);
			}else{
				GameObject box;
				box = (GameObject)Instantiate(anti, bulletpos, Quaternion.identity);
				if(kinetic)
					box.rigidbody.isKinematic=true;
				map_manager.AddMap(bullet_row, bullet_col, box);
			}
		}
		myBox.GetComponent<Player_move>().movable = true;
		if(controlshoot && shootable && (shootnum > 0))
			drawvirtualbox(bullet, bulletnum, dir);
	}


}
