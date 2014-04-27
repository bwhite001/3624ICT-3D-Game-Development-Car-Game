using UnityEngine;
using System.Collections;

public class Waypont : MonoBehaviour {

	public string waypointName;

	public bool selected = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {

		if(other.gameObject.tag == "Player" && selected == true)
		{
			selected = false;

			GameObject waypointcontroler = GameObject.FindGameObjectWithTag("WaypointController");

			waypointcontroler.SendMessage("selectWaypoint");
		}


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

	void setSelected(bool select)
	{
		selected = select;
	}
}
