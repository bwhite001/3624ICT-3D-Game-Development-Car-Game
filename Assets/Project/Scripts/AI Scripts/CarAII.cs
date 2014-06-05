using UnityEngine;
using System.Collections;

public class CarAII : MonoBehaviour {

	public enum FSMState
	{
		None,
		Patrol,
		Chase,
		Attack,
	}

	public FSMState curState;

	private GameObject[] waypoints;
	//private Vector3 destPos = new Vector3 (0, 0, 0);


	public float moveSpeed = 15.0f; //normal ai speed
	public float chaseSpeed = 24.0f; 
	public float chaseRange = 35.0f;
	public float attackRange = 5.0f; // if player inside range attack
	public float stopRange = 5.0f;
	public float rotSpeed = 2.0f;

	public float attackTime = 5.0f; //time between next attack
	public float secondsTillNextAttack = 5.0f;

	public bool start = false;

	public GameObject player;

	protected Transform playerTransform;// Player Position

	private NavMeshAgent nav;
	private float navElapsedTime = 0.0f;
	private float navElapsedUpdate = 0.25f;
	private int curNavPoint = 0;

	void Start()
	{
		curState = FSMState.Patrol;

		nav = GetComponent<NavMeshAgent> ();

		waypoints = GameObject.FindGameObjectsWithTag ("Waypoint");
		//findNextPoint ();

		//GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
		playerTransform = player.transform;

		if(!playerTransform)
			print("Player doesn't exist.. Please add one with Tag named 'Player'");

	}


	void Update()
	{
		switch (curState) {
		case FSMState.Patrol: UpdatePatrolState(); break;
		case FSMState.Chase: UpdateChaseState(); break;
		case FSMState.Attack: UpdateAttackState(); break;
		}
//		// Find another random patrol point if the current point is reached
//		if (Vector3.Distance (transform.position, destPos) <= 10.0f) {
//			curNavPoint = findNextPoint ();
//		} 
	
	}

	/*
     * Patrol state
     */
	protected void UpdatePatrolState() 
	{
		navElapsedTime += Time.deltaTime;
		curNavPoint = findNextPoint();
		if (navElapsedTime > navElapsedUpdate) 
		{

			nav.SetDestination (waypoints[curNavPoint].transform.position);
			navElapsedTime = 0.0f;
		}
		float dis = Vector3.Distance(transform.position, waypoints[curNavPoint].transform.position);

		if (dis <= 1.0f) {
			curNavPoint = findNextPoint();

		}
		Debug.Log(curNavPoint);
//		// Find another random patrol point if the current point is reached
//		if (Vector3.Distance (transform.position, destPos) <= 2.0f) {
//			findNextPoint ();
//		} 

		
		// Rotate to the target point
		Quaternion targetRotation = Quaternion.LookRotation(waypoints[curNavPoint].transform.position - transform.position);
		rigidbody.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed));  
		
		// Go Forward
		rigidbody.MovePosition(rigidbody.position + transform.forward * Time.deltaTime * moveSpeed);
	}

	protected void UpdateChaseState()
	{
		navElapsedTime += Time.deltaTime;
		curNavPoint = findNextPoint();
		if (navElapsedTime > navElapsedUpdate) 
		{
			
			nav.SetDestination (playerTransform.position);
			navElapsedTime = 0.0f;
		}
		// find player position
		//destPos = playerTransform.position;

		// check distance between chaser and target
		float distance = Vector3.Distance(transform.position, playerTransform.transform.position);
		
		if (distance <= attackRange)  {
			curState = FSMState.Attack;
		}

		Vector3 chasePos = new Vector3 (this.transform.position.x, 0.0f, this.transform.position.z);
		Vector3 playerPos = new Vector3 (playerTransform.position.x, 0.0f, playerTransform.position.z);
		rigidbody.MoveRotation (Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (playerPos - chasePos), rotSpeed * Time.deltaTime));
		rigidbody.MovePosition (rigidbody.position + transform.forward * chaseSpeed * Time.deltaTime);

	}

	protected void UpdateAttackState()
	{

		player.SendMessage("spinPlayer");
			//secondsTillNextAttack = 0.0f;

		curState = FSMState.Chase;
		//secondsTillNextAttack += Time.deltaTime;

		Debug.Log (secondsTillNextAttack); Debug.Log (Time.deltaTime);
		//playerTransform.Rotate (Vector3.up * Time.deltaTime * 1000);
		// check distance between chaser and target
//		float distance = Vector3.Distance(transform.position, playerTransform.transform.position);
//		
//		if (distance > attackRange)  {
//			// go home
//		}

	}
	

	public void playerEntered()
	{	
		curState = FSMState.Chase;

	}

	public void playerExits()
	{
		curState = FSMState.Patrol;
	}
	
	protected int findNextPoint()
	{
		if(waypoints.Length > 0)
		{
			int rndIndex = Random.Range(0, waypoints.Length);
			//nav.SetDestination(waypoints[rndIndex].transform.position);
			return rndIndex;
			//destPos = waypoints[rndIndex].transform.position;
		}
		else
			return 0;

	}

	// Create Gizmo
	void OnDrawGizmos(){
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (transform.position, chaseRange);

		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere (transform.position, attackRange);
	}

}
