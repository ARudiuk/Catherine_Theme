using UnityEngine;
using System.Collections;

public class levelmanager_base : MonoBehaviour {


	int[,,] level;
	public int leveldim;
	public GameObject basicBlock;
	// Use this for initialization
	void Awake () {
		level = new int [leveldim,leveldim,leveldim];
		Debug.Log("Reached");
		for(int i = 0;i<leveldim;i++)
		{
			for(int j = 0;j<leveldim;j++)
			{
				level[i,0,j]=1;
			}
		}			
		generateLevel();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void generateLevel()
	{
		for (int i = 0;i<leveldim;i++)
		{
			for(int j = 0;j<leveldim;j++)
			{
				for(int k=0;k<leveldim;k++)				
				{
					Debug.Log("Reached");
					if (level[i,j,k]==1)
					{
						Instantiate(basicBlock,new Vector3(i,j,k),Quaternion.identity);	
					}
				}
			}
		}
	}
}
