using UnityEngine;
using System.Collections;

public class Character_Movement_Ledge : MonoBehaviour {
	
	private Vector3 diagLeft = new Vector3(1,1,0);
	private Vector3 diagRight = new Vector3(-1,1,0);
	private Vector3 diagRightF = new Vector3(-1,1,-1);
	private Vector3 diagLeftF = new Vector3(1,1,-1);
	
	private Character_Movement mov;
	// Use this for initialization
	void Start () {
		mov = gameObject.GetComponent<Character_Movement>();	
	}
	
	// Update is called once per frame
	void Update () {	
		
		//Debug.DrawRay(transform.position,transform.TransformDirection(diagRight),Color.magenta);
		Debug.DrawRay(transform.position,transform.TransformDirection(diagLeft),Color.magenta);
		//Debug.DrawRay(transform.position,transform.TransformDirection(diagRightF),Color.magenta);
		Debug.DrawRay(transform.position,transform.TransformDirection(diagLeftF),Color.magenta);
		
		if(!Physics.Raycast(transform.position,new Vector3(0,-1,0),1)&&Physics.Raycast(transform.position,transform.TransformDirection(new Vector3(0,0,1)),1)
			&& !mov.Hanging)
		{
			mov.Hanging = true;
			transform.Rotate(new Vector3(0,180,0));
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
			Debug.DrawRay(transform.position+transform.TransformDirection(new Vector3(0,0,-1)),transform.TransformDirection(new Vector3(1,0,0)),Color.red); //shows debug of ray collision, check scene view
			if (Input.GetButtonDown("Horizontal"))
			{
				float test = Input.GetAxis("Horizontal");
				{
					//still go through blocks when pressing right. As well, sometimes unresponsive.
					if (test>0 && !(Physics.Raycast(transform.position,diagRight,1) || Physics.Raycast(transform.position,diagRightF,1)))
					{
						if(Physics.Raycast(transform.position, -transform.right,1))
							transform.Rotate(0,90,0);
						else if(Physics.Raycast(transform.position+transform.TransformDirection(new Vector3(0,0,-1)),transform.TransformDirection(new Vector3(-1,0,0)),1))
							transform.Translate(new Vector3(-1,0,0));
						else
						{
							transform.RotateAround(transform.position+transform.TransformDirection(new Vector3(0,0,-1)),new Vector3(0,1,0),-90);
						}
					}

					else if (test<0 && !(Physics.Raycast(transform.position,diagLeft,1) || Physics.Raycast(transform.position,diagLeftF,1)))
					{
						if(Physics.Raycast(transform.position, transform.right,1))
							transform.Rotate(0,-90,0);
						else if(Physics.Raycast(transform.position+transform.TransformDirection(new Vector3(0,0,-1)),transform.TransformDirection(new Vector3(1,0,0)),1))
							transform.Translate(new Vector3(1,0,0));
						else
							//rotating around the wrong object. Also rotating 180 degrees. 
							transform.RotateAround(transform.position+transform.TransformDirection(new Vector3(0,0,-1)),new Vector3(0,1,0),90);
					}
				}

			}

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
}
