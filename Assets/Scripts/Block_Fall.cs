using UnityEngine;
using System.Collections;

public class Block_Fall : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	bool isFalling = false;
	RaycastHit crush;
	
	// Update is called once per frame
	void Update () {
		
	CheckForBelowBlocks other = gameObject.GetComponent<CheckForBelowBlocks>();
	Vector3 start = transform.position;
	Vector3 end = transform.position - new Vector3(0,1,0);
		//before falling begins play shake animation 

	if(!other.supported && !isFalling)
		{
			StartCoroutine(drop(start,end,.25f));
		}
		//This isn't working right now??????

		if(Physics.Raycast(transform.position,new Vector3(0,-1,0),out crush,.1f))
		{
			Debug.Log("crush");
			if(crush.collider.gameObject.name == "crappy_character")
				Destroy(GameObject.Find("crappy_character"));
		}
		//add destroy player when block hits them--just need to be fully working

		//add system to destroy blocks after not being supported for like 4 seconds
	}
	
	IEnumerator drop(Vector3 start, Vector3 end, float duration)
	{
		isFalling = true;
		for(float i = 0.0f; i < duration; i+= Time.deltaTime)
		{
			transform.position=Vector3.Lerp(start,end,i/duration);	
			yield return null;
		}
		isFalling = false;
	}
}
