using UnityEngine;
using System.Collections;

public class Camera_Follow : MonoBehaviour {	

	void Start()
	{		
	}
	// Update is called once per frame
	void Update () {

		if (transform.eulerAngles.x>50f)
			transform.Translate(new Vector3(0,6,0));		

		transform.LookAt(GameObject.Find("CrappyCharacter(Clone)").transform);
		//Camera turns 45deg around the character depending on whethere q/e are pressed
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
