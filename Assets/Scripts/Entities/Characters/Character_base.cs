 //move speeds here later

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character_base : MonoBehaviour
{
	
	private Character_Movement movement;
	private Character_Movement_Ledge ledge_movement;
	
	public Level level;
	
	public float timetoMove;
	
	public bool moving;
	public bool hanging;
	
	public Vector3 rotation;
	
	public TimeDirection timeD; // variable that holds information on what direction is faced, and how long the button for that direction was held down
	
	// Use this for initialization
	void Start ()
	{
		movement = new Character_Movement(timetoMove);
		ledge_movement = new Character_Movement_Ledge();
		moving = false;	
		hanging = false;
		timeD=new TimeDirection(0f,Vector3.forward);	//initializess the timeD varible, see bottom for structure
		rotation = new Vector3(0,180,0); //makes character face forward
		transform.eulerAngles = rotation;
	}
	
	// Update is called once per frame
	void Update ()
	{		
		if(!moving)
		{
			if(hanging)
			{				
				Vector3 move = ledge_movement.move(level, transform, timeD, out rotation);
 				transform.Rotate(rotation);
				if (move == Vector3.down)
					hanging = false;
				else if(move!=Vector3.zero)
					level.moveObject(transform.position,move);				
			}					
			
			else
			{
				this.hanging = hangingTest();
				if(!hanging)
				{					
					Vector3 move = movement.move(level, transform,timeD, out rotation);
					transform.Rotate(rotation);
					if(move != Vector3.zero)
						level.moveObject(transform.position,move);
				}
			}			
		}
	}
	
	bool hangingTest()
	{
		if (level.getEntity(transform.position,Vector3.down).type==states.empty)
		{			
			if(level.getEntity(transform.position,-timeD.direction).type==states.basicblock)
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
}

