using UnityEngine;
using System.Collections;

public class light_manager:MonoBehaviour 
{	
	public GameObject light;
	// Use this for initialization
	void Start()
	{
		light = new GameObject("Light");
		light.AddComponent<Light>();
		light.light.type = LightType.Spot;
		light.light.spotAngle = 60f;
		light.light.intensity = 4.6f;
		light.AddComponent("follow");
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}

