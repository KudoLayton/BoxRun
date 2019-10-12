using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {
	public GameObject myBox;
	public float height;
	public int row;
	public int col;
	public int hight;
	protected void Selfdestroy(){
		if (myBox.transform.position.y <= -height) {
			Destroy (myBox);
		}//떨어지면 자기 삭제
	}

	protected void StopFloor(){
		if (myBox.transform.position.y < 2f && myBox.transform.position.y > 1f && ((GameObject)((ArrayList)((ArrayList)(GameObject.Find("GameManager").GetComponent<Map_manager>().map[row]))[col])[hight]).Equals(myBox)){
			StartCoroutine(StopAtFloor());
		}
	}//1충에서 멈추기

	IEnumerator StopAtFloor(){
		while (myBox.transform.position.y > 1.01f) {
			yield return null;		
		}
		myBox.rigidbody.isKinematic = true;
		myBox.transform.position = new Vector3 (myBox.transform.position.x, 0f, myBox.transform.position.z);

	}
}
