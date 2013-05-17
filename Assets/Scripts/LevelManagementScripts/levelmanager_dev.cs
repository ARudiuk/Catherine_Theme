using UnityEngine;
using System.Collections;

public class levelmanager_dev : levelmanager_base
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetButtonUp("OutputLevel"))
		{
			levelmanager_maplevel mapper = new levelmanager_maplevel();
			mapper.mapLevel();
		}
		if(Input.GetButtonUp("InputLevel"))
		{
			currentlevel = new Level();
			currentlevel.read();
			generateLevel();
		}	
	}
}

