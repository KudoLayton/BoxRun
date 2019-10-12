using UnityEngine;
using System.Collections;

public class CamRotate : MonoBehaviour {
	public GameObject player;
	public float turnSpeed;
	private Vector3 mouseOrigin;
	private bool isRotating;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.LookAt(player.transform.position);

		if(Input.GetMouseButtonDown(1))
		{
			// Get mouse origin
			mouseOrigin = Input.mousePosition;
			isRotating = true;
		}
		if (isRotating)
		{
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
			
			transform.RotateAround(transform.position, transform.right, -pos.y * turnSpeed);
			transform.RotateAround(transform.position, Vector3.up, pos.x * turnSpeed);
		}
		if (!Input.GetMouseButton(0)) isRotating=false;
	}
}
