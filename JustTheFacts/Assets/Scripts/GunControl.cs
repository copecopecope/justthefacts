using UnityEngine;
using System.Collections;

public class GunControl : MonoBehaviour {

	public float radius;
	
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
	
	}
	
	public void Fire() {
		Collider2D coll = Physics2D.OverlapCircle (new Vector2 (transform.position.x, transform.position.y), radius);
		if (coll && coll.tag == "Person Container") {
			coll.GetComponent<PersonControl>().Kill ();
		}
	}
}
