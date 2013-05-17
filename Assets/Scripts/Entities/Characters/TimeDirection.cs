using UnityEngine;

public class TimeDirection //structure that holds direction of player, and time button is held for that direction
	{
		public float time;
		public Vector3 direction;

		public TimeDirection(float x, Vector3 y) //initializer
		{
			time = x;
			direction = y;
		}
	}