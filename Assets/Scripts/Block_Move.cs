using UnityEngine;
using System.Collections;

public class Block_Move : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	Transform curTransform;
        curTransform = gameObject.GetComponent<Transform>();
        curTransform = gameObject.transform;
	}
	
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
			
			if(Physics.Raycast(transform.position,transform.TransformDirection(new Vector3(0,0,-1)),out front,1))
				{
					transform.Rotate(new Vector3(0,0,0));
				}
			
			else if(Physics.Raycast(transform.position,transform.TransformDirection(new Vector3(-1,0,0)),out right,1))
				{
					transform.Rotate(new Vector3(0,-90,0));
				}
			
			else if(Physics.Raycast(transform.position,transform.TransformDirection(new Vector3(1,0,0)),out left,1))
				{
					transform.Rotate(new Vector3(0,90,0));
				}
			
			else if(Physics.Raycast(transform.position,transform.TransformDirection(new Vector3(0,0,1)),out back,1))
				{
					transform.Rotate(new Vector3(0,180,0));
				}
			
			if(Input.GetButtonDown("Horizontal"))
			{
				float test = Input.GetAxis("Horizontal");
				if(Physics.Raycast(transform.position,transform.TransformDirection(new Vector3(0,0,-1)),out front,1))
				{
					if(other.timeD.direction == "right" || other.timeD.direction == "left") 
					{
						if(test>0){
							direction = new Vector3(1,0,0);}
						else{
							direction = new Vector3(-1,0,0);}						

						front.transform.Translate(direction);
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
							direction = new Vector3(0,0,1);}
						else{
							direction = new Vector3(0,0,-1);}	

						front.transform.Translate(direction);
					}
				}
			}
		}
	
	}
	
}
