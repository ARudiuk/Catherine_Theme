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
	

	public void mapLevel()//breadth first mapping of level, that goes one extra block out. Naive implementation
	{
		
		Vector3[] directions = new Vector3[6]; //declare array to go through, maybe find more elegant way
		directions[0] = right;
		directions[1] = left;
		directions[2] = up;
		directions[3] = down;
		directions[4] = forward;
		directions[5] = back;

		List<Point> reached = new List<Point>();
		List<Point> toreach = new List<Point>();

		toreach.Add(new Point(Vector3.zero,false));

		Vector3 position;
		bool occupied;
		Level level = new Level();
		Block template;

		while(toreach.Count!=0)
		{
			position = toreach[0].position;
			occupied = toreach[0].occupied;
			template = new Block();
			
			for(int i = 0;i<6;i++) //check all 6 sides for blocks and unchecked spaces
			{				
				if(Physics.Raycast(position,directions[i],1f))//if object in direction and it hasn't been reached or will be reached, add it to scan que
				{
					Point temp = new Point(position+directions[i],true);					
					if(!reached.Contains(temp)&&!toreach.Contains(temp))
						toreach.Add(temp);					
				}
				
				else if(!Physics.Raycast(position,directions[i],1f)&&occupied == true)//if no object in front and coming from occupied block, and not in either list, then add
				{
					Point temp = new Point(position+directions[i],false);
					if(!reached.Contains(temp)&&!toreach.Contains(temp))
						toreach.Add(temp);
				}
					
			}
			//if block then add
			if(occupied=true)
			{
				template.setBlock((int)position.x,(int)position.y,(int)position.z,1);				
				level.addBlock(template);
			}
			
			//add to reached points, remove from points to scan
			reached.Add(toreach[0]);
			toreach.Remove(toreach[0]);			
		}		
		int count = level.Blocks.Count;		
		level.write();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
