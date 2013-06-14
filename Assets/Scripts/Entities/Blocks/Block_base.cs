using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Block_base:MonoBehaviour
{
	public Level level;
	public bool debug;
	
	public bool baseBlock;
	
	public bool moving;
	void Awake()
	{
		baseBlock = false; //this needs to be called in awake, because in start it overrides previous input
	}
	void Start()
	{		
		moving = false;
		debug=false;		
	}
	
	void Update()
	{	
		//just to debug blogs if necessary. Can be turned on in game
		if(debug==true)
		{			
			Debug.Log(level.getsupportingEntity(transform.position).Count);
			if(level.getsupportingEntity(transform.position).Count!=0)
				foreach(Entity entity in level.getsupportingEntity(transform.position))
			{
					Debug.Log (entity.type);
					Debug.Log (transform.position);
					Debug.Log (entity.obj.transform.position);
			}
		}
		//if not moving, then go ahead and check if block needs to all. 
		//that is if firstly it isn't a base block, then if nothing is supporting it, fall
		//also if block one step away from out of bounds, destroy it
		if(moving==false)
		{
			if(baseBlock==false)
			{
				if(transform.position.y-1==-1)
				{
					level.setMap(transform.position,new Entity());
					Destroy(gameObject);
				}
				else if(level.getsupportingEntity(transform.position).Count==0)
				{
					level.blockfallmoveObject(transform.position);			
				}
			}
		}
	}
	
	
}