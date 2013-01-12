using UnityEngine;
using System.Collections;

public class Block_Move : MonoBehaviour {

	/*
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	*/
	public void move(Vector3 dir) //recursive method for moving forward blocks
	//blocks in front will be moved first, so that transform.position refers to the correct place
	{
		RaycastHit cube;	

		if(Physics.Raycast(transform.position,dir,out cube, 1))
		{
			cube.transform.gameObject.GetComponent<Block_Move>().move(dir); //calls move function recursively 
		}

		transform.Translate(dir);			
	}
}
