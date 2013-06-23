using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Linq;

//used for editing levels quickly, and having powers normaly not available
public class levelmanager_dev : levelmanager_base
{	
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		//if pressing output button, then open savefilepanel with currentlevel name + .json
		//then use mapper to make new level, which can be done even if blocks and characters aren't already in map or object list
		//then call write to output the data
		if(Input.GetButtonUp("OutputLevel"))
		{
			string hold =  EditorUtility.SaveFilePanel("Save File",Application.dataPath+"/Levels/",currentlevel.name+".json","json");
			if(hold!="")
			{
				currentlevel.name = hold.Split('/').Last().Split('.')[0];			
				levelmanager_maplevel mapper = new levelmanager_maplevel();
				currentlevel = mapper.mapLevel(currentlevel.name);
				write();
			}		
		}
		//pick file with openfilepanel
		//if file string isn't empty, contiue inputing level data
		//get all gamobjects with tags block and character and delete. This deletes all blocks and characters
		//then refresh all level data by just making a new level
		//then name the level by the file name, possibly use json data in the future instead
		//then read the level and generate. Read will make sure the saved file has minimized coordinates, and generate level will pad them
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
			
			}
		}	
	}	
	
}