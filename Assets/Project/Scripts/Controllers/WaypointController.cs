using UnityEngine;
using System.Collections;

public class WaypointController : MonoBehaviour {
	public Waypoint waypointPrefab;

	private Waypoint[] waypoints;

	private int selectedWaypointIndex;

	private int ammount;

	public float largest = 500f;

	private string[] WaypontNames = {"G01 Business 1","G02 Clinical Sciences 1","G03 Lecture Theatres 1 & 2",
		"G04 Services","G05 Health Sciences","G06 Business 3","G07 The Link","G08 Flammable Liquid Store",
		"G09 Engineering","G10 Library (Graham Jones Centre)","G11 Learning Commons","G12 Science 2",
		"G13 Multi Storey Carpark 1","G14 Visual Arts","G16 Clinical Sciences 2",
		"G17 Lecture Theatres 3 & 4","G19 Facilities Management",
		"G20 Chiller House 1","G21 Chiller House 2","G22 Chiller House 3",
		"G23 Multimedia","G24 Science 1","G25 Glycomics 2","G26 Glycomics 1",
		"G27 Business 2","G28 Kiosk","G29 Chiller House 4","G30 Arts & Education 1",
		"G31 Arts & Education 2","G32 The Pavilion","G33 Student Centre","G34 The Chancellery",
		"G35 Griffith University Bridge","G36 Law","G37 Chiller House 5","G38 Chiller House 6",
		"G39 Science, Engineering and Architecture","G40 Griffith Health Centre",
		"G42 Griffith Business School","G51 Smart Water Research Centre","G52 International Building",
		"G53 Chiller House 7","G54 End of Trip Facility","G55 Multi Storey Carpark 2 (proposed)",
		"GT2 Coastal Management"};

	public void createWaypoints (int set)
	{
		ammount = set;

		waypoints = new Waypoint[ammount];

		for (int i = 0; i<ammount; i++) {
			Vector3 pos = getRandomPos();

			Waypoint temp;


			temp = Instantiate(waypointPrefab, pos, Quaternion.identity) as Waypoint;


			//set all waypoints to not selected
			temp.selected = false;

			int index = (int)Random.Range (0,WaypontNames.Length-1);
			string tmpName = WaypontNames[index];

			temp.waypointName = tmpName;
			temp.transform.parent = transform;

			//add to waypoints array
			waypoints[i] = temp;
		}
	}

	Vector3 getRandomPos() {
		float x = Random.Range (-largest, largest);
		float z = Random.Range (-largest, largest);
		Vector3 position = new Vector3(x, 0, z);

		return position;
	}

	void startWaypointSelection() {


	}

	public void selectWaypoint() {

		selectedWaypointIndex = (int)Random.Range (0, ammount - 1);

		waypoints [selectedWaypointIndex].SendMessage ("setSelected", true);

		GameObject.FindGameObjectWithTag ("ArrowController").SendMessage ("changeTarget", waypoints [selectedWaypointIndex].transform);
	}

	void getCloseWaypoints()
	{

	}

	public Waypoint getSelectedWaypoint()
	{
		return waypoints [selectedWaypointIndex];
	}
}
