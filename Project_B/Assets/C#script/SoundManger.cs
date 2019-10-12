using UnityEngine;
using System.Collections;

public class SoundManger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("m")){
			if(AudioListener.volume <= 0)
				AudioListener.volume = 1f;
			else
				AudioListener.volume = 0f;
		}
	}
}
