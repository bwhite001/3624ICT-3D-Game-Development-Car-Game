﻿using UnityEngine;
using System.Collections;
using UnityEditor;
using WheelClass;

public class CarScript : MonoBehaviour {
	private Vector3 accel;
	public float throttle;
	private float steer = 0;
	private bool handbrake = false;
	
	public Transform carTransform;
	public Rigidbody carRigidbody;

	private Vector3 dragMultiplier = new Vector3(2, 5, 1);
	private float handbrakeXDragFactor = 0.5f;
	private float initialDragMultiplierX = 10.0f;
	private float handbrakeTime = 0.0f;
	private float handbrakeTimer = 1.0f;


	private float resetTimer  = 0.0f;
	private float resetTime  = 5.0f;

	//center of mass vars
	public Transform centerOfMassTrans;
	
	private Transform[] wheelTransform = new Transform[4];

	//Wheel Transforms for spin.

	public Transform frontLeftWheel;
	public Transform frontRightWheel;
	public Transform rearLeftWheel;
	public Transform rearRightWheel;

	// Car Adjustments for Physcis
	public float currentEnginePower;

	private Wheel[] wheels = new Wheel[4];

	private WheelFrictionCurve wfc = new WheelFrictionCurve();

	//private float[] engineForceValues;

	private bool canDrive;
	private bool canSteer;

	public float maxSpeed = 180f;
	public float maximumTurn = 15;
	public float minimumTurn = 10;

	public int numberOfGears = 5;
	public float[] engineForceValues;
	public float[] gearSpeeds;

	public int currentGear;

	public Vector3 relativeVelocity;

	public float[] signs = new float[2];


	// Use this for initialization
	void Start () {

		initialization();

		setUpWheelFrictionCurve();
	
	}
	void setUpWheelFrictionCurve()
	{
		wfc.extremumSlip = 1;
		wfc.extremumValue = 50;
		wfc.asymptoteSlip = 2;
		wfc.asymptoteValue = 25;
		wfc.stiffness = 1;
	}
	// Update is called once per frame
	void Update () {
		relativeVelocity = transform.InverseTransformDirection(carRigidbody.velocity);

		throttle = Input.GetAxis("Vertical");
		steer = Input.GetAxis("Horizontal");

		CheckHandbrake ();

		Check_If_Car_Is_Flipped();

		UpdateGear(relativeVelocity);
	
	}

	void initialization() {

		carRigidbody = rigidbody;
		carTransform = transform;

		//Set Up Wheels Array
		setUpWheels();
		SetupGears();

		if(centerOfMassTrans != null)
			carRigidbody.centerOfMass = centerOfMassTrans.localPosition;

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

		createWheelColliders();

	}
	void SetupGears()
	{
		engineForceValues = new float[numberOfGears];
		gearSpeeds = new float[numberOfGears];
		
		float tempTopSpeed = maxSpeed;
		
		for(var i = 0; i < numberOfGears; i++)
		{
			if(i > 0)
				gearSpeeds[i] = tempTopSpeed / 4 + gearSpeeds[i-1];
			else
				gearSpeeds[i] = tempTopSpeed / 4;
			
			tempTopSpeed -= tempTopSpeed / 4;
		}
		
		float engineFactor = maxSpeed / gearSpeeds[gearSpeeds.Length - 1];
		
		for(int i = 0; i < numberOfGears; i++)
		{
			float maxLinearDrag = gearSpeeds[i] * gearSpeeds[i];// * dragMultiplier.z;
			engineForceValues[i] = maxLinearDrag * engineFactor;
		}
	}

	void createWheelColliders()
	{
		for (int i = 0; i < wheelTransform.Length; i++) 
		{
			bool frontWheel = (i < 2);
			wheels[i] = new Wheel(wheelTransform[i], frontWheel, wfc);
		}

	}



	void FixedUpdate ()
	{
		// The carRigidbody velocity is always given in world space, but in order to work in local space of the car model we need to transform it first.
		relativeVelocity = transform.InverseTransformDirection(carRigidbody.velocity);
		
		CalculateState();	
		
		UpdateFriction(relativeVelocity);
		
		UpdateDrag(relativeVelocity);
		
		CalculateEnginePower(relativeVelocity);
		
		ApplyThrottle(relativeVelocity);
		
		ApplySteering (relativeVelocity);
	}

