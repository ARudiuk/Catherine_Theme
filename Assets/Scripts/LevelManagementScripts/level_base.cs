//Need to consider what to keep in here, and what to keep in levelmanager_base

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json; //external json library
using System.IO; //for reading writing files

[JsonObject(MemberSerialization.OptIn)] //says to only consider json writable/readable properties to be those with the [JsonProperty] tag
public class Level
{
	private List<Block> blocks; //hold position of all blocks and type in last. Make custom class instead of Vector4 later, so we don't have to think about ints
	private Vector3 count; //keep track of how many blocks there are in total
	private string name; //name of level
	
	[JsonProperty]
	public List<Block> Blocks {get {return blocks;}set {blocks=value;}}
	
	public Block[,,] map;
	
	public Level() //just default test values for name now
	{
		name = "temp";
		blocks = new List<Block>();
		count = getSize();		
	}
	
	public void addBlock(Block newblock)
	{
		if(!blocks.Contains(newblock))
		{
			blocks.Add (newblock);
			Debug.Log("New Blocks"+newblock.getCoordinatesasString());			
		}			
		else
			Debug.LogError("Duplicate Blocks"+newblock.getCoordinates());
	}
	
	public Vector3 getSize()//get limits of x,y,z direction
	{		
		int minx=0,maxx=0,miny=0,maxy=0,minz=0,maxz=0;
		foreach(item Block in blocks)
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
			foreach(item Block in blocks)
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
	
	public void read()
	{
		Debug.Log ("reading level");
		using(StreamReader file = new StreamReader(Application.dataPath+"/Levels/"+name+".json"))
		{
			string hold = file.ReadToEnd();
			blocks = JsonConvert.DeserializeObject<List<Block>>(hold);						
		}
	}
	
	public void write()
	{
		Debug.Log ("writing level");
		using (StreamWriter file = new StreamWriter(Application.dataPath+"/Levels/"+name+".json"))
		{
			string hold = JsonConvert.SerializeObject(blocks,Formatting.Indented); //serialize public properties and format with indentations	
			//Debug.Log(hold);
			file.Write(hold);
		}
	}
	
		
}