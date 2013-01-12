using UnityEngine;
using System.Collections;

public class Block_Move : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	}
	
	public bool chosen = false;
	bool grabbed = false;
	public Animation grab;
	public Animation letGo;
	public GameObject a;
	public Vector3 pushDirection = new Vector3(0,0,0);
	bool blocked;
		
	// Update is called once per frame
	void Update () {
		
		Character_Movement other = gameObject.GetComponent<Character_Movement>();
		
		Character_Movement_Ledge otherLedge = gameObject.GetComponent<Character_Movement_Ledge>();

		RaycastHit front;
		RaycastHit left;
		RaycastHit right;
		RaycastHit back;
		RaycastHit down;

		if(Input.GetButton("Grab") && Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0,-1,0)),1))
		{
			if(!grabbed)
			{
				//a.animation.Play ("grab2");
				grabbed = true;
			}
			if(!chosen)
			{
			if(Physics.Raycast(transform.position,transform.TransformDirection(new Vector3(0,0,-1)),out front,1))
				{
				//unnecessary just for clarity
					//transform.Rotate(new Vector3(0,0,0));
					chosen = true;
				}
				
				else{
				
					if(other.timeD.direction == "right" || other.timeD.direction == "up")
					{
						if(Physics.Raycast(transform.position,transform.TransformDirection(new Vector3(1,0,0)),out left,1))
						{
							transform.Rotate(new Vector3(0,-90,0));
							chosen = true;
							RotateTimeD("l", ref other);
							Debug.Log("block_move");
						}
					else if(Physics.Raycast(transform.position,transform.TransformDirection(new Vector3(-1,0,0)),out right,1))
						{
							transform.Rotate(new Vector3(0,90,0));
							chosen = true;
							RotateTimeD("r", ref other);
							Debug.Log("block_move");
						}
					else if(Physics.Raycast(transform.position,transform.TransformDirection(new Vector3(0,0,1)),out back,1))
						{
							transform.Rotate(new Vector3(0,180,0));
							chosen = true;
							RotateTimeD("d", ref other);
							Debug.Log("block_move");
						}
					}
				
					else //left/down
					{
						if(Physics.Raycast(transform.position,transform.TransformDirection(new Vector3(-1,0,0)),out right,1))
						{
							transform.Rotate(new Vector3(0,90,0));
							chosen = true;
							RotateTimeD("r", ref other);
							Debug.Log("block_move");
						}
					
					else if(Physics.Raycast(transform.position,transform.TransformDirection(new Vector3(1,0,0)),out left,1))
						{
							transform.Rotate(new Vector3(0,-90,0));
							chosen = true;
							RotateTimeD("l", ref other);
							Debug.Log("block_move");
						}
					else if(Physics.Raycast(transform.position,transform.TransformDirection(new Vector3(0,0,1)),out back,1))
						{
							transform.Rotate(new Vector3(0,180,0));
							chosen = true;
							RotateTimeD("d", ref other);
							Debug.Log("block_move");
						}
			}	
				}
			}
			if(Input.GetButtonDown("Horizontal"))
			{
				float test = Input.GetAxis("Horizontal");
				if(Physics.Raycast(transform.position,transform.TransformDirection(new Vector3(0,0,-1)),out front,1))
				{
					if(other.timeD.direction == "right" || other.timeD.direction == "left") 
					{
						//checks if there's a block beind you
						if(test>0){
							if(other.timeD.direction == "left" && Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0,0,1)),1))
								blocked = true;
							else
								{
									pushDirection = new Vector3(1,0,0);
									blocked = false;
								}
							}
						else{
							if(other.timeD.direction == "right" && Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0,0,1)),1))
								blocked = true;
							else
								{	
									pushDirection = new Vector3(-1,0,0);
									blocked = false;
								}						
							}
						if(!blocked)
						{
							front.transform.Translate(pushDirection);
						//this is causing the blocks on top of you
							if(Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0,0,-1)),new Vector3(0,-1,0),1) || (test > 0 && other.timeD.direction == "left") || (test < 0 && other.timeD.direction == "right"))
								other.pushStep(test);
						}
						
						if(Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0,-1,0)),out down, 1))
							other.isFalling = false;
		
						else 
							other.isFalling = true;
						
						if(other.isFalling)
						{
							otherLedge.startHanging(ref other);
							//this is dumb and shouldn't be necessary plese fix
							transform.Rotate(new Vector3(0,180,0));
							//correction factor
							transform.Translate(0,-0.157f,0);
							
						}
					}
				}
			}

			else if(Input.GetButtonDown("Vertical"))
			{
				float test = Input.GetAxis("Vertical");
				if(Physics.Raycast(transform.position,transform.TransformDirection(new Vector3(0,0,-1)),out front,1))
				{
					if(other.timeD.direction == "up" || other.timeD.direction == "down") 
					{
						if(test>0){
						if(other.timeD.direction == "down" && Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0,0,1)),1))
								blocked = true;
							else
								{
									pushDirection = new Vector3(0,0,1);
									blocked = false;
								}
							}
						else{
							if(other.timeD.direction == "up" && Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0,0,1)),1))
								blocked = true;
							else
								{	
									pushDirection = new Vector3(0,0,-1);
									blocked = false;
								}						
							}
						if(!blocked)
						{
							front.transform.Translate(pushDirection);
						//this is causing the blocks on top of you
							if(Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0,0,-1)),new Vector3(0,-1,0),1)|| (test > 0 && other.timeD.direction == "down") || (test < 0 && other.timeD.direction == "up")) 
								other.pushStep(test);
						}
						
						if(Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0,-1,0)),out down, 1))
							other.isFalling = false;
		
						else 
							other.isFalling = true;
						
						if(other.isFalling)
						{
							otherLedge.startHanging(ref other);
							//this is dumb and shouldn't be necessary plese fix
							transform.Rotate(new Vector3(0,180,0));
							//correction factor
							transform.Translate(0,-0.157f,0);
						}
					}
				}
			}
		}
		
		if(Input.GetButtonUp("Grab"))
		{
			chosen = false;
			//animation.Play ("letGo");
			grabbed = false;
		}
	
	}
	
	void RotateTimeD(string dir, ref Character_Movement other)
	{
		if(dir== "l")
		{
			if(other.timeD.direction=="up")
				other.timeD.direction="left";
			else if(other.timeD.direction=="left")
				other.timeD.direction="down";
			else if(other.timeD.direction=="down")
				other.timeD.direction="right";
			else if(other.timeD.direction=="right")
				other.timeD.direction="up";
		}
		
		if(dir== "r")
		{
			if(other.timeD.direction=="up")
				other.timeD.direction="right";
			else if(other.timeD.direction=="left")
				other.timeD.direction="up";
			else if(other.timeD.direction=="down")
				other.timeD.direction="left";
			else if(other.timeD.direction=="right")
				other.timeD.direction="down";
		}
		
		if(dir== "d")
		{
			if(other.timeD.direction=="up")
				other.timeD.direction="down";
			else if(other.timeD.direction=="left")
				other.timeD.direction="right";
			else if(other.timeD.direction=="down")
				other.timeD.direction="up";
			else if(other.timeD.direction=="right")
				other.timeD.direction="left";
		}
	}

}
