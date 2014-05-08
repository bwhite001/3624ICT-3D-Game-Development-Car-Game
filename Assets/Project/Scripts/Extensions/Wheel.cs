using System;
using UnityEngine;
using System.Collections;
using UnityEditor;

namespace WheelClass
{
		public class Wheel
		{
			public WheelCollider collider;
			public Transform wheelGraphic;
			public Transform tireGraphic;
			public bool driveWheel = false;
			public bool steerWheel = false;
			public int lastSkidmark = -1;
			public Vector3 lastEmitPosition = Vector3.zero;
			public float lastEmitTime = Time.time;
			public Vector3 wheelVelo = Vector3.zero;
			public Vector3 groundSpeed = Vector3.zero;

			private float wheelRadius = 0.3f;
			private float suspensionRange = 0.1f;
			private float suspensionDamper = 50f;
			private float suspensionSpringFront = 18500f;
			private float suspensionSpringRear = 9000f;

			public Wheel (Transform wheelTransform, bool frontWheel, WheelFrictionCurve wfc)
			{
				GameObject go = new GameObject(wheelTransform.name + " Collider");
				go.transform.position = wheelTransform.position;
				go.transform.parent = wheelTransform.parent.transform;
				go.transform.rotation = wheelTransform.rotation;
				
				WheelCollider wc = go.AddComponent(typeof(WheelCollider)) as WheelCollider;
				wc.suspensionDistance = suspensionRange;
				JointSpring js = wc.suspensionSpring;
				
				if (frontWheel)
					js.spring = suspensionSpringFront;
				else
					js.spring = suspensionSpringRear;
				
				js.damper = suspensionDamper;
				wc.suspensionSpring = js;
				
				collider = wc;
				
				wc.sidewaysFriction = wfc;
				wheelGraphic = wheelTransform;
				//		wheel.tireGraphic = wheelTransform.GetComponentsInChildren(Transform)[1];
				
				//		wheelRadius = wheel.tireGraphic.renderer.bounds.size.y / 2;	
				collider.radius = wheelRadius;
				
				if (frontWheel)
				{
					steerWheel = true;
					
					go = new GameObject(wheelTransform.name + " Steer Column");
					go.transform.position = wheelTransform.position;
					go.transform.rotation = wheelTransform.rotation;
					go.transform.parent = wheelTransform.parent.transform;
					wheelTransform.parent = go.transform;
				}
				else
					driveWheel = true;
			}

			public void setFriction(WheelFrictionCurve wfc)
			{
				collider.sidewaysFriction = wfc;
				collider.forwardFriction = wfc;
			}

			public bool isGrounded()
			{
				return collider.isGrounded;
			}
		}
}

