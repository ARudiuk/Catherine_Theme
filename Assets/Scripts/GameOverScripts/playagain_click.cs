using UnityEngine;
using System.Collections;

public class playagain_click : MonoBehaviour {

	public string level;

	void OnMouseDown()
	{
		Application.LoadLevel(level);
	}
}
