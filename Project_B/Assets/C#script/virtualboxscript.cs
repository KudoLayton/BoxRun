using UnityEngine;
using System.Collections;

public class virtualboxscript : MonoBehaviour {
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<MeshRenderer>().enabled = GameObject.Find("Player").GetComponent<BoxGun>().virtualbox;
	}
	
	// Update is called once per frame
	void Update () {
		if(GameObject.Find("Player"))
			gameObject.GetComponent<MeshRenderer>().enabled = GameObject.Find("Player").GetComponent<BoxGun>().virtualbox;

	}
}
