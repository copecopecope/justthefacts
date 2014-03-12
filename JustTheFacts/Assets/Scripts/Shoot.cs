using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public GameObject gun;
	public Vector3 gunPosition;
	public float gunDestroyTime;

	private Animator anim;
	private DragControl dragControl;
	private GameObject gClone;


	// Use this for initialization
	void Start () {
		anim = gameObject.transform.GetChild (0).GetComponent<Animator> ();
		dragControl = Camera.main.GetComponent<DragControl> ();
	}
	
	// Update is called once per frame
	void Update () {
		//TODO: support touch
		if (!gClone && Input.GetKeyDown ("g") && dragControl.obj == gameObject) {
			gClone = (GameObject) Instantiate (gun);
			gClone.transform.parent = gameObject.transform;
			gClone.transform.localPosition = gunPosition;
			anim.SetTrigger("Fire");
			gClone.GetComponentInChildren<GunControl>().Fire();
			Destroy(gClone, gunDestroyTime);
		}
	
	}
}
