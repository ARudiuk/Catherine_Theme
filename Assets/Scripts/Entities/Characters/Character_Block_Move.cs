using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character_Block_Move {

	// Use this for initialization
	public Character_Block_Move () {
		grabbing = false;
	}
	//if grabbing
	bool grabbing;

	public Vector3 pushDirection = new Vector3(0,0,0);
	bool blocked;
	
	public List<Vector3> move(Level level, Transform transform, TimeDirection timeD, out Vector3 rotation) {	
		List <Vector3> movement = new List<Vector3>(); //might not want to return empty objects this often
		//move these to start
		rotation = Vector3.zero;
		//first check if grabbing something
		//might want to add delay before going further to allow for turning before moving block
		if(level.getEntity(transform.position,transform.TransformDirection(Vector3.forward)).type==states.block)
		{
			grabbing = true;
		}
		//otherwise turn to block
		else
		{
			List<Entity> temp = level.getsurroundingEntity(transform.position);
			{
				foreach(Entity entity in temp)
				{
					if(entity.type==states.block)
					{
						grabbing = true;
						float angle = Vector3.Angle(transform.TransformDirection(Vector3.forward),entity.obj.transform.position-transform.position);
						Vector3 cross = Vector3.Cross(transform.TransformDirection(Vector3.forward),entity.obj.transform.position-transform.position);
						rotation = (cross.y>0)?new Vector3(0,angle,0):new Vector3(0,-angle,0);
						return movement; 
					}
				}
			}
		}
		//if block is in front
   		if(grabbing)
		{	
			if(transform.TransformDirection(Vector3.forward)==Vector3.right||transform.TransformDirection(Vector3.forward)==Vector3.left)
			{
				if (timeD.direction==Vector3.right)
					movement.Add (Vector3.right);
				else if(timeD.direction==Vector3.left)
					movement.Add (Vector3.left);
			}			
			else if(transform.TransformDirection(Vector3.forward)==Vector3.forward||transform.TransformDirection(Vector3.forward)==Vector3.back)
			{
				if (timeD.direction==Vector3.forward)
					movement.Add (Vector3.forward);
				else if(timeD.direction==Vector3.back)
					movement.Add (Vector3.back);
			}
			else{
				return new List<Vector3>();
			}				
			
			//if returning actual values, then fix up the moves to account for both objects
			if (movement.Count>0&&timeD.time>0.1f)
				return move_checks(level, movement,transform);
			}		
		return new List<Vector3>();
		}	
	
	private List<Vector3> move_checks(Level level, List<Vector3> movement, Transform transform)
	{
		movement.Add (movement[0]);
		//if the position behind the character is the place being moved to, then follow this logic
		if(transform.TransformDirection(Vector3.back)==movement[0])
		{
			//if block behind you, then return empty
			if(level.getEntity(transform.position,transform.TransformDirection(Vector3.back)).type==states.block)
			{
				return new List<Vector3>();
			}
			//other nothing to step on behind you, then fall down and pull block
			if(level.getEntity(transform.position,transform.TransformDirection(Vector3.back)+Vector3.down).type==states.empty)
			{
				movement[1]=(Vector3.down+transform.TransformDirection(Vector3.back));				
			}
		}
		//otherwise just push blocks
		else{
			if(level.getEntity(transform.position+transform.TransformDirection(Vector3.forward),Vector3.down).type==states.empty)
			{
				movement[1]=(Vector3.zero);				
			}
		}
		return movement;		
	}
}
	
	

				

	

