using UnityEngine;
using System.Collections;

public class camerawork : MonoBehaviour {
	public Vector3 mouseDelta = Vector3.zero;
	private Vector3 lastMousePosition = Vector3.zero;
	private bool up;
	private bool down;
	public float yspeed;
	public float xspeed;
	// Use this for initialization
	void Start () {
		up = true;
		down = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(!Input.GetMouseButton(1))
			return;
		if(Input.GetMouseButtonDown(1))
			lastMousePosition=Input.mousePosition;
		mouseDelta = Input.mousePosition - lastMousePosition;
		lastMousePosition = Input.mousePosition;
		Vector3 angleVector = transform.localEulerAngles + new Vector3( 0, mouseDelta.x*xspeed, 0 );;
		if((mouseDelta.y < 0 && up)||(mouseDelta.y >0 && down)){
			
			angleVector = transform.localEulerAngles + new Vector3( -mouseDelta.y*yspeed, 0, 0 );
		}
		transform.localEulerAngles = angleVector;
		if(transform.localEulerAngles.x < -60f || transform.localEulerAngles.x > 270f){
			down = false;
			transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y, transform.localEulerAngles.z);
		}else
			down = true;
		
		if(transform.localEulerAngles.x > 80f){
			up = false;
			transform.localEulerAngles = new Vector3(80f, transform.localEulerAngles.y, transform.localEulerAngles.z);
		}else
			up = true;
	}
}