	void Check_If_Car_Is_Flipped()
	{
		if(carTransform.localEulerAngles.z > 80 && carTransform.localEulerAngles.z < 280)
			resetTimer += Time.deltaTime;
		else
			resetTimer = 0;
		
		if(resetTimer > resetTime)
			FlipCar();
	}
	
	void FlipCar()
	{
		carTransform.rotation = Quaternion.LookRotation(carTransform.forward);
		carTransform.position += Vector3.up * 0.5f;
		carRigidbody.velocity = Vector3.zero;
		carRigidbody.angularVelocity = Vector3.zero;
		resetTimer = 0;
		currentEnginePower = 0;
	}

	void UpdateDrag(Vector3 relativeVelocity)
	{
		Vector3 relativeDrag = new Vector3(	-relativeVelocity.x * Mathf.Abs(relativeVelocity.x), 
		                                         -relativeVelocity.y * Mathf.Abs(relativeVelocity.y), 
		                                         -relativeVelocity.z * Mathf.Abs(relativeVelocity.z) );
		
		var drag = Vector3.Scale(dragMultiplier, relativeDrag);
		
		if(initialDragMultiplierX > dragMultiplier.x) // Handbrake code
		{			
			drag.x /= (relativeVelocity.magnitude / (maxSpeed / ( 1 + 2 * handbrakeXDragFactor ) ) );
			drag.z *= (1 + Mathf.Abs(Vector3.Dot(carRigidbody.velocity.normalized, carTransform.forward)));
			drag += carRigidbody.velocity * Mathf.Clamp01(carRigidbody.velocity.magnitude / maxSpeed);
		}
		else // No handbrake
		{
			drag.x *= maxSpeed / relativeVelocity.magnitude;
		}
		
		if(Mathf.Abs(relativeVelocity.x) < 5 && !handbrake)
			drag.x = -relativeVelocity.x * dragMultiplier.x;
		
		
		carRigidbody.AddForce(carTransform.TransformDirection(drag) * carRigidbody.mass * Time.deltaTime);
	}
	
	void UpdateFriction(Vector3 relativeVelocity)
	{
		float sqrVel = relativeVelocity.x * relativeVelocity.x;
		
		// Add extra sideways friction based on the car's turning velocity to avoid slipping
		wfc.extremumValue = Mathf.Clamp(300 - sqrVel, 0, 300);
		wfc.asymptoteValue = Mathf.Clamp(150 - (sqrVel / 2), 0, 150);
		
		foreach(Wheel w in wheels)
		{
			w.setFriction(wfc);

		}
	}
	
	void CalculateEnginePower(Vector3 relativeVelocity)
	{
		if(throttle == 0)
		{
			currentEnginePower -= Time.deltaTime * 1000;
		}
		else if(HaveTheSameSign(relativeVelocity.z, throttle))
		{
			float normPower = (currentEnginePower / engineForceValues[engineForceValues.Length - 1]) * 2;
			currentEnginePower += Time.deltaTime * 200 * EvaluateNormPower(normPower);
		}
		else
		{
			currentEnginePower -= Time.deltaTime * 300;
		}

		if(currentGear == 0)
			currentEnginePower = Mathf.Clamp(currentEnginePower, 0, engineForceValues[0]);
		else
			currentEnginePower = Mathf.Clamp(currentEnginePower, engineForceValues[currentGear - 1], engineForceValues[currentGear]);
	}
	void CheckHandbrake()
	{
		if(Input.GetKey("space"))
		{
			if(!handbrake)
			{
				handbrake = true;
				handbrakeTime = Time.time;
				dragMultiplier.x = initialDragMultiplierX * handbrakeXDragFactor;
			}
		}
		else if(handbrake)
		{
			handbrake = false;
			float var = Mathf.Min(5, Time.time - handbrakeTime);
			StartCoroutine(StopHandbraking(var));
		}
	}
	
	IEnumerator StopHandbraking(float seconds)
	{
		float diff= initialDragMultiplierX - dragMultiplier.x;
		handbrakeTimer = 1;
		
		// Get the x value of the dragMultiplier back to its initial value in the specified time.
		while(dragMultiplier.x < initialDragMultiplierX && !handbrake)
		{
			dragMultiplier.x += diff * (Time.deltaTime / seconds);
			handbrakeTimer -= Time.deltaTime / seconds;
			yield break;
		}
		
		dragMultiplier.x = initialDragMultiplierX;
		handbrakeTimer = 0;
	}

