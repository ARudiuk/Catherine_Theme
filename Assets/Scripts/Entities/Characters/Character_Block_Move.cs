using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character_Block_Move {

	// Use this for initialization
	public Character_Block_Move () {
		grabbing = false;
	}

	bool grabbing;

	public Vector3 pushDirection = new Vector3(0,0,0);
	bool blocked;

	// Update is called once per frame
	public List<Vector3> move(Level level, Transform transform, TimeDirection timeD, out Vector3 rotation) {	
		List <Vector3> movement = new List<Vector3>(); //might not want to return empty objects this often
		//move these to a start
		rotation = Vector3.zero;
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
						rotation = new Vector3(0,Vector3.Angle(entity.obj.transform.position-transform.position,transform.TransformDirection(Vector3.forward)),0);
						return movement; 
					}
				}
			}
		}
   		if(grabbing)
		{		
			if(Input.GetButtonDown("Horizontal"))
			{
				float test = Input.GetAxis("Horizontal");
				if(transform.TransformDirection(Vector3.forward)!=Vector3.right&&transform.TransformDirection(Vector3.forward)!=Vector3.left)
					return movement;
				
					//if(other.timeD.direction == "right" || other.timeD.direction == "left") 
					{
						//checks if there's a block beind you
						if(test>0){
							movement.Add(Vector3.right);								
							}
						else{
							movement.Add(Vector3.left);
								}						
							}
					
						
					}
				
			

          	else if(Input.GetButtonDown("Vertical"))
			{
				float test = Input.GetAxis("Vertical");
				if(transform.TransformDirection(Vector3.forward)!=Vector3.forward&&transform.TransformDirection(Vector3.forward)!=Vector3.back)
					return movement;
				
					//if(other.timeD.direction == "up" || other.timeD.direction == "down") 
					{
						if(test>0){
							 movement.Add(Vector3.forward);
								}							
						else{
							movement.Add(Vector3.back);
								}						
							}
				}	
			if (movement.Count>0)
				return move_checks(level, movement,transform);
			}		
		return movement;
		}
	
	private List<Vector3> move_checks(Level level, List<Vector3> movement, Transform transform)
	{
		movement.Add (movement[0]);
		if(transform.TransformDirection(Vector3.back)==movement[0])
		{
			if(level.getEntity(transform.position,transform.TransformDirection(Vector3.back)).type==states.block)
			{
				return new List<Vector3>();
			}
			if(level.getEntity(transform.position,transform.TransformDirection(Vector3.back)+Vector3.down).type==states.empty)
			{
				movement[1]=(Vector3.down+transform.TransformDirection(Vector3.back));				
			}
		}
		else{
			if(level.getEntity(transform.position+transform.TransformDirection(Vector3.forward),Vector3.down).type==states.empty)
			{
				movement[1]=(Vector3.zero);				
			}
		}
		return movement;		
	}
}
	
	

				

	

