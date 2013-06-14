using UnityEngine;
using System.Collections;

public class end_Timer : MonoBehaviour {

	public static float startTime;
	public float endTime;
	public static float totalTime;

	void Update()
	{
		if (GameObject.FindGameObjectWithTag("character")!=null) {
			if(GameObject.FindGameObjectWithTag("character").transform.position==transform.position)
			{
				endTime = Time.time;
				totalTime = endTime-startTime;
				Application.LoadLevel("GameOver");
			}
		}
	}

	void Awake()
	{
		DontDestroyOnLoad(this);
	}
}
