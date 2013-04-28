using UnityEngine;
using System.Collections;


public class Block 
{
	public int x{get;set;}
	public int y{get;set;}
	public int z{get;set;}
	
	public int type{get;set;}
	
	public string getCoordinatesasString()
	{
		return x.ToString()+','+y.ToString()+','+z.ToString()+','+type.ToString();
	}	
		
	public Vector3 getCoordinates()
	{
		return new Vector3(x,y,z);
	}
	
	public void setBlock(int x, int y, int z, int type)
	{
		this.x=x;
		this.y=y;
		this.z=z;
		this.type=type;
	}
	
}
