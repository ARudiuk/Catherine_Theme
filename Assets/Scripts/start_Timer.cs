using UnityEngine;
using System.Collections;

public class start_Timer : MonoBehaviour {

	void OnTriggerExit(Collider character)
	{		
		end_Timer.startTime = Time.time;
	}
}
