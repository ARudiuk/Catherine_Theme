//Need to consider what to keep in here, and what to keep in levelmanager_base

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO; //for reading writing files


public class Level
{
	
	public int levelwidth; //width level, not including padding - x
	
	public int levelheight; //y
	
	public int leveldepth;//z
	
	public List<Entity> Objects; //hold position of all blocks and type in last. Make custom class instead of Vector4 later, so we don't have to think about ints
	
	private Vector3 count; //keep track of dimensions
	
	public string name; //name of level
	
	public Entity[,,] map; //maps out entities in the world for quick access
	
	public int lowestlevel;	//tracks the lowest level block
	
	
	
	public Level() //just default test values for now
	{
		name = "temp"; 
		Objects = new List<Entity>();
		lowestlevel = 0;	
		levelwidth=0;levelheight=0;leveldepth = 0;	
	}
	
	public Entity getEntity(Vector3 position, Vector3 move)//simplifies retrieval of entities, takes two positions and adds them
	{		
		return(map[Mathf.RoundToInt(position.x+move.x),Mathf.RoundToInt(position.y+move.y),Mathf.RoundToInt(position.z+move.z)]);
	}
	
	public Entity getEntity(Vector3 position)//simplifies retrieval of entities, takes one position
	{		
		return(map[Mathf.RoundToInt(position.x),Mathf.RoundToInt(position.y),Mathf.RoundToInt(position.z)]);
	}
	
	public void setMap(Vector3 position,Entity entity) //simplifies setting map value
	{
		map[Mathf.RoundToInt(position.x),Mathf.RoundToInt(position.y),Mathf.RoundToInt(position.z)]=entity;
	}
	
	public void setMap(Vector3 position,Vector3 move,Entity entity) //simplifies setting map values, two positons
	{
		map[Mathf.RoundToInt(position.x+move.x),Mathf.RoundToInt(position.y+move.y),Mathf.RoundToInt(position.z+move.z)]=entity;
	}
	//UPDATE THIS FOR STUFF OTHER THAN block
	//checks surround positions.
	public List<Entity> getsurroundingEntity(Vector3 position)
	{
		List<Entity> temp = new List<Entity>();
		Entity test = new Entity();
		test = getEntity (position,Vector3.forward);
		if(test!=null)
		{
			if(test.type == states.block)
				temp.Add (test);
		}
		test = getEntity (position,Vector3.back);
		if(test!=null)
		{
			if(test.type == states.block)
				temp.Add (test);
		}
		test = getEntity (position,Vector3.right);
		if(test!=null)
		{
			if(test.type == states.block)
				temp.Add (test);
		}
		test = getEntity (position,Vector3.left);
		if(test!=null)
		{
			if(test.type == states.block)
				temp.Add (test);
		}
		return temp;
		
	}
	//checks if there are any supporting blocks
	//first it checks below, then it checks surrounding blocks of below block
	//returns the list
	public List<Entity> getsupportingEntity(Vector3 position)
	{
		List<Entity> temp = new List<Entity>();
		if(getEntity(position,Vector3.down).type==states.block)
			temp.Add(getEntity(position,Vector3.down));
		temp.AddRange(getsurroundingEntity(position+Vector3.down));	
		return temp;
	}	
	//add object to object list, used with map level for now
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
	//moves an object, take objects positions, move, and its rotation change
	//sets object position to empty, and makes new position equal to object
	//animation makes sure that the visible representation of the object moves forward with the data
	public void moveObject(Vector3 position, Vector3 move, Vector3 rotation)
	{
		Debug.Log("The initial position is " + getEntity(position).type);
		Debug.Log("The final position is " + getEntity(position,move).type);		
		
		Entity hold = getEntity(position);
		setMap(position,new Entity());				 
		setMap(position,move,hold);		
		
		animate (hold,0.15f,move,rotation);//make it unable to move during animation
	}
	
	//moves a chain of blocks, doesn't properly account for initial position being empty, so that must be done before calling this
	public void chainmoveObject(Vector3 position, Vector3 move, Vector3 rotation)
	{
		Entity hold = getEntity(position);
		if(getEntity(position,move).type==states.block)
		{
			chainmoveObject(position+move,move,rotation);
		}
		setMap(position,move,hold);			
		
		animate (hold,0.15f,move,rotation);
	}
	
	//handles blocks falling
	//it goes up to the top block that will fall, then calls chainmoveobjects going down
	//makes sure to set initial position as empty at the top
	public void blockfallmoveObject(Vector3 position)
	{
				
		if(getEntity(position,Vector3.up).type==states.block)
		{
			if(getsupportingEntity(position+Vector3.up).Count==1)
			{
				blockfallmoveObject(position+Vector3.up);
			}
			else{
				chainmoveObject(position, Vector3.down, Vector3.zero);
				setMap(position,new Entity());
			}			
		}
		else
		{
			chainmoveObject(position, Vector3.down, Vector3.zero);
			setMap(position,new Entity());
		}
	}
	
	//used for moving two objects, like when grabbing
	//first it remembers the objects, and checks if the second entity entity moving forward will hit a block, if it does it starts a chain move
	//then in does the typical new object creation, then moveing objects, then animation
	public void movetwoObjects(Vector3 position1, Vector3 position2, Vector3 move1, Vector3 move2, Vector3 rotation1, Vector3 rotation2)
	{
		Entity temp1 = getEntity(position1);
		Entity temp2 = getEntity(position2);
		
		if(getEntity(position2,move2).type==states.block)
		{
			chainmoveObject(position2+move2,move2,rotation2);
		}
		
		setMap(position1,new Entity());//fix this later to not constantly be making new objects
		setMap(position2,new Entity());//fix this later to not constantly be making new objects
		
		setMap(position1,move1,temp1);
		setMap(position2,move2,temp2);
		
		animate (temp1,0.15f,move1,rotation1);
		animate (temp2,0.15f,move2,rotation2);		
	}
	
	//get limits of x,y,z direction, and fixes position of blocks
	//right now this always resets the coordinates, even in right place. I find this make it more seperated, and easier to modify later
	//also sets the maximium dimension variables width,etc
	public void fixLimits()		
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
	
	//makes an empty  map of level dimension plus padding
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
	//goes through all objects and returns lowest block
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
	
	//temporary animation manager
	//currently sets up proper limits, and calls coroutine in right base class
	//no rotation yet
	public void animate(Entity entity, float duration,Vector3 move, Vector3 rotation)
	{
		Vector3 initial = entity.obj.transform.position;
		Vector3 final = initial+move;		
		
		if (entity.type == states.character)
		{
			entity.obj.GetComponent<Character_base>().StartCoroutine(animation(entity,duration,initial,final,entity.obj.transform.eulerAngles,rotation));
		}
		else if (entity.type == states.block)
		{
			entity.obj.GetComponent<Block_base>().StartCoroutine(animation(entity,duration,initial,final,entity.obj.transform.eulerAngles,rotation));
		}
	}
	
	//the couritine that animates a movement. Simply lerps between start and end position.
	//sets entity to be moving, so it can't be moved while animating
	public IEnumerator animation(Entity entity, float duration, Vector3 start, Vector3 end, Vector3 initialrotation, Vector3 rotation)
	{		
		entity.moving=true;
		for(float t = 0; t < duration; t += Time.deltaTime)
			{
				entity.obj.transform.position = Vector3.Lerp(start, end, t/duration);	
				yield return null;
			}
		entity.obj.transform.position = end; //bad  hack to fix animation not being perfect
		//entity.obj.transform.Rotate(rotation); //double the hack, double 
		entity.moving = false;
	}
		
}