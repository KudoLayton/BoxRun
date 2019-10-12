using UnityEngine;
using System.Collections;

public class GameLog : MonoBehaviour {
	public int thisstage;
	public int nextstage;
	public enum deadcause{win, nobulletlength, breakplayer};
	public deadcause dead;
	public bool result;
	void Start(){
		thisstage = Application.loadedLevel;
		dead = deadcause.win;
	}
}
