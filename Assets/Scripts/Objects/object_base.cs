using UnityEngine;
using System.Collections;


public class Obj 
{
	public int x{get;set;}
	public int y{get;set;}
	public int z{get;set;}
	
	public states type;
	
	public string getCoordinatesasString()
	{
		return x.ToString()+','+y.ToString()+','+z.ToString()+','+type.ToString();
	}	
		
	public Vector3 getCoordinates()
	{
		return new Vector3(x,y,z);
	}
	
	public void setBlock(int x, int y, int z, states type)
	{
		this.x=x;
		this.y=y;
		this.z=z;
		this.type=type;
	}
	
}
