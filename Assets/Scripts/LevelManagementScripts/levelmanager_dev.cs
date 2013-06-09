using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Linq;

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
			string hold = EditorUtility.SaveFilePanel("Save File",Application.dataPath+"/Levels/",currentlevel.name+".json","json");
			if(hold!="")
			{
				currentlevel.name = hold.Split('/').Last().Split('.')[0];			
				levelmanager_maplevel mapper = new levelmanager_maplevel();
				currentlevel = mapper.mapLevel(currentlevel.name);
				write();
			}		
		}
        if (Input.GetButtonUp("InputLevel") )
		{
			string hold = EditorUtility.OpenFilePanel("Load File",Application.dataPath+"/Levels/","json");
			if (hold!="")
			{				
				GameObject[] blockArray = GameObject.FindGameObjectsWithTag("block");
				for(int i = 0; i < blockArray.Length; i++)
				{
					GameObject.Destroy(blockArray[i]);
				}
				GameObject[] characterArray = GameObject.FindGameObjectsWithTag("character");
				for(int i = 0; i < characterArray.Length; i++)
				{
					GameObject.Destroy(characterArray[i]);
				}	
				
				currentlevel = new Level();
				currentlevel.name = hold.Split('/').Last().Split('.')[0];	
				
				read(levelPadding);
				generateLevel();
				currentlevel.lowestlevel = currentlevel.getlowestBlock();				
			
			}
		}	
	}	
	
}