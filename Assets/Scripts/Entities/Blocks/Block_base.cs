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
		baseBlock = true; //this needs to be called in awake, because in start it overrides previous input
	}
	void Start()
	{		
		moving = false;
		debug=false;		
	}
	
	void Update()
	{	
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