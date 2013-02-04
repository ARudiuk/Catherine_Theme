using UnityEngine;
using System.Collections;

public class end_Timer : MonoBehaviour {

	public static float startTime;
	public float endTime;
	public static float totalTime;

	void OnTriggerEnter(Collider character)
	{
		endTime = Time.time;
		totalTime = endTime-startTime;
		Application.LoadLevel("GameOver");
	}

	void Awake()
	{
		DontDestroyOnLoad(this);
	}
}
