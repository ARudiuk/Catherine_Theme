using UnityEngine;
using System.Collections;

public class gameover_Text : MonoBehaviour {

	// Use this for initialization
	void Start () {
		guiText.text = "It took you " + end_Timer.totalTime.ToString()+" seconds";	
	}	
	
}
