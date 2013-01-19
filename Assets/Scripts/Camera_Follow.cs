using UnityEngine;
using System.Collections;

public class Camera_Follow : MonoBehaviour {	

	void Start()
	{		
	}
	// Update is called once per frame
	void Update () {

		transform.LookAt(GameObject.Find("crappy_character").transform);
		//Camera turns 45deg around the character depending on whethere q/e are pressed
		if(Input.GetButtonDown("Camera"))
		{
			float test = Input.GetAxis("Camera");
			GameObject hold = GameObject.Find("crappy_character");

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