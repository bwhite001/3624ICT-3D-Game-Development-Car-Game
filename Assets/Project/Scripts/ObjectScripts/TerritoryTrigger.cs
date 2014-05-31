using UnityEngine;
using System.Collections;



public class territoryTrigger : MonoBehaviour {

	public GameObject patrolAi;

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "playerBody")
		{
			patrolAi.SendMessage("playerEntered");
		}
		Debug.Log(col.gameObject.tag);
	}

	void OnTriggerExit (Collider col) {
		if (col.gameObject.tag == "playerBody")
		{
			patrolAi.SendMessage("playerExits");
		}
		Debug.Log(col.gameObject.tag);
	}
}
