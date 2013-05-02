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

	protected void generateLevel(Level level)
	{
		
		for (int i = 0; i<level.Objects.Count;i++)
			if (level.Objects[i].type==states.basicblock)
			{
				Instantiate(basicBlock,level.Objects[i].getCoordinates(),Quaternion.identity);	
			}			
	}
}
	
	
