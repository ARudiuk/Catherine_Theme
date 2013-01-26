using UnityEngine;
using System.Collections;

public class CheckForBelowBlocks : MonoBehaviour {

	//errors may be caused by a character intersecting. Will have to change to only intersecting with "cube".
	//or possibly use a layer mask attached to the ray cast http://docs.unity3d.com/Documentation/Components/Layers.html
	//this can make it ignore collisions with the character
	// or something like if(hit.collider.gameObject.name == "cube"))
	
	
	private Vector3 Down = new Vector3(0,-1,0); 
	public bool debugBlock = false;

	RaycastHit hit;
	
	//edge may not be 100% necessary, but it does allow you to not have to recheck a raycast collision
	public bool edge = true; 
	public bool supported = true;
	bool wasItEdged;
	
	//public GameObject[] blocks;
	//will be used when dropping a level at a time.
	public int lowestLevel;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void BlockUpdate () {
		
		Debug.DrawRay(transform.position + new Vector3(0,-1,0),transform.TransformDirection(new Vector3(1,0,0)),Color.white); //shows debug of ray collision, check scene view
		Debug.DrawRay(transform.position + new Vector3(0,-1,0),transform.TransformDirection(new Vector3(-1,0,0)),Color.white);
		Debug.DrawRay(transform.position + new Vector3(0,-1,0),transform.TransformDirection(new Vector3(0,0,-1)),Color.white);
		Debug.DrawRay(transform.position + new Vector3(0,-1,0),transform.TransformDirection(new Vector3(0,0,1)),Color.white);	
		
		if(Physics.Raycast(transform.position,transform.TransformDirection(new Vector3(0,-1,0)),out hit, 1))
		{
			if(hit.collider.gameObject.name == "BasicBlock")
			{
				edge = false;
				supported = true;	
			}
		}

		else if(Physics.Raycast(transform.position + new Vector3(0,-1,0),transform.TransformDirection(new Vector3(1,0,0)),out hit, 1))
		{
			if(hit.collider.gameObject.name == "BasicBlock")
			{
				edge = true;
				supported = true;	
			}
		}
		
		else if(Physics.Raycast(transform.position + new Vector3(0,-1,0),transform.TransformDirection(new Vector3(-1,0,0)),out hit, 1))
		{
			if(hit.collider.gameObject.name == "BasicBlock")
			{
				edge = true;
				supported = true;	
			}	
		}
		
		else if(Physics.Raycast(transform.position + new Vector3(0,-1,0),transform.TransformDirection(new Vector3(0,0,1)),out hit, 1))
		{
			if(hit.collider.gameObject.name == "BasicBlock")
			{
				edge = true;
				supported = true;	
			}	
		}
		
		else if(Physics.Raycast(transform.position + new Vector3(0,-1,0),transform.TransformDirection(new Vector3(0,0,-1)),out hit, 1))
		{
			if(hit.collider.gameObject.name == "BasicBlock")
			{
				edge = true;
				supported = true;	
			}	
		}		

		else 
		{
			if(debugBlock)
				Debug.Log("BLOCK FALLING");
			supported = false;
		}
		
		//activate emmitter/sound if it becomes edge after not being supported OR if it loses its support from immediately below, but is still supported
		
		//if(lowestLevel )
	}
}