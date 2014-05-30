using UnityEngine;
using System.Collections;

public class CarAI : MonoBehaviour {

	public enum FSMState
	{
		None,
		Patrol,
		Chase,
		Attack,
	}

	private Transform[] waypoints;
	public PatrolPointController PatrolPointController;

	private Vector3 destPos = new Vector3 (0, 0, 0);

	private Rigidbody _rigidbody;

	public FSMState curState;

	public float moveSpeed = 20.0f;
	public float chaseSpeed = 24.0f;
	public float chaseRange = 35.0f;
	public float attackRange = 20.0f;
	public float patrolRange = 10.0f;
	public float rotSpeed = 5.0f;

	public bool start = false;

	protected Transform playerTransform;// Player Transform

	void Start()
	{
		curState = FSMState.Patrol;
		waypoints = PatrolPointController.getPatrolPoints ();
		findNextPoint ();

		// Get the target (Player)
		GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
		playerTransform = objPlayer.transform;
		if(!playerTransform)
			print("Player doesn't exist.. Please add one with Tag named 'Player'");
	}

	protected void findNextPoint()
	{
		if(waypoints.Length > 0)
		{
			int rndIndex = Random.Range(0, waypoints.Length);
			destPos = waypoints[rndIndex].position;
		}
	}

	void Update()
	{
		switch (curState) {
			case FSMState.Patrol: UpdatePatrolState(); break;
			case FSMState.Chase: UpdateChaseState(); break;
		}
		// Find another random patrol point if the current point is reached
//		if (Vector3.Distance (transform.position, destPos) <= 10.0f) {
//			findNextPoint ();

		
		
		// Rotate to the target point
		Quaternion targetRotation = Quaternion.LookRotation(destPos - transform.position);
		rigidbody.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed));  
		
		// Go Forward
		rigidbody.MovePosition(rigidbody.position + transform.forward * Time.deltaTime * moveSpeed);
	}
	/*
     * Patrol state
     */
	protected void UpdatePatrolState() {
		// Find another random patrol point if the current point is reached
		if (Vector3.Distance (transform.position, destPos) <= 2.0f) {
			findNextPoint ();
		} 
		else if 
		(Vector3.Distance (transform.position, playerTransform.position) <= chaseRange) {
			curState = FSMState.Chase;
		}
		
		
		// Rotate to the target point
		Quaternion targetRotation = Quaternion.LookRotation(destPos - transform.position);
		rigidbody.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed));  
		
		// Go Forward
		rigidbody.MovePosition(rigidbody.position + transform.forward * Time.deltaTime * moveSpeed);
	}

	protected void UpdateChaseState() {
		// find player position
		destPos = playerTransform.position;

		// check distance between chaser and target
		float distance = Vector3.Distance(transform.position, playerTransform.transform.position);
		
		if (distance <= attackRange)  {
			curState = FSMState.Attack;
		}
		// if player escapes
		else if (distance >= patrolRange) {
			curState = FSMState.Patrol;
		}
		transform.Translate (Vector3.forward * Time.deltaTime * moveSpeed);
	}

	// Create Gizmo
	void OnDrawGizmos(){
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (transform.position, chaseRange);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, patrolRange);
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere (transform.position, attackRange);
	}
}
