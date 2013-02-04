using UnityEngine;
using System.Collections;

public class timer_Update : MonoBehaviour {

	
	
	// Update is called once per frame
	void Update () {

		guiText.text="Time:"+(Time.time-end_Timer.startTime).ToString();
	
	}
}
