using UnityEngine;
using System.Collections;

public class CheckForBelowBlocks : MonoBehaviour {

	//errors may be caused by a character intersecting. Will have to change to only intersecting with "cube".
	//or possibly use a layer mask attached to the ray cast http://docs.unity3d.com/Documentation/Components/Layers.html
	//this can make it ignore collisions with the character
	// or something like if(hit.collider.gameObject.name == "cube"))
	
	private Vector3 Down = new Vector3(0,-1,0); 
	private Vector3 In = new Vector3(0,-1,-1);
	private Vector3 Out = new Vector3(0,-1,1);
	private Vector3 Left = new Vector3(-1,-1,0);
	private Vector3 Right = new Vector3(1,-1,0);
	RaycastHit dHit;
	RaycastHit iHit;
	RaycastHit oHit;
	RaycastHit lHit;
	RaycastHit rHit;
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
	void Update () {
		Debug.DrawRay(transform.position,transform.TransformDirection(Down),Color.white); //shows debug of ray collision, check scene view
		Debug.DrawRay(transform.position,transform.TransformDirection(In),Color.white);
		Debug.DrawRay(transform.position,transform.TransformDirection(Out),Color.white);
		Debug.DrawRay(transform.position,transform.TransformDirection(Left),Color.white);	
		Debug.DrawRay(transform.position,transform.TransformDirection(Right),Color.white);	
		
		if(Physics.Raycast(transform.position,transform.TransformDirection(In), out iHit,1))
		{
			if(iHit.collider.gameObject.name == "BasicBlock")
			{
				edge = true;
				supported = true;	
			}
		}
		
		else if(Physics.Raycast(transform.position,transform.TransformDirection(Out), out oHit,1))
		{
			if(oHit.collider.gameObject.name == "BasicBlock")
			{
				edge = true;
				supported = true;	
			}	
		}
		
		else if(Physics.Raycast(transform.position,transform.TransformDirection(Left), out lHit,1))
		{
			if(lHit.collider.gameObject.name == "BasicBlock")
			{
				edge = true;
				supported = true;	
			}	
		}
		
		else if(Physics.Raycast(transform.position,transform.TransformDirection(Right), out rHit,1))
		{
			if(rHit.collider.gameObject.name == "BasicBlock")
			{
				edge = true;
				supported = true;	
			}	
		}
		
		else 
			supported = false;
		
		if(Physics.Raycast(transform.position,transform.TransformDirection(Down), out dHit,1))
		{
			if(dHit.collider.gameObject.name == "BasicBlock")
			{
				edge = false;
				supported = true;	
			}
		}

		else if(!edge)
			supported = false;
		
		//activate emmitter/sound if it becomes edge after not being supported OR if it loses its support from immediately below, but is still supported
		
		//if(lowestLevel )
	}
}
