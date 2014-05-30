using UnityEngine;
using System.Collections;
using SimpleJSON;

public class ProgramObjectController : MonoBehaviour {
	public static JSONNode program;
	public static JSONNode currentMajor;

	public ProgramObjectController() {

	}

	public static void setProgram(JSONNode currentProgram)
	{
		program = currentProgram;
	}

	public static string getProgram()
	{
		return program ["code"] + " - " + program ["name"];
	}

	public static string[] getCoreClasses (int year)
	{
		JSONNode tmp = program ["coreCourses"];


		int length = 0;

		for (int i = 0; i<tmp.Count; i++) 
		{
			if(tmp[i]["year"].AsInt == year)
			{

				length++;
			}
		}

		string[] coreCorses = new string[length];
		int count = 0;

		for (int i = 0; i<tmp.Count; i++) 
		{
			if(tmp[i]["year"].AsInt == year)
			{
				coreCorses[count] = tmp[i]["code"] + " - " + tmp[i]["name"];
				count++;
			}
		}

		return coreCorses;
	}

	public static void setMajor(string major)
	{
		JSONNode tmp = program ["majors"];
		currentMajor = null;

		for (int i = 0; i<tmp.Count; i++) 
		{
			string majorName = tmp[i]["name"];
			Debug.Log(majorName);
			if(majorName==major)
			{
				Debug.Log(major);
				currentMajor = tmp[i];
				return;
			}
		}
		Debug.Log(currentMajor);
	}
	public static void setAndComeBack(string major)
	{
		setMajor (major);
		string[] c = getMajorClasses();
		foreach(string s in c)
			Debug.Log(c);
		GameObject.FindGameObjectWithTag ("GameController").SendMessage ("setMajorCourses", c);
	}
	
	public static string[] getMajors()
	{
		JSONNode tmp = program ["majors"];
		string[] majors = new string[tmp.Count];
		
		for (int i = 0; i<majors.Length; i++) 
		{
			majors[i] = tmp[i]["name"];
		}

		return majors;
	}

	public static string[] getMajorClasses ()
	{
		if (currentMajor == null)
			return null;

		JSONNode tmp = currentMajor["courses"];

		
		string[] corses = new string[tmp.Count];

		for (int i = 0; i<tmp.Count; i++) 
		{
			string cname =  tmp[i]["code"] + " - " + tmp[i]["name"];
			Debug.Log(cname);

			corses[i] = cname;

		}
		
		return corses;
	}

	public static string getMajorName()
	{
		if(currentMajor != null)
			return currentMajor["name"];
		else
			return null;
	}

}
