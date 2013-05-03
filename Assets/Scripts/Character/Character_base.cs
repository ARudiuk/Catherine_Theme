//move speeds here later

using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
	
	public Character_Movement movement;
	
	public Level level;
	public Vector3 position;
	
	public bool moving;
	
	Character(Level level)	
	{
		this.level = level;
	}
	
	// Use this for initialization
	void Start (Level level)
	{
		movement = new Character_Movement();		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!moving)
		{
			Vector3 move = movement.move(level.map, position);		
		}
	}
}

