using UnityEngine;
using System.Collections;

public class Camera_Follow : MonoBehaviour {	
	
	GameObject character;
	public float distance;
	public float degree;
	void Start()
	{	
		distance = 12f;
		degree = 20f;
		character =null;
	}
	// Update is called once per frame
	void Update () {
		
		if(character == null)
			if(GameObject.Find ("CrappyCharacter(Clone)"))
				character = GameObject.Find ("CrappyCharacter(Clone)");

		//Camera turns 45deg around the character depending on whethere q/e are pressed
		if(character!=null)
		{
			transform.position = new Vector3(character.transform.position.x,Mathf.Sin(degree*(Mathf.PI/180))*distance+character.transform.position.y,
				-Mathf.Cos(degree*(Mathf.PI/180))*distance+character.transform.position.z);
			transform.LookAt(character.transform.position);
		}
		
		if(Input.GetButtonDown("Camera"))
		{
			float test = Input.GetAxis("Camera");
			GameObject hold = GameObject.Find("CrappyCharacter(Clone)");

			if(test>0)
			{
				transform.RotateAround(hold.transform.position,Vector3.up,-45);
			}

			else
			{
				transform.RotateAround(hold.transform.position,Vector3.up,45);
			}
		}		

	}
}
