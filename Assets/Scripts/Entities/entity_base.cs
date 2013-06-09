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
	[JsonProperty]
	public int subtype;
	
	public GameObject obj;
	
	public bool moving;
	
	public Entity()
	{
		moving = false;
		obj = null;
		this.type=states.empty;
		this.subtype=0;
	}
	
	public Entity(GameObject reference, states type, int subtype)
	{
		obj = reference;
		x = Mathf.RoundToInt(obj.transform.position.x);
		y = Mathf.RoundToInt(obj.transform.position.y);
		z = Mathf.RoundToInt(obj.transform.position.z);
		this.type = type;
		this.subtype = subtype;
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
	
	public void setData(int x, int y, int z, states type, int subtype)
	{
		this.x=x;
		this.y=y;
		this.z=z;
		this.type=type;
		this.subtype=subtype;
	}
	
}
