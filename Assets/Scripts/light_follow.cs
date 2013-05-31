using UnityEngine;
using System.Collections;

public class light_follow : MonoBehaviour
{

	GameObject character;
	public float distance;
	public float degree;
	void Start()
	{	
		distance = 7f;
		degree = 30f;
		character =null;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(character == null)
			if(GameObject.FindGameObjectWithTag("character"))
				character = GameObject.FindGameObjectWithTag("character");

		//Camera turns 45deg around the character depending on whethere q/e are pressed
		if(character!=null)
		{
			transform.position = new Vector3(character.transform.position.x,Mathf.Sin(degree*(Mathf.PI/180))*distance+character.transform.position.y,
				-Mathf.Cos(degree*(Mathf.PI/180))*distance+character.transform.position.z);
			transform.LookAt(character.transform.position);
		}
	}
}

