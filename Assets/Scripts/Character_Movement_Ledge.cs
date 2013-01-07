using UnityEngine;
using System.Collections;

public class Character_Movement_Ledge : MonoBehaviour {
	
	private Character_Movement mov;
	// Use this for initialization
	void Start () {
		mov = gameObject.GetComponent<Character_Movement>();	
	}
	
	// Update is called once per frame
	void Update () {
		
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
			if (Input.GetButton("Horizontal"))
			{
				float test = Input.GetAxis("Horizontal");
				{
					if (test>0)
					{
						transform.Translate(transform.TransformDirection(new Vector3(1,0,0)));
					}

					else
					{
						transform.Translate(transform.TransformDirection(new Vector3(-1,0,0)));

					}
				}
			}
		}		
	}
}
