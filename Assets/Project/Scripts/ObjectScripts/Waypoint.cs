using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour {

	public string waypointName;

	public bool selected = false;

	private GameObject waypointController;
	

	// Use this for initialization
	void Start () {
		waypointController = GameObject.FindGameObjectWithTag("WaypointController");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(transform.position, 1);
		if(selected)
			Gizmos.color = Color.green;
		else
			Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, 10);
	}

	public void setSelected(bool select)
	{
		selected = select;
	}

	void setWaypointName(string name)
	{
		waypointName = name;
	}

	public bool getSelected()
	{
		return selected;
	}


}
