using UnityEngine;
using System.Collections;
using SimpleJSON;

public class GameController : MonoBehaviour {

	private WaypointController waypointController;
	private GUIController guiController;
	private ArrowController arrowController;
	private JSONController jsonController;

	private JSONNode CurrentProgram;

	void Start()
	{
		if (ProgramObjectController.program == null) 
		{
			Debug.LogError("Program Not Defined!");
			Debug.Break();
			Application.Quit();
		}
		else
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

		guiController.setProgram (ProgramObjectController.getProgramCode());
	}


}
