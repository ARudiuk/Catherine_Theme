using UnityEngine;
using System.Collections;

public class levelmanager_demo : levelmanager_base {

	// Use this for initialization
	void Start () {
		Debug.Log("starting");
		currentlevel = new Level();
		currentlevel.name = "alpha1";
		read (3);
		generateLevel();
		rowfallcount = 0;
		rowfalltime=4;
	}
	
	int rowfallcount;
	int rowfalltime;
	
	// Update is called once per frame
	void Update () {
		if (Time.time-end_Timer.startTime>rowfallcount*rowfalltime)
		{
			Block_base [] fallinglevel = GameObject.Find("Level "+rowfallcount).GetComponentsInChildren<Block_base>();
			Block_base [] baselevel = GameObject.Find("Level "+(rowfallcount+1)).GetComponentsInChildren<Block_base>();
			foreach (Block_base block in baselevel)
			{
				block.baseBlock=true;
			}
			
			foreach (Block_base block in fallinglevel)	
			{
				block.baseBlock=false;
			}
			rowfallcount++;
		}	
	}
	
	//tons of quick code that is pointlessly large, and inefficient. With the slightest minimization of code.
	//it gives the difficulty options
	void OnGUI () {
		// Make a background box
		GUI.Box(new Rect(10,10,100,150), "Difficulty Menu");

		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(20,40,80,20), "Easy")) {
			rowfalltime=6;		
			generate();
		}

		// Make the second button.
		if(GUI.Button(new Rect(20,70,80,20), "Normal")) {			
			rowfalltime=4;
			generate();
		}
		
		if(GUI.Button(new Rect(20,100,80,20), "Hard")) {			
			rowfalltime=2;	
			generate();
		}
		
		if(GUI.Button(new Rect(20,130,80,20), "Impossible")) {			
			rowfalltime=1;		
			generate();
		}
	}
	
	void generate()
	{
		currentlevel = new Level();
			currentlevel.name = "alpha1";			
			read (3);
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
			generateLevel();
			rowfallcount = 0;
			end_Timer.startTime=Time.time;
	}
	
}
