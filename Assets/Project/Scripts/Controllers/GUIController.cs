using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour {
	public GUIText locationGUI;
	public GUIText programGUI;
	public GUIText courseGUI;
	public GUIText infoGUI;

	public GUITexture locationGUIBox;
	public GUITexture courseGUIBox;
	public GUITexture programGUIBox;
	public GUITexture infoGUIBox;

	private string locationString;
	private string courseString;
	private string programString;
	private string infoString;
	
	private Rect locationGUIBoxRect;
	private Rect courseGUIBoxRect;
	private Rect programGUIBoxRect;
	private Rect infoGUIBoxRect;

	// Use this for initialization
	void Start () {

		locationGUI.text = "";
		courseGUI.text = "";
		programGUI.text = "";
		infoGUI.text = "";

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setLocation(string locationName) 
	{
		locationString = "Head to Location: " + locationName;
	}
	public void setCourse (string course)
	{
		courseString = "Finish Course: "+ course;
	}

	public void setProgram(string program, string major)
	{
		programString =  "Program: " + program;

		if (major != null) 
		{
			programString += ", Major: "+major;
		}

	}

	public void setInfo(int year,int semseter, int course)
	{
		infoString = "Current Year: " + year + " Semester: "+semseter+", Completed Courses: " + course+"/24";
	}
	
	void OnGUI()
	{
		locationGUI.text = locationString;
		courseGUI.text = courseString;
		programGUI.text = programString;
		infoGUI.text = infoString;

	}

	Rect getGUIBox(Rect initRect, GUIText guiText)
	{
		Rect outRect = new Rect (initRect);
		outRect.width = guiText.text.Length * guiText.fontSize / 1.5f;
		outRect.x = outRect.width / 2 * -1;

		return outRect;
	}

	Rect getBigest(Rect rect1, GUIText gui1, Rect rect2, GUIText gui2)
	{
		Rect newRect1 = getGUIBox (rect1, gui1);
		Rect newRect2 = getGUIBox (rect2, gui2);

		if(newRect1.width > newRect2.width)
			return newRect1;
		else
			return newRect2;
	}

	Rect setWidth(Rect current, Rect newWidth)
	{
		Rect outRect = new Rect (current);
		outRect.width = newWidth.width;
		outRect.x = outRect.width / 2 * -1;
		return outRect;
	}
}
