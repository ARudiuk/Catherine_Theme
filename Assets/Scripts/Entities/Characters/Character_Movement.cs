using UnityEngine;
using System.Collections;

public class Character_Movement {

	private Vector3 Forward = new Vector3(0,0,1); // saved vectors since used often
	private Vector3 Back = new Vector3(0,0,-1);
	private Vector3 Left = new Vector3(-1,0,0);
	private Vector3 Right = new Vector3(1,0,0);
	private Vector3 Up = new Vector3(0,1,0);
	private Vector3 Down = new Vector3(0,-1,0);
	
	//time to move: variable to tweak how long you have to hold a direction to move
	//public float timetoFall; // variable to tweak how long it takes to fall down one level			
	
	//COMMENT THIS
	//COMMENT THIS
	//COMENT THIS
	public Character_Movement () {		
		
	}
	
	public Vector3 move(Level level, Transform transform, TimeDirection timeD,float timetoMove, out Vector3 rotation)
	{		
		//currently when going to hang off a ledge you are falling for a split second
		//could be an issue??
		
		rotation = Vector3.zero;
		if(level.getEntity(transform.position,Vector3.down).type==states.empty)
		{			
			return Down;
		}
		
		if(timeD.direction!=transform.TransformDirection(Vector3.forward)) //if direction that is designated for character does not equal current rotation
		{
			if (timeD.direction==Vector3.right) //if right
			{				
				rotation = new Vector3(0,90,0)-transform.eulerAngles; //turn towards right					
			}
			else if(timeD.direction==Vector3.left)
			{
				rotation = new Vector3(0,-90,0)-transform.eulerAngles;
			}
			else if(timeD.direction==Vector3.forward)
			{
				rotation = new Vector3(0,0,0)-transform.eulerAngles;
			}
			else if(timeD.direction==Vector3.back)
			{
				rotation = new Vector3(0,180,0)-transform.eulerAngles;
			}						
		}			
			
		if(timeD.time>timetoMove)
			{				
				return displace(level,transform.position, transform);
			}
		
		return Vector3.zero;		
	}
		
		public Vector3 displace(Level level, Vector3 position, Transform transform)
		{	
			Vector3 forward = transform.TransformDirection(Vector3.forward);
			if(level.getEntity(position,forward).type==states.empty) //if there is no collision ahead, and the time is large enough then move
			{				
				if(level.getEntity(position,forward+Vector3.down).type==states.empty)
				{
					return forward+Vector3.down;
				}				
				else
				{							
					return forward; //translate object in forward direction
				}
			}
			else if(level.getEntity(position,forward).type!=states.empty
			&& level.getEntity(position,Vector3.up).type== states.empty
			&& level.getEntity(position+forward,Vector3.up).type==states.empty) //if no unit above the block infront, and no block above, then move up a block
			{					
					return Up+forward;
			}
			
			return Vector3.zero;
			
		}
	
		
	}	


