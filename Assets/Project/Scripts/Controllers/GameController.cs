using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	private WaypointController waypointController;
	private GUIController guiController;
	private ArrowController arrowController;

	void Start()
	{
		instantiateControllers ();
	}

	void Update()
	{
		//Get the Waypoint Name
		Waypoint waypoint = waypointController.getSelectedWaypoint();
		
		guiController.newLocation (waypoint.waypointName);




	}

	void instantiateControllers()
	{
		waypointController = GameObject.FindGameObjectWithTag ("WaypointController").GetComponent<WaypointController>();
		waypointController.SendMessage ("createWaypoints", 45);
		waypointController.SendMessage ("selectWaypoint");

		guiController = GameObject.FindGameObjectWithTag ("GUIController").GetComponent<GUIController>();
	}



}
