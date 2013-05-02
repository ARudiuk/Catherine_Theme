//Need to consider what to keep in here, and what to keep in levelmanager_base

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json; //external json library
using System.IO; //for reading writing files

[JsonObject(MemberSerialization.OptIn)] //says to only consider json writable/readable properties to be those with the [JsonProperty] tag
public class Level
{
	[JsonProperty]
	public List<Obj> Objects; //hold position of all blocks and type in last. Make custom class instead of Vector4 later, so we don't have to think about ints
	private Vector3 count; //keep track of how many blocks there are in total
	private string name; //name of level
	
	public states[,,] map;
	
	public Level() //just default test values for name now
	{
		name = "temp"; 
		Objects = new List<Obj>();
		count = getSize();
		map = new states[(int)(float)count.x,(int)(float)count.y,(int)(float)count.z];//create array for level	
		
		//Fill level
		for (int i = 0;i<count.x;i++)
		{
			for (int j = 0;j<count.y;j++)
			{
				for (int k = 0;k<count.z;k++)
				{
					if(checkforObjects(i,j,k)!=null)
					{
						map[i,j,k] = checkforObjects(i,j,k).type;
					}
				}
			}
		}
	}
	
	public void addObject(Obj newobject)
	{
		if(!Objects.Contains(newobject))
		{
			Objects.Add (newobject);
			Debug.Log("New Blocks"+newobject.getCoordinatesasString());			
		}			
		else
			Debug.LogError("Duplicate Blocks"+newobject.getCoordinates());
	}
	
	public Vector3 getSize()//get limits of x,y,z direction
	{		
		int minx=0,maxx=0,miny=0,maxy=0,minz=0,maxz=0;
		foreach(Obj item in Objects)
		{
			if (item.x<minx)
				minx=item.x;
			else if (item.x>maxx)
				maxx=item.x;
			if (item.y<miny)
				miny=item.y;
			else if (item.y<miny)
				maxy=item.y;
			if (item.z<minz)
				minz=item.z;
			else if (item.z<minz)
				maxz=item.z;
		}
		
		if(minx < 0 || miny < 0 || minz < 0) //if negative, shift blocks and rewrite
		{
			foreach(Obj item in Objects)
			{
				item.x+=minx;
				item.y+=miny;
				item.z+=minz;
			}
			Debug.Log("Shifting blocks in save from negative axis");
			write ();
		}
		
		return new Vector3(maxx,maxy,maxz);
		
	}
					
	public Obj checkforObjects(int x, int y, int z)
	{
		foreach (Obj item in Objects)
		{
			if (item.x==x && item.y==y && item.z==z)
			{
				return item;
			}
		}
		return null;
	}
					
	public void read()
	{
		Debug.Log ("reading level");
		using(StreamReader file = new StreamReader(Application.dataPath+"/Levels/"+name+".json"))
		{
			string hold = file.ReadToEnd();
			Objects = JsonConvert.DeserializeObject<List<Obj>>(hold);						
		}
	}
	
	public void write()
	{
		Debug.Log ("writing level");
		using (StreamWriter file = new StreamWriter(Application.dataPath+"/Levels/"+name+".json"))
		{
			string hold = JsonConvert.SerializeObject(Objects,Formatting.Indented); //serialize public properties and format with indentations	
			//Debug.Log(hold);
			file.Write(hold);
		}
	}
	
		
}