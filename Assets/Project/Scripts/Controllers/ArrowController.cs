using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour {

	public Camera cam; //Camera to use
	private Transform target; //Target to point at (you could set this to any gameObject dynamically)
	private Vector3 targetPos; //Target position on screen
	private Vector3 screenMiddle; //Middle of the screen
	
	void Update() {

		if (target != null) 
		{
			//Get the targets position on screen into a Vector3
			targetPos = cam.WorldToScreenPoint (target.transform.position);
			//Get the middle of the screen into a Vector3
			float midX = Screen.width / 2f;
			float midY = Screen.height / 2f;
			screenMiddle = new Vector3 (midX, midY, 0); 
			//Compute the angle from screenMiddle to targetPos
			float tarAngle = (Mathf.Atan2 (targetPos.x - screenMiddle.x, Screen.height - targetPos.y - screenMiddle.y) * Mathf.Rad2Deg) + 90;
			if (tarAngle < 0)
					tarAngle += 360;

			//Calculate the angle from the camera to the target
			Vector3 targetDir = target.transform.position - cam.transform.position;
			Vector3 forward = cam.transform.forward;
			float angle = Vector3.Angle (targetDir, forward);

			//If the angle exceeds 90deg inverse the rotation to point correctly
			if (angle < 90) {
					transform.localRotation = Quaternion.Euler (-tarAngle, 90, 270);
			} else {
					transform.localRotation = Quaternion.Euler (tarAngle, 270, 90);
			}
		}
	}

	void changeTarget(Transform newtarget)
	{
		target = newtarget;
	}
}
