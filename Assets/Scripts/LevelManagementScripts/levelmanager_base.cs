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
		for (int i = 0; i<currentlevel.Objects.Count;i++)
		{
			if (currentlevel.Objects[i].type==states.block)
			{	
				currentlevel.Objects[i].x+=levelPadding;currentlevel.Objects[i].y+=levelPadding;currentlevel.Objects[i].z+=levelPadding;
				currentlevel.setMap(currentlevel.Objects[i].getCoordinates(),new Entity((GameObject)Instantiate(block,currentlevel.Objects[i].getCoordinates(),Quaternion.identity),states.block,(int) blocktypes.basic));	
	            Block_base temp = currentlevel.getEntity(currentlevel.Objects[i].getCoordinates()).obj.GetComponent<Block_base>();
				temp.level = currentlevel;
			
			}
			if (currentlevel.Objects[i].type==states.character)
			{
				currentlevel.Objects[i].x+=levelPadding;currentlevel.Objects[i].y+=levelPadding;currentlevel.Objects[i].z+=levelPadding;
				currentlevel.setMap(currentlevel.Objects[i].getCoordinates(),new Entity ((GameObject)Instantiate(crappyCharacter,currentlevel.Objects[i].getCoordinates(),Quaternion.identity), states.character, (int)charactertypes.basic));	
				Character_base temp = currentlevel.getEntity(currentlevel.Objects[i].getCoordinates()).obj.GetComponent<Character_base>();
				temp.level = currentlevel;				
			}
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
	
	
