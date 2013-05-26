using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Block_base:MonoBehaviour
{
	public Level level;
	public bool debug;
	
	void Start()
	{
		debug=false;
	}
	
	void Update()
	{	
		if(debug==true)
		{
			Debug.Log(transform.position);
			Debug.Log(level.getsupportingEntity(transform.position).Count);
			if(level.getsupportingEntity(transform.position).Count!=0)
				foreach(Entity entity in level.getsupportingEntity(transform.position))
					Debug.Log (entity.type);
		}
		if(Mathf.RoundToInt(transform.position.y)!=level.lowestlevel && level.getsupportingEntity(transform.position).Count==0)
		{
			level.blockfallmoveObject(transform.position);			
		}
	}
	
}