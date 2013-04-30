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
	private int count; //keep track of how many blocks there are in total
	private string name; //name of level
	
	[JsonProperty]
	public List<Block> Blocks {get {return blocks;}set {blocks=value;}}
	
	public Level() //just default test values for now
	{
		blocks = new List<Block>();
		count = 0;
		name = "temp";
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