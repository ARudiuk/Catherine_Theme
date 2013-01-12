using UnityEngine;
using System.Collections;

public class Character_Movement_Ledge : MonoBehaviour {
	
	private Vector3 diagLeft = new Vector3(1,1,0);
	private Vector3 diagRight = new Vector3(-1,1,0);
	private Vector3 diagRightF = new Vector3(-1,1,-1);
	private Vector3 diagLeftF = new Vector3(1,1,-1);

	public float test;

	private Character_Movement mov;
	// Use this for initialization
	void Start () {
		mov = gameObject.GetComponent<Character_Movement>();	
	}
	
	// Update is called once per frame
	void Update () {	
		
		Character_Movement other = gameObject.GetComponent<Character_Movement>();

		//Debug.DrawRay(transform.position,transform.TransformDirection(diagRight),Color.magenta);
		Debug.DrawRay(transform.position,transform.TransformDirection(diagLeft),Color.magenta);
		//Debug.DrawRay(transform.position,transform.TransformDirection(diagRightF),Color.magenta);
		Debug.DrawRay(transform.position,transform.TransformDirection(diagLeftF),Color.magenta);

		
		if(!Physics.Raycast(transform.position,new Vector3(0,-1,0),1)&&Physics.Raycast(transform.position,transform.TransformDirection(new Vector3(0,0,1)),1)
			&& !mov.Hanging)
		{
			mov.Hanging = true;
			transform.Rotate(new Vector3(0,180,0));
			
			if(other.timeD.direction == "up")
				other.timeD.direction = "down";
			
			if(other.timeD.direction == "left")
				other.timeD.direction = "right";
			
			if(other.timeD.direction == "down")
				other.timeD.direction = "up";
			
			if(other.timeD.direction == "right")
				other.timeD.direction = "left";
			
			rigidbody.useGravity = false;
			rigidbody.velocity = new Vector3(0,0,0);

		}		

		else if(!Physics.Raycast(transform.position,transform.TransformDirection(new Vector3(0,0,-1)),1)&& mov.Hanging)
		{
			mov.Hanging = false;
			rigidbody.useGravity = true;
		}

		if(mov.Hanging)
		{
			Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(-1,0,-1)),new Vector3(0,1,0),Color.red); //shows debug of ray collision, check scene view
			Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(1,0,-1)),new Vector3(0,1,0),Color.red);
			Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(-1,0,0)),Color.cyan);
			
			//"3" is being called when I press left which is impossible. 
			if (Input.GetButtonDown("Horizontal"))
			{
				test = Input.GetAxis("Horizontal");
				
				//still messed
				
				bool r1 = !(Physics.Raycast(transform.position,transform.TransformDirection(diagRight),1) || Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(-1,0,-1)),new Vector3(0,1,0),1));
				bool r2 = Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(-1,0,0)),1);
				bool r3 = Physics.Raycast(transform.position + new Vector3(1,0,0),transform.forward);
				
				bool l1  = !(Physics.Raycast(transform.position,transform.TransformDirection(diagLeft),1) || Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(1,0,-1)),new Vector3(0,1,0),1));
				bool l2  = Physics.Raycast(transform.position,transform.TransformDirection(new Vector3(-1,0,0)),1);
				bool l3  = Physics.Raycast(transform.position + new Vector3(-1,0,0),-transform.forward);
				
				if (test > 0 && (r1 || r2 || r3))
					{
						ledgeMoveR(ref other);

					}
						
				else if( test < 0 && (l1 || l2 || l3))
				{
					ledgeMoveL(ref other);	
				}

			}

			//fix getting blcked chceck for hit on side raycasts


			if(Input.GetButton("Grab"))
				{
					if(Physics.Raycast(transform.position,transform.TransformDirection(new Vector3(0,-1,0)),1))
						{
							transform.Translate(new Vector3(0,-1f,0));
							mov.Hanging = false;
							rigidbody.useGravity = true;
						}
				}
			
			if(Input.GetButtonDown("Release"))
			{
				mov.Hanging = false;
				rigidbody.useGravity = true;
				while(!Physics.Raycast(transform.position,transform.TransformDirection(new Vector3(0,-1,0)),1))
				{
					transform.Translate(new Vector3(0,-1f,0));
				}	
			}
			
			if (Input.GetButtonDown("Vertical"))
				{
					float test = Input.GetAxis("Vertical");

					if (test > 0 && !Physics.Raycast(transform.position-transform.forward,new Vector3(0,1,0),1))
						{
							transform.Translate(new Vector3(0,0.1f,-1));
							mov.Hanging = false;
							rigidbody.useGravity = true;
						}
					else if(test < 0)
					{
						transform.Translate(new Vector3(0,-1f,0));
						mov.Hanging = false;
						rigidbody.useGravity = true;
					}
				}
		}		
	}
	
	void ledgeMoveR(ref Character_Movement other)
	{
		//if there is a block to my right, turn 90 degrees to face it
						if(Physics.Raycast(transform.position, -transform.right,1))
						{
							transform.Rotate(0,90,0);
							
							TurnTimeDRight(ref other);
						
							Debug.Log("1");
						}
						//if I'm along a straight wall, move right along it
						else if(Physics.Raycast(transform.position+transform.TransformDirection(new Vector3(0,0,-1)),transform.TransformDirection(new Vector3(-1,0,0)),1))
						{
							if(!Physics.Raycast(transform.position, transform.TransformDirection(diagRight),1))
								{
									transform.Translate(new Vector3(-1,0,0));
									Debug.Log("2");
								}
						}
						//if the block the avatar is facing is completely unblocked, rotate 90 degrees around it
						else
						{
							transform.RotateAround(transform.position+transform.TransformDirection(new Vector3(0,0,-1)),new Vector3(0,1,0),-90);
			
							RotateTimeDRight(ref other);
							
							Debug.Log("3");
						}
	}
	
	void ledgeMoveL(ref Character_Movement other)
	{
		//if there is a block to my left, turn 90 degrees to face it
						if(Physics.Raycast(transform.position, transform.right,1))
						{
							transform.Rotate(0,-90,0);
			
							TurnTimeDLeft(ref other);
						
							Debug.Log("4");
						}
						//if I'm along a straight wall, move left along it
						else if(Physics.Raycast(transform.position+transform.TransformDirection(new Vector3(0,0,-1)),transform.TransformDirection(new Vector3(1,0,0)),1))
						{
							if(!Physics.Raycast(transform.position, transform.TransformDirection(diagLeft),1))
								{
									transform.Translate(new Vector3(1,0,0));
									Debug.Log("5");
								}
						}
						//if the block the avatar is facing is completely unblocked, rotate 90 degrees around it
						else
						{
							transform.RotateAround(transform.position+transform.TransformDirection(new Vector3(0,0,-1)),new Vector3(0,1,0),90);
			
							RotateTimeDLeft(ref other);
			
							Debug.Log("6");
						}
	}
	
	void TurnTimeDRight(ref Character_Movement other)
	{
		if(other.timeD.direction == "up")
		{
			other.timeD.direction = "right";
		}
		
		else if(other.timeD.direction == "right")
		{
			other.timeD.direction = "down";
		}
		
		else if(other.timeD.direction == "down")
		{
			other.timeD.direction = "left";
		}
		
		else if(other.timeD.direction == "left")
		{
			other.timeD.direction = "up";
		}
	}
	
	void RotateTimeDRight(ref Character_Movement other)
	{
		if(other.timeD.direction == "up")
		{
			other.timeD.direction = "left";
		}
		
		else if(other.timeD.direction == "right")
		{
			other.timeD.direction = "up";
		}
		
		else if(other.timeD.direction == "down")
		{
			other.timeD.direction = "right";
		}
		
		else if(other.timeD.direction == "left")
		{
			other.timeD.direction = "down";
		}
	}
	
	void TurnTimeDLeft(ref Character_Movement other)
	{
		if(other.timeD.direction == "up")
		{
			other.timeD.direction = "left";
		}
		
		else if(other.timeD.direction == "right")
		{
			other.timeD.direction = "up";
		}
		
		else if(other.timeD.direction == "down")
		{
			other.timeD.direction = "right";
		}
		
		else if(other.timeD.direction == "left")
		{
			other.timeD.direction = "down";
		}
	}
	
	void RotateTimeDLeft(ref Character_Movement other)
	{
		if(other.timeD.direction == "up")
		{
			other.timeD.direction = "right";
		}
		
		else if(other.timeD.direction == "right")
		{
			other.timeD.direction = "down";
		}
		
		else if(other.timeD.direction == "down")
		{
			other.timeD.direction = "left";
		}
		
		else if(other.timeD.direction == "left")
		{
			other.timeD.direction = "up";
		}
	}
	
	
}