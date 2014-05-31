using UnityEngine;
using System.Collections;

public class StartingPoint : MonoBehaviour {
	public bool drawGiz = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnDrawGizmos() {
		if (drawGiz) {
				Gizmos.color = Color.magenta;
				Gizmos.DrawWireSphere (transform.position, 10);
		}
	}
}
