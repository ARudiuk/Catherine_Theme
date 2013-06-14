using UnityEngine;
using System.Collections;
using LitJson;
using System.IO; //for reading writing files

public class levelmanager_base : MonoBehaviour {
	
	public GameObject block;//temp public till subtypes are implemented
	public GameObject crappyCharacter;//temp public till subtypes are implemented
	public Level currentlevel;//holds reference to currently loaded level	
	
	public int levelPadding;//amount to pad level on all sides so that checks are always within the map
	// Use this for initialization
	void Awake () {
		currentlevel = new Level(); //when loading levelmanager at the beggining, make a default level. This level must be written over.
		gameObject.AddComponent("light_manager");//initial addition of light manger, not sure if this is how it will be done in the future.
		levelPadding = 3;//three away is the furthest any checks will have to go. Since you can go one block and character over.	
	}
	
	// Update is called once per frame
	void Update () {
		
				
	}

	protected void generateLevel() //generates a level from its loaded data
	{		
		//first pad entity data so that it is position correctly
		//this is done first because it is necessary for lowestlevel to be accurate
		for (int k = 0; k<currentlevel.Objects.Count;k++)
		{
			currentlevel.Objects[k].x+=levelPadding;currentlevel.Objects[k].y+=levelPadding;currentlevel.Objects[k].z+=levelPadding;
		}
		//assign lowest level for use in assigning blocks to levels
		currentlevel.lowestlevel = currentlevel.getlowestBlock();
		//levels are created for blocks to be assigned to, the amount of levels is determineid by the highest block. 
		//The highest block is kept track of in levelheigh in currentlevel. This means that levelheight must be correctly assigned before this point.
		//this correction is done by calling fixlimits(), which is currently done outside of this function because it needs to also be called before saving, which doesn't call generatelevel
		for(int j = 0; j<currentlevel.levelheight;j++)
		{
			GameObject level = new GameObject("Level "+j);
			level.transform.position = new Vector3(0,j+levelPadding,0);//just felt right
			//level.tag = "block";//for deleting --need different tag
		}
		//this generates the gamobjects, and attaches the baseclasses to the right type
		//it goes through all the objects loaded into the objects list of current level
		//the block and character cases are mostly the same
		//first you clone a prefab and set a wrapping entity object at the map position that is equal to its saved coordinates
		//second you create a base class that inherents from gameobject
		//third you set the level refernce in the base class
		//for the block there is an additional steps with setting it as a child of a level for easy access
		
		for (int i = 0; i<currentlevel.Objects.Count;i++)
		{
			if (currentlevel.Objects[i].type==states.block) //blocks
			{				
				currentlevel.setMap(currentlevel.Objects[i].getCoordinates(),new Entity((GameObject)Instantiate(block,currentlevel.Objects[i].getCoordinates(),Quaternion.identity),states.block,(int) blocktypes.basic));
	            Block_base temp = currentlevel.getEntity(currentlevel.Objects[i].getCoordinates()).obj.GetComponent<Block_base>();
				temp.level = currentlevel;
				temp.transform.parent = GameObject.Find("Level " +(temp.transform.position.y-currentlevel.lowestlevel)).transform;				
			}
			if (currentlevel.Objects[i].type==states.character) //character
			{				
				currentlevel.setMap(currentlevel.Objects[i].getCoordinates(),new Entity ((GameObject)Instantiate(crappyCharacter,currentlevel.Objects[i].getCoordinates(),Quaternion.identity), states.character, (int)charactertypes.basic));	
				Character_base temp = currentlevel.getEntity(currentlevel.Objects[i].getCoordinates()).obj.GetComponent<Character_base>();
				temp.level = currentlevel;
			}
		}
		//get the blocks at the lowest level and make then base blocks, ones that don't fall
		Block_base [] children = GameObject.Find("Level 0").transform.GetComponentsInChildren<Block_base>();
		foreach(Block_base child in children)
		{
			child.baseBlock=true;
		}		
	}
	
	//Use stream reader and external json library to read in a saved level
	//after loading in make sure to fix limits to the maximums so that padding will be consistent when added
	//then make the matrix of right size, including padding
	protected void read(int padding)
	{	
		Debug.Log ("reading level");		
		if(!Application.isWebPlayer)
		{
			using(StreamReader file = new StreamReader(Application.dataPath+"/Levels/"+currentlevel.name+".json"))
			{
				string hold = file.ReadToEnd();
				currentlevel = JsonMapper.ToObject<Level>(hold);				
			}		
		currentlevel.fixLimits();
		currentlevel.constructMatrix(padding);
		generateLevel();
		}
		
		else if(Application.isWebPlayer)
		{			
			StartCoroutine(remoteCall());					
		}	
	}	
	
	public bool changinglevels;
	protected IEnumerator remoteCall()
	{
		changinglevels = true;
		WWW content = new WWW(@"http://wiki-412.appspot.com/json/alpha2.json");
			while(!content.isDone)
			{
				Debug.Log("fetching");	
				yield return null;
			}
			string hold = content.text;
			currentlevel = JsonMapper.ToObject<Level>(hold);		
		currentlevel.fixLimits();
		currentlevel.constructMatrix(3);	
		generateLevel();
		changinglevels = false;
	}
	
	
	
	//remove unnecesary space with fixlimits before writing to a file
	protected void write()
	{
		Debug.Log ("writing level");
		currentlevel.fixLimits();
		using (StreamWriter file = new StreamWriter(Application.dataPath+"/Levels/"+currentlevel.name+".json"))
		{
			string hold = JsonMapper.ToJson(currentlevel); //serialize public properties and format with indentations	
			//Debug.Log(hold);
			file.Write(hold);
		}
	}
	
			
}
	
	
