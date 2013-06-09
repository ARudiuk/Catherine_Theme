//Need to consider what to keep in here, and what to keep in levelmanager_base

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json; //external json library
using System.IO; //for reading writing files

[JsonObject(MemberSerialization.OptIn)] //says to only consider json writable/readable properties to be those with the [JsonProperty] tag
public class Level
{
	public int levelwidth;
	public int levelheight;
	public int leveldepth;
	[JsonProperty]
	public List<Entity> Objects; //hold position of all blocks and type in last. Make custom class instead of Vector4 later, so we don't have to think about ints
	
	private Vector3 count; //keep track of dimensions
	
	public string name; //name of level
	
	public Entity[,,] map; //maps out entities in the world for quick access
	
	public int lowestlevel;	
	
	
	
	public Level() //just default test values for now
	{
		name = "temp"; 
		Objects = new List<Entity>();
		lowestlevel = 0;	
		levelwidth=0;levelheight=0;leveldepth = 0;	
	}
	
	public Entity getEntity(Vector3 position, Vector3 move)//simplifies retrieval of entities 
	{		
		return(map[Mathf.RoundToInt(position.x+move.x),Mathf.RoundToInt(position.y+move.y),Mathf.RoundToInt(position.z+move.z)]);
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
	
	public List<Entity> getsupportingEntity(Vector3 position)
	{
		List<Entity> temp = new List<Entity>();
		if(getEntity(position,Vector3.down).type==states.basicblock)
			temp.Add(getEntity(position,Vector3.down));
		temp.AddRange(getsurroundingEntity(position+Vector3.down));	
		return temp;
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
	
	public void moveObject(Vector3 position, Vector3 move, Vector3 rotation)
	{
		Debug.Log("The initial position is " + map[Mathf.RoundToInt(position.x),Mathf.RoundToInt(position.y),Mathf.RoundToInt(position.z)].type);
		Debug.Log("The final position is " + map[Mathf.RoundToInt(position.x)+Mathf.RoundToInt(move.x),Mathf.RoundToInt(position.y)+Mathf.RoundToInt(move.y),Mathf.RoundToInt(position.z)+Mathf.RoundToInt(move.z)].type);		
		
		Entity hold = map[Mathf.RoundToInt(position.x),Mathf.RoundToInt(position.y),Mathf.RoundToInt(position.z)];
		map[Mathf.RoundToInt(position.x),Mathf.RoundToInt(position.y),Mathf.RoundToInt(position.z)]= new Entity();
		if(map[Mathf.RoundToInt(position.x)+Mathf.RoundToInt(move.x),Mathf.RoundToInt(position.y)+Mathf.RoundToInt(move.y),Mathf.RoundToInt(position.z)+Mathf.RoundToInt(move.z)].type==states.basicblock)
		{
			moveObject(position+move,move,rotation);
		}		 
		map[Mathf.RoundToInt(position.x)+Mathf.RoundToInt(move.x),Mathf.RoundToInt(position.y)+Mathf.RoundToInt(move.y),Mathf.RoundToInt(position.z)+Mathf.RoundToInt(move.z)]=hold;		
		
		animate (hold,0.15f,move,rotation);//make it unable to move during animation
	}
	
	public void chainmoveObject(Vector3 position, Vector3 move, Vector3 rotation)
	{
		Entity hold = map[Mathf.RoundToInt(position.x),Mathf.RoundToInt(position.y),Mathf.RoundToInt(position.z)];
		if(map[Mathf.RoundToInt(position.x)+Mathf.RoundToInt(move.x),Mathf.RoundToInt(position.y)+Mathf.RoundToInt(move.y),Mathf.RoundToInt(position.z)+Mathf.RoundToInt(move.z)].type==states.basicblock)
		{
			chainmoveObject(position+move,move,rotation);
		}	
		map[Mathf.RoundToInt(position.x)+Mathf.RoundToInt(move.x),Mathf.RoundToInt(position.y)+Mathf.RoundToInt(move.y),Mathf.RoundToInt(position.z)+Mathf.RoundToInt(move.z)]=hold;		
		
		animate (hold,0.15f,move,rotation);
	}
	
	public void blockfallmoveObject(Vector3 position)
	{
		if(getEntity(position,Vector3.up).type==states.basicblock)
			{
				if(getsupportingEntity(position+Vector3.up).Count<=1)
				{
					blockfallmoveObject(position+Vector3.up);
				}
			}
		moveObject(position,Vector3.down, Vector3.zero);
	}
	
	public void movetwoObjects(Vector3 position1, Vector3 position2, Vector3 move1, Vector3 move2, Vector3 rotation1, Vector3 rotation2)
	{
		Entity temp1 = map[Mathf.RoundToInt(position1.x),Mathf.RoundToInt(position1.y),Mathf.RoundToInt(position1.z)];
		Entity temp2 = map[Mathf.RoundToInt(position2.x),Mathf.RoundToInt(position2.y),Mathf.RoundToInt(position2.z)];
		
		if(getEntity(position2,move2).type==states.basicblock)
		{
			chainmoveObject(position2+move2,move2,rotation2);
		}
		
		map[Mathf.RoundToInt(position1.x),Mathf.RoundToInt(position1.y),Mathf.RoundToInt(position1.z)] = new Entity(); //fix this later to not constantly be making new objects
		map[Mathf.RoundToInt(position2.x),Mathf.RoundToInt(position2.y),Mathf.RoundToInt(position2.z)] = new Entity(); //fix this later to not constantly be making new objects
		
		map[Mathf.RoundToInt(position1.x)+Mathf.RoundToInt(move1.x),Mathf.RoundToInt(position1.y)+Mathf.RoundToInt(move1.y),Mathf.RoundToInt(position1.z)+Mathf.RoundToInt(move1.z)]=temp1;
		map[Mathf.RoundToInt(position2.x)+Mathf.RoundToInt(move2.x),Mathf.RoundToInt(position2.y)+Mathf.RoundToInt(move2.y),Mathf.RoundToInt(position2.z)+Mathf.RoundToInt(move2.z)]=temp2;		
		
		animate (temp1,0.15f,move1,rotation1);
		animate (temp2,0.15f,move2,rotation2);		
	}
	
	public void fixLimits()//get limits of x,y,z direction, and fixes position of blocks		
		//right now this always resets the coordinates, even in right place. I find this make it more seperated, and easier to modify later
	{		
		int minx=Objects[0].x,maxx=Objects[0].x,miny=Objects[0].y,maxy=Objects[0].y,minz=Objects[0].z,maxz=Objects[0].z;
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
		if(minx != 0 || miny != 0 || minz != 0) //if negative, shift blocks and rewrite
		{
			foreach(Entity item in Objects)
			{
				if(minx!=0)
				{
					item.x-=(minx);					
				}				
				if(miny!=0)
				{
					item.y-=(miny);					
				}				
				if(minz!=0)
				{
					item.z-=(minz);					
				}				
			}						
			Debug.Log("Fixing Positions");			
		}		
		levelwidth=maxx-minx+1;
		levelheight=maxy-miny+1;
		leveldepth=maxz-minz+1;
		//return new Vector3(maxx+1,maxy+1,maxz+1);		
	}
	
	public void constructMatrix(int padding)//dist is the distance from the limits
	{
		map = new Entity[levelwidth+padding*2,levelheight+padding*2,leveldepth+padding*2];
		//Fill level with empty values
		for (int i = 0;i<levelwidth+padding*2;i++)
		{
			for (int j = 0;j<levelheight+padding*2;j++)
			{
				for (int k = 0;k<leveldepth+padding*2;k++)
				{
					map[i,j,k] = new Entity();
				}
			}
		}
	}
	
	public int getlowestBlock()
	{
		int result=Objects[1].y;
		foreach(Entity item in Objects)
		{
			if (item.y<result)
				result = item.y;
		}
		return result;
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
					
	public void read(int padding)
	{
		Debug.Log ("reading level");
		using(StreamReader file = new StreamReader(Application.dataPath+"/Levels/"+name+".json"))
		{
			string hold = file.ReadToEnd();
			Objects = JsonConvert.DeserializeObject<List<Entity>>(hold);						
		}
		fixLimits();
		constructMatrix(padding);
		
	}
	
	public void write()
	{
		Debug.Log ("writing level");
		fixLimits();
		using (StreamWriter file = new StreamWriter(Application.dataPath+"/Levels/"+name+".json"))
		{
			string hold = JsonConvert.SerializeObject(Objects,Formatting.Indented); //serialize public properties and format with indentations	
			//Debug.Log(hold);
			file.Write(hold);
		}
	}
	
	public void animate(Entity entity, float duration,Vector3 move, Vector3 rotation)
	{
		Vector3 initial = entity.obj.transform.position;
		Vector3 final = initial+move;
		
		if (entity.type == states.character)
		{
			entity.obj.GetComponent<Character_base>().StartCoroutine(animation(entity,duration,initial,final,Vector3.zero,Vector3.zero));
		}
		if (entity.type == states.basicblock)
		{
			entity.obj.GetComponent<Block_base>().StartCoroutine(animation(entity,duration,initial,final,Vector3.zero,Vector3.zero));
		}
	}
	
	public IEnumerator animation(Entity entity, float duration, Vector3 start, Vector3 end, Vector3 initialrotation, Vector3 finalrotation)
	{
		entity.moving=true;
		for(float t = 0; t < duration; t += Time.deltaTime)
			{
				entity.obj.transform.position = Vector3.Lerp(start, end, t/duration);	
				yield return null;
			}
		entity.obj.transform.position = end; //bad  hack to fix animation not being perfect
		entity.moving = false;
	}
		
}