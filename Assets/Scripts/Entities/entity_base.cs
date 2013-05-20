using UnityEngine;
using System.Collections;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class Entity : System.IEquatable<Entity>
{
	[JsonProperty]
	public int x;
	[JsonProperty]
	public int y;
	[JsonProperty]
	public int z;
	[JsonProperty]
	public states type;
	
	public GameObject obj;
	
	public Entity()
	{
		obj = null;
		this.type=states.empty;
	}
	
	public Entity(GameObject reference, states type)
	{
		obj = reference;
		x = Mathf.RoundToInt(obj.transform.position.x);
		y = Mathf.RoundToInt(obj.transform.position.y);
		z = Mathf.RoundToInt(obj.transform.position.z);
		this.type = type;
	}
	
	public bool Equals(Entity other)
	{
		return this.x==other.x&&this.y==other.y
			&&this.z==other.z&&this.type==other.type;
	}
			
	public string getCoordinatesasString()
	{
		return x.ToString()+','+y.ToString()+','+z.ToString()+','+type.ToString();
	}	
		
	public Vector3 getCoordinates()
	{
		return new Vector3(x,y,z);
	}
	
	public void setData(int x, int y, int z, states type)
	{
		this.x=x;
		this.y=y;
		this.z=z;
		this.type=type;
	}
	
}
