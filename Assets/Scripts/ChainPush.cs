using UnityEngine;
using System.Collections;

public class ChainPush : MonoBehaviour {

	public GameObject triggerBox;
	
	void OnTriggerEnter(Collider otherBox)
	{
		Debug.Log("collide");
		Block_Move other = gameObject.GetComponent<Block_Move>();
		otherBox.transform.Translate(other.pushDirection);
	}
}
