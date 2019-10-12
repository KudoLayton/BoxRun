using UnityEngine;
using System.Collections;

public class WhiteBox : MatterBox{
	// Use this for initialization
	void Start () {
		isstepped = true;
	}
	// Update is called once per frame
	void Update () {
		isgodown = GameObject.Find("GameManager").GetComponent<Map_manager>().isgodown;
		Selfdestroy ();
		DetBoxGoDown ();
	}
}
