using UnityEngine;
using System.Collections;

public class Block_Move : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		RaycastHit hit;
		Vector3 direction;

		if(Input.GetButton("Grab"))
		{
			if(Input.GetButtonDown("Horizontal"))
			{
				float test = Input.GetAxis("Horizontal");
				if(Physics.Raycast(transform.position,transform.TransformDirection(new Vector3(0,0,-1)),out hit,1))
				{
					if(test>0){
						direction = transform.TransformDirection(new Vector3(-1,0,0));}
					else{
						direction = transform.TransformDirection(new Vector3(1,0,0));}						

					hit.transform.Translate(direction);
				}
				
			}

			else if(Input.GetButtonDown("Vertical"))
			{
				float test = Input.GetAxis("Vertical");
				if(Physics.Raycast(transform.position,transform.TransformDirection(new Vector3(0,0,-1)),out hit,1))
				{
					if(test>0){
						direction = transform.TransformDirection(new Vector3(0,0,-1));}
					else{
						direction = transform.TransformDirection(new Vector3(0,0,1));}	

					hit.transform.Translate(direction);
				}
			}
		}
	
	}
	
}
