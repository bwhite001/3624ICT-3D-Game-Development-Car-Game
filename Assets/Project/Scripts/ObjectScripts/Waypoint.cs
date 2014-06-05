using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour {

	public bool selected;
	public GameObject modelPrefab;
	private GameObject model;

	public string waypointName;


	// Use this for initialization
	void Start () {
		model = Instantiate(modelPrefab, transform.position, Quaternion.identity) as GameObject;
		model.name = "Model " + name;
		model.transform.parent = transform;
	}
	
	// Update is called once per frame
	void Update () {

		model.SetActive (selected);
	
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(transform.position, 1);
		if(selected)
			Gizmos.color = Color.green;
		else
			Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, 10);
	}

	public void setSelected(bool select)
	{
		selected = select;
	}

	public string getName()
	{
		return waypointName;
	}

	public bool getSelected()
	{
		return selected;
	}

	public Transform getTransform()
	{
		return transform;
	}
}
