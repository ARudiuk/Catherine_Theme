using UnityEngine;
using System.Collections;

public class levelmanager_base : MonoBehaviour {
	
	public GameObject basicBlock;//temp public till types are implemented	
	// Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {	
		
	}

	void generateLevel(Level level)
	{
		
		for (int i = 0; i<level.Blocks.Count;i++)
			if (level.Blocks[i].type==1)
			{
				Instantiate(basicBlock,level.Blocks[i].getCoordinates(),Quaternion.identity);	
			}			
	}
}
	
	
