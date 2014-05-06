using UnityEngine;
using System.Collections;
using UnityEditor;

public class CarScript : MonoBehaviour {
	private Vector3 accel;
	public float throttle;
	private Vector3 myRight;
	private Vector3 velo;
	private Vector3 flatVelo;
	private Vector3 relativeVelocity;
	private Vector3 dir;
	private Vector3 flatDir;
	private Vector3 carUp;
	private Transform carTransform;
	private Rigidbody carRigidbody;
	private Vector3 engineForce;

	//center of mass vars
	public float comX = 0f;
	public float comY = -0.5f;
	public float comZ = 0.34f;


	private Vector3 turnVec;
	private Vector3 imp;
	private float rev;
	private float actualTurn;
	private float carMass;
	private Transform[] wheelTransform = new Transform[4];

	public float actualGrip;
	public float horizontal;
	private float maxSpeedToTurn = 2;

	//Wheel Transforms for spin.

	public Transform frontLeftWheel;
	public Transform frontRightWheel;
	public Transform rearLeftWheel;
	public Transform rearRightWheel;


	//Wheel Transforms for turning.

	public Transform LFWheelTransform;
	public Transform RFWheelTransform;

	// Car Adjustments for Physcis

	public float power = 1500;
	public float maxSpeed = 50;
	public float carGrip = 70;
	public float turnSpeed = 3.0f; 

	public float slideSpeed;
	public float mySpeed;

	private Vector3 carRight;
	private Vector3 carFwd;
	private Vector3 tempVEC;


	// Use this for initialization
	void Start () {

		initialization();
	
	}
	
	// Update is called once per frame
	void Update () {

		updateCarPhysics();

		horizontal = Input.GetAxis("Horizontal");
		throttle = Input.GetAxis("Vertical");


	
	}

	void LateUpdate() {
		transformWheels();
	}

	void initialization() {

		carTransform = transform;
		carRigidbody = rigidbody;
		carUp = carTransform.up;
		carMass = carRigidbody.mass;
		carFwd = Vector3.forward;
		carRight = Vector3.right;

		//Set Up Wheels Array
		setUpWheels();

		carRigidbody.centerOfMass = new Vector3(comX, comY, comZ);

	}

	void setUpWheels()
	{
		if(frontRightWheel == null || frontLeftWheel == null || rearRightWheel == null || rearLeftWheel == null)
		{
			Debug.LogError("One or more of the wheels are null");
			Debug.Break();
		}
		else
		{
			wheelTransform[0] = frontRightWheel;
			wheelTransform[1] = frontLeftWheel;
			wheelTransform[2] = rearRightWheel;
			wheelTransform[3] = rearLeftWheel;
		}

	}

	private Vector3 rotationAmmount;

	void transformWheels() {


	}
	public float SomeVal;
	void updateCarPhysics() {

		myRight = carTransform.right;

		velo = carRigidbody.velocity;

		tempVEC = new Vector3(velo.x, 0, velo.z);

		flatVelo = tempVEC;

		dir = carTransform.TransformDirection(carFwd);

		tempVEC = new Vector3(dir.x, 0, dir.z);

		flatDir = tempVEC;

		relativeVelocity = carTransform.InverseTransformDirection(flatVelo);

		slideSpeed = Vector3.Dot(myRight, flatVelo);

		mySpeed = flatVelo.magnitude;

		rev = Mathf.Sign(Vector3.Dot(flatVelo, flatDir));


		engineForce = flatDir * (power * throttle) * carMass;


		//Do Turning

		actualTurn = horizontal;

		//if we are in reverse the change direction.

		if(rev < 0)
		{
			actualTurn =- actualTurn;
		}


		//Calulate Torque
		turnVec = carUp * turnSpeed * actualTurn * carMass * 1000;

		actualGrip = Mathf.Lerp(100f, carGrip, (mySpeed));
		imp = myRight * (-slideSpeed * carMass * actualGrip);

		//Debug.Log (turnVec);

//		Debug.Log (carTransform.rotation);
	}

//	void slowVelocity()
//	{
//		carRigidbody.AddForce(-flatVelo * 0.8);
//	}

	void FixedUpdate ()
	{
		if(mySpeed < maxSpeed)
		{
			carRigidbody.AddForce(engineForce * Time.deltaTime);
		}

		if(mySpeed > maxSpeedToTurn)
		{
			carRigidbody.AddTorque(turnVec * Time.deltaTime);
		}

		carRigidbody.AddForce(imp * Time.deltaTime);
	}
}
