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
	public List<Entity> Objects; //hold position of all blocks and type in last. Make custom class instead of Vector4 later, so we don't have to think about ints
	
	private Vector3 count; //keep track of dimensions
	private string name; //name of level
	
	public Entity[,,] map; //maps out entities in the world for quick access
	
	public Entity getEntity(Vector3 position, Vector3 move)//simplifies retrieval of entities 
	{
		return(map[(int)(position.x+move.x),(int)(position.y+move.y),(int)(position.z+move.z)]);
	}
	
	//UPDATE THIS FOR STUFF OTHER THAN BASICBLOCK
	public List<Entity> getsurroundingEntity(Vector3 position)
	{
		List<Entity> temp = new List<Entity>();
		Entity test = new Entity();
		test = getEntity (position,Vector3.forward);
		if(test!=null)
		{
			if(test.type == states.basicblock)
				temp.Add (test);
		}
		test = getEntity (position,Vector3.back);
		if(test!=null)
		{
			if(test.type == states.basicblock)
				temp.Add (test);
		}
		test = getEntity (position,Vector3.right);
		if(test!=null)
		{
			if(test.type == states.basicblock)
				temp.Add (test);
		}
		test = getEntity (position,Vector3.left);
		if(test!=null)
		{
			if(test.type == states.basicblock)
				temp.Add (test);
		}
		return temp;
		
	}
		
	public Level() //just default test values for name now
	{
		name = "temp"; 
		Objects = new List<Entity>();
	}
	
	public void addObject(Entity newobject)
	{
		if(!Objects.Contains(newobject))
		{
			Objects.Add (newobject);
			Debug.Log("New Blocks"+newobject.getCoordinatesasString());			
		}			
		else
			Debug.LogError("Duplicate Blocks"+newobject.getCoordinates());
	}
	
	public void moveObject(Vector3 position, Vector3 move)
	{
		Entity hold = map[(int)position.x,(int)position.y,(int)position.z];
		map[(int)position.x,(int)position.y,(int)position.z]=new Entity(); //fix this later to not constantly be making new objects
		map[(int)position.x+(int)move.x,(int)position.y+(int)move.y,(int)position.z+(int)move.z]=hold;	
		hold.obj.transform.position=position+move;
	}
	
	public Vector3 getSize()//get limits of x,y,z direction, and fixes position of blocks
	{		
		int minx=0,maxx=0,miny=0,maxy=0,minz=0,maxz=0;
		foreach(Entity item in Objects)
		{		
			if (item.x<minx)
				minx=item.x;
			else if (item.x>maxx)
				maxx=item.x;
			if (item.y<miny)
				miny=item.y;
			else if (item.y>maxy)
				maxy=item.y;
			if (item.z<minz)
				minz=item.z;
			else if (item.z>maxz)
				maxz=item.z;		
		}
		if(minx < 0 || miny < 0 || minz < 0) //if negative, shift blocks and rewrite
		{
			foreach(Entity item in Objects)
			{
				if(minx<0)
				{
					item.x+=(-minx);
					maxx+=(-minx);
				}
				if(miny<0)
				{
					item.y+=(-miny);
					maxy+=(-miny);
				}
				if(minz<0)
				{
					item.z+=(-minz);
					maxz+=(-minz);
				}
			}
			Debug.Log("Shifting blocks in save from negative axis");
			write ();
		}
		
		return new Vector3(maxx+1,maxy+1,maxz+1);		
	}
					
	public Entity checkforObjects(int x, int y, int z)
	{
		foreach (Entity item in Objects)
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
			Objects = JsonConvert.DeserializeObject<List<Entity>>(hold);						
		}
		count = getSize(); //get size for creating array
		map = new Entity[(int)(float)count.x+4,(int)(float)count.y+4,(int)(float)count.z+4];//create array for level
		//Fill level with empty values
		for (int i = 0;i<count.x+4;i++)
		{
			for (int j = 0;j<count.y+4;j++)
			{
				for (int k = 0;k<count.z+4;k++)
				{
					map[i,j,k] = new Entity();
				}
			}
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