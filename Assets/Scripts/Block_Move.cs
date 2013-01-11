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

	// Update is called once per frame
	void Update () {
		
		Character_Movement other = gameObject.GetComponent<Character_Movement>();
        
		
		RaycastHit front;
		RaycastHit left;
		RaycastHit right;
		RaycastHit back;

		Vector3 direction;
				
		
		if(Input.GetButton("Grab"))
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
					transform.Rotate(new Vector3(0,0,0));
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
						}
					else if(Physics.Raycast(transform.position,transform.TransformDirection(new Vector3(-1,0,0)),out right,1))
						{
							transform.Rotate(new Vector3(0,90,0));
							chosen = true;
							RotateTimeD("r", ref other);
						}
					else if(Physics.Raycast(transform.position,transform.TransformDirection(new Vector3(0,0,1)),out back,1))
						{
							transform.Rotate(new Vector3(0,180,0));
							chosen = true;
							RotateTimeD("d", ref other);
						}
					}
				
					else //left/down
					{
						if(Physics.Raycast(transform.position,transform.TransformDirection(new Vector3(-1,0,0)),out right,1))
						{
							transform.Rotate(new Vector3(0,90,0));
							chosen = true;
							RotateTimeD("r", ref other);
						}
					
					else if(Physics.Raycast(transform.position,transform.TransformDirection(new Vector3(1,0,0)),out left,1))
						{
							transform.Rotate(new Vector3(0,-90,0));
							chosen = true;
							RotateTimeD("l", ref other);
						}
					else if(Physics.Raycast(transform.position,transform.TransformDirection(new Vector3(0,0,1)),out back,1))
						{
							transform.Rotate(new Vector3(0,180,0));
							chosen = true;
							RotateTimeD("d", ref other);
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
						if(test>0){
							pushDirection = new Vector3(1,0,0);}
						else{
							pushDirection = new Vector3(-1,0,0);}						

						front.transform.Translate(pushDirection);

						front.transform.Translate(direction);

						
						if(Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0,0,-1)),new Vector3(0,-1,0),1))
							other.pushStep(test);
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
							pushDirection = new Vector3(0,0,1);}
						else{
							pushDirection = new Vector3(0,0,-1);}	


						front.transform.Translate(pushDirection);

						front.transform.Translate(direction);

						
						if(Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0,0,-1)),new Vector3(0,-1,0),1))
							other.pushStep(test);
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
