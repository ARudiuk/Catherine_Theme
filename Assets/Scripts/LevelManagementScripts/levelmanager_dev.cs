using UnityEngine;
using System.Collections;

public class levelmanager_dev : levelmanager_base
{	
	bool gOutputting;
	bool gInputting;
	// Use this for initialization
	void Start ()
	{
		gInputting = false;
		gOutputting = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetButtonUp("OutputLevel")&& gOutputting==false)
		{
			gOutputting = true;
			if(currentlevel.name == "temp")
				currentlevel.name = "Type Name to Save to File";			
		}
        if (Input.GetButtonUp("InputLevel") && gInputting == false)
		{
			gInputting = true;
			if(currentlevel.name == "temp")
				currentlevel.name = "Type Name to Load to Level";
		}	
	}
	
	void OnGUI()
	{
		if(gOutputting)
		{
			currentlevel.name = GUI.TextField(new Rect(Screen.width*.05f, Screen.height*.05f, Screen.width*.45f, Screen.height*.1f), currentlevel.name);
		
			if(Input.GetButtonDown("enter"))
			{
				levelmanager_maplevel mapper = new levelmanager_maplevel();
				mapper.mapLevel();
				gOutputting = false;	
			}
		}
		
		if(gInputting)
		{
			currentlevel.name = GUI.TextField(new Rect(Screen.width*.05f, Screen.height*.05f, Screen.width*.45f, Screen.height*.1f), currentlevel.name);
			if(Input.GetButtonDown("enter"))
			{
				GameObject[] blockArray = GameObject.FindGameObjectsWithTag("block");
				for(int i = 0; i < blockArray.Length; i++)
				{
					GameObject.Destroy(blockArray[i]);
				}
				
				string temp = currentlevel.name;
				
				currentlevel = new Level();
				currentlevel.name = temp;	
				
				currentlevel.read();
				generateLevel();
				gInputting = false;
			}
		}
	}
}