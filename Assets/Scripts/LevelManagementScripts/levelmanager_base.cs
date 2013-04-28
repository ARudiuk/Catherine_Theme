using UnityEngine;
using System.Collections;

public class levelmanager_base : MonoBehaviour {
	
	public GameObject basicBlock;//temp public till types are implemented	
	// Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetButtonUp("OutputLevel"))
		{
			levelmanager_maplevel mapper = new levelmanager_maplevel();
			mapper.mapLevel();
		}
		if(Input.GetButtonUp("InputLevel"))
		{
			Level level = new Level();
			level.read();
			generateLevel(level);
		}
	}

	void generateLevel(Level level)
	{
		
		for (int i = 0; i<level.Blocks.Count;i++)
			if (level.Blocks[i].type==1)
			{
				Instantiate(basicBlock,level.Blocks[i].getCoordinates(),Quaternion.identity);	
			}			
	}
	
	void designPlane() //needs to be replaced
	{		
//		int [,,]level = new int [leveldim,leveldim,leveldim];
//		Debug.Log("Reached");
//		for(int i = 0;i<leveldim;i++)
//		{
//			for(int j = 0;j<leveldim;j++)
//			{
//				level[i,0,j]=1;
//			}
//		}			
//		generateLevel(leveldim,level);
	}
}
