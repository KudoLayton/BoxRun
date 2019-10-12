using UnityEngine;
using System.Collections;

public class Crackedbox : MonoBehaviour {
	public bool isvibe = false;
	public bool isvibrated = true;
	private int lefttime;
	// Use this for initialization
	void Start () {
		animation["vibrate_0"].wrapMode = WrapMode.Loop;
		animation["vibrate_1"].wrapMode = WrapMode.Loop;
		animation["vibrate_2"].wrapMode = WrapMode.Loop;
		animation["vibrate_3"].wrapMode = WrapMode.Loop;
	}
	
	// Update is called once per frame
	void Update () {
		if(isvibe && isvibrated)
			StartCoroutine(playvibrate());
	}

	public void detlefttime(int newtime){
		if(!isvibe){
			lefttime = newtime;
		}else if(lefttime == 0){
			lefttime = newtime;
		}else if(lefttime > newtime){
			lefttime = newtime;
		}
	}

	IEnumerator playvibrate(){
		if(isvibe){
			isvibrated = false;
			while(lefttime > -5){
				if(!GameObject.Find ("GameManager").GetComponent<Map_manager>().isgodown ||!GameObject.Find ("GameManager").GetComponent<Map_manager>().islinedelete){
					break;
				}
				switch(lefttime){
				case 7:
					animation.Play("vibrate_0");
					break;
				case 5:
					animation.Play ("vibrate_1");
					break;
				case 3:
					animation.Play("vibrate_2");
					break;
				case 1:
					animation.Play("vibrate_3");
					break;
				default:
					break;
				}
				yield return new WaitForSeconds(1f);
				lefttime--;
			}
		}
	}
}
