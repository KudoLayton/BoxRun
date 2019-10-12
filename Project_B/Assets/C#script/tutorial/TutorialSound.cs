using UnityEngine;
using System.Collections;

public class TutorialSound : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = new Vector3(GameObject.FindWithTag("MainCamera").transform.position.x, GameObject.FindWithTag("MainCamera").transform.position.y, GameObject.FindWithTag("MainCamera").transform.position.z+4f);
	}	
}
