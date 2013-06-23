using UnityEngine;
using System.Collections;

public class Character_Movement_Ledge {
	
	public Character_Movement_Ledge () {
			
	}
	
	// Update is called once per frame
	public Vector3 move (Level level, Transform transform, TimeDirection timeD, out Vector3 rotation) {	
		
		//set up return value
		rotation = Vector3.zero;
		
		//if pressing grab then let go
		if(Input.GetButtonDown("Grab"))
		{			
			return Vector3.down;
		}
		
		//if left or right pressed
		if (Input.GetButtonDown("Horizontal"))
			{
				float test = Input.GetAxis("Horizontal");
			//if right
			//check if block directly to right, if so then rotate
			if(test>0)
			{
				
				if(level.getEntity(transform.position,transform.TransformDirection(Vector3.right)).type==states.block)
				{
					rotation = transform.TransformDirection(new Vector3(0,90,0));
					return Vector3.zero;
				}
				
				
				//if no block to the right, then check if block forward and right from the characters is empty. 
				//if so then move around the corner
				//otherwise if there is a block forward and right, move right
				//in both cases, make sure there is no block above the new position
				
				else
				{
					if(level.getEntity(transform.position+transform.TransformDirection(Vector3.forward),transform.TransformDirection(Vector3.right)).type==states.block
						&& level.getEntity(transform.position,Vector3.up+transform.TransformDirection(Vector3.right)).type==states.empty)
					{						
						return transform.TransformDirection(Vector3.right);
					}
					else if(level.getEntity(transform.position,transform.TransformDirection(Vector3.forward)+transform.TransformDirection(Vector3.right)).type==states.empty
						&& level.getEntity(transform.position,Vector3.up+transform.TransformDirection(Vector3.right)+transform.TransformDirection(Vector3.forward)).type==states.empty)
					{
						rotation = transform.TransformDirection(new Vector3(0,-90,0));
						return transform.TransformDirection(Vector3.forward+Vector3.right);
					}
				}
				
			}
			//inverse of previous section
			else
			{
				
				if(level.getEntity(transform.position,transform.TransformDirection(Vector3.left)).type==states.block)
				{
					rotation = new Vector3(0,-90,0);
					return Vector3.zero;
				}
				 
				else
				{					
					if(level.getEntity(transform.position,transform.TransformDirection(Vector3.forward)+transform.TransformDirection(Vector3.left)).type==states.block
						&& level.getEntity(transform.position,Vector3.up+transform.TransformDirection(Vector3.left)).type==states.empty)
					{		
						return transform.TransformDirection(Vector3.left);
					}
					else if(level.getEntity(transform.position,transform.TransformDirection(Vector3.forward)+transform.TransformDirection(Vector3.left)).type==states.empty
						&& level.getEntity(transform.position,Vector3.up+transform.TransformDirection(Vector3.left)+transform.TransformDirection(Vector3.forward)).type==states.empty)
					{
						rotation = transform.TransformDirection(new Vector3(0,90,0));						
						return transform.TransformDirection(Vector3.forward+Vector3.left);
					}
				}
				
			}	
			
		}
		
		//if up or down is pressed
		//if it's up, check if character can climb up
		//if down then fall
		if (Input.GetButtonDown("Vertical"))
			{
				float test = Input.GetAxis("Vertical");				
				if (test > 0)
				{
					if (level.getEntity(transform.position,Vector3.up).type==states.empty&&
						level.getEntity(transform.position+Vector3.up,transform.TransformDirection(Vector3.forward)).type==states.empty)
					{					
							return Vector3.up+transform.TransformDirection(Vector3.forward);							
					}
				}
				else 
				{
					return Vector3.down;							
					
				}
			}		
		return Vector3.zero;
	}
	
}

