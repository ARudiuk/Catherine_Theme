 //move speeds here later

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character_base : MonoBehaviour
{
	
	private Character_Movement movement;
	private Character_Movement_Ledge ledge_movement;
	private Character_Block_Move block_movement;
	
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
		block_movement = new Character_Block_Move();
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
					if (move == Vector3.down)
					{
						hanging = false;
						if(level.getEntity(transform.position,Vector3.down).type==states.empty)
							level.moveObject(transform.position,Vector3.down);
					}
					
					else if(move==Vector3.up+transform.TransformDirection(Vector3.forward))
					{
						hanging = false;
						level.moveObject(transform.position,move);	
					}
					else if(move!=Vector3.zero)
						level.moveObject(transform.position,move);
					transform.Rotate(rotation);
				}
			else{
				if(Input.GetButton("Grab"))
				{					
					List<Vector3> move = block_movement.move(level,transform,timeD,out rotation);
					transform.Rotate(rotation);
					if(move.Count != 0)
					{
						if(move.Count<=2)
						{
							level.movetwoObjects(transform.position,transform.position+transform.TransformDirection(Vector3.forward),move[1],move[0]);//move[1] is character other is block, maybe switch around to be less confusing
							if(move[1]==Vector3.down+transform.TransformDirection(Vector3.back))
								hanging = true;
						}
						else if(move.Count>2){
							Debug.LogError("More than two movements counted");
						}
					}
				}			
				else
				{					
					this.hanging = hangingTest();
					if(!hanging)
					{					
						Vector3 move = movement.move(level, transform,timeD, out rotation);
						transform.Rotate(rotation);
						if(move != Vector3.zero)
						{
							level.moveObject(transform.position,move);
							StartCoroutine(animate (0.5f,move));//make it unable to move during animation
						}
					}
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
	
	public IEnumerator animate(float duration,Vector3 move)
	{
		Vector3 initial = transform.position;
		Vector3 final = transform.position+move;
		for(float t = 0; t < duration; t += Time.deltaTime)
			{
				transform.position = Vector3.Lerp(initial, final, t/duration);	
				yield return null;
			}		
	}
}

