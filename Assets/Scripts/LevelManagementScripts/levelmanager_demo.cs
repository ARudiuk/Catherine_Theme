using UnityEngine;
using System.Collections;

public class levelmanager_demo : levelmanager_base {

	// Use this for initialization
	void Start () {
		Debug.Log("starting");
		currentlevel = new Level();
		currentlevel.name = "alpha1";
		read (3);
		generateLevel();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
