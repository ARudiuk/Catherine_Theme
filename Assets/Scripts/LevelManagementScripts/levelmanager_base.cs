using UnityEngine;
using System.Collections;

public class levelmanager_base : MonoBehaviour {
	
	public GameObject basicBlock;//temp public till types are implemented
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
			if (currentlevel.Objects[i].type==states.basicblock)
			{	
				currentlevel.Objects[i].x+=levelPadding;currentlevel.Objects[i].y+=levelPadding;currentlevel.Objects[i].z+=levelPadding;
				currentlevel.map[currentlevel.Objects[i].x,currentlevel.Objects[i].y,currentlevel.Objects[i].z]=new Entity((GameObject)Instantiate(basicBlock,currentlevel.Objects[i].getCoordinates(),Quaternion.identity),states.basicblock);	
	            Block_base temp = currentlevel.map[currentlevel.Objects[i].x,currentlevel.Objects[i].y,currentlevel.Objects[i].z].obj.GetComponent<Block_base>();
				temp.level = currentlevel;
			
			}
			if (currentlevel.Objects[i].type==states.character)
			{
				currentlevel.Objects[i].x+=levelPadding;currentlevel.Objects[i].y+=levelPadding;currentlevel.Objects[i].z+=levelPadding;
				currentlevel.map[currentlevel.Objects[i].x,currentlevel.Objects[i].y,currentlevel.Objects[i].z]=new Entity ((GameObject)Instantiate(crappyCharacter,currentlevel.Objects[i].getCoordinates(),Quaternion.identity), states.character);	
				Character_base temp = currentlevel.map[currentlevel.Objects[i].x,currentlevel.Objects[i].y,currentlevel.Objects[i].z].obj.GetComponent<Character_base>();
				temp.level = currentlevel;				
			}
		}
	
	}
			
}
	
	
