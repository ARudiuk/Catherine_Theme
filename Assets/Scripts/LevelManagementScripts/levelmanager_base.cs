using UnityEngine;
using System.Collections;

public class levelmanager_base : MonoBehaviour {
	
	public GameObject basicBlock;//temp public till types are implemented
	public GameObject crappyCharacter;//temp public till types are implemented
	public Level currentlevel;
	// Use this for initialization
	void Awake () {
		
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
				currentlevel.map[currentlevel.Objects[i].x+2,currentlevel.Objects[i].y+2,currentlevel.Objects[i].z+2]=new Entity((GameObject)Instantiate(basicBlock,currentlevel.Objects[i].getCoordinates(),Quaternion.identity),states.basicblock);	
	                                                         		}
			if (currentlevel.Objects[i].type==states.character)
			{
				currentlevel.map[currentlevel.Objects[i].x+2,currentlevel.Objects[i].y+2,currentlevel.Objects[i].z+2]=new Entity ((GameObject)Instantiate(crappyCharacter,currentlevel.Objects[i].getCoordinates(),Quaternion.identity), states.character);	
				Character_base temp = currentlevel.map[currentlevel.Objects[i].x+2,currentlevel.Objects[i].y+2,currentlevel.Objects[i].z+2].obj.GetComponent<Character_base>();
				temp.level = currentlevel;
				temp.timetoMove = 0.5f;
			}
		}
	
	}
			
}
	
	
