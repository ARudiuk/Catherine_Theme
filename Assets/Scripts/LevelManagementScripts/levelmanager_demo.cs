using UnityEngine;
using System.Collections;

public class levelmanager_demo : levelmanager_base {

	// Use this for initialization
	void Start () {
		currentlevel = new Level();
		currentlevel.name = "Alpha1";
		read (3);
		generateLevel();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
