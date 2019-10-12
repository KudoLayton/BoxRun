using UnityEngine;
using System.Collections;

public class MatterBox : Box {
	public float waitsec;
	public bool isgodown;
	public GameObject cracked;
	protected bool isstepped;
	protected bool IsUpBoxThere(){
		RaycastHit h;//raycast 선언
		if (Physics.Raycast (myBox.transform.position, transform.up, out h, 1F)) {
			if(h.collider.gameObject.tag == "Player" || h.collider.gameObject.tag == "black" || h.collider.gameObject.tag == "white")//위쪽에 다른 물질 박스가 있는지 여부 확인
				return true;
		}
		return false;
	}//위에 사물이 잇는지 여부 확인


	protected void DetBoxGoDown(){
		if(isgodown && IsUpBoxThere() && isstepped){
			isstepped = false;
			myBox.GetComponent<MeshRenderer>().enabled = false;
			cracked.GetComponent<Crackedbox>().detlefttime((int) waitsec);
			if(!cracked.GetComponent<Crackedbox>().isvibe){
				cracked.GetComponent<Crackedbox>().isvibe = true;
			}
			gameObject.GetComponent<AudioSource>().enabled = true;
			StartCoroutine(PauseGoDown());
		}
	}

	protected IEnumerator PauseGoDown(){
		yield return new WaitForSeconds(waitsec);
		if(isgodown){
			GameObject.Find("GameManager").GetComponent<Map_manager>().DeleteBottom(row, col);
			myBox.rigidbody.isKinematic = false;
			myBox.transform.Translate(new Vector3 (0f, -0.01f, 0f));
		}
	}//waitsec 동안 정지후 낙하 하는 함수
}
