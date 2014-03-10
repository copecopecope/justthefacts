using UnityEngine;
using System.Collections;

public class PersonControl : MonoBehaviour {

	public enum PersonType
	{
		Red,
		Blue,
		Green
	}

	public PersonType type;

	[HideInInspector]
	public float initialX;

	// Use this for initialization
	void Start () {
		
	}

	public void SetRandomType() {
		System.Array values = System.Enum.GetValues(typeof(PersonType));
		PersonType newType = (PersonType)values.GetValue (Random.Range (0, values.Length));
		type = newType;
	}
	
	// Update is called once per frame
	void Update () {
		switch (type) 
		{
			case PersonType.Red:
				renderer.material.color = Color.red;
				break;
			case PersonType.Green:
				renderer.material.color = Color.green;
				break;
			case PersonType.Blue:
				renderer.material.color = Color.blue;
				break;
		}
	}
}
