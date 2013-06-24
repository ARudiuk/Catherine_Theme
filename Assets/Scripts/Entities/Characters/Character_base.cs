 //move speeds here later

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character_base : MonoBehaviour
{
	
	//references to three required movement libraries
	private Character_Movement movement;
	private Character_Movement_Ledge ledge_movement;
	private Character_Block_Move block_movement;
	
	//reference to level
	public Level level;
	
	//holds time to move
	public float timetoMove;
	
	//if this is true than no imput is taken for moving
	public bool moving;
	
	//determins which movement library to use
	public bool hanging;
	
	//rotation variable to pass. This is create class level because it is always returned
	public Vector3 rotation;
	
	public TimeDirection timeD; // variable that holds information on what direction is faced, and how long the button for that direction was held down
	
	// Use this for initialization
	void Start ()
	{
		//initialize all the things
		movement = new Character_Movement();
		ledge_movement = new Character_Movement_Ledge();
		block_movement = new Character_Block_Move();
		moving = false;	
		hanging = false;
		timeD=new TimeDirection(0f,Vector3.back);	//initializess the timeD varible, see bottom for structure
		rotation = new Vector3(0,180,0); //makes character face forward
		transform.eulerAngles = rotation;
		timetoMove = 0.15f;
	}
	
	// Update is called once per frame
	void Update ()
	{		
		if(inputvalue()!=timeD.direction)
		{
			timeD.time = 0f;
			timeD.direction = inputvalue();
		}
		else if(timeD.direction!=Vector3.zero)
		{
			timeD.time+=Time.deltaTime;
		}
		//if not moving get input
		if(!moving)
		{		
			//if hanging, get movement info from ledge class
			if(hanging)
				{		
					//get movement from class
					Vector3 move = ledge_movement.move(level, transform, timeD, out rotation);
					//if direction is down while hanging, then make hanging false
					//also if no blocks below, then start falling
					if (move == Vector3.down)
					{
						hanging = false;
					
					//might not want to do type check in character class
						if(level.getEntity(transform.position,Vector3.down).type==states.empty)
							level.moveObject(transform.position,Vector3.down, rotation);
					}
					//if direction is forward and up, then character is no longer moving, they move untop of the block
					else if(move==Vector3.up+transform.TransformDirection(Vector3.forward))
					{
						hanging = false;
						level.moveObject(transform.position,move, rotation);	
					}
					//all any other non zero movement, then it has to be a movement along an edge
					else if(move!=Vector3.zero)
						level.moveObject(transform.position,move, rotation);					
				}
			//situation if character isn't hanging
			else{
				//if user is holding down grab
				//EXAM THIS SECTION IN MORE DETAIL IN THE FUTURE
				if(Input.GetButton("Grab"))
				{					
					List<Vector3> move = block_movement.move(level,transform,timeD,out rotation);				
   					if(move.Count==0&&rotation != Vector3.zero)
					{
						level.moveObject(transform.position,Vector3.zero,rotation);
					}
					else if(move.Count==2)
					{
						level.movetwoObjects(transform.position,transform.position+transform.TransformDirection(Vector3.forward),move[1],move[0],Vector3.zero,Vector3.zero);//move[1] is character other is block, maybe switch around to be less confusing
						if(move[1]==Vector3.down+transform.TransformDirection(Vector3.back))
							hanging = true;
					}
					else if(move.Count>2)
					{
						Debug.LogError("More than two movements counted");
					}
				
				}
				//if not hanging, then make sure not falling. If falling then check if you grab a ledge.				
				else
				{					
					this.hanging = hangingTest();
					if(!hanging)
					{					
						Vector3 move = movement.move(level, transform,timeD,timetoMove, out rotation);						
						if(move != Vector3.zero || rotation!= Vector3.zero)
						{							
							level.moveObject(transform.position,move, rotation);							
						}
					}
				}			
			}
		}		
	}	
	
	//check to see if hanging
	//first make sure bottom block is empty
	//then check if there is block behind player to hang. Maybe this should be changed, to just auto do it when moving of a ledge
	//then check surroundingobjects to grab. Should try to make sure he grabs forward direction
	//if can't grab anything, or no empty space below, return false
	bool hangingTest()
	{
		if (level.getEntity(transform.position,Vector3.down).type==states.empty)
		{			
			if(level.getEntity(transform.position,-timeD.direction).type==states.block)
			{
				if (level.getEntity(transform.position-timeD.direction,Vector3.up).type==states.empty)
				{
					transform.LookAt(transform.position-timeD.direction);
					return true;
				}
			}
			
			if(level.getsurroundingEntity(transform.position).Count>0)
			{
				List<Entity> values = level.getsurroundingEntity(transform.position);
				foreach (Entity entity in values)
				{
					if(level.getEntity(entity.getCoordinates(),Vector3.up).type==states.empty)
					{
						transform.LookAt(entity.getCoordinates());
						return true;			
					}
				}
			}
		}
		return false;
	}
	
	Vector3 inputvalue()
	{		
		if(Input.GetButton("Horizontal"))
			{
				float test = Input.GetAxis("Horizontal");
				if(test>0)
					return Vector3.right;
				else
					return Vector3.left;					
			}
		else if(Input.GetButton("Vertical"))
			{
				float test = Input.GetAxis("Vertical");
				if(test>0)
					return Vector3.forward;
				else
					return Vector3.back;	
			}
		else 
		{
			return Vector3.zero;
		}
				
	}
}

