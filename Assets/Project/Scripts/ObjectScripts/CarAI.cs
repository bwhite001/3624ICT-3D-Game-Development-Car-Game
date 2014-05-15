using UnityEngine;
using System.Collections;

public class CarAI : MonoBehaviour {
	private GameObject[] waypoints;
	private Vector3 destPos = new Vector3 (0, 0, 0);

	public float moveSpeed = 20.0f;
	public float chaseSpeed = 24.0f;
	public float chaseRange = 35.0f;
	public float attackRange = 20.0f;
	public float patrolRange = 10.0f;
	public float rotSpeed = 5.0f;

	public bool start = false;


	void Start()
	{
		waypoints = GameObject.FindGameObjectsWithTag ("Waypoint");
		findNextPoint ();
	}

	void findNextPoint()
	{
		if(waypoints.Length > 0)
		{
			int rndIndex = Random.Range(0, waypoints.Length);
			destPos = waypoints[rndIndex].transform.position;
		}
	}

	void Update()
	{
		// Find another random patrol point if the current point is reached
		if (Vector3.Distance (transform.position, destPos) <= 10.0f) {
			findNextPoint ();
		} 
		
		
		// Rotate to the target point
		Quaternion targetRotation = Quaternion.LookRotation(destPos - transform.position);
		rigidbody.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed));  
		
		// Go Forward
		rigidbody.MovePosition(rigidbody.position + transform.forward * Time.deltaTime * moveSpeed);
	}
}
