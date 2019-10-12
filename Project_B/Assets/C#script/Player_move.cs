using UnityEngine;
using System.Collections;

public class Player_move : Box {
	public GameObject player;
	public float time;
	public bool movable;
	public bool movecontrol;
	private enum movingpose{east, west, south, north};
	bool right;
	bool left;
	bool forward;
	bool back;
	bool moving;
	// Use this for initialization
	void Start () {
		moving = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(movable && movecontrol){
			movecheck ();
			move (right, left, forward, back);
		}
		if(!moving)
			myBox.transform.position = new Vector3(Mathf.Round(myBox.transform.position.x), myBox.transform.position.y, Mathf.Round(myBox.transform.position.z));
		Selfdestroy ();
	}

	new void Selfdestroy(){
		if (myBox.transform.position.y <= -height) {
			Destroy (myBox);
		}//떨어지면 자기및 타겟팅 삭제
	}

	public bool selfcheckdie(){
		RaycastHit h;
		if(Physics.Raycast (myBox.transform.position, -transform.up, out h, 1F)){
			if(h.collider.tag == "black" || h.collider.gameObject.tag == "white"){
				return true;
			}
			return false;
		}
		return false;
	}

	void movecheck(){
		RaycastHit h;
		right = true;
		left = true;
		forward = true;
		back = true;
		if (Physics.Raycast (myBox.transform.position, -transform.up, out h, 1F)) {
			if(h.collider.tag != "anti"){
				if(Physics.Raycast (myBox.transform.position, transform.right, out h, 1F)){
					if(h.collider.tag == "black" || h.collider.gameObject.tag == "white"){
						right = false;
					}
				}
				if(Physics.Raycast (myBox.transform.position, -transform.right, out h, 1F)){
					if(h.collider.tag == "black" || h.collider.gameObject.tag == "white"){
						left = false;
					}
				}
				if(Physics.Raycast (myBox.transform.position, transform.forward, out h, 1F)){
					if(h.collider.tag == "black" || h.collider.gameObject.tag == "white"){
						forward = false;
					}
				}
				if(Physics.Raycast (myBox.transform.position, -transform.forward, out h, 1F)){
					if(h.collider.tag == "black" || h.collider.gameObject.tag == "white"){
						back = false;
					}
				}
			}else{
				movable = false;
				right = false;
				left = false;
				forward = false;
				back = false;
			}
		}else{
			movable = false;
			right = false;
			left = false;
			forward = false;
			back = false;
		}
	}

	void move(bool right, bool left, bool forward, bool back){
		if (left && Input.GetKeyDown("a")&&(!Input.GetKey("s"))&&(!Input.GetKey("d")&&(!Input.GetKey("w")))) {
			myBox.GetComponent<BoxGun>().shootable = false;
			StartCoroutine(move (movingpose.east));
		}
		if(back &&Input.GetKeyDown("s")&&(!Input.GetKey("a"))&&(!Input.GetKey("d"))&&(!Input.GetKey("w"))){
			myBox.GetComponent<BoxGun>().shootable = false;
			StartCoroutine(move (movingpose.south));
		}
		if(right && Input.GetKeyDown("d")&&(!Input.GetKey("a"))&&(!Input.GetKey("s"))&&(!Input.GetKey("w"))){
			myBox.GetComponent<BoxGun>().shootable = false;
			StartCoroutine(move (movingpose.west));
		}
		if(forward && Input.GetKeyDown ("w")&&(!Input.GetKey("a"))&&(!Input.GetKey("d"))&&(!Input.GetKey("s"))){
			myBox.GetComponent<BoxGun>().shootable = false;
			StartCoroutine(move (movingpose.north));
		}//플레이어 이동
	}

	IEnumerator move(movingpose a){
		float speed = 1 / time;
		float road = 0;
		myBox.GetComponent<BoxGun>().shootable = false;
		switch (a) {
		case movingpose.east:
			movable = false;
			moving = true;
			do{
				player.transform.Translate(new Vector3(-speed, 0f, 0f));
				road += speed;
				yield return new WaitForSeconds (0.01f);
			}while(road + speed < 1f);//서서히 가는 애니메이션 완성
			player.transform.position = new Vector3(Mathf.Floor(player.transform.position.x), player.transform.position.y, player.transform.position.z);//제위치로 이동
			//if (Input.GetKey ("a")){
				//yield return StartCoroutine(move (movingpose.east));
			//}//계속 눌려있는지 확인
			movable = true;
			moving = false;
			movecheck();
			break;
		case movingpose.west:
			movable = false;
			moving = true;
			do{
				player.transform.Translate(new Vector3(speed, 0f, 0f));
				road += speed;
				yield return new WaitForSeconds (0.01f);
			}while(road + speed < 1f);//서서히 가는 애니메이션 완성
			player.transform.position = new Vector3(Mathf.Floor(player.transform.position.x)+1f, player.transform.position.y, player.transform.position.z);//제위치로 이동
			//if (Input.GetKey ("d")){
				//yield return StartCoroutine(move (movingpose.west));
			//}//계속 눌려있는지 확인
			movable = true;
			moving = false;
			movecheck ();
			break;
		case movingpose.north:
			moving = true;
			movable = false;
			do{
				player.transform.Translate(new Vector3(0f, 0f, speed));
				road += speed;
				yield return new WaitForSeconds (0.01f);
			}while(road + speed < 1f);//서서히 가는 애니메이션 완성
			player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, Mathf.Floor(player.transform.position.z)+1);//제위치로 이동
			//if (Input.GetKey ("w")){
				//yield return StartCoroutine(move (movingpose.north));
			//}//계속 눌려있는지 확인
			movable = true;
			moving = false;
			movecheck();
			break;
		case movingpose.south:
			moving = true;
			movable = false;
			do{
				player.transform.Translate(new Vector3(0f, 0f, -speed));
				road += speed;
				yield return new WaitForSeconds (0.01f);
			}while(road + speed < 1f);//서서히 가는 애니메이션 완성
			player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, Mathf.Floor(player.transform.position.z));//제위치로 이동
			//if (Input.GetKey ("s")){
				//yield return StartCoroutine(move (movingpose.south));
			//}//계속 눌려있는지 확인
			movable = true;
			moving = false;
			movecheck ();
			break;
		default:
			break;
		}
		myBox.GetComponent<BoxGun>().shootable = true;
		gameObject.GetComponent<BoxGun>().drawvirbox();
	}
}
