using UnityEngine;
using System.Collections;

public class Character_Movement {

	private Vector3 Forward = new Vector3(0,0,1); // saved vectors since used often
	private Vector3 Back = new Vector3(0,0,-1);
	private Vector3 Left = new Vector3(-1,0,0);
	private Vector3 Right = new Vector3(1,0,0);
	private Vector3 Up = new Vector3(0,1,0);
	private Vector3 Down = new Vector3(0,-1,0);
	
	public float timetoMove; // variable to tweak how long you have to hold a direction to move
	public float timetoFall; // variable to tweak how long it takes to fall down one level

			
	public bool Hanging;	

	public Character_Movement (float timetoMove) {
		Hanging = false; // doesn't do normal movement if hanging
		this.timetoMove = timetoMove;
	}
	
	public Vector3 move(Level level, Transform transform, TimeDirection timeD)
	{		
		//currently when going to hang off a ledge you are falling for a split second
		//could be an issue??
		
		if(level.getEntity(transform.position,Vector3.down).type==states.empty)
			return Down;	
		
		if(Input.GetButton("Horizontal") && !Input.GetButton("Grab")) //checks left/right directional keys and if grab isn't pressed
		{
			float test = Input.GetAxis("Horizontal");//check if movement is along x-axis
			if (test>0) //if right
			{
				transform.eulerAngles = new Vector3(0,-90,0); //turn towards right
				if (timeD.direction!=Right) //checks if last button was for right
				{
					timeD.time = 0; //resets time, and sets direction to right
					timeD.direction = Right;
					Debug.Log("right"); //debugs right in console
				}
				else
				{
					timeD.time += Time.deltaTime; //only situation where direction isn't right is if it is right, so adds up time to move
				}				
			}
			else //same as last block, different direction
			{
				transform.eulerAngles = new Vector3(0,90,0);
				if (timeD.direction!=Left)
				{
					timeD.time = 0;
					timeD.direction = Left;
					Debug.Log("left");
				}
				else
				{
					timeD.time += Time.deltaTime;
				}	
			}					
		}

		if(Input.GetButton("Vertical")&& !Input.GetButton("Grab")) //same as last if block, except checks up/down directional keys
			{
			float test = Input.GetAxis("Vertical");//check if movement is along z-axis
			if (test>0)
			{
				transform.eulerAngles = new Vector3(0,180,0);
				if (timeD.direction!=Forward)
				{
					timeD.time = 0;
					timeD.direction = Forward;
					Debug.Log("up");
				}
				else
				{
					timeD.time += Time.deltaTime;
				}				
			}
			else
			{
				transform.eulerAngles = new Vector3(0,0,0);
				if (timeD.direction!=Back)
				{
					timeD.time = 0;
					timeD.direction = Back;
					Debug.Log("down");
				}
				else
				{
					timeD.time += Time.deltaTime;
				}				
			}		
			
			}
		if(timeD.time>timetoMove)
			{
				return displace(level,transform.position, timeD);
			}
		
		if((!Input.GetButton("Vertical")&&!Input.GetButton("Horizontal")) || Input.GetButton("Grab")) //resets if no direction is held or grab held
			timeD.time = 0f;			

		if(timeD.time!=0)
			Debug.Log(timeD.time);
		
		return Vector3.zero;	
	}	

		
		public Vector3 displace(Level level, Vector3 position, TimeDirection timeD)
		{			
			if(level.getEntity(position,timeD.direction).type==states.empty) //if there is no collision ahead, and the time is large enough then move
					{						
						timeD.time -= timetoMove; //subtracts time so unit doesn't move immediately 	
						return timeD.direction; //translate object in forward direction
					}	
			else if(level.getEntity(position,timeD.direction).type!=states.empty
			&& level.getEntity(position,Vector3.up).type== states.empty
			&& level.getEntity(position+timeD.direction,Vector3.up).type==states.empty) //if not unit above the block infront, and no block above, then move up a block
			{
					timeD.time -= timetoMove;
					return Up+timeD.direction;
			}
			
			return Vector3.zero;
			
		}
	
		
	}	


