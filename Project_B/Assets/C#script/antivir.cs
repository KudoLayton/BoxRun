using UnityEngine;
using System.Collections;

public class antivir : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<MeshRenderer>().enabled = GameObject.Find("Player").GetComponent<BoxGun>().virtualbox;
		if(GameObject.Find("Player").GetComponent<BoxGun>().virtualbox){
			gameObject.GetComponent<ParticleSystem>().Play();
		}else{
			gameObject.GetComponent<ParticleSystem>().Clear();
			gameObject.GetComponent<ParticleSystem>().Stop();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(GameObject.Find ("Player")){
			gameObject.GetComponent<MeshRenderer>().enabled = GameObject.Find("Player").GetComponent<BoxGun>().virtualbox;
			if(GameObject.Find("Player").GetComponent<BoxGun>().virtualbox){
				gameObject.GetComponent<ParticleSystem>().Play();
		
			}else{
				gameObject.GetComponent<ParticleSystem>().Clear();
				gameObject.GetComponent<ParticleSystem>().Stop();
			}
		}
	}
}
