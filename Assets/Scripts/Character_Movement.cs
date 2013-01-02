using UnityEngine;
using System.Collections;

public class Character_Movement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if(Input.GetButtonDown("Horizontal")){
		float test = Input.GetAxis("Horizontal");
		if (test>0){
			transform.Translate(transform.TransformDirection(new Vector3(-1,0,0)));}
		else
			transform.Translate(transform.TransformDirection(new Vector3(1,0,0)));
	}

	if(Input.GetButtonDown("Vertical")){
		float test = Input.GetAxis("Vertical");
		if (test>0){
			transform.Translate(transform.TransformDirection(new Vector3(0,0,-1)));}
		else
			transform.Translate(transform.TransformDirection(new Vector3(0,0,1)));
	}
	}
}
