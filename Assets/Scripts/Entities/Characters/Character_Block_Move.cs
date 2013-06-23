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
			if(Input.GetButtonDown("Horizontal"))
			{
				float test = Input.GetAxis("Horizontal");
				//if forward direction isn't global left or right, and user is pressing left or right. Return nothing.
				if(transform.TransformDirection(Vector3.forward)!=Vector3.right&&transform.TransformDirection(Vector3.forward)!=Vector3.left)
					return movement;
				//otherwise we have valid input
				else
				{
					if(test>0){
						movement.Add(Vector3.right);								
						}
					else{
						movement.Add(Vector3.left);
						}	
				}
					
			}		
		
			//same as previous section, but for up and down
			else if(Input.GetButtonDown("Vertical"))
			{
				float test = Input.GetAxis("Vertical");
				if(transform.TransformDirection(Vector3.forward)!=Vector3.forward&&transform.TransformDirection(Vector3.forward)!=Vector3.back)
					return movement;
				else
				{
					if(test>0){
						movement.Add(Vector3.forward);
						}							
					else{
						movement.Add(Vector3.back);
						}						
				}
			}	
			//if returning actual values, then fix up the moves to account for both objects
			if (movement.Count>0)
				return move_checks(level, movement,transform);
			}		
		return movement;
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
	
	

				

	

