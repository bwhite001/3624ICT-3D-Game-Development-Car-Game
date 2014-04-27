using UnityEngine;
using System.Collections;

public class WaypointController : MonoBehaviour {
	public GameObject waypointPrefab;

	private GameObject[] waypoints;

	public int ammount = 5;
	private float largest = 100f;

	// Use this for initialization
	void Start () {

		createWaypoints ();

		selectWaypoint ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void createWaypoints()
	{
		waypoints = new GameObject[ammount];

		for (int i = 0; i<ammount; i++) {
			Vector3 pos = getRandomPos();

			GameObject temp = Instantiate(waypointPrefab, pos, Quaternion.identity) as GameObject;

			waypoints[i] = temp;


			Debug.Log("Created Object at: " + pos.ToString());
		}
	}

	Vector3 getRandomPos() {
		float x = Random.Range (-largest, largest);
		float z = Random.Range (-largest, largest);
		Vector3 position = new Vector3(x, 0, z);

		return position;
	}

	void selectWaypoint() {

		int index = (int)Random.Range (0, ammount - 1);

		waypoints [index].SendMessage ("setSelected", true);

	}
}
