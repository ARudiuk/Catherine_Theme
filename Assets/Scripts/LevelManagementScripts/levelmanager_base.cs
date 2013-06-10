using UnityEngine;
using System.Collections;
using Newtonsoft.Json; //external json library
using System.IO; //for reading writing files

public class levelmanager_base : MonoBehaviour {
	
	public GameObject block;//temp public till types are implemented
	public GameObject crappyCharacter;//temp public till types are implemented
	public Level currentlevel;	
	
	public int levelPadding;//amount to pad level on all sides so that checks are always within the map
	// Use this for initialization
	void Awake () {
		currentlevel = new Level();
		gameObject.AddComponent("light_manager");
		levelPadding = 3;//three away is the furthest any checks will have to go. Since you can go one block and character over.	
	}
	
	// Update is called once per frame
	void Update () {
		
				
	}

	protected void generateLevel()
	{		
		for (int k = 0; k<currentlevel.Objects.Count;k++)
		{
			currentlevel.Objects[k].x+=levelPadding;currentlevel.Objects[k].y+=levelPadding;currentlevel.Objects[k].z+=levelPadding;
		}
		currentlevel.lowestlevel = currentlevel.getlowestBlock();
		for(int j = 0; j<currentlevel.levelheight;j++)
		{
			GameObject level = new GameObject("Level "+j);
			//level.transform.position = new Vector3(0,j+levelPadding,0);
			level.tag = "block";
		}
		for (int i = 0; i<currentlevel.Objects.Count;i++)
		{
			if (currentlevel.Objects[i].type==states.block)
			{				
				currentlevel.setMap(currentlevel.Objects[i].getCoordinates(),new Entity((GameObject)Instantiate(block,currentlevel.Objects[i].getCoordinates(),Quaternion.identity),states.block,(int) blocktypes.basic));	
	            Block_base temp = currentlevel.getEntity(currentlevel.Objects[i].getCoordinates()).obj.GetComponent<Block_base>();
				temp.level = currentlevel;
				temp.transform.parent = GameObject.Find("Level " +(temp.transform.position.y-currentlevel.lowestlevel)).transform;
				temp.baseBlock=false;
			}
			if (currentlevel.Objects[i].type==states.character)
			{				
				currentlevel.setMap(currentlevel.Objects[i].getCoordinates(),new Entity ((GameObject)Instantiate(crappyCharacter,currentlevel.Objects[i].getCoordinates(),Quaternion.identity), states.character, (int)charactertypes.basic));	
				Character_base temp = currentlevel.getEntity(currentlevel.Objects[i].getCoordinates()).obj.GetComponent<Character_base>();
				temp.level = currentlevel;				
				temp.transform.parent = GameObject.Find("Level " +(temp.transform.position.y-currentlevel.lowestlevel)).transform;
			}
		}		
		Block_base [] children = GameObject.Find("Level 0").transform.GetComponentsInChildren<Block_base>();
		foreach(Block_base child in children)
		{
			child.baseBlock=true;
		}		
	}
	
	protected void read(int padding)
	{	
		Debug.Log ("reading level");
		using(StreamReader file = new StreamReader(Application.dataPath+"/Levels/"+currentlevel.name+".json"))
		{
			string hold = file.ReadToEnd();
			currentlevel = JsonConvert.DeserializeObject<Level>(hold);						
		}
		currentlevel.fixLimits();
		currentlevel.constructMatrix(padding);
		
	}
	
	protected void write()
	{
		Debug.Log ("writing level");
		currentlevel.fixLimits();
		using (StreamWriter file = new StreamWriter(Application.dataPath+"/Levels/"+currentlevel.name+".json"))
		{
			string hold = JsonConvert.SerializeObject(currentlevel,Formatting.Indented); //serialize public properties and format with indentations	
			//Debug.Log(hold);
			file.Write(hold);
		}
	}
	
			
}
	
	
