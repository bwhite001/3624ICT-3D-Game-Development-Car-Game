using UnityEngine;
using System.Collections;

public class WaypointController : MonoBehaviour {

	private Waypoint[] waypoints;
	private int selectedWaypointIndex;

	void Start()
	{
		GameObject[] gos = GameObject.FindGameObjectsWithTag ("Waypoint");

		waypoints = new Waypoint[gos.Length];
		for(int i = 0; i<gos.Length; i++) {
			GameObject go = gos[i];
			waypoints[i] = go.GetComponent<Waypoint>();
		}
	}

	public Waypoint selectWaypoint() {
		waypoints [selectedWaypointIndex].selected = false;

		selectedWaypointIndex = (int)Random.Range (0, waypoints.Length - 1);

		waypoints [selectedWaypointIndex].selected = true;

		Debug.Log (waypoints [selectedWaypointIndex].getName ());
		return waypoints [selectedWaypointIndex];
	}
	
	public Waypoint getSelectedWaypoint()
	{
		return waypoints [selectedWaypointIndex];
	}
}
