using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour {
	public GUIText locationGUI;

	public string locationString;

	// Use this for initialization
	void Start () {

		locationGUI.text = "";
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void newLocation(string locationName) 
	{
		locationString = "Head to Location: " + locationName;
	}

	void OnGUI()
	{
		locationGUI.text = locationString;
	}
}
