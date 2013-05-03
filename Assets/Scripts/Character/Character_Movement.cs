using UnityEngine;
using System.Collections;

public class Character_Movement : MonoBehaviour {

	private Vector3 Forward = new Vector3(0,0,-1); // saved this forward vector since used often
	private Vector3 Back = new Vector3(0,0,1);
	private Vector3 Left = new Vector3(-1,0,0);
	private Vector3 Right = new Vector3(1,0,0);
	private Vector3 Up = new Vector3(0,1,0);
	private Vector3 Down = new Vector3(0,-1,0);
	
	public float timetoMove; // variable to tweak how long you have to hold a direction to move
	public float timetoFall; // variable to tweak how long it takes to fall down one level

	public timeDirection timeD; // variable that holds information on what direction is faced, and how long the button for that direction was held down
	public Vector3 checkDir;
	public bool Hanging;
	public bool Moving;
	
	public Vector3 position;

	void Start () {
		timeD=new timeDirection(0f,Forward);	//initializess the timeD varible, see bottom for structure
		Hanging = false; // doesn't do normal movement if hanging
	}
	
	Vector3 move(states [,,] map, Vector3 position)
	{
		Debug.DrawRay(transform.position,transform.TransformDirection(Forward),Color.red); //shows debug of ray collision, check scene view
		//Debug.DrawRay(transform.position,transform.TransformDirection(Back),Color.blue);
		//Debug.DrawRay(transform.position,transform.TransformDirection(Left),Color.green);
		//Debug.DrawRay(transform.position,transform.TransformDirection(Right),Color.green);		
		//Debug.DrawRay(transform.position,transform.TransformDirection(Up),Color.black);	
		//Debug.DrawRay(transform.position,transform.TransformDirection(Down),Color.black);	
		
		//currently when going to hang off a ledge you are falling for a split second
		//could be an issue??
		
		if(map[(int)position.x,(int)position.y,(int)position.z-1]==states.empty)
			return new Vector3(0,0,-1);	
		
		checkDir = timeD.direction;

		Debug.DrawRay(transform.position,transform.TransformDirection(Forward),Color.red); //shows debug of ray collision, check scene view
		
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
				return displace(map,position);
			}
		
		if(!Input.GetButton("Vertical")&&!Input.GetButton("Horizontal") || Input.GetButton("Grab")) //resets if no direction is held or grab held
			timeD.time = 0f;			

		if(timeD.time!=0)
			Debug.Log(timeD.time);
		
		return Vector3.zero;	
	}	

		
		public Vector3 displace(states [,,] map, Vector3 position)
		{			
			if(map[(int)(position.x+timeD.direction.x),(int)(position.y+timeD.direction.y),(int)(position.z+timeD.direction.z)]==states.empty) //if there is no collision ahead, and the time is large enough then move
					{
						transform.Translate(Forward); //translate object in forward direction
						timeD.time -= timetoMove; //subtracts time so unit doesn't move immediately 
					}	
			else if(map[(int)(position.x+timeD.direction.x),(int)(position.y+timeD.direction.y),(int)(position.z+timeD.direction.z)]==states.empty 
			&& map[(int)position.x,(int)position.y+1,(int)position.z]== states.empty
			&& map[(int)(position.x+timeD.direction.x),(int)(position.y+timeD.direction.y)+1,(int)(position.z+timeD.direction.z)]==states.empty) //if not unit above the block infront, and no block above, then move up a block
			{
					transform.Translate(new Vector3(0,1,-1));
					timeD.time -= timetoMove;
			}
			
			return Vector3.zero;
			
		}
	
		public void pushStep(float dir)
		{
		
			if(timeD.direction== Forward || timeD.direction== Right )
				if(dir>0)
					transform.Translate(Forward);
				else
					transform.Translate(-Forward);
			else
				if(dir>0)
					transform.Translate(-Forward);
				else
					transform.Translate(Forward);
		}

	}	
	public struct timeDirection //structure that holds direction of player, and time button is held for that direction
	{
		public float time;
		public Vector3 direction;

		public timeDirection(float x, Vector3 y) //initializer
		{
			time = x;
			direction = y;
		}
	}


