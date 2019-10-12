using UnityEngine;
using System.Collections;

public class Item_plus : Box {
	bool used;
	// Use this for initialization
	void Start () {
		used = true;
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit h;//raycast 선언
		if (Physics.Raycast (myBox.transform.position, -transform.up, out h, 1F)) {
			myBox.rigidbody.isKinematic=false;
			myBox.transform.position = new Vector3(Mathf.Round(myBox.transform.position.x), Mathf.Round(myBox.transform.position.y), Mathf.Round(myBox.transform.position.z));
		}else{
			myBox.rigidbody.isKinematic=true;
		}
		Selfdestroy();
	}
	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag != "target"){
			StartCoroutine (EatItem(col.gameObject));
		}
	}
	IEnumerator EatItem(GameObject target){
		float det = Vector3.Distance(target.transform.position, myBox.transform.position);
		bool go=true;
		while (det > 0.03f) {
			yield return null;
			try{
				det = Vector3.Distance(target.transform.position, myBox.transform.position);
			}catch(MissingReferenceException){
				go  = false;
			}
		}
		if(go){
			if(target.tag == "Player"&&used){
				used = false;
				target.gameObject.GetComponent<BoxGun>().bulletnum++;
			}
			Destroy (myBox);
		}
	}
}