	void CalculateState()
	{
		canDrive = false;
		canSteer = false;
		
		foreach(Wheel w in wheels)
		{
			if(w.isGrounded())
			{
				if(w.steerWheel)
					canSteer = true;
				if(w.driveWheel)
					canDrive = true;
			}
		}
	}
	
	void ApplyThrottle(Vector3 relativeVelocity)
	{
		if(canDrive)
		{
			float throttleForce = 0;
			float brakeForce = 0;
			
			if (HaveTheSameSign(relativeVelocity.z, throttle))
			{
				if (!handbrake)
					throttleForce = Mathf.Sign(throttle) * currentEnginePower * carRigidbody.mass;
			}
			else
				brakeForce = Mathf.Sign(throttle) * engineForceValues[0] * carRigidbody.mass;
			
			carRigidbody.AddForce(carTransform.forward * Time.deltaTime * (throttleForce + brakeForce));
		}
	}
	void UpdateGear(Vector3 relativeVelocity)
	{
		currentGear = 0;
		for(var i = 0; i < numberOfGears - 1; i++)
		{
			if(relativeVelocity.z > gearSpeeds[i])
				currentGear = i + 1;
		}
	}

	void ApplySteering(Vector3 relativeVelocity)
	{
		if(canSteer)
		{
			float turnRadius = 3.0f / Mathf.Sin((90 - (steer * 30)) * Mathf.Deg2Rad);
			float minMaxTurn = EvaluateSpeedToTurn(carRigidbody.velocity.magnitude);
			float turnSpeed = Mathf.Clamp(relativeVelocity.z / turnRadius, -minMaxTurn / 10, minMaxTurn / 10);
			
			carTransform.RotateAround(	carTransform.position + carTransform.right * turnRadius * steer, 
			                       carTransform.up, 
			                       turnSpeed * Mathf.Rad2Deg * Time.deltaTime * steer);
			
			Vector3 debugStartPoint = carTransform.position + carTransform.right * turnRadius * steer;
			Vector3 debugEndPoint = debugStartPoint + Vector3.up * 5;

			if(initialDragMultiplierX > dragMultiplier.x) // Handbrake
			{
				float rotationDirection = Mathf.Sign(steer); // rotationDirection is -1 or 1 by default, depending on steering
				if(steer == 0)
				{
					if(carRigidbody.angularVelocity.y < 1) // If we are not steering and we are handbraking and not rotating fast, we apply a random rotationDirection
						rotationDirection = Random.Range(-1.0f, 1.0f);
					else
						rotationDirection = carRigidbody.angularVelocity.y; // If we are rotating fast we are applying that rotation to the car
				}
				// -- Finally we apply this rotation around a point between the cars front wheels.
				Vector3 betweenWheels = (wheelTransform[0].localPosition + wheelTransform[1].localPosition) * 0.5f;
				carTransform.RotateAround(carTransform.TransformPoint(betweenWheels), 
				                       carTransform.up, 
				                       carRigidbody.velocity.magnitude * Mathf.Clamp01(1 - carRigidbody.velocity.magnitude / maxSpeed) * rotationDirection * Time.deltaTime * 2);
			}
		}
	}
	bool HaveTheSameSign(float first, float second)
	{
		if (Mathf.Sign(first) == Mathf.Sign(second))
			return true;
		else
			return false;
	}
	
	float Convert_Miles_Per_Hour_To_Meters_Per_Second(float value)
	{
		return value * 0.44704f;
	}
	
	float Convert_Meters_Per_Second_To_Miles_Per_Hour(float value)
	{
		return value * 2.23693629f;	
	}
	
	
	
	float EvaluateSpeedToTurn(float speed)
	{
		if(speed > maxSpeed / 2)
			return minimumTurn;
		
		float speedIndex = 1 - (speed / (maxSpeed / 2));
		return minimumTurn + speedIndex * (maximumTurn - minimumTurn);
	}
	
	float EvaluateNormPower(float normPower)
	{
		if(normPower < 1)
			return 10f - normPower * 9f;
		else
			return 1.9f - normPower * 0.9f;
	}


}
