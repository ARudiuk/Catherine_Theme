using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class levelmanager_maplevel : MonoBehaviour {

	Vector3 right = new Vector3(1,0,0);
	Vector3 left = new Vector3(-1,0,0);
	Vector3 up = new Vector3(0,1,0);
	Vector3 down = new Vector3(0,-1,0);
	Vector3 forward = new Vector3(0,0,1);
	Vector3 back = new Vector3(0,0,-1);


	public struct Point
	{
		public Vector3 position;
		public bool occupied; //occupied by box

		public Point(Vector3 pos, bool occ)
		{
			position = pos;
			occupied = occ;
		}
	}
	

	void mapLevel()
	{		

		List<Point> reached = new List<Point>();
		List<Point> toreach = new List<Point>();

		toreach.Add(new Point(Vector3.zero,false));

		Vector3 position;
		bool occupied;

		while(toreach.Count!=0)
		{
			position = toreach[0].position;
			occupied = toreach[0].occupied;

		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
