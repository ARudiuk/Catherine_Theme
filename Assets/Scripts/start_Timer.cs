using UnityEngine;
using System.Collections;

public class start_Timer : MonoBehaviour {	
	
	void Update()
	{
		if(GameObject.FindGameObjectWithTag("character").transform.position==transform.position)
			end_Timer.startTime=Time.time;
	}
}
