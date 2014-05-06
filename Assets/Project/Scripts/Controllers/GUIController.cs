using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour {
	public GUIText locationGUI;
	public GUIText programGUI;
	public GUIText courseGUI;

	private string locationString;
	private string courseString;

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
	public void newCourse (string courseCode, string courseName)
	{
		courseString = courseCode + " - " + courseName;
	}

	public void setProgram(string programCode)
	{
		programGUI.text = programCode;
	}
	void OnGUI()
	{
		locationGUI.text = locationString;
		courseGUI.text = courseString;
	}
}
