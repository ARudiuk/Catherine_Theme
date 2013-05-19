using UnityEngine;
using System.Collections;

public class Character_Movement_Ledge {
	
	public Character_Movement_Ledge () {
			
	}
	
	// Update is called once per frame
	public Vector3 move (Level level, Transform transform, TimeDirection timeD, out Vector3 rotation) {	
		
		//Character_Block_Move otherBlock = gameObject.GetComponent<Character_Block_Move>();
				
		//I broke ledge hanging I'm shit I know
		//this input check may need fixing later when movement has been smootehr
		
		rotation = Vector3.zero;
		
		if(Input.GetButton("Grab"))
		{			
			return Vector3.down;
		}
		
		if (Input.GetButtonDown("Horizontal"))
			{
				float test = Input.GetAxis("Horizontal");
			
			if(test>0)
			{
				
				if(level.getEntity(transform.position,transform.TransformDirection(Vector3.right)).type==states.basicblock)
				{
					rotation = transform.TransformDirection(new Vector3(0,90,0))-transform.eulerAngles;
					return Vector3.zero;
				}
				
				else
				{
					if(level.getEntity(transform.position+transform.TransformDirection(Vector3.forward),transform.TransformDirection(Vector3.right)).type==states.basicblock)
					{						
						return transform.TransformDirection(Vector3.right);
					}
					else if(level.getEntity(transform.position,transform.TransformDirection(Vector3.forward)+transform.TransformDirection(Vector3.right)).type==states.empty)
					{
						rotation = transform.TransformDirection(new Vector3(0,-90,0));
						return transform.TransformDirection(Vector3.forward+Vector3.right);
					}
				}
				
			}
			
			else
			{
				
				if(level.getEntity(transform.position,transform.TransformDirection(Vector3.left)).type==states.basicblock)
				{
					rotation = transform.TransformDirection(new Vector3(0,-90,0));
					return Vector3.zero;
				}
				 
				else
				{					
					if(level.getEntity(transform.position,transform.TransformDirection(Vector3.forward)+transform.TransformDirection(Vector3.left)).type==states.basicblock)
					{		
						return transform.TransformDirection(Vector3.left);
					}
					else if(level.getEntity(transform.position,transform.TransformDirection(Vector3.forward)+transform.TransformDirection(Vector3.left)).type==states.empty)
					{
						rotation = transform.TransformDirection(new Vector3(0,90,0));						
						return transform.TransformDirection(Vector3.forward+Vector3.left);
					}
				}
				
			}	
			
		}
			
		if (Input.GetButtonDown("Vertical"))
			{
				float test = Input.GetAxis("Vertical");
				rotation = transform.eulerAngles;
				if (test > 0)
				{
					if (level.getEntity(transform.position,Vector3.up).type==states.empty&&
						level.getEntity(transform.position+Vector3.up,transform.TransformDirection(Vector3.forward)).type==states.empty)
					{					
							return Vector3.down;							
					}
				}
				else 
				{
					if (level.getEntity(transform.position,Vector3.down).type==states.empty)
					{				
						return Vector3.down;							
					}
				}
			}		
		return Vector3.zero;
	}
	
}

