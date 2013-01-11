using UnityEngine;
using System.Collections;

public class Character_Movement : MonoBehaviour {

	// Use this for initialization
	private Vector3 Forward = new Vector3(0,0,-1); // saved this forward vector since used often
	private Vector3 Back = new Vector3(0,0,1);
	private Vector3 Left = new Vector3(-1,0,0);
	private Vector3 Right = new Vector3(1,0,0);
	private Vector3 Up = new Vector3(0,1,0);
	private Vector3 Down = new Vector3(0,-1,0);
	
	public float timetoMove; // variable to tweak how long you have to hold a direction to move

	public timeDirection timeD; // variable that holds information on what direction is faced, and how long the button for that direction was held down
	public string checkDir;
	public bool Hanging;

	void Start () {
		timeD=new timeDirection(0f,"none");	//initializess the timeD varible, see bottom for structure
		Hanging = false; // doesn't do normal movement if hanging
	}
	
	void Update()
	{
		Debug.DrawRay(transform.position,transform.TransformDirection(Forward),Color.red); //shows debug of ray collision, check scene view
		Debug.DrawRay(transform.position,transform.TransformDirection(Back),Color.blue);
		Debug.DrawRay(transform.position,transform.TransformDirection(Left),Color.green);
		Debug.DrawRay(transform.position,transform.TransformDirection(Right),Color.green);		
		Debug.DrawRay(transform.position,transform.TransformDirection(Up),Color.black);	
		Debug.DrawRay(transform.position,transform.TransformDirection(Down),Color.black);	
		
		checkDir = timeD.direction;

		if(!Hanging)
		{
			Debug.DrawRay(transform.position,transform.TransformDirection(Forward),Color.red); //shows debug of ray collision, check scene view
			if(Input.GetButton("Horizontal") && !Input.GetButton("Grab")) //checks left/right directional keys and if grab isn't pressed
			{
				float test = Input.GetAxis("Horizontal");//check if movement is along x-axis
				if (test>0) //if right
				{
					transform.eulerAngles = new Vector3(0,-90,0); //turn towards right
					if (timeD.direction!="right") //checks if last button was for right
					{
						timeD.time = 0; //resets time, and sets direction to right
						timeD.direction = "right";
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
					if (timeD.direction!="left")
					{
						timeD.time = 0;
						timeD.direction = "left";
						Debug.Log("left");
					}
					else
					{
						timeD.time += Time.deltaTime;
					}	
				}		

				move();			
			}

			if(Input.GetButton("Vertical")&& !Input.GetButton("Grab")) //same as last if block, except checks up/down directional keys
				{
				float test = Input.GetAxis("Vertical");//check if movement is along z-axis
				if (test>0)
				{
					transform.eulerAngles = new Vector3(0,180,0);
					if (timeD.direction!="up")
					{
						timeD.time = 0;
						timeD.direction = "up";
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
					if (timeD.direction!="down")
					{
						timeD.time = 0;
						timeD.direction = "down";
						Debug.Log("down");
					}
					else
					{
						timeD.time += Time.deltaTime;
					}				
				}			
				
				move();

				}
			if(!Input.GetButton("Vertical")&&!Input.GetButton("Horizontal") || Input.GetButton("Grab")) //resets if no direction is held or grab held
				timeD.time = 0f;			

			if(timeD.time!=0)
				Debug.Log(timeD.time);
		}

		}

		public void move()
		{
			if(!Physics.Raycast(transform.position,transform.TransformDirection(Forward),1)&&timeD.time>timetoMove) //if there is no collision ahead, and the time is large enough then move
					{
						transform.Translate(Forward); //translate object in forward direction
						timeD.time -= timetoMove; //subtracts time so unit doesn't move immediately 
					}	
			else if(Physics.Raycast(transform.position,transform.TransformDirection(Forward),1) && timeD.time>timetoMove && !Physics.Raycast(transform.position,new Vector3(0,1,0),1) 
			&& !Physics.Raycast(transform.position+transform.TransformDirection(Forward),new Vector3(0,1,0),1)) //if not unit above the block infront, and no block above, then move up a block
			{
					transform.Translate(new Vector3(0,1,-1));
					timeD.time -= timetoMove;
			}
		}
	
		public void pushStep(float dir)
		{
			if(timeD.direction== "up" || timeD.direction== "right" )
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
		public string direction;

		public timeDirection(float x, string y) //initializer
		{
			time = x;
			direction = y;
		}
	}


