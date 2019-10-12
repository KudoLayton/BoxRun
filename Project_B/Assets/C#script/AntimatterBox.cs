using UnityEngine;
using System.Collections;

public class AntimatterBox : Box {
	bool deletable;
	// Use this for initialization
	void Start () {
		deletable = true;
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit h;//raycast 선언
		if (Physics.Raycast (myBox.transform.position, -transform.up, out h, 1F)) {
			if(h.collider.gameObject.tag == "Player" || h.collider.gameObject.tag == "black" || h.collider.gameObject.tag == "white"){
				myBox.rigidbody.isKinematic=false;
			}
			if(h.collider.gameObject.tag == "anti"){
				if(Vector3.Distance(myBox.transform.position, h.collider.gameObject.transform.position) > 1f){
					myBox.rigidbody.isKinematic = false;
				}
				myBox.rigidbody.isKinematic=true;
			}
		}else{
			if((myBox.transform.position.y > 0f) && (myBox.transform.position.y > hight)){
				myBox.rigidbody.isKinematic = false;
				myBox.transform.Translate(new Vector3(0f, -0.01f, 0f));
			}
			if((Mathf.Round(myBox.transform.position.y) == 0) && (hight == 0)){
				myBox.rigidbody.isKinematic = true;
				myBox.transform.position = new Vector3(myBox.transform.position.x , 0f, myBox.transform.position.z);
			}
		}
		if(myBox.rigidbody.isKinematic == true){
			myBox.transform.position = new Vector3(myBox.transform.position.x, Mathf.Round(myBox.transform.position.y), myBox.transform.position.z);
		}
		Selfdestroy ();
	}

	void OnTriggerEnter(Collider col){
		if ((col.gameObject.tag == "Player" || col.gameObject.tag == "black" || col.gameObject.tag == "white") && deletable){
			StartCoroutine (EatTarget(col.gameObject));//충돌 확인후 거리가 가까워질 때 삭제
		}
		if(col.gameObject.tag == "anti"){
			col.gameObject.transform.position = new Vector3(col.gameObject.transform.position.x, Mathf.Round(col.gameObject.transform.position.y), col.transform.position.z);
		}
	}

	IEnumerator EatTarget(GameObject target){
		float det = Vector3.Distance(target.transform.position, myBox.transform.position);
		bool go=true;
		while (det > 0.1f) {
			yield return null;
			try{
				det = Vector3.Distance(target.transform.position, myBox.transform.position);
				if(det < 0.8f)
					GameObject.Find("Player").GetComponent<BoxGun>().shootable = false;
			}catch(MissingReferenceException){
				if(GameObject.Find ("Player"))
					GameObject.Find("Player").GetComponent<BoxGun>().shootable = true;
				go  = false;
			}
		}
		if(go && deletable){
			deletable = false;
			if(target.tag != "Player"){
				GameObject.Find("Player").GetComponent<BoxGun>().shootable = false;
				GameObject.Find ("GameManager").GetComponent<Map_manager>().DeleteMap(target.GetComponent<Box>().row, target.GetComponent<Box>().col, target.GetComponent<Box>().hight);
			}
			GameObject.Find ("GameManager").GetComponent<Map_manager>().DeleteMap(row, col, hight);
			Destroy (target);
			if(target.tag != "Player")
				GameObject.Find("Player").GetComponent<BoxGun>().shootable = true;
			Destroy (myBox);
		}
	}
}