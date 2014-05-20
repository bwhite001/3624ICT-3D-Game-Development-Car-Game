using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {
	private GameController gc;

	void Start()
	{
		gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();
	}

	void OnTriggerEnter(Collider other) {
		switch (other.gameObject.tag) 
		{
			case "Waypoint":
				Waypoint wp = other.gameObject.GetComponent<Waypoint>();
				if(wp.getSelected())
				{
					wp.setSelected(false);
					gc.playerAtWaypoint();
				}
				break;
		}
	}
}
